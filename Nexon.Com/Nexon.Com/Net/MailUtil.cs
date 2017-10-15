using System;
using System.Net.Mail;
using System.Text;

namespace Nexon.Com.Net
{
	public static class MailUtil
	{
		public static void Send(MailMessage mailMessage)
		{
			string[] strMailHost = new string[]
			{
				"192.168.4.2",
				"192.168.4.3"
			};
			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Host = strMailHost.Do(delegate(string[] pHost)
			{
				switch (Platform.GetPlatform())
				{
				case emPlatform.service:
				case emPlatform.preservice:
					return pHost[new Random().Next(0, strMailHost.Length)];
				default:
					return "211.218.231.130";
				}
			});
			SmtpClient smtpClient2 = smtpClient;
			smtpClient2.Send(mailMessage);
		}

		public static void Send(string strMailServer, MailMessage mailMessage)
		{
			SmtpClient smtpClient = new SmtpClient
			{
				Host = strMailServer
			};
			smtpClient.Send(mailMessage);
		}

		public static void SendNxInfoMail(MailAddressCollection arrMailAdress, string strMailContents)
		{
			MailUtil.SendNxInfoMail(arrMailAdress, "안녕하세요. 넥슨입니다.", strMailContents);
		}

		public static void SendNxInfoMail(MailAddressCollection arrMailAdress, string strSubject, string strMailContents)
		{
			MailMessage mailMessage = new MailMessage
			{
				From = new MailAddress(string.Format("{0}{1}", MailUtil.strEncodeMimeHeader("넥슨"), "<NEXON-MAILER@nexon.com>")),
				Subject = strSubject,
				Body = strMailContents,
				IsBodyHtml = true,
				BodyEncoding = Encoding.GetEncoding("ks_c_5601-1987")
			};
			foreach (MailAddress item in arrMailAdress)
			{
				mailMessage.To.Add(item);
			}
			MailUtil.Send(mailMessage);
		}

		public static string strEncodeMimeHeader(string strMessage)
		{
			return "=?ks_c_5601-1987?B?" + Convert.ToBase64String(Encoding.GetEncoding("ks_c_5601-1987").GetBytes(strMessage)) + "?=";
		}
	}
}
