using System;
using Microsoft.AspNetCore.Mvc;
using OPS_API.Domain.Services;

namespace OPS_API.Controllers
{
    public class InboundMessage
    {
        public string from { get; set; }
        public string to { get; set; }
        public string text { get; set; }
        public string time { get; set; }
        public string msgid { get; set; }
    }

    [ApiController]
    [Route("/api/messages/inbound")]
    public class InboundMessagesController : Controller
    {
        private IMessageService _service;
        
        public InboundMessagesController(IMessageService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public IActionResult OnInboundMessage([FromQuery] InboundMessage message)
        {
            Console.WriteLine($"Message from {message.from}: {message.text}");

            return Ok(new {message});
        }

        [HttpPost]
        public IActionResult SendMessage()
        {
            _service.SendMessage("+37258072364", "test");
            
            return Ok();
        }
    }
}