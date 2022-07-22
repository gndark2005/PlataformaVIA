using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PlataformaVIA.Presentacion.Helpers
{
    public class Cipher
    {
        public static string EncryptString(string toEncrypt, string key, bool useHashing)



        {

            byte[] keyArray;

            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

            // Get the key from config file

            //string key = "2525NOHAYQUEROBARINFORMACIONCONFIDENCIAL4747";

            if (useHashing)

            {

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

            }

            else

                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            //set the secret key for the tripleDES algorithm

            tdes.Key = keyArray;

            //mode of operation. there are other 4 modes.

            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //transform the specified region of bytes array to resultArray

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0,

                toEncryptArray.Length);

            //Release resources held by TripleDes Encryptor

            tdes.Clear();

            //Return the encrypted data into unreadable string format

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);



        }

        //As well as when user want to retrive password then required to decrypt that string from database and sent it to UI layer.

        //That decryption is reverse logic of encryption with help of haskey value.


        public static string DecryptString(string cipherString, string key, bool useHashing)

        {
            byte[] keyArray;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace(" ", "+"));

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();

            //Get your key from config file to open the lock!

            // string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            //string key = "2525NOHAYQUEROBARINFORMACIONCONFIDENCIAL4747";

            if (useHashing)

            {

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

            }

            else

            keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyArray;

            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);

        }
    }
}