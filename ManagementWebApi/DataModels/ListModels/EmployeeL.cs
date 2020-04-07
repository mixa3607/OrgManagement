namespace ManagementWebApi.DataModels.ListModels
{
    public class EmployeeL
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string WorkingPosition { get; set; }
        public string Ipv4Address { get; set; }
        public string DomainNameEntry { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}