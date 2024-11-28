using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers
{
    public class ShoppingCartItemController : Controller<ShoppingCartItem>
    {
        public ShoppingCartItemController(ILogger<Controller<ShoppingCartItem>> logger, IRepository<ShoppingCartItem> repository) : base(logger, repository)
        {
        }
    }
}
