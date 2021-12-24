using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LRaf
    {
        public LRaf(int raf_sifra, string raf_lokacija, int psl_poslovnica)
        {
            this.raf_sifra = raf_sifra;
            this.raf_lokacija = raf_lokacija;
            this.psl_poslovnica = psl_poslovnica;
        }

        public int raf_sifra { get; set; }
        public string raf_lokacija { get; set; }
        public int psl_poslovnica { get; set; }
    }
}