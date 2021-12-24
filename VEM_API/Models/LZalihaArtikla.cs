using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LZalihaArtikla
    {
        public int zal_sifra { get; set; }
        public ArtikalDTO artikal { get; set; }
        public int art_dostupna_kolicina { get; set; }
        public int art_rezervisana_kolicina { get; set; }
        public LPoslovnica poslovnica { get; set; }
        public LRaf raf { get; set; }
    }
}