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
    
    public partial class Zaliha_artikla
    {
        public int zal_sifra { get; set; }
        public Nullable<int> art_sifra { get; set; }
        public Nullable<int> art_dostupna_kolicina { get; set; }
        public Nullable<int> art_rezervisana_kolicina { get; set; }
        public Nullable<int> psl_poslovnica { get; set; }
        public Nullable<int> raf_sifra { get; set; }
    
        public virtual Artikal Artikal { get; set; }
        public virtual Poslovnica Poslovnica { get; set; }
        public virtual Raf Raf { get; set; }
    }
}