using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models
{
    public class AppSettingsModel
    {
        public static string DefaultConnection { get; set; }
        public static string StorageConnectionstring { get; set; }
        public static string ContainerName { get; set; }
    }
}
