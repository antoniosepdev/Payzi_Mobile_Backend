using Azure.Core;
using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Asn1.X509;
using Payzi.Abstraction.PartialOverload;
using Payzi.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Business
{
    public static class Account
    {
        public class LoginParametros
        {
            public string Email { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;

            public bool Persistent { get; set; }

            public Payzi.Context.Context? Context { get; set; }
        }

        public static async Task<(LoginStatus loginStatus, string accessToken)> Logear(LoginParametros loginParametros)
        {
            try
            {
                if (loginParametros.Context != null)
                {
                    Payzi.Business.Usuario usuario = await Payzi.Business.Usuario.GetAsync(loginParametros.Context, loginParametros.Email);

                    string clave = Account.DecodePassword(usuario.Clave);

                    if (!Account.EncryptPassword(loginParametros.Password).Equals(usuario.Clave))
                    {
                        return (LoginStatus.InvalidEmailOrPassword, string.Empty);
                    }
                    try
                    {
                        usuario.UltimoAcceso = DateTime.Now;
                        usuario.Bloqueado = false;
                        usuario.FechaIntentoFallido = null;

                        await usuario.Save(loginParametros.Context);

                        await loginParametros.Context.SaveChangesAsync();

                        string? connectionString = loginParametros.Context.Database.GetConnectionString();

                        string accessToken = Payzi.Business.AccessToken.GenerateAccessToken(usuario);

                        var identity = await AccessToken.GetClaimsIdentityFromToken(accessToken);
                        identity.AddEmailClaim(loginParametros.Email);

                        return (LoginStatus.Success, accessToken);
                    }
                    catch
                    {
                        return (LoginStatus.InvalidEmailOrPassword, "0");
                    }
                }
                else
                {
                    return (LoginStatus.InvalidEmailOrPassword, "0");
                }
            }
            catch
            {
                return (LoginStatus.InvalidEmailOrPassword, "0");
            }
        }

        public static string EncryptPassword(string unencodedPassword)
        {
            string password = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            string hashAlgorithm = "MD5";
            string saltValue = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            int keySize = 192;
            int passwordIteration = 1;
            string initialVector = ("DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0").Remove(16);

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(unencodedPassword);

            using (PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration))
            {
                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(keyBytes, initialVectorBytes))
                    using (MemoryStream memoryStream = new MemoryStream())
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();

                        byte[] cipherTextBytes = memoryStream.ToArray();

                        string encodedPassword = System.Convert.ToBase64String(cipherTextBytes);

                        return encodedPassword;
                    }
                }
            }
        }

        public static string DecodePassword(string encodePassword)
        {
            string password = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            string hashAlgorithm = "MD5";
            string saltValue = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            int keySize = 192;
            int passwordIteration = 1;
            string initialVector = ("DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0").Remove(16);

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = System.Convert.FromBase64String(encodePassword);

            using (PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration))
            {
                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Mode = CipherMode.CBC;

                    using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(keyBytes, initialVectorBytes))
                    using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}
