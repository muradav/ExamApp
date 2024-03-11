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
    public class ExamDetailMapperProfile : Profile
    {
        public ExamDetailMapperProfile()
        {
            CreateMap<ExaminationDetail, ExamDetailsResponseDto>();
        }
    }
}
