using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace AspNetCoreAdminPanel.UI.Services
{
    public class MailManager : IMailService
    {
        private IHostingEnvironment _hostingEnviroment;
        private IEmailConfiguration _emailConfiguration;
        public MailManager(IHostingEnvironment hostingEnviroment, IEmailConfiguration emailConfiguration)
        {
            _hostingEnviroment = hostingEnviroment;
            _emailConfiguration = emailConfiguration;
        }
        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage(emailMessage);
            message.To.AddRange(emailMessage.ToAddress.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddress.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject=emailMessage.Subject;

            var templatePath = _hostingEnviroment.WebRootPath + Path.DirectorySeparatorChar.ToString() + "MailTemplate" +
                Path.DirectorySeparatorChar.ToString() + "MailTemplate.html";
            var builder = new BodyBuilder();
            using (StreamReader sourceReader= File.OpenText(templatePath))
            {
                builder.HtmlBody=sourceReader.ReadToEnd();
            }
            string messageBody = string.Format(builder.HtmlBody, emailMessage.Subject, emailMessage.Content);

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = messageBody
            };

            try
            {
                using(var emailClient=new SmtpClient())
                {
                    emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                };
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
