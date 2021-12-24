using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEM_API.Models
{
    public class AppUser
    {
        public int user_sifra { get; set; }
        public string user_ime_prezime { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}