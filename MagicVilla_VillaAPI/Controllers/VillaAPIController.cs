using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
    
    //[Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        [HttpGet("")]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("{Id}")]
        public ActionResult<VillaDTO> GetVilla(int Id)//all id should to be with equals literal,need => ID = ID;not id = ID 
        {
            if(Id == 0)return BadRequest();
            var villa = VillaStore.villaList.FirstOrDefault(villa => villa.Id == Id);
            if (villa == null) return NotFound();
            return Ok(villa);
        }
    }
}
