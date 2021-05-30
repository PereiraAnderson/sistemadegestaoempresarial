using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Infra.Utils
{
	public class SMTP
	{
		private readonly string Username;
		private readonly string Password;
		private readonly string Host;
		private readonly int Port;
		private SmtpClient Client;
		private readonly IConfiguration _config;

		public SMTP(string username, string pass, string host, int port)
		{
			Username = username;
			Password = pass;
			Host = host;
			Port = port;
		}

		public SMTP(IConfiguration config)
		{
			_config = config;
			Username = _config.GetSection("Email:UserName").Value;
			Password = _config.GetSection("Email:Pass").Value;
			Host = _config.GetSection("Email:Host").Value;
			Port = Int16.Parse(_config.GetSection("Email:Port").Value);
		}

		public void Connect()
		{
			try
			{
				Client = new SmtpClient
				{
					Port = Port,
					Host = Host,
					Timeout = 10000,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					Credentials = new NetworkCredential(Username, Password),
					EnableSsl = false
				};
			}
			catch (Exception e)
			{
				System.Console.Write("smtp connect: " + e);
			}
		}

		public bool Send(MailMessage email)
		{
			try
			{
				Client.Send(email);
				Client.Dispose();
				return true;
			}
			catch (Exception e)
			{
				System.Console.Write("smtp send: " + e);
				Client.Dispose();
				return false;
			}
		}

	}
}