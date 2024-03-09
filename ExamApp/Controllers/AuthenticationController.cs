using Exam.Business.Managers.IManagers;
using Exam.Dto.Dtos.AccountDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateManager AuthenticateManager;


        public AuthenticationController(IAuthenticateManager AuthenticateManager)
        {
            this.AuthenticateManager = AuthenticateManager;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequestDto model)
        {
            var result = await AuthenticateManager.Registration(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var result = await AuthenticateManager.Login(model);
            return Ok(result);
        }

        [HttpGet("creationRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string role) 
        {
            var result = await AuthenticateManager.CreateRole(role);
            return Ok(result);
        }

        [HttpPost("registrationUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegistrationUser(RegistrationUserDto model)
        {
            var result = await AuthenticateManager.RegistrationUser(model);
            return Ok(result);
        }

    }
}
