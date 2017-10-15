using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;

namespace Devcat.Core.Data.SqlClient
{
	public class SqlProcedureLinker
	{
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		public SqlProcedureLinker()
		{
			this.connectionString = string.Format("Data Source=m2-build\\sqlexpress, 1433;Initial Catalog=Mabinogi2_{0};Integrated Security=SSPI;Connection Timeout=15", Dns.GetHostName());
		}

		public void TestConnection()
		{
			using (SqlConnection sqlConnection = new SqlConnection(this.connectionString))
			{
				sqlConnection.Open();
				sqlConnection.Close();
			}
		}

		public SqlProcedureLinker(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public void CreateTable(Type type)
		{
			SqlConnection sqlConnection = new SqlConnection(this.connectionString);
			sqlConnection.Open();
			string str = "IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('" + type.Name + "'))";
			string str2 = "CREATE TABLE " + type.Name + "(";
			List<string> list = new List<string>();
			this.GetFieldInfos(type, ref list);
			string[] array = list.ToArray();
			string text = string.Join(")", array, 0, array.Length - 1);
			text += array[array.Length - 1];
			SqlCommand sqlCommand = new SqlCommand(str + str2 + text, sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		private void GetFieldInfos(Type type, ref List<string> fieldInfoList)
		{
			if (type.BaseType != null && type.BaseType != typeof(object) && !type.BaseType.IsValueType)
			{
				this.GetFieldInfos(type.BaseType, ref fieldInfoList);
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				fieldInfoList.Add(string.Format("{0} {1} NOT NULL", fieldInfo.Name, SqlProcedureLinker.ConvertToString(fieldInfo)));
			}
		}

		public void DropTable(string tableName)
		{
			SqlConnection sqlConnection = new SqlConnection(this.connectionString);
			sqlConnection.Open();
			string str = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('" + tableName + "'))";
			string str2 = "DROP TABLE " + tableName;
			SqlCommand sqlCommand = new SqlCommand(str + str2, sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		private static string ConvertToString(FieldInfo field)
		{
			SqlDbType sqlDbType = SqlProcedureLinker.ConvertPrimitiveToSqlDBType(field.FieldType);
			string text = sqlDbType.ToString();
			object[] customAttributes = field.GetCustomAttributes(false);
			if (customAttributes.Length == 1)
			{
				SqlColumnSizeAttribute sqlColumnSizeAttribute = customAttributes[0] as SqlColumnSizeAttribute;
				if (sqlColumnSizeAttribute == null)
				{
					throw new ArgumentException(string.Format("SqlColumnSizeAttribute type casting error : {0}", field.Name));
				}
				if (sqlColumnSizeAttribute.Size == -1)
				{
					text += "(max)";
				}
				else
				{
					text += string.Format("({0})", sqlColumnSizeAttribute.Size.ToString());
				}
			}
			return text;
		}

		private static SqlDbType ConvertPrimitiveToSqlDBType(Type primitive)
		{
			switch (Type.GetTypeCode(primitive))
			{
			case TypeCode.Object:
				return SqlDbType.Variant;
			case TypeCode.Boolean:
				return SqlDbType.Bit;
			case TypeCode.Byte:
				return SqlDbType.TinyInt;
			case TypeCode.Int16:
				return SqlDbType.SmallInt;
			case TypeCode.Int32:
				return SqlDbType.Int;
			case TypeCode.Int64:
				return SqlDbType.BigInt;
			case TypeCode.Single:
				return SqlDbType.Real;
			case TypeCode.Double:
				return SqlDbType.Float;
			case TypeCode.Decimal:
				return SqlDbType.Decimal;
			case TypeCode.DateTime:
				return SqlDbType.DateTime;
			case TypeCode.String:
				return SqlDbType.NVarChar;
			}
			if (primitive.IsArray && primitive.HasElementType && primitive.GetElementType().IsPrimitive && Type.GetTypeCode(primitive.GetElementType()) == TypeCode.Byte)
			{
				return SqlDbType.VarBinary;
			}
			throw new ArgumentException(string.Format("Not supported type : {0}", primitive.Name));
		}

		public T Generate<T>(Type bindingType) where T : class
		{
			string storedProcedureName;
			MethodInfo methodInfo;
			CommandType commandType = this.ParsingAttributes(bindingType, out storedProcedureName, out methodInfo);
			Type reflectedType = methodInfo.ReflectedType;
			ParameterInfo[] parameters = methodInfo.GetParameters();
			Type parameterType = methodInfo.ReturnParameter.ParameterType;
			Type[] array = new Type[parameters.Length + 1];
			array[0] = typeof(string);
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i + 1] = parameters[i].ParameterType;
			}
			DynamicMethod dynamicMethod = new DynamicMethod(string.Format("{0}.InternalQueryExecute", bindingType.Namespace), parameterType, array, bindingType.Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			LocalBuilder localBuilder = SqlProcedureLinker.NewSqlconnection(ilgenerator);
			LocalBuilder localBuilder2 = SqlProcedureLinker.NewSqlCommand(ilgenerator, storedProcedureName, localBuilder);
			LocalBuilder sqlParameterCollection = SqlProcedureLinker.InitSqlCommand(ilgenerator, localBuilder2);
			LocalBuilder localBuilder3 = SqlProcedureLinker.NewInstance2(ilgenerator, bindingType);
			SqlProcedureLinker.BindingParameters(ilgenerator, bindingType, localBuilder3, parameters, sqlParameterCollection);
			if (parameterType.Equals(typeof(void)))
			{
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
				ilgenerator.Emit(OpCodes.Call, typeof(SqlCommand).GetMethod("ExecuteNonQuery", Type.EmptyTypes));
				ilgenerator.Emit(OpCodes.Pop);
			}
			else if (parameterType.Equals(bindingType))
			{
				LocalBuilder localBuilder4 = SqlProcedureLinker.NewSqlDataReader(ilgenerator, localBuilder2);
				SqlProcedureLinker.ExecuteReader(ilgenerator, bindingType, localBuilder3, localBuilder4, 0);
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder4);
				ilgenerator.EmitCall(OpCodes.Call, typeof(SqlDataReader).GetMethod("Close", Type.EmptyTypes), null);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder2);
				ilgenerator.Emit(OpCodes.Call, typeof(SqlCommand).GetMethod("ExecuteScalar", Type.EmptyTypes));
				ilgenerator.Emit(OpCodes.Unbox_Any, parameterType);
			}
			SqlProcedureLinker.GettingParameters(ilgenerator, parameters, sqlParameterCollection);
			SqlProcedureLinker.GettingReturnValue(ilgenerator, bindingType, localBuilder3, sqlParameterCollection);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder);
			ilgenerator.Emit(OpCodes.Call, typeof(SqlConnection).GetMethod("Close"));
			if (parameterType.Equals(bindingType))
			{
				ilgenerator.Emit(OpCodes.Ldloc, localBuilder3);
			}
			ilgenerator.Emit(OpCodes.Ret);
			T t = dynamicMethod.CreateDelegate(reflectedType, this.connectionString) as T;
			if (t == null)
			{
				throw new ArgumentException(string.Format("Generic delegator type-casting occurs error: {0}", typeof(T).Name));
			}
			return t;
		}

