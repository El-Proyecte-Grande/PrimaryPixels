using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Services;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    protected IRepository<Order> _repository;
    protected ILogger<OrderController> _logger;
    protected IOrderService _orderService;
  
    public OrderController(ILogger<OrderController> logger, IRepository<Order> repository, IOrderService orderService)
    {
        _logger = logger;
        _repository = repository;
        _orderService = orderService;
    }
    [HttpPost(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] OrderDTO entity)
    {
        try
        {
            // Get the userId from claims
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            int orderId = await _orderService.CreateOrder(entity, userId);
            return Ok(orderId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
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