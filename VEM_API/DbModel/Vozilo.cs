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
    
    public partial class Vozilo
    {
        public Vozilo()
        {
            this.Vozacs = new HashSet<Vozac>();
        }
    
        public int vzl_sifra { get; set; }
        public string vzl_marka { get; set; }
        public string vzl_model { get; set; }
        public string vzl_registracija { get; set; }
        public string vzl_slika { get; set; }
        public Nullable<bool> vzl_aktivno { get; set; }
    
        public virtual ICollection<Vozac> Vozacs { get; set; }
    }
}
