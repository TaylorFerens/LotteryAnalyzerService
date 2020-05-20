using LotteryAnalyzer.Classes;
using LotteryAnalyzer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace LotteryAnalyzer.Services
{
    public class UserService
    {

        #region Private Variables

        private readonly AppSettings _appSettings;
        private readonly DatabaseContext _context;

        #endregion
        #region Constructors

        public UserService(IOptions<AppSettings> appSettings, DatabaseContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        #endregion
        #region Public Methods

        public bool ResetPassword(User user)
        {
            bool retval = false;
            User tmpUser = null;

            try
            {
                // Get the user (to ensure they exist, and the provided the correct login info)
                tmpUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId && u.Password == user.Password);

                // Only proceed to change the password if the user was validated above
                if (tmpUser != null)
                {
                    // Set the new Password
                    tmpUser.Password = user.NewPassword;

                    // Update the User
                    _context.Users.Update(tmpUser);
                    _context.SaveChanges();

                    // Indicate to the caller that we succeeded
                    retval = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retval;
        }

        public User Authenticate(string userName, string password, ref bool noDatabaseConnection)
        {
            User user = new User();
            user.UserId = Guid.NewGuid();
            int tokenExpiryInDays = 1;
            try
            {
                // Always ensure the user has a valid connection string before attempting to log in
                noDatabaseConnection = TestInvalidDatabaseConnection();
                // Note: Passwords should really be hashed when stored (doing this for simplicity)
                // To Do: Encrypt user passwords/tokens?
                user = _context.Users
                    .FirstOrDefault(u => u.Username == userName && u.Password == password);

                if (user != null)
                {
                    // generate an JWT for this user (it will be stored in a domain specific cookie)
                    user.AccessToken = JwtTokenizer.CreateJWT(_appSettings.SecretKey, user.UserId.ToString(), tokenExpiryInDays);

                    // remove password before returning
                    user.Password = null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return user;
        }

        private bool TestInvalidDatabaseConnection()
        {
            bool retval;

            try
            {
                // User login is different.  Since we require a Practice Login first, which designates the users with a connection string
                // If the connection string is invalid, we'll still get this far.  Test for this state, to return the proper http status code up-stream
                // Note: this is a bit of a cave-man approach to testing this.  It appears theres no "safe" way with EF Core... yet, at least.
                retval = !_context.Database.CanConnect();
            }
            catch (Exception ex)
            {
                retval = true;
                Console.WriteLine(ex);
            }
            return retval;
        }
        #endregion
    }
}
