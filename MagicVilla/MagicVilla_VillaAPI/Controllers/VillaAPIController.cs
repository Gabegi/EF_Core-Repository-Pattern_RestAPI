using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ILogger<VillaAPIController> _logger;

        public VillaAPIController(ILogger<VillaAPIController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }



        // doesn't expect any parameters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");
            return Ok(await _dbContext.Villas.ToListAsync());
        }

        // indicates that GET expects an id parameter 
        // {id:int} is a constraint that the id parameter must be an integer
        // Document API, with Type that indicates expected response type, Type only need if ActionResult doesnt have a type
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id < 1)
            {
                _logger.LogError("Get Villa Error with Id " + id);
                return BadRequest("Id must be greater than 0");
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                   return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody]VillaCreateDTO villaDto)
        {
            // not required with data annotations?
            // wont hit breakpoint if model is invalid
            if(!ModelState.IsValid) // checks if the model is valid, i.e. if the data annotations are respected
            {
                return BadRequest(ModelState); // ModelState will contain the errors
            }

            // check if villa name already exists
            if(_dbContext.Villas.Any(x => x.Name == villaDto.Name))
            {
                ModelState.AddModelError("", "Villa name must be unique"); // key can be empty string
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            //if(villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}   

            var villa = new Villa
            {
                Id = _dbContext.Villas.Max(x => x.Id) + 1,
                Name = villaDto.Name
            };
            // villaDto.Id = VillaStore.villaList.Max(x => x.Id) + 1;

            await _dbContext.Villas.AddAsync(villa); // villaDto
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);
            // CreatedAtRoute: return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
            // CreatedAtAction - returns a 201 status code and a location header with the URI of the newly created resource
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }



            _dbContext.Villas.Remove(villa);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDto)
        {
            if (villaDto == null || id!= villaDto.Id)
            {
                return BadRequest();
            }

            var villa = new Villa
            {
                Id = _dbContext.Villas.Max(x => x.Id) + 1,
                Name = villaDto.Name
            };

            _dbContext.Villas.Update(villa);
            await _dbContext.SaveChangesAsync();


            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDto)
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            // AsNoTracking() - tells EF Core not to track changes to the entity

            VillaUpdateDTO villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name
            };

            if(villa == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name
            };  

            _dbContext.Villas.Update(model);
            await _dbContext.SaveChangesAsync();

            return NoContent(); // return Ok(villa)
        }

    }
}