		private static void ExecuteReader(ILGenerator il, Type bindingType, LocalBuilder bindingInstance, LocalBuilder sqlDataReader, int fieldIndex)
		{
			il.Emit(OpCodes.Ldloc, sqlDataReader);
			il.Emit(OpCodes.Call, typeof(SqlDataReader).GetMethod("Read", Type.EmptyTypes));
			Label label = il.DefineLabel();
			il.Emit(OpCodes.Brfalse, label);
			SqlProcedureLinker.ReadFields(il, bindingType, bindingInstance, sqlDataReader, fieldIndex);
			il.MarkLabel(label);
		}

		private static void ReadFields(ILGenerator il, Type bindingType, LocalBuilder bindingInstance, LocalBuilder sqlDataReader, int fieldIndex)
		{
			if (bindingType.BaseType != null && bindingType.BaseType != typeof(object) && !bindingType.IsValueType)
			{
				SqlProcedureLinker.ReadFields(il, bindingType.BaseType, bindingInstance, sqlDataReader, fieldIndex);
			}
			foreach (FieldInfo fieldInfo in bindingType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Type fieldType = fieldInfo.FieldType;
				if ((fieldInfo.GetCustomAttributes(false).Length <= 0 || !(fieldInfo.GetCustomAttributes(false)[0].GetType() == typeof(SqlReturnAttribute))) && !fieldType.IsInterface && !fieldType.IsAbstract && !fieldInfo.IsInitOnly && !fieldInfo.IsLiteral && !fieldType.IsByRef)
				{
					if (fieldType.IsArray)
					{
						Label label = il.DefineLabel();
						il.Emit(OpCodes.Ldloc, sqlDataReader);
						il.Emit(OpCodes.Call, typeof(SqlDataReader).GetMethod("NextResult"));
						il.Emit(OpCodes.Brfalse, label);
						Type elementType = fieldType.GetElementType();
						LocalBuilder local = SqlProcedureLinker.ReadList(il, elementType, sqlDataReader, 0);
						LocalBuilder localBuilder = il.DeclareLocal(elementType.MakeArrayType());
						il.Emit(OpCodes.Ldloc, local);
						il.Emit(OpCodes.Call, typeof(List<>).MakeGenericType(new Type[]
						{
							elementType
						}).GetMethod("ToArray"));
						il.Emit(OpCodes.Stloc, localBuilder);
						SqlProcedureLinker.BindField(il, bindingInstance, fieldInfo, localBuilder);
						il.MarkLabel(label);
					}
					else if (SqlProcedureLinker.IsGenericCollection(fieldType))
					{
						Label label2 = il.DefineLabel();
						il.Emit(OpCodes.Ldloc, sqlDataReader);
						il.Emit(OpCodes.Call, typeof(SqlDataReader).GetMethod("NextResult"));
						il.Emit(OpCodes.Brfalse, label2);
						Type elementType2 = fieldType.GetGenericArguments()[0];
						LocalBuilder sourceField = SqlProcedureLinker.ReadList(il, elementType2, sqlDataReader, 0);
						SqlProcedureLinker.BindField(il, bindingInstance, fieldInfo, sourceField);
						il.MarkLabel(label2);
					}
					else if (!fieldType.IsPrimitive && fieldType != typeof(string))
					{
						SqlProcedureLinker.ReadObject(il, bindingInstance, sqlDataReader, fieldIndex, fieldInfo);
					}
					else
					{
						SqlProcedureLinker.ReadField(il, bindingInstance, sqlDataReader, fieldIndex, fieldInfo);
						fieldIndex++;
					}
				}
			}
		}

