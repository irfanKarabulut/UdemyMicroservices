using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyMicroservices.Basket.Dtos;
using UdemyMicroservices.Basket.Services;
using UdemyMicroservices.Shared.Services;

namespace UdemyMicroservices.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var result = await _basketService.GetBasket(_sharedIdentityService.GetUserId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            basketDto.UserId = _sharedIdentityService.GetUserId;
            var result = await _basketService.SaveOrUpdate(basketDto);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            var result = await _basketService.Delete(_sharedIdentityService.GetUserId);

            return Ok();
        }
    }
}
