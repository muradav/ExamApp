using AutoMapper;
using Exam.Business.Managers.IManagers;
using Exam.Dto.AppModel;
using Exam.Dto.Dtos.AccountDto;
using Exam.Entities.Models;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exam.Business.Managers
{
    public class AuthenticateManager : IAuthenticateManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public AuthenticateManager(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, ILog logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultModel<bool>> Registration(RegistrationRequestDto model)
        {
            var result = new ResultModel<bool>();

                var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                result.Message = "User already exists!";
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            AppUser user = _mapper.Map<AppUser>(model);

            var registerResult = await _userManager.CreateAsync(user, model.Password);
            if (!registerResult.Succeeded)
            {
                result.Message = registerResult.ToString();
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            registerResult = await _userManager.AddToRoleAsync(user, "Examinee");
            if (!registerResult.Succeeded)
            {
                result.Message = registerResult.ToString();
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            result.IsSuccess = true;
            result.Message = "User Successfully created.";
            _logger.Info(result.Message);
            return result;
        }

        public async Task<ResultModel<LoginResponseDto>> Login(LoginRequestDto model)
        {
            var result = new ResultModel<LoginResponseDto>();

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                result.Data = new LoginResponseDto
                {
                    User = user,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                result.IsSuccess = true;
                result.Message = "Login Success.";
                _logger.Info(result.Message);

            }


            return result;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        
        public async Task<ResultModel<bool>> CreateRole(string role)
        {
            var result = new ResultModel<bool>();

            var upperRole = char.ToUpper(role[0]) + role.Substring(1);
            await _roleManager.CreateAsync(new IdentityRole { Name = upperRole });

            result.Data = true;
            result.IsSuccess = true;
            result.Message = "Role created";
            _logger.Info(result.Message);

            return result;
        }

        public async Task<ResultModel<bool>> RegistrationUser(RegistrationUserDto model)
        {
            var result = new ResultModel<bool>();

            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                result.Message = "User already exists!";
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            AppUser user = _mapper.Map<AppUser>(model);

            var registerResult = await _userManager.CreateAsync(user, model.Password);
            if (!registerResult.Succeeded)
            {
                result.Message = registerResult.ToString();
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            var upperRole = char.ToUpper(model.Role[0]) + model.Role.Substring(1);
            var roles = _roleManager.FindByNameAsync(upperRole);
            
            if (roles == null)
            {
                result.Message = "Role does not exist";
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            registerResult = await _userManager.AddToRoleAsync(user, upperRole);
            if (!registerResult.Succeeded)
            {
                result.Message = registerResult.ToString();
                result.IsSuccess = false;
                _logger.Info(result.Message);
                return result;
            }

            result.IsSuccess = true;
            result.Message = "User Successfully created.";
            _logger.Info(result.Message);

            return result;
        }
    }
}
