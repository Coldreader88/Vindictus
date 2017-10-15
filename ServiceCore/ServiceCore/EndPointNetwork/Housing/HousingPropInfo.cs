using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingPropInfo
	{
		public long PropID { get; set; }

		public int PropClass { get; set; }

		public float PosX { get; set; }

		public float PosY { get; set; }

		public float PosZ { get; set; }

		public float AngleX { get; set; }

		public float AngleY { get; set; }

		public float AngleZ { get; set; }

		public HousingPropInfo(long propID, int propClass, float posX, float posY, float posZ, float angleX, float angleY, float angleZ)
		{
			this.PropID = propID;
			this.PropClass = propClass;
			this.PosX = posX;
			this.PosY = posY;
			this.PosZ = posZ;
			this.AngleX = angleX;
			this.AngleY = angleY;
			this.AngleZ = angleZ;
		}

		public override string ToString()
		{
			return string.Format("HousingItemInfo [{0}/{1}] / Pos[{2}/{3}/{4}] / Angle[{5}/{6}/{7}]\n", new object[]
			{
				this.PropID,
				this.PropClass,
				this.PosX,
				this.PosY,
				this.PosZ,
				this.AngleX,
				this.AngleY,
				this.AngleZ
			});
		}
	}
}
