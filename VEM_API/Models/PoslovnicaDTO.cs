using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class PoslovnicaDTO
    {
        public int psl_sifra { get; set; }
        public string psl_naziv { get; set; }
        public bool? psl_aktivna { get; set; }
    }
}