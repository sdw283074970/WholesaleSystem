using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    public class ResponseJson
    {
        public string Ask { get; set; }

        public string Message { get; set; }

        public bool NextPage { get; set; }

        public Pagenation Pagenation { get; set; }

        public int Count { get; set; }
    }

    public  class Pagenation
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
