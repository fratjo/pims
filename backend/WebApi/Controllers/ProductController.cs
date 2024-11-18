using System.Globalization;
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
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductInsertRequest product)
        {
            throw new NotImplementedException();
        }
        
        #endregion
        
        #region Puts

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] ProductUpdateRequest product)
        {
            throw new NotImplementedException();
        }
        
        
        #endregion
    }
    
    
}
