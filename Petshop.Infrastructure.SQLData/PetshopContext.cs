using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entity;
using System.Collections.Concurrent;

namespace Petshop.Infrastructure.SQLData
{
    public class PetshopContext : DbContext
    {
        public PetshopContext(DbContextOptions<PetshopContext> opt) : base(opt) 
        {

        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
