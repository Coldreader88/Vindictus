using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Nexon.CafeAuth.Packets
{
	[Serializable]
	public sealed class ExtendInformation
	{
		public int PCRoomNo { get; internal set; }

		public bool IsValidPCRoom { get; internal set; }

		public string PCRoomName { get; internal set; }

		public string PCRoomZipCode { get; internal set; }

		public byte PCRoomLevel { get; internal set; }

		public long UniqueID { get; internal set; }

		public string ShutDownPolicyName { get; internal set; }

		public bool IsShutDownEnabled { get; internal set; }

		public DateTime ShutDownTime { get; internal set; }

		private int ShutDownErrorCodeValue { get; set; }

		public PolicyInfoErrorCode ShutDownErrorCode
		{
			get
			{
				return this.ShutDownErrorCodeValue.ToErrorCode();
			}
		}

		public List<string> ShutDownPolicies { get; internal set; }

		public bool IsNeedShutDown
		{
			get
			{
				return this.IsShutDownEnabled || this.ShutDownErrorCodeValue != 0;
			}
		}

		internal ExtendInformation(ref Packet packet)
		{
			byte b = packet.ReadByte();
			for (int i = 0; i < (int)b; i++)
			{
				short num = packet.ReadInt16();
				if (num == 7)
				{
					this.PCRoomNo = packet.ReadInt32();
				}
				else if (num == 8)
				{
					this.IsValidPCRoom = (packet.ReadByte() == 1);
				}
				else if (num == 9)
				{
					this.PCRoomName = packet.ReadString();
				}
				else if (num == 10)
				{
					this.PCRoomZipCode = packet.ReadString();
				}
				else if (num == 14)
				{
					this.PCRoomLevel = packet.ReadByte();
				}
				else if (num == 19)
				{
					byte b2 = packet.ReadByte();
					for (int j = 0; j < (int)b2; j++)
					{
						byte b3 = packet.ReadByte();
						string shutDownPolicyName = packet.ReadString();
						bool isShutDownEnabled = packet.ReadByte() == 1;
						string policyInfos = packet.ReadString();
						if (b3 == 10)
						{
							DateTime minValue = DateTime.MinValue;
							this.ShutDownPolicyName = shutDownPolicyName;
							this.IsShutDownEnabled = isShutDownEnabled;
							int shutDownErrorCodeValue;
							this.GetPolicyInfo(policyInfos, out minValue, out shutDownErrorCodeValue);
							if (minValue != DateTime.MinValue)
							{
								this.ShutDownTime = minValue;
							}
							this.ShutDownErrorCodeValue = shutDownErrorCodeValue;
						}
					}
				}
			}
		}

		private void GetPolicyInfo(string policyInfos, out DateTime time, out int errorCode)
		{
			time = DateTime.MinValue;
			errorCode = 0;
			char[] separator = new char[]
			{
				'&'
			};
			List<string> list = new List<string>(policyInfos.Split(separator));
			foreach (string text in list)
			{
				char[] separator2 = new char[]
				{
					'='
				};
				if (text.StartsWith("time="))
				{
					string[] array = text.Split(separator2);
					bool flag = DateTime.TryParseExact(array[1], "yyMMddHH", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
					if (array[1] != null && array[1].Length > 0 && !flag)
					{
					}
				}
				else if (text.StartsWith("error="))
				{
					string[] array2 = text.Split(separator2);
					if (array2[1] == null || array2[1].Length <= 0)
					{
						errorCode = 0;
					}
					else if (!int.TryParse(array2[1], out errorCode))
					{
						errorCode = 0;
					}
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ ShutDownPolicyName = ");
			stringBuilder.Append(this.ShutDownPolicyName);
			stringBuilder.Append(", IsShutDownEnabled = ");
			stringBuilder.Append(this.IsShutDownEnabled);
			stringBuilder.Append(", ShutDownTime = ");
			stringBuilder.Append(this.ShutDownTime);
			stringBuilder.Append(", ShutDownErrorCodeValue = ");
			stringBuilder.Append(this.ShutDownErrorCodeValue);
			stringBuilder.Append(", IsNeedShutDown = ");
			stringBuilder.Append(this.IsNeedShutDown);
			stringBuilder.Append(", PcRoomNo = ");
			stringBuilder.Append(this.PCRoomNo);
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}
	}
}
