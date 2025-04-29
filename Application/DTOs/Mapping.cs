using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.DTOs
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseAddDTO, Course>()
                .ForMember(dest => dest.DiscountDate, opt => opt.Condition(src => src.DiscountDate != null));
            CreateMap<CourseUpdateDTO, Course>()
                .ForMember(dest => dest.DiscountDate, opt => opt.Condition(src => src.DiscountDate != null));
        }
    }

    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuizCreateDTO, Quiz>();
            CreateMap<QuizUpdateDTO, Quiz>();
            CreateMap<QuestionCreateDTO, Question>();
            CreateMap<QuestionUpdateDTO, Question>();
        }
    }
}
