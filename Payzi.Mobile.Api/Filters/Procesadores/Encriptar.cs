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

        public static string EncryptPassword(string unencodePassword)
        {
            String password = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            String hashAlgorithm = "MD5";
            String saltValue = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            Int32 keySize = 192;
            Int32 passwordIteration = 1;
            String initialVector = ("DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0").Remove(16);

            Byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            Byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            Byte[] plainTextBytes = Encoding.UTF8.GetBytes(unencodePassword);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration);

            Byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, InitialVectorBytes);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            Byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            String encodePassword = System.Convert.ToBase64String(cipherTextBytes);

            return encodePassword;
        }

        public static string DecodePassword(string encodePassword)
        {
            String password = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            String hashAlgorithm = "MD5";
            String saltValue = "DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0";
            Int32 keySize = 192;
            Int32 passwordIteration = 1;
            String initialVector = ("DRKHIEDICM7ZZJE0UNV3RUEBZUOXWDHEVKC920KN9ULWGPORHN7M4IU6EH14GQG8 XZT5ORPBLL11SPIFNYO7SSRYRNNKNWTFX3ZWUUVY16DLVLJAM2CK2PO05SJC0ZH0").Remove(16);
            Byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            Byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            Byte[] cipherTextBytes = System.Convert.FromBase64String(encodePassword);

            PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIteration);

            Byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, InitialVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            Byte[] plainTextBytes = new Byte[cipherTextBytes.Length];

            Int32 decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close(); cryptoStream.Close();

            String plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }
        #endregion
    }
}