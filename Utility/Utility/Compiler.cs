using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Utility
{
	public static class Compiler
	{
		static Compiler()
		{
			Compiler.providerOptions.Add(typeof(CSharpCodeProvider), new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"3.5"
				}
			});
			Compiler.providerOptions.Add(typeof(VBCodeProvider), new Dictionary<string, string>
			{
				{
					"CompilerVersion",
					"3.5"
				}
			});
		}

		public static CodeDomProvider CreateProvider(string language)
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			Type codeDomProviderType = compilerInfo.CodeDomProviderType;
			if (Compiler.providerOptions.ContainsKey(codeDomProviderType))
			{
				ConstructorInfo constructor = codeDomProviderType.GetConstructor(new Type[]
				{
					typeof(IDictionary<string, string>)
				});
				return constructor.Invoke(new object[]
				{
					Compiler.providerOptions[codeDomProviderType]
				}) as CodeDomProvider;
			}
			return compilerInfo.CreateProvider();
		}

		private static IDictionary<Type, IDictionary<string, string>> providerOptions = new Dictionary<Type, IDictionary<string, string>>();
	}
}
