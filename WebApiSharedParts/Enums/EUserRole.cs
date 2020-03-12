using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApiSharedParts.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EUserRole : short
    {
        User = 0,
        Admin = 1,
        Mitm = 2
    }
}