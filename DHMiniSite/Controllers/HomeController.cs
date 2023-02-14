using DHData.Models;
using DHMiniSite.Operations.Interfaces;
using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Consts;
using DHRabbitMQCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DHMiniSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostOperations postOperations;
        private readonly IPublisherService publisherService;

        public HomeController(IPostOperations postOperations, IPublisherService publisherService)
        {
            this.postOperations = postOperations;
            this.publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 0)
        {
            if (page > 0)
            {
                page--;
            }
            var list = await postOperations.GetAsync(page);
            return View(list);
        }

        public IActionResult Test()
        {
            var messages = new List<MailMessageData>();
            messages.Add(new MailMessageData()
            {
                To = "erdinc.karaman60@gmail.com",
                Subject = "Test Title",
                Body = "Test Content"
            });

            publisherService.Enqueue(messages, RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString());
            return Ok();
        }

    }
}
