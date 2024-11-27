using PrimaryPixels.Models;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers.ProductControllers
{
    public class PhoneController : Controller<Phone>
    {
        public PhoneController(ILogger<Controller<Phone>> logger, IRepository<Phone> repository) : base(logger, repository)
        {
        }
    }
}
