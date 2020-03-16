﻿using System;

namespace ManagementWebApi.DataModels
{
    public class CertUploadResult : Cert, IUploadResult
    {
        public string Hash { get; set; }

        public DateTime NotBefore { get; set; }
        public DateTime NotAfter { get; set; }
    }
}