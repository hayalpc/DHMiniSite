using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Consts;
using DHRabbitMQCore.Entities;
using System.Net;
using System.Net.Mail;

namespace DHRabbitMQCore.Concrete
{
    public class MailSender : IMailSender
    {
        
        private readonly ISmtpConfiguration _smtpConfiguration;
        public MailSender(ISmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public MailSendResult SendMail(MailMessageData emailMessage)
        {
            Console.WriteLine($"EmailRabbitMQProcessor SendMailAsync method  => Calısma zamanı: {DateTime.Now.ToShortTimeString()}");

            MailSendResult result;
            MailMessage mailMessage = emailMessage.GetMailMessage();
            mailMessage.From = new MailAddress(_smtpConfiguration.From);
            try
            {
                using (var client = CreateSmtpClient(_smtpConfiguration.GetSmtpConfig()))
                {
                    client.Send(mailMessage);
                    string resultMessage = $"donus mesajı metni  {string.Join(",", mailMessage.To)}.";
                    result = new MailSendResult(mailMessage, true, resultMessage);
                    Console.WriteLine($"EmailRabbitMQProcessor running => resultMessage to:{ mailMessage.To}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EmailRabbitMQProcessor exception => " + ex.Message);
                result = new MailSendResult(mailMessage, false, $"Hata: {ex.Message}");
            }
            return result;
        }

        private SmtpClient CreateSmtpClient(SmtpConfig config)
        {

            var smtpClient = new SmtpClient(config.Host)
            {
                Port = config.Port,
                Credentials = new NetworkCredential(config.User, config.Password),
                EnableSsl = config.UseSsl,
            };

            //SmtpClient client = new SmtpClient(config.Host, config.Port)
            //{
            //    EnableSsl = config.UseSsl,
            //    UseDefaultCredentials = !(string.IsNullOrWhiteSpace(config.User) && string.IsNullOrWhiteSpace(config.Password))
            //};
            //if (client.UseDefaultCredentials == true)
            //{
            //    client.Credentials = new NetworkCredential(config.User, config.Password);
            //}


            return smtpClient;
        }



       
    }
}
