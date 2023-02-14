using DHRabbitMQCore.Entities;
using System.Text;

namespace DHRabbitMQCore.Abstract
{
    public interface ISmtpConfiguration
    {
        string Host { get; }
        int Port { get; }
        string User { get; }
        string From { get; }
        string Password { get; }
        bool UseSSL { get; }
        SmtpConfig GetSmtpConfig();
    }
}
