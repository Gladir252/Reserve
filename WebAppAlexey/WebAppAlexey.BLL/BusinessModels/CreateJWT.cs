using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAppAlexey.BLL.BusinessModels
{
    class CreateJwt
    {
        public CreateJwt(string email, string role, string subscriptionStatus)
        {
            Email = email;
            Role = role;
            SubscriptionStatus = subscriptionStatus;
        }

        string Role { get; }
        private string SubscriptionStatus { get; }
        string Email { get; }

        public string GetJwt()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:50133",
                audience: "http://localhost:50133",
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, Email),//
                    new Claim(ClaimTypes.Role, Role),//
                    new Claim(ClaimTypes.Spn, SubscriptionStatus)
                },
                expires: DateTime.Now.AddMinutes(99900),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);



            return tokenString;
        }
    }
}
