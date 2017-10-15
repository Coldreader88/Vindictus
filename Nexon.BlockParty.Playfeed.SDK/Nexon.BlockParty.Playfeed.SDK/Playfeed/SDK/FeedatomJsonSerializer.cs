using System;

namespace Nexon.BlockParty.Playfeed.SDK
{
	internal class FeedatomJsonSerializer
	{
		public static string ToPlayfeedJson(string serviceCode, uint feedTypeNo, uint logType, uint feedCategory, uint userNo, string gamefeed)
		{
			string text = string.Concat(new object[]
			{
				"{\"serviceCode\":\"",
				serviceCode,
				"\",\"feedTypeNo\":",
				feedTypeNo,
				",\"type\":",
				logType,
				",\"category\":",
				feedCategory,
				",\"userNo\":",
				userNo,
				",\"gameLog\":\"",
				gamefeed,
				"\"}"
			});
			if (text.Length > 512)
			{
				return null;
			}
			return text;
		}
	}
}
