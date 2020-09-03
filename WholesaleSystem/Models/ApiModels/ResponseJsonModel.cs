using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WholesaleSystem.Models.ApiModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ResponseJsonModel
    {
        public string Ask { get; set; }

        public string Message { get; set; }

        public bool NextPage { get; set; }

        public Pagenation Pagenation { get; set; }

        public int Count { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Pagenation
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
