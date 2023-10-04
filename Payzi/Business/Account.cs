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

namespace Payzi.MySQL.Model.Business
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
            string sqlUser = @"SELECT * FROM payzi.usuario WHERE Email = '" + loginParametros.Email + "' ";

            //Aqui
            Payzi.Business.Usuario usuario = await Payzi.Business.Usuario.GetAsync(loginParametros.Context, loginParametros.Email);

            string clave = Account.DecodePassword(usuario.Clave);

            if (!Account.EncryptPassword(loginParametros.Password).Equals(usuario.Clave))
            {
                return (LoginStatus.InvalidRunOrPassword, string.Empty);
            }
            try
            {
                usuario.UltimoAcceso = DateTime.Now;
                usuario.Bloqueado = false;
                usuario.FechaIntentoFallido = null;

                await usuario.Save(loginParametros.Context);

                await loginParametros.Context.SaveChangesAsync();

                string? connectionString = loginParametros.Context.Database.GetConnectionString();

                await loginParametros.Context.LoadStoredProc(connectionString, "").WithSqlParam("Id", usuario.Id).ExecuScalarAsync<object>(connectionString);

                string accessToken = Payzi.Business.AccessToken.GenerateAccessToken(usuario);

                return (LoginStatus.Success, accessToken);
            }
            catch
            {
                return (LoginStatus.InvalidRunOrPassword, "0");
            }

        }



        private static string GenerateRandomSalt()
        {
            string characters = "0123456789ABCDEF";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 64; i++)
            {
                // Genera
                int index = new Random().Next(0, characters.Length);
                sb.Append(characters[index]);
            }

            return sb.ToString();
        }

        public static string EncryptPassword(string unencodePassword)
        {
            string saltValue = GenerateRandomSalt();

            string password = saltValue;

            string hashAlgorithm = "MD5";

            int keySize = 192;

            int passwordIteration = 1;

            string initialVector = saltValue.Remove(16);

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);

            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(unencodePassword);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration);

            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string encodePassword = System.Convert.ToBase64String(cipherTextBytes);

            return encodePassword;
        }

        public static string DecodePassword(string encodePassword)
        {
            string password = encodePassword;
            string hashAlgorithm = "MD5";
            string saltValue = encodePassword;
            int keySize = 192;
            int passwordIteration = 1;
            string initialVector = encodePassword.Remove(16);
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = System.Convert.FromBase64String(encodePassword);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration);

            byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close(); cryptoStream.Close();

            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }
    }
}
