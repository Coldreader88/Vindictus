using System;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
	public class GroupCreateMemberJoin
	{
		public string Answer { get; set; }

		public byte codeAdmission { get; set; }

		public bool IsRequiredAge { get; set; }

		public bool IsRequiredArea { get; set; }

		public bool IsRequiredBirthday { get; set; }

		public bool IsRequiredName { get; set; }

		public bool IsRequiredSchool { get; set; }

		public bool IsRequiredTel { get; set; }

		public string Question { get; set; }

		public bool UseNote { get; set; }
	}
}
