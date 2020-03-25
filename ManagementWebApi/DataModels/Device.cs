using System.Collections.Generic;

namespace ManagementWebApi.DataModels
{
    public class Device
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InvNumber { get; set; }
        public long TypeId { get; set; }

        public IEnumerable<DeviceAction> Actions { get; set; }
        public IEnumerable<Software> Softwares { get; set; }
        public Employee Employee { get; set; }
    }
}