using System;
using System.Collections.Generic;

namespace Drone_client_WebAPI {
    static class Utilities {
        public static byte[] ToByteArray(string[] stringArray) {
            List<byte> result = new List<byte>();
            foreach (string str in stringArray) {
                result.Add(Convert.ToByte(str));
            }
            return result.ToArray();
        }
        public static string GetFileName(string filePath) {
            return filePath.Substring(filePath.LastIndexOf('\\') + 1);
        }

    }
}
