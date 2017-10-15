using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class BeautyShopCustomizeMessage : IMessage
	{
		public List<CustomizeItemRequestInfo> CustomizeItems { get; set; }

		public int PaintingPosX { get; set; }

		public int PaintingPosY { get; set; }

		public int PaintingSize { get; set; }

		public int PaintingRotation { get; set; }

		public int payment { get; set; }

		public int BodyPaintingPosX { get; set; }

		public int BodyPaintingPosY { get; set; }

		public int BodyPaintingSize { get; set; }

		public int BodyPaintingRotation { get; set; }

		public int BodyPaintingSide { get; set; }

		public int BodyPaintingClip { get; set; }

		public int BodyPaintingMode { get; set; }

		public int HeightValue { get; set; }

		public int BustValue { get; set; }

		public int ShinenessValue { get; set; }

		public int SkinColor { get; set; }

		public int EyeColor { get; set; }

		public string EyebrowItemClass { get; set; }

		public int EyebrowColor { get; set; }

		public string LookChangeItemClass { get; set; }

		public int LookChangeDuration { get; set; }

		public string LookChangePrice { get; set; }

		public IDictionary<int, float> BodyShapeInfo { get; set; }
	}
}
