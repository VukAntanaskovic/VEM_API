using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LVozaci
    {
        public int vzc_sifra { get; set; }
        public LKorisnik korisnik { get; set; }
        public LVozilo vozilo { get; set; }
    }
}