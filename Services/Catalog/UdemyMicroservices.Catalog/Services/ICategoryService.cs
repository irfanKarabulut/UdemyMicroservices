using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyMicroservices.Catalog.Dtos;
using UdemyMicroservices.Catalog.Models;
using UdemyMicroservices.Shared.Dtos;

namespace UdemyMicroservices.Catalog.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}