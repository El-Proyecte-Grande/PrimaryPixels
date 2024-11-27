using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedClasses;

public class OrderController : Controller<Order>
{
    public OrderController(ILogger<Controller<Order>> logger, IRepository<Order> repository) : base(logger, repository)
    {
    }
}