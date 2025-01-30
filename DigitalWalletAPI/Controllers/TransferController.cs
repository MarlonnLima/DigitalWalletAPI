using DigitalWalletAPI.Domain.Services;
using DigitalWalletAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/transfer")]
    public class TransferController : ControllerBase
    {
        private readonly TransferService _transferService;
        public TransferController(TransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public IActionResult Transfer(TransferDTO model)
        {
            try
            {
                _transferService.Transfer(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
