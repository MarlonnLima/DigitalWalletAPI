using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Interfaces
{
    public interface IBaseApiController
    {
        public IActionResult List();
        public IActionResult GetOne(int id);
    }
}
