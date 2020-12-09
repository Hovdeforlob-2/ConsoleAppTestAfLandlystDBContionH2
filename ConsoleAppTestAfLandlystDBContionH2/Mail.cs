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
        public void SendMail(string usrMail, string text, string senderMail, string senderMailPassword, string smtpServer, int port )
        {
            string to = usrMail;
            string from = senderMail;
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Using the new SMTP client.";
            message.Body = text;
            SmtpClient client = new SmtpClient(smtpServer, port);
            SmtpClient SmtpServer = new SmtpClient(smtpServer);
            SmtpServer.Credentials = new System.Net.NetworkCredential(senderMail, senderMailPassword);
            client.UseDefaultCredentials = true;
            client.Send(message);
            
        }
    }
}
