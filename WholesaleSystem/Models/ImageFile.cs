using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models
{
    public class ImageFile
    {
        public int Id { get; set; }

        public string PictureName { get; set; }

        public string Path { get; set; }

        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public string UploadBy { get; set; }

        public bool Active { get; set; }

        public bool IsMainPicture { get; set; }

        public ProductInventory ProductInventory { get; set; }

        public ICollection<OperationLog> OperationLogs { get; set; }
    }
}
