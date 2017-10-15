using System;
using System.ComponentModel;
using System.Reflection;

namespace Devcat.Core.Design
{
	internal abstract class MemberPropertyDescriptor : PropertyDescriptor
	{
		public MemberPropertyDescriptor(MemberInfo member) : base(member.Name, (Attribute[])member.GetCustomAttributes(typeof(Attribute), true))
		{
			this._member = member;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			MemberPropertyDescriptor memberPropertyDescriptor = obj as MemberPropertyDescriptor;
			return memberPropertyDescriptor != null && memberPropertyDescriptor._member.Equals(this._member);
		}

		public override int GetHashCode()
		{
			return this._member.GetHashCode();
		}

		public override void ResetValue(object component)
		{
		}

		public override bool ShouldSerializeValue(object component)
		{
			return true;
		}

		public override Type ComponentType
		{
			get
			{
				return this._member.DeclaringType;
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		private MemberInfo _member;
	}
}
