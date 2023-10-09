using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Payzi.Abstraction.Security
{
    public class Security
    {
        public static (Action<AuthenticationOptions> authenticationOptions, Action<JwtBearerOptions> jwtBearerOptions) AddAuthentication(string secret)
        {
            Action<AuthenticationOptions> actionAuthentication = new Action<AuthenticationOptions>(authenticationOptions =>
            {
                authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            Action<JwtBearerOptions> actionJwtBearer = new Action<JwtBearerOptions>(jwtBearerOptions =>
            {
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnTokenValidated = x =>
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, x.Principal.Claims.First().Value)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

                        claimsPrincipal.AddIdentity(claimsIdentity);

                        x.HttpContext.User = claimsPrincipal;

                        x.HttpContext.SignInAsync(claimsPrincipal);

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = x =>
                    {
                        x.Response.Headers.Add("authentication-failed", "true");

                        return Task.CompletedTask;
                    }
                };

                jwtBearerOptions.RequireHttpsMetadata = false;

                jwtBearerOptions.SaveToken = false;

                jwtBearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret)),
                    TokenDecryptionKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return (actionAuthentication, actionJwtBearer);
        }
    }
}
