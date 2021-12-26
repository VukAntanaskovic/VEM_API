using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class ArtikalDTO
    {
        public int art_sifra { get; set; }
        public string art_naziv { get; set; }
        public string art_proizvodjac { get; set; }
        public string art_ean { get; set; }

        public JedinicaMereDTO jedinica_mere{ get; set; }

        public KomitentDTO dobavljac { get; set; }

        public string art_tip { get; set; }
        public bool? art_aktivan { get; set; }
    }
}