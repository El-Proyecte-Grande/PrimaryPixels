using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Repositories;
namespace PrimaryPixels.Controllers.DerivedControllers
{
    public class ProductController : Controller<Product>
    {
        public ProductController(ILogger<Controller<Product>> logger, IRepository<Product> repository) : base(logger, repository)
        {
        }
    }
}
