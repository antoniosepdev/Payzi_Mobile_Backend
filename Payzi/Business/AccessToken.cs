using Microsoft.IdentityModel.Tokens;
using Payzi.Enumerate;
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

            ClaimsIdentity identity = new ClaimsIdentity(listClaims);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = "payzi",
                Audience = "payzi",
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            string encryptedJWT = tokenHandler.WriteToken(securityToken);

            return encryptedJWT;
        }


        public static string GenerateAccessTokenEnterpriseConnection(Usuario user, string connectionString = default(string), Guid enterpriseId = default(Guid))
        {
            List<Claim> listClaims = new List<Claim>();

            Claim userClaim = new Claim(Payzi.Enumerate.EnumerateClaims.Rut.ToString(), user.Negocio.Rut);

            listClaims.Add(userClaim);

            if (!string.IsNullOrEmpty(connectionString))
            {
                Claim connectionStringClaim = new Claim(Payzi.Enumerate.EnumerateClaims.ConnectionString.ToString(), connectionString);

                listClaims.Add(connectionStringClaim);
            }

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

        public static ClaimsIdentity GetClaimsIdentityFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken?.Claims == null)
                return new ClaimsIdentity();

            var identity = new ClaimsIdentity(jsonToken.Claims);

            return identity;
        }

        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null || jsonToken.Payload == null)
            {
                // Token inválido
                return string.Empty;
            }

            object emailClaimValue;

            if (jsonToken.Payload.TryGetValue("Email", out emailClaimValue))
            {
                return emailClaimValue.ToString();
            }

            return string.Empty;
        }
    }
}