using Exam.Dto.AppModel;
using Exam.Dto.Dtos.AccountDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Managers.IManagers
{
    public interface IAuthenticateManager
    {
        Task<ResultModel<bool>> Registration(RegistrationRequestDto model);
        Task<ResultModel<LoginResponseDto>> Login(LoginRequestDto model);
        Task<ResultModel<bool>> CreateRole(string role);
        Task<ResultModel<bool>> RegistrationUser(RegistrationUserDto model);
        JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
