using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Services;
using DigitalWalletAPI.DTOs;
using DigitalWalletAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/wallet")]
    public class WalletController : ControllerBase, IBaseApiController
    {

        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        /// <summary>
        /// Adiciona saldo a uma carteira
        /// </summary>
        [HttpPost]
        [Route("balance/")]
        public IActionResult AddBalance([FromBody] AddBalanceDTO model)
        {
            try
            {
                _walletService.AddBalance(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recupera uma carteira
        /// </summary>
        /// <param name="userId">id do usuário</param>
        /// <returns>uma carteira</returns>
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetOne(int userId)
        {
            try
            {
                var wallet = _walletService.GetOne(userId);
                return Ok(wallet);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recupera uma lista de carteiras
        /// </summary>
        /// <returns>uma lista de carteiras</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
