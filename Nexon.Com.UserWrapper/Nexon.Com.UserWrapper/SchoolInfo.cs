using System;

namespace Nexon.Com.UserWrapper
{
	public class SchoolInfo
	{
		public SchoolInfo(int schoolSN, SchoolCode schoolCode, string schoolRealName)
		{
			this._n4SchoolSN = schoolSN;
			this._SchoolCode = schoolCode;
			this._strSchoolRealName = schoolRealName;
		}

		public SchoolCode SchoolCode
		{
			get
			{
				SchoolCode schoolCode;
				try
				{
					schoolCode = this._SchoolCode;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return schoolCode;
			}
			internal set
			{
				this._SchoolCode = value;
			}
		}

		public int SchoolSN
		{
			get
			{
				int n4SchoolSN;
				try
				{
					n4SchoolSN = this._n4SchoolSN;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return n4SchoolSN;
			}
			internal set
			{
				this._n4SchoolSN = value;
			}
		}

		public string SchoolRealName
		{
			get
			{
				string strSchoolRealName;
				try
				{
					if (this._strSchoolRealName == null)
					{
						throw new UserInvalidAccessException();
					}
					strSchoolRealName = this._strSchoolRealName;
				}
				catch
				{
					throw new UserInvalidAccessException();
				}
				return strSchoolRealName;
			}
			internal set
			{
				this._strSchoolRealName = value;
			}
		}

		private SchoolCode _SchoolCode;

		private int _n4SchoolSN;

		private string _strSchoolRealName;
	}
}
