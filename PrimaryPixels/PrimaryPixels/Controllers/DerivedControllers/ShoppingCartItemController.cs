using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        protected IShoppingCartItemRepository _repository;
        protected ILogger<ShoppingCartItemController> _logger;

        public ShoppingCartItemController(ILogger<ShoppingCartItemController> logger,
            IShoppingCartItemRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost(""), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] ShoppingCartItemDTO entity)
        {
            try
            {
                // Get the user ID from claims.
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("UserId not found.");
                }

                // create the real shoppingCartItem instance.
                ShoppingCartItem shoppingCartItem = new()
                    { ProductId = entity.ProductId, UserId = userId, Quantity = 1 };
                // try to add the new shopping cart item to the DB
                int idOfAddedEntity = await _repository.Add(shoppingCartItem);
                _logger.LogInformation(
                    $"{typeof(ShoppingCartItem).Name} with id {idOfAddedEntity} successfully added!");
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
                _logger.LogInformation($"{typeof(ShoppingCartItem).Name} successfully deleted!");
                return Ok(deletedEntityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartItem[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ShoppingCartItem[] entities = (ShoppingCartItem[])await _repository.GetAll();
                _logger.LogInformation($"{typeof(ShoppingCartItem).Name}s successfully retrieved!");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                ShoppingCartItem retrievedEntity = await _repository.GetById(id);
                _logger.LogInformation($"{typeof(ShoppingCartItem).Name} with id {id} successfully retrieved!");
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
        public async Task<IActionResult> Update([FromBody] ShoppingCartItem entity)
        {
            try
            {
                int idOfUpdatedEntity = await _repository.Update(entity);
                _logger.LogInformation(
                    $"{typeof(ShoppingCartItem).Name} with id {idOfUpdatedEntity} successfully updated!");
                return Ok(idOfUpdatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("/api/ShoppingCartItem/user/{userId}"), Authorize]
        public async Task<IActionResult> GetProductForOrder(string userId)
        {
            try
            {
                ShoppingCartItemRepository repository = _repository as ShoppingCartItemRepository;
                var cartProducts = await repository.GetByUserId(userId);
                return Ok(cartProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("user"), Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingCartItemsByUserId()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("UserId not found.");
                }

                var succeed = await _repository.DeleteByUserId(userId);
                if (succeed) return Ok();
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't delete cart items.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new
                    { Message = "An error occurred while deleting order details.", Details = ex.Message });
            }
        }
    }


}