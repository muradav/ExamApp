using AutoMapper;
using Exam.Dto.Dtos.QuestionDto;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Mapping
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Answer, AnswerResponseDto>().ReverseMap();
            CreateMap<Question, QuestionResponseDto>().ReverseMap();
            CreateMap<Answer, AnswerUpdateDto>().ReverseMap();
            CreateMap<Question, QuestionUpdateDto>().ReverseMap();
            CreateMap<Answer, AnswerCreateDto>().ReverseMap();
            CreateMap<Question, QuestionCreateDto>().ReverseMap();
        }
    }
}
