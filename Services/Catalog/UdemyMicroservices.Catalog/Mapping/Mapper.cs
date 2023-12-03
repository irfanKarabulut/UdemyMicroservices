using AutoMapper;
using UdemyMicroservices.Catalog.Dtos;
using UdemyMicroservices.Catalog.Models;

namespace UdemyMicroservices.Catalog.Mapping
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        }
    }
}
