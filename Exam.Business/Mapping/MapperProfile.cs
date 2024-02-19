
using AutoMapper;
using Exam.Business.Dtos.ExamCategoryDto;
using Exam.Business.Dtos.ExaminationDto;
using Exam.Business.Dtos.QuestionDto;
using Exam.Entities.Models;

namespace Exam.Business.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region QuestionMapper
            CreateMap<Question, QuestionDto>().ReverseMap();
            #endregion

            #region ExaminationMapper
            CreateMap<Examination, ExaminationDto>().ReverseMap();
            #endregion

            #region ExamCategoryMapper
            CreateMap<ExamCategory, ExamCategoryDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryCreateDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
