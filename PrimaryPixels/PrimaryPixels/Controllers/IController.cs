using Microsoft.AspNetCore.Mvc;

namespace PrimaryPixels.Controllers
{
    public interface IController<T>
    {
        Task<IActionResult> GetAll();
        Task<IActionResult> GetById(int id);
        Task<IActionResult> Add(T entity);
        Task<IActionResult> Update(T entity);
        Task<IActionResult> Delete(int id);

    }
}
