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
    
    public partial class Korisnik_Sistema
    {
        public Korisnik_Sistema()
        {
            this.Dokuments = new HashSet<Dokument>();
            this.Vozacs = new HashSet<Vozac>();
        }
    
        public int kor_sifra { get; set; }
        public string kor_ime { get; set; }
        public string kor_prezime { get; set; }
        public string kor_telefon { get; set; }
        public string kor_username { get; set; }
        public string kor_password { get; set; }
        public int atr_autorizacija { get; set; }
        public Nullable<int> psl_poslovnica_rada { get; set; }
        public Nullable<bool> VEM_WEB { get; set; }
        public Nullable<bool> VEM_GUI { get; set; }
        public Nullable<bool> VEM_MOBILE { get; set; }
    
        public virtual Autorizacija_Korisnika Autorizacija_Korisnika { get; set; }
        public virtual ICollection<Dokument> Dokuments { get; set; }
        public virtual Poslovnica Poslovnica { get; set; }
        public virtual ICollection<Vozac> Vozacs { get; set; }
    }
}
