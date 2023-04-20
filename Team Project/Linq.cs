using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Team_Project
{
    public static class LinqClass
    {
        public static string connectionstring = "Data Source = DESKTOP-SH54RD2; Initial Catalog = Game_Users ;Trusted_Connection=True; TrustServerCertificate = True ";

        public static string ToSHA256(string value)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
