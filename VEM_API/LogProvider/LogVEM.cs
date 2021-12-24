using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.LogProvider
{
    public class LogVEM
    {
        public int log_Id { get; set; }
        public string log_function { get; set; }
        public DateTime log_date { get; set; }
        public string log_time { get; set; }
        public string log_message { get; set; }
    }
}