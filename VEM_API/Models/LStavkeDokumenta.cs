using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LStavkeDokumenta
    {
        public int stv_id { get; set; }
        public int stv_broj_dokumenta { get; set; }
        public ArtikalDTO artikal { get; set; }
        public int stv_kolicina { get; set; }
        public int rbr { get; set; }
    }
}