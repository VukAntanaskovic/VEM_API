//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VEM_API.DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Krajnji_Primalac
    {
        public Krajnji_Primalac()
        {
            this.Dokuments = new HashSet<Dokument>();
            this.Isporukas = new HashSet<Isporuka>();
        }
    
        public int pri_sifra { get; set; }
        public string pri_ime_prezime { get; set; }
        public string pri_adresa { get; set; }
        public string pri_adresa_broj { get; set; }
        public string pri_grad { get; set; }
        public Nullable<int> pri_ptt { get; set; }
        public string pri_telefon { get; set; }
        public string pri_email { get; set; }
    
        public virtual ICollection<Dokument> Dokuments { get; set; }
        public virtual ICollection<Isporuka> Isporukas { get; set; }
    }
}