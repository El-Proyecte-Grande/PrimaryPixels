﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.ShoppingCartItem;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        protected IRepository<ShoppingCartItem> _repository;
        protected ILogger<ShoppingCartItemController> _logger;
        public ShoppingCartItemController(ILogger<ShoppingCartItemController> logger, IRepository<ShoppingCartItem> repository)
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
                ShoppingCartItem shoppingCartItem = new()
                    { ProductId = entity.ProductId, UserId = entity.UserId, Quantity = 1 };
                int idOfAddedEntity = await _repository.Add(shoppingCartItem);
                _logger.LogInformation($"{typeof(ShoppingCartItem).Name} with id {idOfAddedEntity} successfully added!");
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
                _logger.LogInformation($"{typeof(ShoppingCartItem).Name} with id {idOfUpdatedEntity} successfully updated!");
                return Ok(idOfUpdatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
        
        [HttpGet("/api/ShoppingCartItem/user/{userId}")]
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
    }
}
