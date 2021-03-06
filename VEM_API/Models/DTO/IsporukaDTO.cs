using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class IsporukaDTO
    {
        public int isp_broj { get; set; }
        public PrimalacDTO pri_primalac { get; set; }
        public DateTime? isp_datum { get; set; }
        public int? dok_veza { get; set; }
        public StatusIsporukeDTO sts_status { get; set; }
    }
}