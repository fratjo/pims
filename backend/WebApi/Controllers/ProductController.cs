using System.Globalization;
using Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.ProductService;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(
        IProductService service, 
        IEnumerable<IExceptionHandler> exceptionHandlers) : ControllerBase
    {
        #region Gets
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            return await this.HandleRequestAsync(async () =>
            {
                var products = await service.GetAllProductsAsync();
                return Ok(products);
            }, exceptionHandlers);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(string id)
        {
            return await this.HandleRequestAsync(async () =>
            {
                var product = await service.GetProductByIdAsync(id);
                return Ok(product);
            }, exceptionHandlers);
        }
        
        [HttpGet("api/bundles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBundles()
        {
            return await this.HandleRequestAsync(async () =>
            {
                var bundles = await service.GetAllBundlesAsync();
                return Ok(bundles);
            }, exceptionHandlers);
        }
        
        [HttpGet("api/bundles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBundleById(string id)
        {
            return await this.HandleRequestAsync(async () =>
            {
                var bundle = await service.GetBundleByIdAsync(id);
                return Ok(bundle);
            }, exceptionHandlers);
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            return await this.HandleRequestAsync(async () =>
            {
                var categories = await service.GetCategoryNamesAsync();
                return Ok(categories);
            }, exceptionHandlers);
        }
        
        #endregion
        
        #region Posts

        // api/products
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductInsertRequest product)
        {
            return await this.HandleRequestAsync(async () =>
            {
                var newId = await service.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = newId }, new { id = newId });
            }, exceptionHandlers);
        }
        
        [HttpPost("api/bundles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBundle([FromBody] BundleInsertRequest bundle)
        {
            return await this.HandleRequestAsync(async () =>
            {
                var bundleId = await service.CreateProductBundleAsync(bundle);
                return CreatedAtAction(nameof(GetBundleById), new { id = bundleId }, new { id = bundleId });
            }, exceptionHandlers);
        }
        
        #endregion
        
        #region Puts

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductUpdateRequest product)
        {
            return await this.HandleRequestAsync(async () =>
            {
                var updatedProduct = await service.UpdateProductAsync(id, product);
                return Ok(updatedProduct);
            }, exceptionHandlers);
        }
        
        #endregion
    }
    
    
}
