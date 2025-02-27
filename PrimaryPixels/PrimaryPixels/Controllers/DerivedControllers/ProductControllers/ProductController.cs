using System.Collections;
using Microsoft.AspNetCore.Mvc;
using PrimaryPixels.Models.Products;
using PrimaryPixels.Services.Repositories;

namespace PrimaryPixels.Controllers.DerivedControllers.ProductControllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> GetAll()
    {
        try
        {
            IEnumerable<Product> products = await _repository.GetAll();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
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
            Product product = await _repository.GetById(id);
            return Ok(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("Popular")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> GetPopulars()
    {
        try
        {
            IEnumerable<Product> products = await _repository.GetPopular();
            return Ok(products);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound();
        }
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchProducts([FromQuery(Name = "name")] string word)
    {
        try
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Query string is empty");
            }

            var products = await _repository.Search(word);
            var productDtos = products.Select(p => new ProductDTO() { Image = p.Image, Name = p.Name, Id = p.Id });
            return Ok(productDtos);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return NotFound();
        }
    }
}