using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data2
{
    public class OwnerRepository : IOwnerRepository
    {
        static int id = 1;
        static List<Owner> _owners = new List<Owner>();
        readonly PetShopContext _ctx;


        public OwnerRepository(PetShopContext ctx) 
        {
            _ctx = ctx;
        }

        public Owner Create(Owner owner)
        {
            var own = _ctx.Owners.Add(owner).Entity;
            _ctx.SaveChanges();
            return own;
        }
        public Owner ReadById(int id)
        {
            return _ctx.Owners
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _ctx.Owners;
        }
        public Owner Update(Owner ownerUpdate)
        {
            var ownerDB = this.ReadById(ownerUpdate.Id);
            if (ownerDB != null)
            {
                ownerDB.Name = ownerUpdate.Name;
                ownerDB.Address = ownerUpdate.Address;
            }
            return null;
        }

        public Owner Delete(int id)
        {
            var ownerFound = this.ReadById(id);
            if (ownerFound != null)
            {
                _owners.Remove(ownerFound);
                return ownerFound;
            }
            return null;
        }
    }
}
