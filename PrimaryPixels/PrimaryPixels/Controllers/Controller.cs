using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Services.Repositories;
namespace PrimaryPixels.Controllers;


[Route("api/[controller]")]
[ApiController]
public abstract class Controller<T> : ControllerBase, IController<T>
{
    protected IRepository<T> _repository;
    protected ILogger<Controller<T>> _logger;
    protected Controller(ILogger<Controller<T>> logger, IRepository<T> repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [HttpPost(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> Add([FromBody] T entity)
    {
        try
        {
            int idOfAddedEntity = await _repository.Add(entity);
            _logger.LogInformation($"{typeof(T).Name} with id {idOfAddedEntity} successfully added!");
            return Ok(idOfAddedEntity);
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    [HttpDelete("{id}"), Authorize(Roles = "Admin") ]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deletedEntityId = await _repository.DeleteById(id);
            _logger.LogInformation($"{typeof(T).Name} successfully deleted!");
            return Ok(deletedEntityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
       
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> GetAll()
    {
        try
        {
            T[] entities = (T[])await _repository.GetAll();
            _logger.LogInformation($"{typeof(T).Name}s successfully retrieved!");
            return Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
        
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> GetById(int id)
    {
        try
        {
            T retrievedEntity = await _repository.GetById(id);
            _logger.LogInformation($"{typeof(T).Name} with id {id} successfully retrieved!");
            return Ok(retrievedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }
    }
    [HttpPut, Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> Update([FromBody] T entity)
    {
        try
        {
            int idOfUpdatedEntity = await _repository.Update(entity);
            _logger.LogInformation($"{typeof(T).Name} with id {idOfUpdatedEntity} successfully updated!");
            return Ok(idOfUpdatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
}