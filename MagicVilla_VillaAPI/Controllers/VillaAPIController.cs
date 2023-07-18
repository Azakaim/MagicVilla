using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Log;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]

    //[Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        public readonly ILogging _logger;

        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting All Villas","");
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{Id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        //[ProducesResponseType(200, Type = typeof( VillaDTO ))]
        public ActionResult<VillaDTO> GetVilla(int Id)//all id should to be with equals literal,need => ID = ID;not id = ID 
        {
            if (Id == 0)
            {
                _logger.Log(@"Get villa with this Id """ + Id + @""" is not availible","Error");
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(villa => villa.Id == Id);
            if (villa == null) return NotFound();
            return Ok(villa);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult <VillaDTO> GreateVilla([FromBody]VillaDTO villaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (VillaStore.villaList.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError ", villaDTO.Name + " ==> is exists");
                return BadRequest(ModelState);
            }

            if(villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("GetVilla", new { id =  villaDTO.Id },villaDTO);
            
        }

        [HttpDelete("{Id:int}",Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int Id)
        {
            if(Id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == Id);
            if(villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{Id:int}",Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int Id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || Id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == Id);
            if(villa == null)
            {
                return NotFound(villaDTO);
            }
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Name = villaDTO.Name;

            return NoContent() ;
        }

        [HttpPatch("{Id:int}",Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdatePartialVilla(int Id,JsonPatchDocument<VillaDTO> putchDTO)
        {
            if( putchDTO is null || Id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == Id);
            if(villa == null)
            {
                return NotFound();
            }
            putchDTO.ApplyTo(villa,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
