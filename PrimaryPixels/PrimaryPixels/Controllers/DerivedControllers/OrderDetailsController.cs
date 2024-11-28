using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers;

public class OrderDetailsController : Controller<OrderDetails>
{
    public OrderDetailsController(ILogger<OrderDetailsController> logger, IRepository<OrderDetails> repository) : base(logger, repository)
    {
    }

    [HttpGet("/api/OrderDetails/order/{orderId}")]
    public async Task<IActionResult> GetProductForOrder(int orderId)
    {
        try
        {
            OrderDetailsRepository repository = _repository as OrderDetailsRepository;
            var orderProducts = await repository.GetProductsForOrder(orderId);
            return Ok(orderProducts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
}