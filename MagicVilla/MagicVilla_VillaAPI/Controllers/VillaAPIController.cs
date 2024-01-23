using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
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
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        protected APIResponse _response;


        public VillaAPIController(
            ILogger<VillaAPIController> logger, 
            ApplicationDbContext dbContext, 
            IMapper mapper,
            IVillaRepository villaRepository,
            IUnitOfWork unitOfWork
            )
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
            _villaRepository = villaRepository;
            _response = new();
            _unitOfWork = unitOfWork;
        }



        // doesn't expect any parameters
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            _logger.LogInformation("Getting all villas");

           //  var villas = await _villaRepository.GetAllAsync();

            var villas = await _unitOfWork.Villa.GetAllAsync();

            _response.Result = _mapper.Map<List<VillaDTO>>(villas);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;


            return Ok(_response);

            // IEnumerable<Villa> villas = await _dbContext.Villas.ToListAsync();

        }

        // indicates that GET expects an id parameter 
        // {id:int} is a constraint that the id parameter must be an integer
        // Document API, with Type that indicates expected response type, Type only need if ActionResult doesnt have a type
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetVilla(int id)
        {
            if (id < 1)
            {
                _logger.LogError("Get Villa Error with Id " + id);
                return BadRequest("Id must be greater than 0");
            }

            // var villa = await _villaRepository.GetAsync(x => x.Id == id);
            var villa = await _unitOfWork.Villa.GetAsync(x => x.Id == id);

            if (villa == null)
            {
                   return NotFound();
            }

            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);

            // var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id)
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody]VillaCreateDTO createDto)
        {
            // not required with data annotations?
            // wont hit breakpoint if model is invalid
            if(!ModelState.IsValid) // checks if the model is valid, i.e. if the data annotations are respected
            {
                return BadRequest(ModelState); // ModelState will contain the errors
            }

            // check if villa name already exists
            if(_dbContext.Villas.Any(x => x.Name == createDto.Name))
            {
                ModelState.AddModelError("", "Villa name must be unique"); // key can be empty string
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            var model = _mapper.Map<Villa>(createDto);

            // await _villaRepository.CreateAsync(model);
            await _unitOfWork.Villa.CreateAsync(model);

            _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtAction(nameof(GetVilla), new { id = model.Id }, _response);


            //if(villaDto.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}   

            //var villa = new Villa
            //{
            //    Id = _dbContext.Villas.Max(x => x.Id) + 1,
            //    Name = createDto.Name
            //};
            // villaDto.Id = VillaStore.villaList.Max(x => x.Id) + 1;

            //await _dbContext.Villas.AddAsync(model); // villaDto
            //await _dbContext.SaveChangesAsync();


            // CreatedAtRoute: return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
            // CreatedAtAction - returns a 201 status code and a location header with the URI of the newly created resource
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            // var villa = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);

            var villa = await _unitOfWork.Villa.GetAsync(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            await _villaRepository.RemoveAsync(villa);

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;

            return Ok(_response);

            //_dbContext.Villas.Remove(villa);
            //await _dbContext.SaveChangesAsync();
        }


        [HttpPut("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDto)
        {
            if (villaUpdateDto == null || id!= villaUpdateDto.Id)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Villa>(villaUpdateDto);

            // await _villaRepository.UpdateAsync(model);

            await _unitOfWork.Villa.UpdateAsync(model);

            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;

            return Ok(_response);

            //var villa = new Villa
            //{
            //    Id = _dbContext.Villas.Max(x => x.Id) + 1,
            //    Name = villaUpdateDto.Name
            //};

            //_dbContext.Villas.Update(model);
            //await _dbContext.SaveChangesAsync();
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
            
            // var villa = await _villaRepository.GetAsync(v => v.Id == id, tracked:false);

            var villa = await _unitOfWork.Villa.GetAsync(v => v.Id == id, tracked: false);

            var villaDto = _mapper.Map<VillaUpdateDTO>(villa);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDto.ApplyTo(villaDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var model = _mapper.Map<Villa>(villaDto);

            await _villaRepository.UpdateAsync(model);

            return NoContent(); // return Ok(villa)

            // var villa = await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            // AsNoTracking() - tells EF Core not to track changes to the entity

            //VillaUpdateDTO villaDto = new()
            //{
            //    Id = villa.Id,
            //    Name = villa.Name
            //};

            //Villa model = new()
            //{
            //    Id = villaDto.Id,
            //    Name = villaDto.Name
            //}; 

            //_dbContext.Villas.Update(model);
            //await _dbContext.SaveChangesAsync();
        }

    }
}
