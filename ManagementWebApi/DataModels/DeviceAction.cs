using System;

namespace ManagementWebApi.DataModels
{
    public class DeviceAction
    {
        public long Id { get; set; }

        public DateTime? ReceiptDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public long TypeId { get; set; }
    }
}