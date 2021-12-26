using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class PrimalacDTO
    {
        public int pri_sifra { get; set; }
        public string pri_ime_pezime { get; set; }
        public string pri_adresa { get; set; }
        public string pri_adresa_broj { get; set; }
        public string pri_grad { get; set; }
        public int? pri_ptt { get; set; }

        public string pri_telefon { get; set; }
        public string pri_email { get; set; }
    }
}