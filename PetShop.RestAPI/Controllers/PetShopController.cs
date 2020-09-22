using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationServices;
using PetShop.Core.ApplicationServices.Services;
using PetShop.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetShopController : ControllerBase
    {
        private readonly IPetService _petService;
        private List<Pet> PetList = new List<Pet>();

        public PetShopController(IPetService petService)
        {
            _petService = petService;
            PetList = _petService.GetPets();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            if (PetList.Count == 0)
            {
                return NoContent();
            }
            return Ok(_petService.GetPets());
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            try
            {
                return _petService.FindPetById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Id must be greater than 0");
            }              

            
        }

        // JSON
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if(string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("Name is required for creating a pet");
            }

            if(string.IsNullOrEmpty(pet.Color))
            {
                return BadRequest("Color is required for creating a pet");
            }
            return _petService.CreatePet(pet); 
        }


        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            var updatePet = _petService.UpdatePet(pet);
            if (updatePet == null) 
            {
                return StatusCode(404, "not found bro");
            }

            try
            {
                return updatePet;
            } catch (Exception) 
            {
                return StatusCode(500, "try again");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            var pet = _petService.DeletePet(id);
            if (pet == null) 
            {
                return StatusCode(404, "did not find pet with id " + id);
            }

            try
            {
                return _petService.DeletePet(id);
            }
            catch (Exception) 
            {
                return StatusCode(500, "Task Fucked up");
            }
        }

        
        [HttpGet("{type}")]
        [Route("[action]/{type}")]
        public ActionResult<List<Pet>> getFilteredPets(string type)
        {
            try
            {
                return _petService.GetAllByType(type);
            } catch (Exception) 
            {
                return StatusCode(500, "fucked up");
            }
            
        }
    }
}
