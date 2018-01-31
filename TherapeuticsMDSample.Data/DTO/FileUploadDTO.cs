using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TherapeuticsMDSample.Data.DataModels;

namespace TherapeuticsMDSample.Data.DTO
{
    public class FileUploadDTO
    {
        public bool success { get; set; }
        public string error { get; set; }
        public ReceiptDataModel data { get; set; }

        public FileUploadDTO(bool s, string e)
        {
            this.success = s;
            this.error = e;
        }

        public FileUploadDTO(bool s, ReceiptDataModel data)
        {
            this.success = s;
            this.data = data;
        }
    }
}
