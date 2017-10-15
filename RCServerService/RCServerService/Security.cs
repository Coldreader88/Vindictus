using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem.Server
{
	public class Security
	{
		public ICollection Users
		{
			get
			{
				return this._userList.Values;
			}
		}

		public Security()
		{
			this._userList = new Dictionary<string, Security.UserInformation>();
			this._userList.Add("root", new Security.UserInformation("root", Security.GetHashedPassword("akdudwjsvhdpqj"), Authority.Root));
		}

		private bool IsPassword(Security.UserInformation user, byte[] hashedPassword)
		{
			if (hashedPassword.Length != user.HashedPassword.Length)
			{
				return false;
			}
			for (int i = 0; i < hashedPassword.Length; i++)
			{
				if (hashedPassword[i] != user.HashedPassword[i])
				{
					return false;
				}
			}
			return true;
		}

		public bool IsPassword(string id, byte[] hashedPassword)
		{
			Security.UserInformation userInformation = this._userList[id];
			return userInformation != null && this.IsPassword(userInformation, hashedPassword);
		}

		public void LoadSecurity(XmlReader reader)
		{
			Security.UserInformation[] array = (Security.UserInformation[])new XmlSerializer(typeof(Security.UserInformation[]), new XmlRootAttribute("Authority")).Deserialize(reader);
			foreach (Security.UserInformation userInformation in array)
			{
				userInformation.CheckSelf();
				this._userList[userInformation.ID] = userInformation;
			}
		}

		public void WriteSecurity(XmlWriter writer)
		{
			Security.UserInformation[] array = new Security.UserInformation[this._userList.Count];
			this._userList.Values.CopyTo(array, 0);
			new XmlSerializer(typeof(Security.UserInformation[]), new XmlRootAttribute("Authority")).Serialize(writer, array, new XmlSerializerNamespaces(new XmlQualifiedName[]
			{
				XmlQualifiedName.Empty
			}));
		}

		public static byte[] GetHashedPassword(string password)
		{
			return new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(password));
		}

		public Authority GetUserAuthority(string id)
		{
			Security.UserInformation userInformation = this._userList[id];
			if (userInformation == null)
			{
				return Authority.None;
			}
			return userInformation.Authority;
		}

		public Authority GetUserAuthority(string id, byte[] password)
		{
			if (this._userList.ContainsKey(id))
			{
				Security.UserInformation userInformation = this._userList[id];
				if (userInformation == null)
				{
					return Authority.None;
				}
				if (this.IsPassword(userInformation, password))
				{
					return userInformation.Authority;
				}
			}
			return Authority.None;
		}

		public void AddUser(string id, byte[] hashedPassword, Authority authority)
		{
			this._userList.Add(id, new Security.UserInformation(id, hashedPassword, authority));
		}

		public void RemoveUser(string id)
		{
			this._userList.Remove(id);
		}

		public void ChangePassword(string id, byte[] newPassword)
		{
			if (newPassword == null)
			{
				throw new ArgumentNullException("newPassword");
			}
			Security.UserInformation userInformation = this._userList[id];
			if (userInformation != null)
			{
				userInformation.HashedPassword = newPassword;
			}
		}

		public void ChangeAuthority(string id, Authority newAuthority)
		{
			if (newAuthority == Authority.None)
			{
				throw new ArgumentException("newAuthority", "it cannot be none!");
			}
			Security.UserInformation userInformation = this._userList[id];
			if (userInformation != null)
			{
				userInformation.Authority = newAuthority;
			}
		}

		public bool CheckAuthority(string id, Authority authority)
		{
			Security.UserInformation userInformation = this._userList[id];
			return userInformation != null && userInformation.Authority >= authority;
		}

		private Dictionary<string, Security.UserInformation> _userList;

		public class UserInformation
		{
			[XmlAttribute]
			public string ID { get; set; }

			[XmlAttribute]
			public byte[] HashedPassword { get; set; }

			[XmlAttribute]
			public Authority Authority { get; set; }

			public UserInformation()
			{
			}

			public UserInformation(string id, byte[] hashedPassword, Authority authority)
			{
				this.ID = id;
				this.HashedPassword = hashedPassword;
				this.Authority = authority;
				this.CheckSelf();
			}

			public void CheckSelf()
			{
				if (this.ID == null || this.ID.Length == 0)
				{
					throw new ArgumentException("user id is null or length-0 string");
				}
				if (this.HashedPassword == null)
				{
					throw new ArgumentException("user password is null");
				}
				if (this.Authority == Authority.None)
				{
					throw new ArgumentException("user authority is none");
				}
			}
		}
	}
}
