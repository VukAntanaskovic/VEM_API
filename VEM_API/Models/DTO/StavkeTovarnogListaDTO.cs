using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class StavkeTovarnogListaDTO
    {
        public int tl_broj { get; set; }
        public IsporukaDTO isporuka { get; set; }
    }
}