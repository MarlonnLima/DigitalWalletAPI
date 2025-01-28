using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Interfaces
{
    public interface IBaseApiController
    {
        public IActionResult GetAll();
        public IActionResult GetOne(int id);
    }
}
