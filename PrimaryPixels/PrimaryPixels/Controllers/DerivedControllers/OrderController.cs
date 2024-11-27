using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedClasses;

public class OrderController : Controller<Order>
{
    public OrderController(ILogger<OrderController> logger, IRepository<Order> repository) : base(logger, repository)
    {
    }
}