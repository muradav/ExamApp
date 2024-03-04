
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
            CreateMap<Answer, AnswerResponseDto>().ReverseMap();
            CreateMap<Question, QuestionResponseDto>().ReverseMap();
            CreateMap<Answer, AnswerUpdateDto>().ReverseMap();
            CreateMap<Question, QuestionUpdateDto>().ReverseMap();
            CreateMap<Answer, AnswerCreateDto>().ReverseMap();
            CreateMap<Question, QuestionCreateDto>().ReverseMap();
            #endregion

            #region ExaminationMapper
            CreateMap<Examination, ExaminationDto>().ReverseMap();
            CreateMap<Answer, ExamAnswerDto>().ReverseMap();
            CreateMap<Question, ExamQuestionDto>().ReverseMap();
            CreateMap<Examination, ExaminationResponseDto>().ReverseMap();
            CreateMap<Examination, ExamDetailExportDto>().ForMember(d=> d.ExamCategory, map => map.MapFrom(s => s.ExamCategory.Name))
                            .ForMember(d => d.InCorrectAnswersCount, map => map.MapFrom(s => s.Questions.Count() - s.CorrectAnswersCount));
            #endregion

            #region ExamCategoryMapper
            CreateMap<ExamCategory, ExamCategoryDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryCreateDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryUpdateDto>().ReverseMap();
            #endregion

            #region ExamDetailMapper
            CreateMap<ExaminationDetail, ExamDetailsResponseDto>();
            #endregion

            #region AuthenticationMapper
            CreateMap<AppUser, RegistrationRequestDto>().ReverseMap();
            CreateMap<AppUser, RegistrationUserDto>().ReverseMap();
            #endregion
        }
    }
}
