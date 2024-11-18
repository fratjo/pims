using System.Globalization;
using Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(IProductService service) : ControllerBase
    {
        #region Gets
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await service.GetAllProductsAsync();
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            Product? product = await service.GetProductByIdAsync(id);
            
            return product is not null ? Ok(product) : NotFound();
        }
        
        #endregion
        
        #region Posts

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductInsertRequest product)
        {
            try
            {
                var NewId = await service.CreateProductAsync(product);

                return CreatedAtAction(nameof(GetProductById), new { id = NewId }, NewId);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        #endregion
        
        #region Puts

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] ProductUpdateRequest product)
        {
            try
            {
                var p = await service.UpdateProductAsync(id, product);
                
                return Ok(p);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        #endregion
    }
    
    
}
