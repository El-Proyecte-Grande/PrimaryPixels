using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.DTO;
using PrimaryPixels.Models.Order;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase, IController<OrderDetails>
{
    protected IOrderDetailsRepository _repository;
    protected ILogger<OrderDetailsController> _logger;
    public OrderDetailsController(ILogger<OrderDetailsController> logger, IOrderDetailsRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    [HttpPost(""), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] OrderDetails entity)
    {
        try
        {
            int idOfAddedEntity = await _repository.Add(entity);
            _logger.LogInformation($"{typeof(OrderDetails).Name} with id {idOfAddedEntity} successfully added!");
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
            _logger.LogInformation($"{typeof(OrderDetails).Name} successfully deleted!");
            return Ok(deletedEntityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }

    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetails[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            OrderDetails[] entities = (OrderDetails[])await _repository.GetAll();
            _logger.LogInformation($"{typeof(OrderDetails).Name}s successfully retrieved!");
            return Ok(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return NotFound();
        }

    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            OrderDetails retrievedEntity = await _repository.GetById(id);
            _logger.LogInformation($"{typeof(OrderDetails).Name} with id {id} successfully retrieved!");
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
    public async Task<IActionResult> Update([FromBody] OrderDetails entity)
    {
        try
        {
            int idOfUpdatedEntity = await _repository.Update(entity);
            _logger.LogInformation($"{typeof(OrderDetails).Name} with id {idOfUpdatedEntity} successfully updated!");
            return Ok(idOfUpdatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("Order/{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDetails[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductForOrderDetails(int orderId)
    {
        try
        {
            OrderDetailsRepository repository = _repository as OrderDetailsRepository;
            // get orderDetails
            var orderProducts = await repository.GetProductsForOrder(orderId);
            // convert orderDetails to orderDetailsResponses with ProductDTO
            var returnData = orderProducts.Select(od => new OrderDetailsResponse()
            {
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                Product = new ProductDTO(){Image = od.Product.Image, Name = od.Product.Name}
            });
            return Ok(returnData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest();
        }
    }

    
}