using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadphoneController : ControllerBase, IController<Headphone>
    {
        protected IRepository<Headphone> _repository;
        protected ILogger<HeadphoneController> _logger;
        public HeadphoneController(ILogger<HeadphoneController> logger, IRepository<Headphone> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpPost(""), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] Headphone entity)
        {
            try
            {
                int idOfAddedEntity = await _repository.Add(entity);
                _logger.LogInformation($"{typeof(Headphone).Name} with id {idOfAddedEntity} successfully added!");
                return Ok(idOfAddedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedEntityId = await _repository.DeleteById(id);
                _logger.LogInformation($"{typeof(Headphone).Name} successfully deleted!");
                return Ok(deletedEntityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Headphone[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                Headphone[] entities = (Headphone[])await _repository.GetAll();
                _logger.LogInformation($"{typeof(Headphone).Name}s successfully retrieved!");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Headphone))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Headphone retrievedEntity = await _repository.GetById(id);
                _logger.LogInformation($"{typeof(Headphone).Name} with id {id} successfully retrieved!");
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
        public async Task<IActionResult> Update([FromBody] Headphone entity)
        {
            try
            {
                int idOfUpdatedEntity = await _repository.Update(entity);
                _logger.LogInformation($"{typeof(Headphone).Name} with id {idOfUpdatedEntity} successfully updated!");
                return Ok(idOfUpdatedEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

    }
}
