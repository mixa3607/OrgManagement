namespace ManagementWebApi.DataModels
{
    public class UploadResult : IUploadResult
    {
        public long Id { get; set; }
        public string Hash { get; set; }
    }
}