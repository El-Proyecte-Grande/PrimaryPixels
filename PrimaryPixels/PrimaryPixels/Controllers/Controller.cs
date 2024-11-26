using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Services.Repositories;
namespace PrimaryPixels.Controllers;


[ApiController]
[Route("[controller]")]
public abstract class Controller<T> : ControllerBase, IController<T>
{
    protected IRepository<T> _repository;
    protected ILogger<Controller<T>> _logger;
    protected Controller(ILogger<Controller<T>> logger, IRepository<T> repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual IActionResult Add([FromBody] T entity)
    {
        try
        {
            int? idOfAddedEntity = _repository.Add(entity);
            _logger.LogInformation($"{nameof(T)} with id {idOfAddedEntity} successfully added!");
            return Ok(idOfAddedEntity);
        }
        catch (Exception ex) 
        {
            _logger.LogError($"Error during adding new {nameof(T)}!");
            return BadRequest();
        }
    }
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult Delete(int id)
    {
        try
        {
            _repository.DeleteById(id);
            _logger.LogInformation($"{nameof(T)} successfully deleted!");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during deleting {nameof(T)}!");
            return NotFound();
        }
       
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult GetAll()
    {
        try
        {
            IEnumerable<T> entities = _repository.GetAll();
            _logger.LogInformation($"{nameof(T)}s successfully retrieved!");
            return Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during retrieving {nameof(T)}s !");
            return NotFound();
        }
        
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual IActionResult GetById(int id)
    {
        try
        {
            T retrievedEntity = _repository.GetById(id);
            _logger.LogInformation($"{nameof(T)} with id {id} successfully retrieved!");
            return Ok(retrievedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during retrieving {nameof(T)}!");
            return NotFound();
        }
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual IActionResult Update([FromBody] T entity)
    {
        try
        {
            int? idOfUpdatedEntity = _repository.Update(entity);
            _logger.LogInformation($"{nameof(T)} with id {idOfUpdatedEntity} successfully updated!");
            return Ok(idOfUpdatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error during updating {nameof(T)}!");
            return BadRequest();
        }
    }
}