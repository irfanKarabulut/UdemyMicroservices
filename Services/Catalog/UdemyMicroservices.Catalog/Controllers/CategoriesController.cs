using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UdemyMicroservices.Catalog.Dtos;
using UdemyMicroservices.Catalog.Models;
using UdemyMicroservices.Catalog.Services;
using UdemyMicroservices.Shared.Dtos;

namespace UdemyMicroservices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();

            Console.WriteLine("Categories GetAll metoduna girildi.");

            // TODO : categorilerin insert edildiği bir sayfa tasarlanacak
            if (response == null)
            {
                await Create(new CategoryDto { Name = "Asp.net Core MVC" });
                await Create(new CategoryDto { Name = "Asp.net Core API" });
                await Create(new CategoryDto { Name = "Microsoft Sql Server" });
                await Create(new CategoryDto { Name = "C# Programming Language" });

                response = await _categoryService.GetAllAsync();

                Console.WriteLine("Test Dataları yüklendi...");
            }

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            return new ObjectResult(response) { StatusCode = response.StatusCode };
        }
    }
}
