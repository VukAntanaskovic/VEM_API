using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class KorisnikDTO
    {
        public int kor_sifra { get; set; }
        public string kor_ime { get; set; }
        public string kor_prezime { get; set; }
        public string kor_telefon { get; set; }
        public string kor_username { get; set; }
        public string kor_password { get; set; }
        public int atr_autorizacija { get; set; }
        public string psl_poslovnica_rada { get; set; }
        public string atr_naziv { get; set; }
        public bool WEB { get; set; } = false;
        public bool GUI { get; set; } = false;
        public bool MOBILE { get; set; } = false;

    }
}