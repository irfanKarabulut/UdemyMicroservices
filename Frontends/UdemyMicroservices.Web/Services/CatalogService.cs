using System.Net.Http.Json;
using UdemyMicroservices.Shared.Dtos;
using UdemyMicroservices.Web.Helpers;
using UdemyMicroservices.Web.Models;
using UdemyMicroservices.Web.Models.Catalog;
using UdemyMicroservices.Web.Services.Interfaces;

namespace UdemyMicroservices.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (resultPhotoService != null) courseCreateInput.PictureUrl = resultPhotoService.Url;

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses",courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            Console.WriteLine("GetAllCategoryAsync istek yapıldı");

            var response = await _httpClient.GetAsync("categories");

            Console.WriteLine("GetAllCategoryAsync istek alındı");

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return result.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");
            if (!response.IsSuccessStatusCode) return null;
            
            var result = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            result.Data.ForEach(data =>
            {
                data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(data.PictureUrl);
            });

            return result.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/getallbyuserid/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            result.Data.ForEach(data =>
            {
                data.StockPictureUrl=_photoHelper.GetPhotoStockUrl(data.PictureUrl);
            });

            return result.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            
            result.Data.StockPictureUrl = _photoHelper.GetPhotoStockUrl(result.Data.PictureUrl);

            return result.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                await _photoStockService.DeletePhoto(courseUpdateInput.PictureUrl);
                courseUpdateInput.PictureUrl = resultPhotoService.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
