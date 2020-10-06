using PetShop.Core.DomainServices;
using PetShop.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Petshop.Infrastructure.SQLData.Repo
{
    public class PetRepository : IPetRepository
    {
        readonly PetshopContext _ctx;
        public PetRepository(PetshopContext ctx) 
        {
            _ctx = ctx;
        }
        public Pet Create(Pet pet)
        {
           var pe = _ctx.Pets.Add(pet).Entity;
            _ctx.SaveChanges();
            return pe;
        }

        public Pet Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Pet ReadById(int id)
        {
            return _ctx.Pets.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Pet> ReadPets()
        {
            throw new System.NotImplementedException();
        }

        public Pet Update(Pet petUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