		private static LocalBuilder ReadList(ILGenerator il, Type elementType, LocalBuilder sqlDataReader, int fieldIndex)
		{
			Label label = il.DefineLabel();
			Label label2 = il.DefineLabel();
			LocalBuilder localBuilder = il.DeclareLocal(typeof(List<>).MakeGenericType(new Type[]
			{
				elementType
			}));
			il.Emit(OpCodes.Newobj, typeof(List<>).MakeGenericType(new Type[]
			{
				elementType
			}).GetConstructor(Type.EmptyTypes));
			il.Emit(OpCodes.Stloc, localBuilder);
			il.MarkLabel(label);
			il.Emit(OpCodes.Ldloc, sqlDataReader);
			il.Emit(OpCodes.Call, typeof(SqlDataReader).GetMethod("Read", Type.EmptyTypes));
			il.Emit(OpCodes.Brfalse, label2);
			LocalBuilder localBuilder2 = SqlProcedureLinker.NewInstance2(il, elementType);
			SqlProcedureLinker.ReadFields(il, elementType, localBuilder2, sqlDataReader, fieldIndex);
			il.Emit(OpCodes.Ldloc, localBuilder);
			il.Emit(OpCodes.Ldloc, localBuilder2);
			il.EmitCall(OpCodes.Call, typeof(List<>).MakeGenericType(new Type[]
			{
				elementType
			}).GetMethod("Add"), null);
			il.Emit(OpCodes.Br, label);
			il.MarkLabel(label2);
			return localBuilder;
		}

		private static void ReadObject(ILGenerator il, LocalBuilder bindingInstance, LocalBuilder sqlDataReader, int fieldIndex, FieldInfo targetField)
		{
			LocalBuilder localBuilder = SqlProcedureLinker.NewInstance(il, targetField.FieldType);
			SqlProcedureLinker.BindField(il, bindingInstance, targetField, localBuilder);
			SqlProcedureLinker.ReadFields(il, targetField.FieldType, localBuilder, sqlDataReader, fieldIndex);
		}

