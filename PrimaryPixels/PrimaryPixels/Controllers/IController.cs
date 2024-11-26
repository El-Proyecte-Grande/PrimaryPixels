using Microsoft.AspNetCore.Mvc;

namespace PrimaryPixels.Controllers
{
    public interface IController<T>
    {
        IActionResult GetAll();
        IActionResult GetById(int id);
        IActionResult Add(T entity);
        IActionResult Update(T entity);
        IActionResult Delete(int id);

    }
}
