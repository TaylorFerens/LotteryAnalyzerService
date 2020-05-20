using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LotteryAnalyzer.Classes
{
    public static class JwtTokenizer
    {
        public static string CreateJWT(string secretKey, string objID, int tokenExpiryInDays)
        {
            string retval = string.Empty;
            byte[] key;
            JwtSecurityTokenHandler tokenHandler;
            SecurityTokenDescriptor tokenDescriptor;

            try
            {
                // authentication successful so generate jwt token
                tokenHandler = new JwtSecurityTokenHandler();
                key = Encoding.ASCII.GetBytes(secretKey);

                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, objID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(tokenExpiryInDays),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                retval = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return retval;
        }


    }
}
