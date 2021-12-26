using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class ZalihaArtiklaDTO
    {
        public int zal_sifra { get; set; }
        public ArtikalDTO artikal { get; set; }
        public int? art_dostupna_kolicina { get; set; }
        public int? art_rezervisana_kolicina { get; set; }
        public PoslovnicaDTO poslovnica { get; set; }
        public RafDTO raf { get; set; }
    }
}