namespace ManagementWebApi.DataModels.ListModels
{
    public class DeviceL
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string InvNumber { get; set; }
        public long TypeId { get; set; }

        public long EmployeeId { get; set; }
        public long EmployeeName { get; set; }
    }
}