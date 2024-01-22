using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        // doesn't expect any parameters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        // indicates that GET expects an id parameter 
        // {id:int} is a constraint that the id parameter must be an integer
        // Document API, with Type that indicates expected response type, Type only need if ActionResult doesnt have a type
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
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
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
        {
            // not required with data annotations?
            // wont hit breakpoint if model is invalid
            if(!ModelState.IsValid) // checks if the model is valid, i.e. if the data annotations are respected
            {
                return BadRequest(ModelState); // ModelState will contain the errors
            }

            // check if villa name already exists
            if(VillaStore.villaList.Any(x => x.Name == villaDto.Name))
            {
                ModelState.AddModelError("", "Villa name must be unique"); // key can be empty string
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }   


            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            if(villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }   

            var villa = new VillaDto
            {
                Id = VillaStore.villaList.Max(x => x.Id) + 1,
                Name = villaDto.Name
            };
            // villaDto.Id = VillaStore.villaList.Max(x => x.Id) + 1;

            VillaStore.villaList.Add(villa); // villaDto

            return CreatedAtAction(nameof(GetVilla), new { id = villa.Id }, villa);
            // CreatedAtRoute: return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
            // CreatedAtAction - returns a 201 status code and a location header with the URI of the newly created resource
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);

            return NoContent();
        }
    }
}
