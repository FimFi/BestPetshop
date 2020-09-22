using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationServices;
using PetShop.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;
        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }
        // GET: api/<PetTypeController>
        [HttpGet]
        public ActionResult<IEnumerable<PetType>> Get()
        {
            return Ok(_petTypeService.GetPetTypes());
        }

        // GET api/<PetTypeController>/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            try
            {
                return _petTypeService.FindPetTypeById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "FAILED");
            }              
        }

        // POST api/<PetTypeController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType petType)
        {
            if(string.IsNullOrEmpty(petType.Type))
            {
                return BadRequest("Type is required for creating a petType");
            }

            return _petTypeService.CreatePetType(petType);
        }

        // PUT api/<PetTypeController>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType petType)
        {
            var updatePetType = _petTypeService.UpdatePetType(petType);
            if (updatePetType == null)
            {
                return StatusCode(404, "not found bro");
            }

            try
            {
                return updatePetType;
            }
            catch (Exception)
            {
                return StatusCode(500, "try again");
            }
        }

        // DELETE api/<PetTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            var petType = _petTypeService.DeletePetType(id);
            if (petType == null)
            {
                return StatusCode(404, "did not find pettype with id " + id);
            }

            try
            {
                return _petTypeService.DeletePetType(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Task Fucked up");
            }
            
        }


        [HttpGet("{type}")]
        [Route("[action]/{type}")]
        public ActionResult<List<PetType>> getFilteredPetTypes(string type)
        {
            try
            {
                return _petTypeService.GetAllByPetType(type);
            }
            catch (Exception)
            {
                return StatusCode(500, "fucked up");
            }
        }
    }
}
