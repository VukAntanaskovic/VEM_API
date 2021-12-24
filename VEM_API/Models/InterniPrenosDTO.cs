using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class InterniPrenosDTO
    {
        public int Id { get; set; }
        public int dokument { get; set; }
        public int SaPoslovnice { get; set; }
        public int NaPoslovnicu { get; set; }
        public DateTime? datum_kreiranja { get; set; }
    }
}