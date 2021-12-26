using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class KomitentDTO
    {
        public int kom_sifra { get; set; }
        public string kom_naziv { get; set; }
        public string kom_adresa { get; set; }

        public string kom_grad { get; set; }

        public string kom_ptt { get; set; }
        public bool? kom_dobavljac { get; set; }
        public bool? kom_aktivan { get; set; }
        public string kom_PIB { get; set; }
        public string kom_MBR { get; set; }
    }
}