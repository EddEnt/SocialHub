using Domain;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SocialHub.Server.API.Services
{
    public class TokenService
    {
        //Once complete consider switching to Azure Key Vault, once hosting on Azure
        //https://docs.microsoft.com/en-us/azure/key-vault/general/overview
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };
            //Curent key is temporary, update to be longer before production! <-------
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Y5E)*pWoHPTHL)tNuPY+U-kN*@}X.yciwtZ,2CRGvP]t=Q,jLGU!-x_?M9Ao9h_Z"));

            //Signing credentials are used to sign the token, using the key and the algorithm
            //HmacSha512Signature is a hashing algorithm that uses the key to hash the token
            //Sha512 is the hashing algorithm, Hmac is the encryption algorithm
            //More info: https://www.w3.org/2001/04/xmldsig-more/ and
            //https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha512?view=net-5.0
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Claims are the data we want to store in the token
                Subject = new ClaimsIdentity(claims),
                //Token expires after 7 days
                Expires = DateTime.UtcNow.AddDays(2),
                //Signing credentials are used to sign the token
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); //Returns the token as a string

        }
    }
}
