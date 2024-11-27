using PrimaryPixels.Models.User;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedClasses;

public class UserController : Controller<User>
{
    public UserController(ILogger<UserController> logger, IRepository<User> repository) : base(logger, repository)
    {
    }
}