		private static LocalBuilder NewInstance(ILGenerator il, Type newType)
		{
			LocalBuilder localBuilder;
			if (newType.IsValueType)
			{
				localBuilder = il.DeclareLocal(newType.MakeByRefType());
			}
			else
			{
				localBuilder = il.DeclareLocal(newType);
				ConstructorInfo constructor = newType.GetConstructor(Type.EmptyTypes);
				if (constructor == null)
				{
					throw new ArgumentException("Default contructor does not exist");
				}
				il.Emit(OpCodes.Newobj, constructor);
				il.Emit(OpCodes.Stloc, localBuilder);
			}
			return localBuilder;
		}

		private static LocalBuilder NewInstance2(ILGenerator il, Type newType)
		{
			LocalBuilder localBuilder = il.DeclareLocal(newType);
			if (!newType.IsValueType)
			{
				localBuilder = il.DeclareLocal(newType);
				ConstructorInfo constructor = newType.GetConstructor(Type.EmptyTypes);
				if (constructor == null)
				{
					throw new ArgumentException("Default contructor does not exist");
				}
				il.Emit(OpCodes.Newobj, constructor);
				il.Emit(OpCodes.Stloc, localBuilder);
			}
			return localBuilder;
		}

		private static void BindField(ILGenerator il, LocalBuilder bindingInstance, FieldInfo bindingField, LocalBuilder sourceField)
		{
			if (bindingInstance.LocalType.IsValueType)
			{
				il.Emit(OpCodes.Ldloca, bindingInstance);
			}
			else
			{
				il.Emit(OpCodes.Ldloc, bindingInstance);
			}
			if (sourceField.LocalType.IsByRef)
			{
				il.Emit(OpCodes.Ldflda, bindingField);
				il.Emit(OpCodes.Stloc, sourceField);
				return;
			}
			il.Emit(OpCodes.Ldloc, sourceField);
			il.Emit(OpCodes.Stfld, bindingField);
		}

		private static void ReadField(ILGenerator il, LocalBuilder bindingInstance, LocalBuilder sqlDataReader, int fieldIndex, FieldInfo targetField)
		{
			if (bindingInstance.LocalType.IsValueType)
			{
				il.Emit(OpCodes.Ldloca, bindingInstance);
			}
			else
			{
				il.Emit(OpCodes.Ldloc, bindingInstance);
			}
			il.Emit(OpCodes.Ldloc, sqlDataReader);
			il.Emit(OpCodes.Ldc_I4, fieldIndex);
			il.Emit(OpCodes.Call, typeof(SqlDataReader).GetMethod("GetValue"));
			il.Emit(OpCodes.Unbox_Any, targetField.FieldType);
			il.Emit(OpCodes.Stfld, targetField);
		}

