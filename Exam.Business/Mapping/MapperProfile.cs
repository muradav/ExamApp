
using AutoMapper;
using Exam.Dto.Dtos.AccountDto;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;

namespace Exam.Business.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region QuestionMapper
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Question, QuestionResponseDto>().ReverseMap();
            CreateMap<Question, QuestionUpdateDto>().ReverseMap();
            #endregion

            #region ExaminationMapper
            CreateMap<Examination, ExaminationDto>().ReverseMap();
            #endregion

            #region ExamCategoryMapper
            CreateMap<ExamCategory, ExamCategoryDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryCreateDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryUpdateDto>().ReverseMap();
            #endregion

            #region AuthenticationMapper
            CreateMap<AppUser, RegistrationRequestDto>().ReverseMap();
            CreateMap<AppUser, RegistrationUserDto>().ReverseMap();
            #endregion
        }
    }
}
