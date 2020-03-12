using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApiSharedParts.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    [Flags]
    public enum EUserState : int
    {
        InChallenge = 1,
        NeedPassChange = 2,
        Ok = 255
    }
}