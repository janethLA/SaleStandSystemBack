using Microsoft.Extensions.ObjectPool;
using System.Security.Cryptography;
using System.Text;

namespace MySalesStandSystem.Utils
{
    public class Encrypt
    {
        public static string getSHA256(string message) { 

            SHA256 sha256= SHA256.Create();
            ASCIIEncoding encoding= new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb= new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(message));

            for (int i = 0; i< stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}" , stream[i]);
            }
            return sb.ToString();
        }
    }
}
