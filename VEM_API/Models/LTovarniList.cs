using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LTovarniList
    {
        public int tl_broj { get; set; }
        public DateTime? tl_datum { get; set; }
        public LVozaci vzc_vozac { get; set; }
    }
}