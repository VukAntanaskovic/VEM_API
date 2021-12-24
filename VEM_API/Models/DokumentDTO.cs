using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class DokumentDTO
    {
        public int dok_sifra_dokumenta { get; set; }
        public LTipDokumenta tip_dokumenta { get; set; }
        public DateTime? dok_datum_Kreiranja { get; set; }
        public KorisnikDTO korisnik { get; set; }
        public KomitentDTO komitent { get; set; }
        public int? dok_veza { get; set; }
        public LPrimalac pri_primalac { get; set; }
        public LPoslovnica psl_sifra_poslovnica { get; set; }
        public DateTime? dok_datum_isporuke { get; set; }
        public string dok_napomena { get; set; }
        public string dok_broj_dokumenta { get; set; }
        public bool? dok_otvoren { get; set; }

    }
}