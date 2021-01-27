using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServer.Models
{
    public class Security
    {
        //Making it public for testing purposes
        public static Dictionary<int, string> indexesPasswords = new Dictionary<int, string>();
        

        public static void VerifyUser(int buildingIndex, string password)
        {
            //Testing purposes
            indexesPasswords.Add(1, "password");
            
            var en = indexesPasswords.GetEnumerator();
            if (!(indexesPasswords.ContainsKey(buildingIndex) && indexesPasswords.ContainsValue(password)))
                throw new Exception("Not found password or building index");
            var ok = false;
            while (en.MoveNext() && !ok)
            {
                if (en.Current.Key == buildingIndex && en.Current.Value == password)
                {
                    ok = true;
                }
            }
            if (!ok) throw new Exception("Building index and password not matching");
        }

        /// <summary>
        ///     Checks received token from the request and returns validation.
        ///     Throws exception in case of incorrect credentials.
        /// </summary>
        /// <returns>True if valed, false if the token is expired.</returns>
        public static bool CheckToken(string token)
        {
            byte[] tokenData = Convert.FromBase64String(token);
            byte[] time = tokenData.Take(8).ToArray();
            byte[] key = tokenData.Skip(8).Take(4).ToArray();
            byte[] key2 = tokenData.Skip(12).ToArray();

            //24 hours for token expiration
            if (DateTime.FromBinary(BitConverter.ToInt64(time, 0)) < DateTime.Now.AddHours(-24))
            {
                return false;
            }

            string password = Encoding.ASCII.GetString(key);
            char[] decryptPassword = password.ToCharArray();
            char[] pKey = Utilities.tokenCipherKey.ToCharArray();
            int pKeyIndex = 0;
            for (int i = 0; i < decryptPassword.Length; ++i) {
                decryptPassword[i] = (char)(decryptPassword[i] - pKey[pKeyIndex]);
                ++pKeyIndex;
                if (pKeyIndex == pKey.Length) pKeyIndex = 0;
            }
            for (int i = 0; i < decryptPassword.Length; ++i) {
                decryptPassword[i] = (char)(decryptPassword[i] - 5);
            }
            password = decryptPassword.ToString();
            if (indexesPasswords.ContainsValue(password))
            {
                throw new Exception("Password incorrect.");
            }
            var buildingIndex = BitConverter.ToInt32(key2, 0);
            if (indexesPasswords.ContainsKey(buildingIndex))
            {
                throw new Exception("Buildingindex incorrect");
            }

            return true;
        }
    }
}