using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiSharedParts;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public class RngGeneratorService : IRngGeneratorService
    {
        private static readonly Random Rng = new Random();

        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public string GetString(ushort len, EAlphabetType alphabet = EAlphabetType.AZ19)
        {
            return GetString(len, Alphabets.Get(alphabet));
        }

        public string GetString(ushort len, IEnumerable<char> alphabet)
        {
            var strBuilder = new StringBuilder(len);
            var c = alphabet as char[] ?? alphabet.ToArray();
            for (int i = 0; i < len; i++)
            {
                strBuilder.Append(c[Rng.Next(c.Length - 1)]);
            }

            return strBuilder.ToString();
        }
    }
}