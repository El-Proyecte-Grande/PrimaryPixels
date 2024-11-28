using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers.ProductControllers
{
    public class HeadphoneController : Controller<Headphone>
    {
        public HeadphoneController(ILogger<Controller<Headphone>> logger, IRepository<Headphone> repository) : base(logger, repository)
        {
        }
    }
}
