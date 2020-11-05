using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Core.Extensions
{
    public class MailExtensions
    {
		public static bool MailSend(List<string> mailTo, List<string> mailCC, List<string> mailBCC, string subject, string body)
		{
			try
			{
				MailMessage msg = new MailMessage();
				SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
				client.EnableSsl = true;
				client.Timeout = 100000;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential("burak@tuas.com.tr", "Burak1978");

				if (mailTo.Count == 0)
				{
					//return 0;
				}
				if (mailTo != null)
				{
					foreach (var mTo in mailTo)
					{
						if (IsValid(Convert.ToString(mTo).Trim().ToLower()))
						{
							msg.To.Add(mTo);
						}
					}
				}
				if (mailCC != null)
				{
					foreach (var mCC in mailCC)
					{
						if (IsValid(Convert.ToString(mCC).Trim().ToLower()))
						{
							msg.To.Add(mCC);
						}
					}
				}
				if (mailBCC != null)
				{
					foreach (var mBCC in mailBCC)
					{
						if (IsValid(Convert.ToString(mBCC).Trim().ToLower()))
						{
							msg.Bcc.Add(mBCC);
						}
					}
				}

				msg.From = new MailAddress("burak@tuas.com.tr");
				msg.Subject = subject;
				msg.IsBodyHtml = true;
				msg.BodyEncoding = System.Text.Encoding.UTF8;
				msg.Body = body;
				client.Send(msg);

				return true;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);

			}
		}

		public static bool IsValid(string emailaddress)
		{
			try
			{
				MailAddress m = new MailAddress(emailaddress);
				return true;
			}
			catch (FormatException)
			{

				return false;
			}
		}
	}
}
