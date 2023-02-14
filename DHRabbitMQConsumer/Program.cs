using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DHRabbitMQConsumer
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("DHRabbitMQConsumer.ConsumerConsole Program.cs Acıldı.");

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            var configuration = builder.Build();


            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<IRabbitMQConfiguration, RabbitMQConfiguration>()
                .AddSingleton<IRabbitMQService, RabbitMQService>()
                .AddSingleton<ISmtpConfiguration, SmtpConfiguration>()
                .AddSingleton<IMailSender, MailSender>()
                .AddSingleton<IConsumerService, ConsumerManager>()
                .BuildServiceProvider();

            var consumerService = serviceProvider.GetService<IConsumerService>();
            Console.WriteLine("consumerService alındı.");
            Console.WriteLine($"consumerService.Start() başladı: {DateTime.Now.ToShortTimeString()}");
            consumerService.Start();

            Console.ReadKey();

        }
    }
}