using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class VoziloDTO
    {
        public int vzl_sifra { get; set; }
        public string vzl_marka { get; set; }
        public string vzl_model { get; set; }
        public string vzl_registracija { get; set; }
        public string vzl_slika { get; set; }
        public bool? vzl_aktivno { get; set; }
    }
}