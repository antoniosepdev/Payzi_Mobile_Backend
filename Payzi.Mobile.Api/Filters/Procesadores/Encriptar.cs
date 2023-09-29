using System.Security.Cryptography;
using System.Text;

namespace Payzi.Mobile.Api.Filters.Procesadores
{
    public class Encriptar
    {
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

        #region Procesador

        internal static string EncryptPassword(string unencodePassword)
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

            string encodePassword = Convert.ToBase64String(cipherTextBytes);

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
            byte[] cipherTextBytes = Convert.FromBase64String(encodePassword);

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
        #endregion
    }
}