using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DigitalWalletController : ControllerBase
    {

        private readonly ILogger<DigitalWalletController> _logger;

        public DigitalWalletController(ILogger<DigitalWalletController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBalance")]
        public IActionResult Get()
        {
            return NotFound();
        }
    }
}
