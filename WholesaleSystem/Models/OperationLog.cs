using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models
{
    public class OperationLog
    {
        public int Id { get; set; }

        public string OperationName { get; set; }

        public DateTime OperationTime { get; set; }

        public string OperatedBy { get; set; }
    }
}