		private static LocalBuilder NewSqlCommand(ILGenerator il, string storedProcedureName, LocalBuilder sqlConnection)
		{
			LocalBuilder localBuilder = il.DeclareLocal(typeof(SqlCommand));
			il.Emit(OpCodes.Ldstr, storedProcedureName);
			il.Emit(OpCodes.Ldloc, sqlConnection);
			il.Emit(OpCodes.Newobj, typeof(SqlCommand).GetConstructor(new Type[]
			{
				typeof(string),
				typeof(SqlConnection)
			}));
			il.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		private static LocalBuilder InitSqlCommand(ILGenerator il, LocalBuilder sqlCommand)
		{
			LocalBuilder localBuilder = il.DeclareLocal(typeof(SqlParameterCollection));
			il.Emit(OpCodes.Ldloc, sqlCommand);
			il.Emit(OpCodes.Ldc_I4_S, 4);
			il.Emit(OpCodes.Call, typeof(SqlCommand).GetProperty("CommandType", typeof(CommandType)).GetSetMethod());
			il.Emit(OpCodes.Ldloc, sqlCommand);
			il.Emit(OpCodes.Call, typeof(SqlCommand).GetProperty("Parameters", typeof(SqlParameterCollection)).GetGetMethod());
			il.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		private static LocalBuilder NewSqlconnection(ILGenerator il)
		{
			LocalBuilder localBuilder = il.DeclareLocal(typeof(SqlConnection));
			il.Emit(OpCodes.Ldarg_0);
			il.Emit(OpCodes.Newobj, typeof(SqlConnection).GetConstructor(new Type[]
			{
				typeof(string)
			}));
			il.Emit(OpCodes.Stloc, localBuilder);
			il.Emit(OpCodes.Ldloc, localBuilder);
			il.Emit(OpCodes.Call, typeof(SqlConnection).GetMethod("Open", BindingFlags.Instance | BindingFlags.Public));
			return localBuilder;
		}

		private static LocalBuilder NewSqlDataReader(ILGenerator il, LocalBuilder sqlCommand)
		{
			LocalBuilder localBuilder = il.DeclareLocal(typeof(SqlDataReader));
			il.Emit(OpCodes.Ldloc, sqlCommand);
			il.Emit(OpCodes.Call, typeof(SqlCommand).GetMethod("ExecuteReader", Type.EmptyTypes));
			il.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		private static void BindingParameters(ILGenerator il, Type bindingType, LocalBuilder bindingInstance, ParameterInfo[] parameters, LocalBuilder sqlParameterCollection)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				il.Emit(OpCodes.Ldloc, sqlParameterCollection);
				il.Emit(OpCodes.Ldstr, "@" + parameters[i].Name);
				il.Emit(OpCodes.Ldarg, i + 1);
				if (parameters[i].ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Box, parameters[i].ParameterType.GetElementType());
				}
				else
				{
					il.Emit(OpCodes.Box, parameters[i].ParameterType);
				}
				il.Emit(OpCodes.Newobj, typeof(SqlParameter).GetConstructor(new Type[]
				{
					typeof(string),
					typeof(object)
				}));
				il.Emit(OpCodes.Call, typeof(SqlParameterCollection).GetMethod("Add", new Type[]
				{
					typeof(SqlParameter)
				}));
				if (parameters[i].ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldc_I4_S, 3);
					il.Emit(OpCodes.Call, typeof(SqlParameter).GetProperty("Direction").GetSetMethod());
				}
				else
				{
					il.Emit(OpCodes.Pop);
				}
			}
			foreach (FieldInfo fieldInfo in bindingType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (fieldInfo.GetCustomAttributes(false).Length == 1 && fieldInfo.GetCustomAttributes(false)[0].GetType() == typeof(SqlReturnAttribute))
				{
					il.Emit(OpCodes.Ldloc, sqlParameterCollection);
					il.Emit(OpCodes.Ldstr, "RETURN_VALUE");
					if (bindingInstance.LocalType.IsValueType)
					{
						il.Emit(OpCodes.Ldloca, bindingInstance);
					}
					else
					{
						il.Emit(OpCodes.Ldloc, bindingInstance);
					}
					il.Emit(OpCodes.Ldfld, fieldInfo);
					il.Emit(OpCodes.Newobj, typeof(SqlParameter).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(object)
					}));
					il.Emit(OpCodes.Call, typeof(SqlParameterCollection).GetMethod("Add", new Type[]
					{
						typeof(SqlParameter)
					}));
					il.Emit(OpCodes.Ldc_I4_S, 6);
					il.Emit(OpCodes.Call, typeof(SqlParameter).GetProperty("Direction").GetSetMethod());
					return;
				}
			}
		}

		private static void GettingParameters(ILGenerator il, ParameterInfo[] parameters, LocalBuilder sqlParameterCollection)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].ParameterType.IsByRef)
				{
					il.Emit(OpCodes.Ldarg, i + 1);
					il.Emit(OpCodes.Ldloc, sqlParameterCollection);
					il.Emit(OpCodes.Ldc_I4, i);
					il.Emit(OpCodes.Call, typeof(SqlParameterCollection).GetProperty("Item", typeof(SqlParameter), new Type[]
					{
						typeof(int)
					}).GetGetMethod());
					il.Emit(OpCodes.Call, typeof(SqlParameter).GetProperty("Value").GetGetMethod());
					il.Emit(OpCodes.Unbox_Any, parameters[i].ParameterType.GetElementType());
					il.Emit(OpCodes.Stind_I4);
				}
			}
		}

		private static void GettingReturnValue(ILGenerator il, Type bindingType, LocalBuilder bindingInstance, LocalBuilder sqlParameterCollection)
		{
			foreach (FieldInfo fieldInfo in bindingType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (fieldInfo.GetCustomAttributes(false).Length == 1 && fieldInfo.GetCustomAttributes(false)[0].GetType() == typeof(SqlReturnAttribute))
				{
					if (bindingInstance.LocalType.IsValueType)
					{
						il.Emit(OpCodes.Ldloca, bindingInstance);
					}
					else
					{
						il.Emit(OpCodes.Ldloc, bindingInstance);
					}
					il.Emit(OpCodes.Ldloc, sqlParameterCollection);
					il.Emit(OpCodes.Ldstr, "RETURN_VALUE");
					il.Emit(OpCodes.Call, typeof(SqlParameterCollection).GetProperty("Item", typeof(SqlParameter), new Type[]
					{
						typeof(string)
					}).GetGetMethod());
					il.Emit(OpCodes.Call, typeof(SqlParameter).GetProperty("Value").GetGetMethod());
					il.Emit(OpCodes.Unbox_Any, fieldInfo.FieldType);
					il.Emit(OpCodes.Stfld, fieldInfo);
				}
			}
		}

		private static bool IsGenericCollection(Type type)
		{
			if (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(ICollection<>)))
			{
				return Array.Exists<Type>(type.GetInterfaces(), (Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>));
			}
			return true;
		}

		private CommandType ParsingAttributes(Type bindingType, out string storedProcedureName, out MethodInfo delegateMethodInfo)
		{
			object[] customAttributes = bindingType.GetCustomAttributes(false);
			switch (customAttributes.Length)
			{
			case 0:
				storedProcedureName = string.Empty;
				delegateMethodInfo = null;
				return CommandType.Text;
			case 1:
			{
				SqlProcedureAttribute sqlProcedureAttribute = customAttributes[0] as SqlProcedureAttribute;
				if (sqlProcedureAttribute == null)
				{
					throw new ArgumentException("SqlProcedureAttribute type error");
				}
				storedProcedureName = sqlProcedureAttribute.ProcedureName;
				delegateMethodInfo = sqlProcedureAttribute.LinkType.GetMethod("Invoke");
				return CommandType.StoredProcedure;
			}
			default:
				throw new ArgumentException("SqlProcedureAttribute count error");
			}
		}

		public static void DebugIL<X>(X value)
		{
			Debugger.Break();
		}

		public static void DebugRef<X>(ref X value)
		{
			Debugger.Break();
		}

		public static void DebugPrint(int ilOffset)
		{
			Console.WriteLine("IL Offset - {0}", ilOffset.ToString());
		}

		private static void DebugHelper(ILGenerator il)
		{
			SqlProcedureLinker.DebugHelper(typeof(object), il);
		}

		private static void DebugHelper(Type type, ILGenerator il)
		{
			il.Emit(OpCodes.Dup);
			il.EmitCall(OpCodes.Call, typeof(SqlProcedureLinker).GetMethod("DebugIL").MakeGenericMethod(new Type[]
			{
				type
			}), null);
		}

		private static void DebugHelperRef(Type type, ILGenerator il)
		{
			il.Emit(OpCodes.Dup);
			il.EmitCall(OpCodes.Call, typeof(SqlProcedureLinker).GetMethod("DebugRef").MakeGenericMethod(new Type[]
			{
				type
			}), null);
		}

		private static void DebugBreak(ILGenerator il)
		{
			il.EmitCall(OpCodes.Call, typeof(SqlProcedureLinker).GetMethod("DebugBreakHelper"), null);
		}

		public static void DebugBreakHelper()
		{
			Debugger.Break();
		}

		private static void DebugTracer(ILGenerator il)
		{
			il.Emit(OpCodes.Newobj, typeof(StackFrame).GetConstructor(Type.EmptyTypes));
			il.EmitCall(OpCodes.Call, typeof(StackFrame).GetMethod("GetILOffset"), null);
			il.EmitCall(OpCodes.Call, typeof(SqlProcedureLinker).GetMethod("DebugPrint"), null);
		}

		private static void DebugWrite(ILGenerator il, string format, params object[] args)
		{
			if (args.Length > 0)
			{
				format = string.Format(format, args);
			}
			il.EmitWriteLine(format);
		}

		private string connectionString;
	}
}
