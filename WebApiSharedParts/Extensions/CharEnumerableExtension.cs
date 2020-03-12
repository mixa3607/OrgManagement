using System.Collections.Generic;
using System.Linq;
using WebApiSharedParts.Enums;

namespace WebApiSharedParts.Extensions
{
    public static class CharEnumerableExtension
    {
        public static bool OnlySpecChars(this IEnumerable<char> chars, EAlphabetType alphabetType)
        {
            var alphabet = Alphabets.Get(alphabetType);
            return OnlySpecChars(chars, alphabet);
        }

        public static bool OnlySpecChars(this IEnumerable<char> chars, IEnumerable<char> alphabet)
        {
            return chars.All(alphabet.Contains);
        }
    }
}