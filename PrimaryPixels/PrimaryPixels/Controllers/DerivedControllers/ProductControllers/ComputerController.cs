using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Repositories;
namespace PrimaryPixels.Controllers.DerivedControllers.ProductControllers
{
    public class ComputerController : Controller<Computer>
    {
        public ComputerController(ILogger<Controller<Computer>> logger, IRepository<Computer> repository) : base(logger, repository)
        {
        }
    }
}
