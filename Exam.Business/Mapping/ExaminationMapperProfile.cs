using AutoMapper;
using Exam.Dto.Dtos.ExaminationDto;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Mapping
{
    public class ExaminationMapperProfile : Profile
    {
        public ExaminationMapperProfile()
        {
            CreateMap<Examination, ExaminationDto>().ReverseMap();
            CreateMap<Answer, ExamAnswerDto>().ReverseMap();
            CreateMap<Question, ExamQuestionDto>().ReverseMap();
            CreateMap<Examination, ExaminationResponseDto>().ReverseMap();
            CreateMap<Examination, ExamDetailExportDto>().ForMember(d => d.ExamCategory, map => map.MapFrom(s => s.ExamCategory.Name))
                            .ForMember(d => d.InCorrectAnswersCount, map => map.MapFrom(s => s.Questions.Count() - s.CorrectAnswersCount));
        }
    }
}
