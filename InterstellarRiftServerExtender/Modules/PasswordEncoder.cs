using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IRSe.Modules
{
    public class PasswordEncoder
    {
        private string mEncryptedPassword;

        #region DONT CHANGE ANYTHING IN THIS

        // Change the two values below to be something other than the example.
        // Once changed and in use, do not change the value below again or you
        // won't be able to decrypt previously stored passwords.
        private string mByteArray = "&%^&#*#TSE^#&*IRSE*#HJGH^UO%$##";  // DONT EVER CHANGE WILL BREAK PEOPLES IRSE

        private byte[] mInitializationVector = { 0x01, 0xad, 0x89, 0x90, 0xAB, 0xf7, 0xEF, 0x57 }; // DONT EVER CHANGE WILL BREAK PEOPLES IRSE

        #endregion DONT CHANGE ANYTHING IN THIS

        public PasswordEncoder()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string path = Path.Combine(appdata, "IRSE");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string file = Path.Combine(path, "HASH");

            if (File.Exists(file))
                mByteArray = File.ReadAllText(file);
            else
            {
                mByteArray = RandomString(512);
                File.WriteAllText(file, mByteArray);
            }
        }

        private static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLM[][{}';;NOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public PasswordEncoder(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, mByteArray);
        }

        public string EncryptWithByteArray(string inPassword)
        {
            mEncryptedPassword = EncryptWithByteArray(inPassword, mByteArray);
            return mEncryptedPassword;
        }

        private string EncryptWithByteArray(string inPassword, string inByteArray)
        {
            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(inByteArray.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(inPassword);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputArray, 0, inputArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DecryptWithByteArray()
        {
            return DecryptWithByteArray(mEncryptedPassword, mByteArray);
        }

        public string DecryptWithByteArray(string strText, string strEncrypt = "")
        {
            if (string.IsNullOrEmpty(strEncrypt))
                strEncrypt = mByteArray;

            try
            {
                byte[] tmpKey = new byte[20];
                tmpKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(tmpKey, mInitializationVector), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EncryptedPassword
        {
            get { return mEncryptedPassword; }
            set { mEncryptedPassword = value; }
        }

        public string ByteArray
        {
            get { return mByteArray; }
            set { mByteArray = value; }
        }
    }
}