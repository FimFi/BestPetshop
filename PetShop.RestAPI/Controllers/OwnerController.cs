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
        public IEnumerable<Owner> Get()
        {
            return _ownerService.GetAllOwners();
        }

        // GET api/<PetShopController>/5
        [HttpGet("{id}")]
        public Owner Get(int id)
        {
            return _ownerService.FindOwnerById(id);
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
        public void Put(int id, [FromBody] Owner owner)
        {
            _ownerService.UpdateOwner(owner);
        }

        // DELETE api/<PetShopController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ownerService.DeleteOwner(id);
        }
    }
}
