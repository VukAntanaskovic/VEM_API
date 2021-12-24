using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class AdresarDTO
    {
        public int id { get; set; }
        public string adresa { get; set; }
        public string grad { get; set; }
        public string opstina { get; set; }
        public int? ptt { get; set; }
    }
}