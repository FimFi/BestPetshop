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
    public class PetShopController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetShopController(IPetService petService)
        {
            _petService = petService;
        }
        /// <summary>
        /// Returns list of all pets as JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return _petService.GetPets();
        }

        /// <summary>
        /// Returns pet with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Pet Get(int id)
        {
            return _petService.FindPetById(id);
        }

        /// <summary>
        /// Returns the Pet that was created
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if(string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("Name is required for creating a pet");
            }
            //if(string.IsNullOrEmpty(pet.Type))
            //{
            //    return BadRequest("Type is required for creating a pet");
            //}
            if(string.IsNullOrEmpty(pet.Color))
            {
                return BadRequest("Color is required for creating a pet");
            }
            return _petService.CreatePet(pet); 
        }


        /// <summary>
        /// Returns the Pet that was updated
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Pet pet)
        {
            _petService.UpdatePet(pet);
        }

        /// <summary>
        /// Returns the pet deleted
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petService.DeletePet(id);
        }
    }
}
