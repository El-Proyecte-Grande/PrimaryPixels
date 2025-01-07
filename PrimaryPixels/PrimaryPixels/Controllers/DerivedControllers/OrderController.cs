using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase, IController<Order>
{
    protected IRepository<Order> _repository;
    protected ILogger<OrderController> _logger;
    public OrderController(ILogger<OrderController> logger, IRepository<Order> repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [HttpPost(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] Order entity)
    {
        try
        {
            int idOfAddedEntity = await _repository.Add(entity);
            _logger.LogInformation($"{typeof(Order).Name} with id {idOfAddedEntity} successfully added!");
            return Ok(idOfAddedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deletedEntityId = await _repository.DeleteById(id);
            _logger.LogInformation($"{typeof(Order).Name} successfully deleted!");
            return Ok(deletedEntityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }

    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            Order[] entities = (Order[])await _repository.GetAll();
            _logger.LogInformation($"{typeof(Order).Name}s successfully retrieved!");
            return Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }

    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            Order retrievedEntity = await _repository.GetById(id);
            _logger.LogInformation($"{typeof(Order).Name} with id {id} successfully retrieved!");
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
    public async Task<IActionResult> Update([FromBody] Order entity)
    {
        try
        {
            int idOfUpdatedEntity = await _repository.Update(entity);
            _logger.LogInformation($"{typeof(Order).Name} with id {idOfUpdatedEntity} successfully updated!");
            return Ok(idOfUpdatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }
}