using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace ConsoleAppTestAfLandlystDBContionH2
{
    class Mail
    {
        /// <summary>
        /// virker ikke
        /// </summary>
        public void SendMail()
        {
            string to = "mari01n9@zbc.dk";
            string from = "duniniki@outlook.dk";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = @"Using this new feature, you can send an email message from an application very easily.";
            SmtpClient client = new SmtpClient("smtp.gmail.com",465);
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Credentials = new System.Net.NetworkCredential("duniniki@outlook.dk", "Spason123");
            client.UseDefaultCredentials = true;
            client.Send(message);
            
        }
    }
}
