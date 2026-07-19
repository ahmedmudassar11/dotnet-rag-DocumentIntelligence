using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RabbitMqProducer _producer;

        public UsersController(RabbitMqProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            var message = new UserRegisteredMessage
            {
                UserId = 1,
                Name = "Ali",
                Email = "ali@gmail.com"
            };

            await _producer.PublishAsync(message);

            return Ok("User Registered");
        }
    }
}
