using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CostumeInfo
	{
		public IDictionary<int, int> CostumeTypeInfo { get; set; }

		public IDictionary<int, int> ColorInfo { get; set; }

		public IDictionary<int, bool> AvatarInfo { get; set; }

		public IDictionary<int, int> AvatarHideInfo { get; set; }

		public IDictionary<int, byte> PollutionInfo { get; set; }

		public IDictionary<int, int> EffectInfo { get; set; }

		public IDictionary<int, int> DecorationInfo { get; set; }

		public IDictionary<int, int> DecorationColorInfo { get; set; }

		public int Shineness { get; set; }

		public int Height { get; set; }

		public int Bust { get; set; }

		public int PaintingPosX { get; set; }

		public int PaintingPosY { get; set; }

		public int PaintingRotation { get; set; }

		public int PaintingSize { get; set; }

		public bool HideHeadCostume { get; set; }

		public int CafeType { get; set; }

		public bool IsReturn { get; set; }

		public int VIPCode { get; set; }

		public int BodyPaintingPosX { get; set; }

		public int BodyPaintingPosY { get; set; }

		public int BodyPaintingRotation { get; set; }

		public int BodyPaintingSize { get; set; }

		public int BodyPaintingSide { get; set; }

		public int BodyPaintingClip { get; set; }

		public int BodyPaintingMode { get; set; }

		public IDictionary<int, float> BodyShapeInfo { get; set; }

		public int TownEffect { get; set; }

		public CostumeInfo()
		{
			this.CostumeTypeInfo = new Dictionary<int, int>();
			this.ColorInfo = new Dictionary<int, int>();
			this.AvatarInfo = new Dictionary<int, bool>();
			this.AvatarHideInfo = new Dictionary<int, int>();
			this.PollutionInfo = new Dictionary<int, byte>();
			this.EffectInfo = new Dictionary<int, int>();
			this.DecorationInfo = new Dictionary<int, int>();
			this.DecorationColorInfo = new Dictionary<int, int>();
			this.BodyShapeInfo = new Dictionary<int, float>();
			this.Shineness = (this.Height = (this.Bust = -1));
			this.PaintingPosX = (this.PaintingPosY = (this.PaintingRotation = (this.PaintingSize = -1)));
			this.HideHeadCostume = false;
			this.CafeType = 0;
			this.IsReturn = false;
			this.VIPCode = 0;
			this.BodyPaintingPosX = (this.BodyPaintingPosY = (this.BodyPaintingRotation = (this.BodyPaintingSize = -1)));
			this.BodyPaintingSide = (this.BodyPaintingClip = -1);
			this.BodyPaintingMode = -1;
			this.TownEffect = 0;
		}

		public override string ToString()
		{
			return string.Format("CostumeInfo( costume x {0} face x {1} )", this.CostumeTypeInfo.Count, this.DecorationInfo.Count);
		}
	}
}
