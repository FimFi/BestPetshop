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
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        // GET: api/<PetShopController>
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get()
        {
            return _ownerService.GetAllOwners();
        }

        // GET api/<PetShopController>/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {
                return _ownerService.FindOwnerById(id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Id must be greater than 0");
            }        
            
        }

        // POST api/<PetShopController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if (string.IsNullOrEmpty(owner.Name))
            {
                return BadRequest("Name is required for creating a owner");
            }

            if (string.IsNullOrEmpty(owner.Address))
            {
                return BadRequest("Address is required for creating an owner");
            }
            return _ownerService.CreateOwner(owner);
        }

        // PUT api/<PetShopController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            var updateOwner = _ownerService.UpdateOwner(owner);
            if (updateOwner == null)
            {
                return StatusCode(404, "not found bro");
            }

            try
            {
                return updateOwner;
            }
            catch (Exception g)
            {
                return StatusCode(500, "try again");
            }
        }

        // DELETE api/<PetShopController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            var owner = _ownerService.DeleteOwner(id);
            if (owner == null)
            {
                return StatusCode(404, "did not find pet with id " + id);
            }

            try
            {
                return _ownerService.DeleteOwner(id);
            }
            catch (Exception g)
            {
                return StatusCode(500, "Task Fucked up");
            }
        }

        [HttpGet("{name}")]
        [Route("[action]/{name}")]
        public ActionResult<List<Owner>> GetFilteredOwners(string name)
        {
            try
            {
                return _ownerService.GetAllByName(name);
            }
            catch (Exception g)
            {
                return StatusCode(500, "fucked up");
            }
        }
    }
}
