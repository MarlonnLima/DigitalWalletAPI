using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Domain.Services;
using DigitalWalletAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWalletAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase, IBaseApiController
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Recupera todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        [HttpGet]
        public IActionResult List()
        {
            return Ok();
        }

        /// <summary>
        /// Recupera um usuário por id
        /// </summary>
        /// <param name="id">id do usuário</param>
        /// <returns>Um usuário</returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOne(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }

        /// <summary>
        /// Atualiza um usuário
        /// </summary>
        /// <param name="id">id do usuário</param>
        [HttpPost]
        [Route("{id}")]
        public IActionResult Update(int id)
        {
            return Ok();
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        /// <param name="id">id do usuário</param>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
