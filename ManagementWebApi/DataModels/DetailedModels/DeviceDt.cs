using System.Collections.Generic;
using ManagementWebApi.DataModels.ListModels;

namespace ManagementWebApi.DataModels.DetailedModels
{
    public class DeviceDt
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InvNumber { get; set; }
        public long TypeId { get; set; }

        //public IEnumerable<DeviceAction> Actions { get; set; }
        //public IEnumerable<Software> Softwares { get; set; }

        public EmployeeL Employee { get; set; }
    }
}