using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public class AccessToken
    {
        public static string GenerateAccessToken(Payzi.Business.Usuario user)
        {
            List<Claim> listClaims = new List<Claim>()

            {
                new Claim(Payzi.Enumerate.EnumerateClaims.Email.ToString(), user.Email)
            };

            SymmetricSecurityKey key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Payzi.Abstraction.StaticParams.StaticParams.Secret));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            EncryptingCredentials encryptingCredentials = new EncryptingCredentials(key, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                issuer: "payzi",
                audience: "payzi",
                subject: new ClaimsIdentity(listClaims),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(24),
                issuedAt: DateTime.Now,
                signingCredentials: creds,
                encryptingCredentials: encryptingCredentials,
                claimCollection: listClaims.ToDictionary(k => k.Type, v => (object)v.Value)
                );

            string encryptedJWT = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return encryptedJWT;
        }
    }
}
