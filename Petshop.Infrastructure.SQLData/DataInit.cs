using Petshop.Infrastructure.SQLData;
using Petshop.Infrastructure.SQLData.Service;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class DataInit
    {
        private readonly IPetRepository _petRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetTypeRepository _petTypeRepository;
        private IAuthenticationHelper authenticationHelper;

        public static readonly List<Pet> Pets = new List<Pet>();
        public static readonly List<PetType> PetTypes = new List<PetType>();

        public DataInit(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository, IAuthenticationHelper authHelper)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
            _petTypeRepository = petTypeRepository;
            authenticationHelper = authHelper;
        }

        public void InitData(PetshopContext context)
        {
            string password = "1234";
            byte[] passwordHashJoe, passwordSaltJoe, passwordHashAnn, passwordSaltAnn;
            authenticationHelper.CreatePasswordHash(password, out passwordHashJoe, out passwordSaltJoe);
            authenticationHelper.CreatePasswordHash(password, out passwordHashAnn, out passwordSaltAnn);
            List<User> users = new List<User>
            {
                new User {
                    Username = "UserJoe",
                    PasswordHash = passwordHashJoe,
                    PasswordSalt = passwordSaltJoe,
                    IsAdmin = false
                },
                new User {
                    Username = "AdminAnn",
                    PasswordHash = passwordHashAnn,
                    PasswordSalt = passwordSaltAnn,
                    IsAdmin = true
                }
            };
            var petType = new PetType
            {
                Type = "Golden Retriever"

            };
            
            var pet1 = new Pet
            {
                Name = "Rex",
                Type = "Golden Retriever",
                Color = "Golden",
                BirthDate = new DateTime(2018, 6, 10),
                Price = 100,
                SoldDate = new DateTime(2018, 7, 10),
                PreviousOwner = "owner1"
            };

            var owner1 = new Owner
            {
                Name = "FimFi",
                Address = "Skolegade 13"
            };
            _petTypeRepository.Create(petType);
            _petRepository.Create(pet1);
            _ownerRepository.Create(owner1);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
