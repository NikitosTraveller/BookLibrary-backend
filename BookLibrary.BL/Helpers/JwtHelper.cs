using BookLibrary.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Helpers
{
    public static class JwtHelper
    {
        public static string Generate(int id, string secret, int lifeTime)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var claims = new ClaimsIdentity(new Claim[]
                {
                      new Claim(ClaimTypes.Name, id.ToString())
                });

            var payload = new JwtPayload(id.ToString(), null, claims.Claims, null, DateTime.Today.AddDays(lifeTime));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public static JwtSecurityToken Verify(string jwt, string secret)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            try
            {
                tokenhandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                },
                out SecurityToken validatedToken);
                return (JwtSecurityToken)validatedToken;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

    }
}
