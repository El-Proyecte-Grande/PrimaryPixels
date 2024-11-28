using PrimaryPixels.Models.Users;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers;

public class UserController : Controller<User>
{
    public UserController(ILogger<UserController> logger, IRepository<User> repository) : base(logger, repository)
    {
    }
}