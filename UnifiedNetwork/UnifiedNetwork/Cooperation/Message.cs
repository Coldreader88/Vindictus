using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Net.Message;

namespace UnifiedNetwork.Cooperation
{
	internal static class Message
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				yield return typeof(sbyte);
				yield return typeof(byte);
				yield return typeof(short);
				yield return typeof(ushort);
				yield return typeof(int);
				yield return typeof(uint);
				yield return typeof(long);
				yield return typeof(ulong);
				yield return typeof(char);
				yield return typeof(float);
				yield return typeof(double);
				yield return typeof(bool);
				yield return typeof(string);
				yield return typeof(Guid);
				yield return typeof(DateTime);
				yield return typeof(TimeSpan);
				yield return typeof(IPAddress);
				yield return typeof(IPEndPoint);
				yield return typeof(FailMessage);
				yield return typeof(OkMessage);
				yield return typeof(int[]);
				yield return typeof(long[]);
				yield return typeof(string[]);
				yield return typeof(List<int>);
				yield return typeof(List<long>);
				yield return typeof(List<string>);
				yield return typeof(HashSet<int>);
				yield return typeof(HashSet<long>);
				yield return typeof(HashSet<string>);
				yield return typeof(LinkedList<int>);
				yield return typeof(LinkedList<long>);
				yield return typeof(LinkedList<string>);
				yield return typeof(Dictionary<short, int>);
				yield return typeof(Dictionary<int, int>);
				yield return typeof(Dictionary<int, long>);
				yield return typeof(Dictionary<int, byte>);
				yield return typeof(Dictionary<int, string>);
				yield return typeof(Dictionary<long, int>);
				yield return typeof(Dictionary<long, IList<byte>>);
				yield return typeof(Dictionary<string, int>);
				yield return typeof(Dictionary<string, List<int>>);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				IDictionary<Type, int> converter = Message.Types.GetConverter(16384);
				converter.Add(typeof(Exception), 0);
				return converter;
			}
		}
	}
}
