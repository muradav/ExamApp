using AutoMapper;
using Exam.Dto.Dtos.AccountDto;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Mapping
{
    public class AuthenticationMapperProfile : Profile
    {
        public AuthenticationMapperProfile()
        {
            CreateMap<AppUser, RegistrationRequestDto>().ReverseMap();
            CreateMap<AppUser, RegistrationUserDto>().ReverseMap();
        }
    }
}
