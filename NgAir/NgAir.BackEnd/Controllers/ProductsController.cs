using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NgAir.BackEnd.UnitsOfWork.Interfaces;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.BackEnd.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProductsController : GenericController<Product>
    {
        private readonly IProductsUnitOfWork _productsUnitOfWork;

        public ProductsController(IGenericUnitOfWork<Product> unitOfWork, IProductsUnitOfWork productsUnitOfWork) : base(unitOfWork)
        {
            _productsUnitOfWork = productsUnitOfWork;
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(int id)
        {
            var action = await _productsUnitOfWork.DeleteAsync(id);
            if (!action.WasSuccess)
            {
                return NotFound();
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var action = await _productsUnitOfWork.GetAsync(id);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] RequestParams requestParams)
        {
            var response = await _productsUnitOfWork.GetAsync(requestParams);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetPagesAsync([FromQuery] RequestParams requestParams)
        {
            var action = await _productsUnitOfWork.GetTotalPagesAsync(requestParams);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpPost("addImages")]
        public async Task<IActionResult> PostAddImagesAsync(ImageDto imageDto)
        {
            var action = await _productsUnitOfWork.AddImageAsync(imageDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPost("full")]
        public async Task<IActionResult> PostFullAsync(ProductDto productDto)
        {
            var action = await _productsUnitOfWork.AddFullAsync(productDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

        [HttpPost("removeLastImage")]
        public async Task<IActionResult> PostRemoveLastImageAsync(ImageDto imageDto)
        {
            var action = await _productsUnitOfWork.RemoveLastImageAsync(imageDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest(action.Message);
        }

        [HttpPut("full")]
        public async Task<IActionResult> PutFullAsync(ProductDto productDto)
        {
            var action = await _productsUnitOfWork.UpdateFullAsync(productDto);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return NotFound(action.Message);
        }

    }
}
