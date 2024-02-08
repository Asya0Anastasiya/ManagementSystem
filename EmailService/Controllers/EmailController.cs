using EmailService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet]
        public void Get()
        {
            var message = new Message(new string[] { "nastia.rynkevich@gmail.com" }, "Test email", "This is the content from our email.");
            _emailSender.SendEmail(message);
        }
    }
}
