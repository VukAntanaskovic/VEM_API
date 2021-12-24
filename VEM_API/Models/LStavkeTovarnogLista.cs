using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class LStavkeTovarnogLista
    {
        public int tl_broj { get; set; }
        public LIsporuka isporuka { get; set; }
    }
}