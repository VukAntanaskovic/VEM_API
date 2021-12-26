using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class VozaciDTO
    {
        public int vzc_sifra { get; set; }
        public KorisnikDTO korisnik { get; set; }
        public VoziloDTO vozilo { get; set; }
    }
}