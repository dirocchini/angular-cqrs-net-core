using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SharedOps
{
        public class TokenTools
        {
            private readonly IConfiguration _config;

            public TokenTools(IConfiguration config)
            {
                _config = config;
            }
            public string MakeToken(int userId, string login)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, login)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }

            public string DecodeToken(string token)
            {
                token = token.Replace("Bearer ", "");

                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                return tokenS.Payload["nameid"].ToString();
            }
        }
}