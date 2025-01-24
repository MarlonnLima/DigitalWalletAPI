using DigitalWalletAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase, IBaseApiController
    {

        private readonly ILogger<WalletController> _logger;

        public WalletController(ILogger<WalletController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adiciona saldo a uma carteira
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("{id}/balance")]
        public IActionResult AddBalance()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recupera uma carteira
        /// </summary>
        /// <param name="id"></param>
        /// <returns>uma carteira</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recupera uma lista de carteiras
        /// </summary>
        /// <returns>uma lista de carteiras</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public IActionResult List()
        {
            throw new NotImplementedException();
        }
    }
}
