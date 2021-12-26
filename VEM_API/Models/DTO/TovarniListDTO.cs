using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class TovarniListDTO
    {
        public int tl_broj { get; set; }
        public DateTime? tl_datum { get; set; }
        public VozaciDTO vzc_vozac { get; set; }
    }
}