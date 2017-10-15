using System;
using System.Text;

namespace Nexon.Com.Playfeed.SDK
{
    internal class FeedatomJsonSerializer
    {
        public FeedatomJsonSerializer()
        {
        }

        public static StringBuilder EscapeString(string s)
        {
            int length = s.Length;
            StringBuilder stringBuilder = new StringBuilder(length * 2);
            for (int index = 0; index < length; ++index)
            {
                char ch = s[index];
                switch (ch)
                {
                    case '\b':
                        stringBuilder.Append("\\b");
                        break;
                    case '\t':
                        stringBuilder.Append("\\t");
                        break;
                    case '\n':
                        stringBuilder.Append("\\n");
                        break;
                    case '\f':
                        stringBuilder.Append("\\f");
                        break;
                    case '\r':
                        stringBuilder.Append("\\r");
                        break;
                    case '"':
                    case '\\':
                        stringBuilder.Append('\\');
                        stringBuilder.Append(ch);
                        break;
                    default:
                        stringBuilder.Append(ch);
                        break;
                }
            }
            return stringBuilder;
        }

        public static string ToPlayfeedJson(string serviceCode, uint feedTypeNo, uint logType, uint feedCategory, uint userNo, string gamefeed)
        {
            object[] objArray = new object[] { "{\"serviceCode\":\"", serviceCode, "\",\"feedTypeNo\":", feedTypeNo, ",\"type\":", logType, ",\"category\":", feedCategory, ",\"userNo\":", userNo, ",\"gameLog\":\"", FeedatomJsonSerializer.EscapeString(gamefeed).ToString(), "\"}" };
            return string.Concat(objArray);
        }
    }
}