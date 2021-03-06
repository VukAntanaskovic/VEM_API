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
    
    public partial class Isporuka
    {
        public Isporuka()
        {
            this.Stavke_Isporuke = new HashSet<Stavke_Isporuke>();
            this.Stavke_Tovarnog_Lista = new HashSet<Stavke_Tovarnog_Lista>();
        }
    
        public int isp_broj { get; set; }
        public Nullable<int> pri_primalac { get; set; }
        public Nullable<System.DateTime> isp_datum { get; set; }
        public Nullable<int> dok_veza { get; set; }
        public Nullable<int> sts_status { get; set; }
    
        public virtual Dokument Dokument { get; set; }
        public virtual Krajnji_Primalac Krajnji_Primalac { get; set; }
        public virtual ICollection<Stavke_Isporuke> Stavke_Isporuke { get; set; }
        public virtual ICollection<Stavke_Tovarnog_Lista> Stavke_Tovarnog_Lista { get; set; }
    }
}
