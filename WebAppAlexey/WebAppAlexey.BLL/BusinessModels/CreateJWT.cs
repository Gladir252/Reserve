using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAppAlexey.BLL.BusinessModels
{
    class CreateJWT
    {
        public CreateJWT(string _email, string _role, string _subscriptionStatus)
        {
            email = _email;
            role = _role;
            subscriptionStatus = _subscriptionStatus;
        }

        string role { get; }
        string subscriptionStatus { get; }
        string email { get; }

        public string GetJwt()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:50133",
                audience: "http://localhost:50133",
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email),//
                    new Claim(ClaimTypes.Role, role),//
                    new Claim(ClaimTypes.Spn, subscriptionStatus)
                },
                expires: DateTime.Now.AddMinutes(99900),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);



            return tokenString;
        }
    }
}
