using System;
using System.Collections.Generic;
using System.Linq;
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
                return StatusCode(500, "Id must be greater than 0");
            }              
        }

        // POST api/<PetTypeController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType petType)
        {
            return _petTypeService.CreatePetType(petType);
        }

        // PUT api/<PetTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PetType petType)
        {
            _petTypeService.UpdatePetType(petType);
        }

        // DELETE api/<PetTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petTypeService.DeletePetType(id);
        }
    }
}
