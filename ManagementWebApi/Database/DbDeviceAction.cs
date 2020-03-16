using System;

namespace ManagementWebApi.Database
{
    public class DbDeviceAction
    {
        public long Id { get; set; }

        public DateTime ReceiptDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public long DeviceId { get; set; }
        public DbDevice NavDevice { get; set; }

        public long ActionTypeId { get; set; }
        public DbDeviceActionType NavActionType { get; set; }
    }
}
