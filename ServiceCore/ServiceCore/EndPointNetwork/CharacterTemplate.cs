using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class CharacterTemplate
	{
		public byte CharacterClass { get; set; }

		public List<CustomizeItemRequestInfo> CustomizeItems { get; set; }

		public int SkinColor { get; set; }

		public int Shineness { get; set; }

		public int EyeColor { get; set; }

		public int Height { get; set; }

		public int Bust { get; set; }

		public int PaintingPosX { get; set; }

		public int PaintingPosY { get; set; }

		public int PaintingRotation { get; set; }

		public int PaintingSize { get; set; }

		public IDictionary<int, float> BodyShapeInfo { get; set; }

		public override string ToString()
		{
			return string.Format("({0}) customize x {1}", this.CharacterClass, this.CustomizeItems.Count);
		}
	}
}
