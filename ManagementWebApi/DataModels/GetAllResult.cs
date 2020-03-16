using System.Collections.Generic;

namespace ManagementWebApi.DataModels
{
    public class GetAllResult<T> where T:new()
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Values { get; set; }
    }
}