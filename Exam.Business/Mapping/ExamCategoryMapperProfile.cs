using AutoMapper;
using Exam.Dto.Dtos.ExamCategoryDto;
using Exam.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Business.Mapping
{
    public class ExamCategoryMapperProfile : Profile
    {
        public ExamCategoryMapperProfile()
        {
            CreateMap<ExamCategory, ExamCategoryDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryCreateDto>().ReverseMap();
            CreateMap<ExamCategory, ExamCategoryUpdateDto>().ReverseMap();
        }
    }
}
