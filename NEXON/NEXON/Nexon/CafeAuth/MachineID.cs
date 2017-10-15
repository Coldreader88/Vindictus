using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Nexon.CafeAuth
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct MachineID
	{
		public MachineID(byte[] machineID)
		{
			if (machineID == null)
			{
				throw new ArgumentNullException("machineID");
			}
			if (machineID.Length != 16)
			{
				throw new ArgumentException("Argument should be 16 bytes array.", "machineID");
			}
			this.eui0 = machineID[0];
			this.eui1 = machineID[1];
			this.eui2 = machineID[2];
			this.eui3 = machineID[3];
			this.eui4 = machineID[4];
			this.eui5 = machineID[5];
			this.volumeSerialNumber = ((int)machineID[6] | (int)machineID[7] << 8 | ((int)machineID[8] | (int)machineID[9] << 8) << 16);
			this.reserved = ((int)machineID[10] | (int)machineID[11] << 8 | ((int)machineID[12] | (int)machineID[13] << 8) << 16);
			if (this.reserved != 0)
			{
				throw new ArgumentException("Reserved fields should be filled with 0s.", "machineID");
			}
			this.hash = (short)((int)machineID[14] | (int)machineID[15] << 8);
		}

		public byte[] ToByteArray()
		{
			return new byte[]
			{
				this.eui0,
				this.eui1,
				this.eui2,
				this.eui3,
				this.eui4,
				this.eui5,
				(byte)this.volumeSerialNumber,
				(byte)(this.volumeSerialNumber >> 8),
				(byte)(this.volumeSerialNumber >> 16),
				(byte)(this.volumeSerialNumber >> 24),
				(byte)this.reserved,
				(byte)(this.reserved >> 8),
				(byte)(this.reserved >> 16),
				(byte)(this.reserved >> 24),
				(byte)this.hash,
				(byte)(this.hash >> 8)
			};
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("MachineID { Address = ");
			stringBuilder.AppendFormat("{0:X2}", this.eui0);
			stringBuilder.Append(':');
			stringBuilder.AppendFormat("{0:X2}", this.eui1);
			stringBuilder.Append(':');
			stringBuilder.AppendFormat("{0:X2}", this.eui2);
			stringBuilder.Append(':');
			stringBuilder.AppendFormat("{0:X2}", this.eui3);
			stringBuilder.Append(':');
			stringBuilder.AppendFormat("{0:X2}", this.eui4);
			stringBuilder.Append(':');
			stringBuilder.AppendFormat("{0:X2}", this.eui5);
			stringBuilder.Append(", VolumeSerialNumber = ");
			stringBuilder.Append(this.volumeSerialNumber);
			stringBuilder.Append(", Hash = ");
			stringBuilder.Append(this.hash);
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		internal byte eui0;

		internal byte eui1;

		internal byte eui2;

		internal byte eui3;

		internal byte eui4;

		internal byte eui5;

		internal int volumeSerialNumber;

		internal int reserved;

		internal short hash;
	}
}
