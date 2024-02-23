using System;

namespace Vindi.SDK.Services
{
    public class VindRequestParamsRaw
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public string Query { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}