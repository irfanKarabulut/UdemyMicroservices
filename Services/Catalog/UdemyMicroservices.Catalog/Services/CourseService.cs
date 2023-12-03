using AutoMapper;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyMicroservices.Catalog.Dtos;
using UdemyMicroservices.Catalog.Models;
using UdemyMicroservices.Catalog.Settings;
using UdemyMicroservices.Shared.Dtos;
using UdemyMicroservices.Shared.Messages;

namespace UdemyMicroservices.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService, IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);

            _mapper = mapper;
            _categoryService = categoryService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    var result = await _categoryService.GetByIdAsync(course.CategoryId);
                    if (result.IsSuccesful)
                    {
                        course.Category = _mapper.Map<Category>(result.Data);
                    }
                }
            }

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(course => course.Id == id).FirstOrDefaultAsync();

            if (course == null)
            {
                return Shared.Dtos.Response<CourseDto>.Fail("Course not found", 404);
            }

            var result = await _categoryService.GetByIdAsync(course.CategoryId);
            if (result.IsSuccesful)
            {
                course.Category = _mapper.Map<Category>(result.Data);
            }

            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Shared.Dtos.Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    var result = await _categoryService.GetByIdAsync(course.CategoryId);
                    if (result.IsSuccesful)
                    {
                        course.Category = _mapper.Map<Category>(result.Data);
                    }
                }
            }

            return Shared.Dtos.Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Shared.Dtos.Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);
            newCourse.CreatedTime = DateTime.Now;

            await _courseCollection.InsertOneAsync(newCourse);

            return Shared.Dtos.Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Shared.Dtos.Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var filter = Builders<Course>.Filter
                .Eq(course => course.Id, courseUpdateDto.Id);

            var update = Builders<Course>.Update
                .Set(course => course.Name, courseUpdateDto.Name)
                .Set(course => course.Price, courseUpdateDto.Price)
                .Set(course => course.Description, courseUpdateDto.Description)
                .Set(course => course.UserId, courseUpdateDto.UserId)
                .Set(course => course.PictureUrl, courseUpdateDto.PictureUrl)
                .Set(course => course.Feature.Duration, courseUpdateDto.Feature.Duration)
                .Set(course => course.CategoryId, courseUpdateDto.CategoryId);

            var result = await _courseCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 1 && result.ModifiedCount == 1)
            {
                await _publishEndpoint.Publish<CourseNameChangedEvent>(new CourseNameChangedEvent
                {
                    CourseId = courseUpdateDto.Id,
                    UpdatedName = courseUpdateDto.Name
                });

                return Shared.Dtos.Response<NoContent>.Success(204);
            }

            return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
        }

        public async Task<Shared.Dtos.Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if (result.DeletedCount < 1)
            {
                return Shared.Dtos.Response<NoContent>.Fail("Course not found", 404);
            }

            return Shared.Dtos.Response<NoContent>.Success(204);
        }
    }
}
