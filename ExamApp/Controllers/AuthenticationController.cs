using AutoMapper;
using Exam.Business.Managers;
using Exam.Dto.Dtos.AccountDto;
using Exam.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticateManager AuthenticateManager;


        public AuthenticationController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            AuthenticateManager = new AuthenticateManager(userManager, roleManager, configuration, mapper);
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

        [HttpGet]
        public async Task<IActionResult> CreateRole() 
        {
            await AuthenticateManager.CreateRole();
            return Ok("Roles Created");
        }

    }
}
