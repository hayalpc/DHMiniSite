using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace DHRabbitMQCore.Entities
{
    public class MailMessageData
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailMessage GetMailMessage()
        {
            var mailMessage = new MailMessage
            {
                Subject = this.Subject,
                Body = this.Body,
            };
            mailMessage.To.Add(To);
            return mailMessage;
        }


    }
}
