using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HeroesOpTool
{
	public class Utility
	{
		public static void ShowInformationMessage(string message)
		{
			MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void ShowInformationMessage(IWin32Window owner, string message)
		{
			MessageBox.Show(owner, message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		public static void ShowWarningMessage(string message)
		{
			MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public static void ShowErrorMessage(string message)
		{
			MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		public static void ShowErrorMessage(IWin32Window owner, string message)
		{
			MessageBox.Show(owner, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		public static bool InputYesNoFromWarning(string message)
		{
			return MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
		}

		public static byte[] GetHashedPassword(string password)
		{
			return new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(password));
		}

		public static DateTime ConvertTime32(int time32)
		{
			return new DateTime((long)time32 * 10000000L + Utility.CTimeBase).ToLocalTime();
		}

		public static int ConvertDateTime(DateTime time)
		{
			return (int)((time.ToUniversalTime().Ticks - 621355968000000000L) / 10000000L);
		}

		public static object[] MergeSort(IList list1, IList list2)
		{
			int i = 0;
			int j = 0;
			object[] array = new object[list1.Count + list2.Count];
			if (list1.Count > 0 && list2.Count > 0)
			{
				for (;;)
				{
					int num = ((IComparable)list1[i]).CompareTo(list2[j]);
					if (num <= 0)
					{
						array[i + j] = list1[i];
						if (++i == list1.Count)
						{
							break;
						}
					}
					else
					{
						array[i + j] = list2[j];
						if (++j == list2.Count)
						{
							break;
						}
					}
				}
			}
			while (i < list1.Count)
			{
				array[i + j] = list1[i];
				i++;
			}
			while (j < list2.Count)
			{
				array[i + j] = list2[j];
				j++;
			}
			return array;
		}

		public static string ConvertLogListItemToString(ICollection itemList)
		{
			StringBuilder stringBuilder = new StringBuilder(1048576);
			foreach (object obj in itemList)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				stringBuilder.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", new object[]
				{
					listViewItem.SubItems[0].Text,
					listViewItem.SubItems[1].Text,
					listViewItem.SubItems[2].Text,
					listViewItem.SubItems[3].Text
				}));
			}
			return stringBuilder.ToString();
		}

		public static string ShowInputBox(string description, string defaultString)
		{
			return InputBox.Show(description, defaultString);
		}

		private static readonly long CTimeBase = new DateTime(1970, 1, 1).Ticks;

		public class NumericTextBox : TextBox
		{
			protected override void OnKeyPress(KeyPressEventArgs e)
			{
				base.OnKeyPress(e);
				NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
				string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
				string negativeSign = numberFormat.NegativeSign;
				string text = e.KeyChar.ToString();
				if (!char.IsDigit(e.KeyChar) && !text.Equals(numberDecimalSeparator) && !text.Equals(negativeSign))
				{
					if (e.KeyChar == '\b')
					{
						return;
					}
					e.Handled = true;
				}
			}
		}

		public interface ILViewComparer : IComparer
		{
			bool IsAscending { get; set; }
		}

		public class ListViewItemComparer : Utility.ILViewComparer, IComparer
		{
			public bool IsAscending { get; set; }

			public ListViewItemComparer()
			{
				this.col = 0;
			}

			public ListViewItemComparer(int column)
			{
				this.col = column;
			}

			public int Compare(object x, object y)
			{
				return (this.IsAscending ? 1 : -1) * string.Compare(((ListViewItem)x).SubItems[this.col].Text, ((ListViewItem)y).SubItems[this.col].Text);
			}

			private int col;
		}

		public class ListViewItemImageIndexComparer : Utility.ILViewComparer, IComparer
		{
			public bool IsAscending { get; set; }

			public int Compare(object x, object y)
			{
				return (this.IsAscending ? 1 : -1) * ((ListViewItem)x).ImageIndex.CompareTo(((ListViewItem)y).ImageIndex);
			}
		}

		public class ListViewItemIPComparer : Utility.ILViewComparer, IComparer
		{
			public bool IsAscending { get; set; }

			public ListViewItemIPComparer()
			{
				this.col = 0;
			}

			public ListViewItemIPComparer(int column)
			{
				this.col = column;
			}

			public int Compare(object x, object y)
			{
				IPAddress ipaddress = IPAddress.Parse(((ListViewItem)x).SubItems[this.col].Text);
				IPAddress ipaddress2 = IPAddress.Parse(((ListViewItem)y).SubItems[this.col].Text);
				if (ipaddress == null || ipaddress2 == null)
				{
					return 0;
				}
				byte[] addressBytes = ipaddress.GetAddressBytes();
				byte[] addressBytes2 = ipaddress2.GetAddressBytes();
				long num = (long)((int)addressBytes[0] * 256 * 256 * 256 + (int)addressBytes[1] * 256 * 256 + (int)addressBytes[2] * 256 + (int)addressBytes[3]);
				long num2 = (long)((int)addressBytes2[0] * 256 * 256 * 256 + (int)addressBytes2[1] * 256 * 256 + (int)addressBytes2[2] * 256 + (int)addressBytes2[3]);
				if (num > num2)
				{
					if (!this.IsAscending)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (num2 <= num)
					{
						return 0;
					}
					if (!this.IsAscending)
					{
						return 1;
					}
					return -1;
				}
			}

			private int col;
		}

		public class StringNumberComparer : Utility.ILViewComparer, IComparer
		{
			public bool IsAscending { get; set; }

            public int Compare(object obj1, object obj2)
            {
                string s = (string)obj1;
                string str2 = (string)obj2;
                int index = 0;
                while (true)
                {
                    if (index == s.Length)
                    {
                        if (index == str2.Length)
                        {
                            return 0;
                        }
                        if (!this.IsAscending)
                        {
                            return 1;
                        }
                        return -1;
                    }
                    if (index == str2.Length)
                    {
                        if (!this.IsAscending)
                        {
                            return -1;
                        }
                        return 1;
                    }
                    if (char.IsDigit(s[index]) && char.IsDigit(str2[index]))
                    {
                        int num2 = 0;
                        while (true)
                        {
                            if (index == s.Length)
                            {
                                if (index == str2.Length)
                                {
                                    return num2;
                                }
                                if (!this.IsAscending)
                                {
                                    return 1;
                                }
                                return -1;
                            }
                            if (index == str2.Length)
                            {
                                if (!this.IsAscending)
                                {
                                    return -1;
                                }
                                return 1;
                            }
                            if (!char.IsDigit(s, index))
                            {
                                if (char.IsDigit(str2, index))
                                {
                                    if (!this.IsAscending)
                                    {
                                        return 1;
                                    }
                                    return -1;
                                }
                                if (num2 != 0)
                                {
                                    return num2;
                                }
                                break;
                            }
                            if (!char.IsDigit(str2, index))
                            {
                                if (!this.IsAscending)
                                {
                                    return -1;
                                }
                                return 1;
                            }
                            if (num2 == 0)
                            {
                                num2 = s[index] - str2[index];
                            }
                            index++;
                        }
                    }
                    int num3 = s[index] - str2[index];
                    if (num3 != 0)
                    {
                        return ((this.IsAscending ? 1 : -1) * num3);
                    }
                    index++;
                }
            }
        }
	}
}
