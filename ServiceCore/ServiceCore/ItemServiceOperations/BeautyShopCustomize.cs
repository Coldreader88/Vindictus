using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class BeautyShopCustomize : Operation
	{
		public IList<CustomizeItemRequestInfo> CustomizeItems { get; set; }

		public int PaintingPosX { get; set; }

		public int PaintingPosY { get; set; }

		public int PaintingSize { get; set; }

		public int PaintingRotation { get; set; }

		public int NexonSN { get; set; }

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

		public BeautyShopCustomize(BeautyShopCustomizeArgument argument)
		{
			this.NexonSN = argument.NexonSN;
			this.CustomizeItems = argument.CustomizeItems;
			this.PaintingPosX = argument.PaintingPosX;
			this.PaintingPosY = argument.PaintingPosY;
			this.PaintingSize = argument.PaintingSize;
			this.PaintingRotation = argument.PaintingRotation;
			this.payment = argument.payment;
			this.BodyPaintingPosX = argument.BodyPaintingPosX;
			this.BodyPaintingPosY = argument.BodyPaintingPosY;
			this.BodyPaintingSize = argument.BodyPaintingSize;
			this.BodyPaintingRotation = argument.BodyPaintingRotation;
			this.BodyPaintingSide = argument.BodyPaintingSide;
			this.BodyPaintingClip = argument.BodyPaintingClip;
			this.BodyPaintingMode = argument.BodyPaintingMode;
			this.HeightValue = argument.HeightValue;
			this.BustValue = argument.BustValue;
			this.ShinenessValue = argument.ShinenessValue;
			this.SkinColor = argument.SkinColor;
			this.EyeColor = argument.EyeColor;
			this.EyebrowItemClass = argument.EyebrowItemClass;
			this.EyebrowColor = argument.EyebrowColor;
			this.LookChangeItemClass = argument.LookChangeItemClass;
			this.LookChangeDuration = argument.LookChangeDuration;
			this.LookChangePrice = argument.LookChangePrice;
			this.BodyShapeInfo = argument.BodyShapeInfo;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
