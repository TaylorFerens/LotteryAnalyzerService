using LotteryAnalyzer.Models;
using LotteryAnalyzer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LotteryAnalyzer.Controllers
{
    public class LoginController : ControllerBase
    {

        #region Private Variables

        private readonly UserService _service;

        #endregion
        #region Constructors

        public LoginController(UserService service)
        {
            _service = service;
        }

        #endregion
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate([FromBody] User user)
        {
            User tmpUser;
            ActionResult retval = Ok(new User());
            bool noDatabaseConnection = false;

            try
            {
                tmpUser = _service.Authenticate(user.Username, user.Password, ref noDatabaseConnection);

                if (tmpUser != null)
                {
                    // For cookie authentication
                    // We can add more user information to this if needed
                    var claims = new List<Claim>
                    {
                        new Claim("access_token", tmpUser.AccessToken),
                    };

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        IssuedUtc = DateTime.Now,
                        // Cookie expiration date
                        ExpiresUtc = DateTime.Now.AddDays(1)
                    };

                    ClaimsIdentity claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sets the Auth Cookie for the Session
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authProperties);

                    // Clear the password and token from user, this data shouldnt be stored anywhere on the front end
                    tmpUser.AccessToken = null;
                    tmpUser.Password = null;

                    // Set the return value
                    retval = Ok(tmpUser);
                }
            }
            catch (Exception ex)
            {
                if (noDatabaseConnection)
                    retval = StatusCode(419);   // Connection string is invalid.  Get the fuck back to the Practice Login.
                else
                    retval = BadRequest(new User());

                Console.WriteLine(ex);
            }

            return retval;
        }

    }
}
