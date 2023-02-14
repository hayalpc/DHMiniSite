using DHRabbitMQCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DHRabbitMQCore.Abstract
{
    public interface IMailSender
    {
        MailSendResult SendMail(MailMessageData emailMessage);
    }
}
