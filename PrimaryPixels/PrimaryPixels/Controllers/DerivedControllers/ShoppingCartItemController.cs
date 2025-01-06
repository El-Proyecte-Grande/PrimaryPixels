using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers
{
    public class ShoppingCartItemController : Controller<ShoppingCartItem>
    {
        public ShoppingCartItemController(ILogger<Controller<ShoppingCartItem>> logger, IRepository<ShoppingCartItem> repository) : base(logger, repository)
        {
            
        }
        [HttpGet("/api/ShoppingCartItem/user/{userId}")]
        public async Task<IActionResult> GetProductForOrder(string userId)
        {
            try
            {
                ShoppingCartItemRepository repository = _repository as ShoppingCartItemRepository;
                var cartProducts = await repository.GetByUserId(userId);
                return Ok(cartProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
    }
}
