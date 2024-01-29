using BrandApplication.Business.DTOs;
using BrandApplication.Business.Services.IServices.IServiceMappings;
using Microsoft.AspNetCore.Mvc;

namespace Plutus.ProductPricing.API.Controllers.Assets_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandMapping _service;

        public BrandController(IBrandMapping service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BrandDto>> GetBrandByID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            return Ok(await _service.GetByIdAsync(id));
        }
    }
}
