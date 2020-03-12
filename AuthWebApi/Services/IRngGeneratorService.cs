using System;
using System.Collections.Generic;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public interface IRngGeneratorService
    {
        Guid GetGuid();
        string GetString(ushort len, EAlphabetType alphabet = EAlphabetType.AZ19);
        string GetString(ushort len, IEnumerable<char> alphabet);
    }
}