using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PetShop.Core.Entity;
using Petshop.Infrastructure.SQLData.Service;
using PetShop.Core.DomainServices;
using Microsoft.AspNetCore.Authorization;

namespace PetShop.RestAPI.Controllers
{
    [Route("/token")]
    public class LoginController : Controller
    {
        private IUserRepository repository;
        private IAuthenticationHelper authenticationHelper;

        public LoginController(IUserRepository repos, IAuthenticationHelper authHelper)
        {
            repository = repos;
            authenticationHelper = authHelper;
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }
    }
}
