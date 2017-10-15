using System;

namespace Devcat.Core.Testing
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
	public class AutomatedTestAttribute : Attribute
	{
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public AutomatedTestAttribute(string name, string description)
		{
			this.name = name;
			this.description = description;
		}

		private string name;

		private string description;
	}
}
