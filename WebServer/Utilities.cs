using System;
using System.Linq;

namespace WebServer
{
    public static class Utilities
    {
        public static string tokenCipherKey = "wANJqVN8xG";
        public static string[] DeserializeJsonStringArray(string s)
        {
            var tmp = s.Split(',');
            //Remove the first [
            tmp[0] = tmp[0].Substring(1);
            for (var i = 0; i < tmp.Length; i++)
            {
                tmp[i] = tmp[i].Substring(1, tmp[i].Length - 2);
            }
            //Remove the last ]
            tmp[tmp.Length - 1] = tmp[tmp.Length - 1].Substring(0, tmp[tmp.Length - 1].Length - 1);
            return tmp;
        }

        public static int GetBuildingIndexFromToken(string token)
        {
            return BitConverter.ToInt32(Convert.FromBase64String(token).Skip(12).ToArray(), 0);
        }
    }
}