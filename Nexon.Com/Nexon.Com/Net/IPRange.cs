using System;
using System.Text;

namespace Nexon.Com.Net
{
	internal sealed class IPRange
	{
		public IPRange(string iprange)
		{
			string[] array = iprange.Split(new char[]
			{
				'/'
			});
			if (array.Length < 1 || array.Length > 2)
			{
				throw new Exception("Invalid IP : " + iprange);
			}
			string[] array2 = array[0].Split(new char[]
			{
				'.'
			});
			string[] array3 = null;
			if (array2.Length != 4)
			{
				throw new Exception("Invalid IP : " + iprange);
			}
			if (array.Length == 2)
			{
				if (array[1].IndexOf('.') > -1)
				{
					array3 = array[1].Split(new char[]
					{
						'.'
					});
				}
				else
				{
					array3 = new string[4];
					int num = Convert.ToInt32(array[1]);
					for (int i = 0; i < 4; i++)
					{
						if (i < num / 8)
						{
							array3[i] = "255";
						}
						else if (i == num / 8)
						{
							int num2 = 0;
							switch (num % 8)
							{
							case 1:
								num2 = 128;
								break;
							case 2:
								num2 = 192;
								break;
							case 3:
								num2 = 224;
								break;
							case 4:
								num2 = 240;
								break;
							case 5:
								num2 = 248;
								break;
							case 6:
								num2 = 252;
								break;
							case 7:
								num2 = 254;
								break;
							}
							array3[i] = num2.ToString();
						}
						else
						{
							array3[i] = "0";
						}
					}
				}
			}
			for (int j = 0; j < 4; j++)
			{
				if (array2[j].Trim() == "*")
				{
					this._networkPortion[j] = 0;
				}
				else
				{
					this._networkPortion[j] = Convert.ToByte(array2[j]);
				}
				if (array3 != null)
				{
					this._subnetMask[j] = Convert.ToByte(array3[j]);
				}
				else if (array2[j].Trim() == "*")
				{
					this._subnetMask[j] = 0;
				}
				else
				{
					this._subnetMask[j] = byte.MaxValue;
				}
			}
		}

		public bool Contains(string strIP)
		{
			string[] array = strIP.Split(new char[]
			{
				'.'
			});
			if (array.Length != 4)
			{
				throw new Exception("Invalid IP : " + strIP);
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num = Convert.ToInt32(array[i]);
				if ((num & (int)this._subnetMask[i]) != (int)this._networkPortion[i])
				{
					return false;
				}
			}
			return true;
		}

		public string Trace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._networkPortion.Length; i++)
			{
				stringBuilder.AppendFormat("{0}{1}", (i == 0) ? "" : ".", this._networkPortion[i]);
			}
			stringBuilder.Append("/");
			for (int j = 0; j < this._subnetMask.Length; j++)
			{
				stringBuilder.AppendFormat("{0}{1}", (j == 0) ? "" : ".", this._subnetMask[j]);
			}
			return stringBuilder.ToString();
		}

		private byte[] _networkPortion = new byte[4];

		private byte[] _subnetMask = new byte[4];
	}
}
