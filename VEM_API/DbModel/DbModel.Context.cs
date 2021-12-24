﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class VEMTESTEntities : DbContext
    {
        public VEMTESTEntities()
            : base("name=VEMTESTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Adresar> Adresars { get; set; }
        public DbSet<Api_Pristup> Api_Pristup { get; set; }
        public DbSet<Artikal> Artikals { get; set; }
        public DbSet<Autorizacija_Korisnika> Autorizacija_Korisnika { get; set; }
        public DbSet<Dokument> Dokuments { get; set; }
        public DbSet<Isporuka> Isporukas { get; set; }
        public DbSet<Jedinica_Mere> Jedinica_Mere { get; set; }
        public DbSet<Komitent> Komitents { get; set; }
        public DbSet<Korisnik_Sistema> Korisnik_Sistema { get; set; }
        public DbSet<Krajnji_Primalac> Krajnji_Primalac { get; set; }
        public DbSet<log_VEM> log_VEM { get; set; }
        public DbSet<Poslovnica> Poslovnicas { get; set; }
        public DbSet<Raf> Rafs { get; set; }
        public DbSet<Status_Isporuke> Status_Isporuke { get; set; }
        public DbSet<Stavke_Dokumenta> Stavke_Dokumenta { get; set; }
        public DbSet<Stavke_Isporuke> Stavke_Isporuke { get; set; }
        public DbSet<Stavke_Tovarnog_Lista> Stavke_Tovarnog_Lista { get; set; }
        public DbSet<Tip_Dokumenta> Tip_Dokumenta { get; set; }
        public DbSet<Tovarni_List> Tovarni_List { get; set; }
        public DbSet<Vozac> Vozacs { get; set; }
        public DbSet<Vozilo> Voziloes { get; set; }
        public DbSet<Zaliha_artikla> Zaliha_artikla { get; set; }
        public DbSet<Interni_Prenos> Interni_Prenos { get; set; }
    
        public virtual int usp_Svi_Artikli_Na_Poslovnicu(Nullable<int> sifraPoslovnice)
        {
            var sifraPoslovniceParameter = sifraPoslovnice.HasValue ?
                new ObjectParameter("sifraPoslovnice", sifraPoslovnice) :
                new ObjectParameter("sifraPoslovnice", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Svi_Artikli_Na_Poslovnicu", sifraPoslovniceParameter);
        }
    
        public virtual int usp_Unos_Artikla_Na_Zalihu(Nullable<int> prm_art_sifra)
        {
            var prm_art_sifraParameter = prm_art_sifra.HasValue ?
                new ObjectParameter("prm_art_sifra", prm_art_sifra) :
                new ObjectParameter("prm_art_sifra", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Unos_Artikla_Na_Zalihu", prm_art_sifraParameter);
        }
    
        public virtual int usp_UpisVezaBroj(Nullable<int> brojDokumenta)
        {
            var brojDokumentaParameter = brojDokumenta.HasValue ?
                new ObjectParameter("BrojDokumenta", brojDokumenta) :
                new ObjectParameter("BrojDokumenta", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_UpisVezaBroj", brojDokumentaParameter);
        }
    
        public virtual int usp_ZatvaranjeDokumenata(Nullable<int> vezaDokumenta, Nullable<int> tipDokumenta)
        {
            var vezaDokumentaParameter = vezaDokumenta.HasValue ?
                new ObjectParameter("VezaDokumenta", vezaDokumenta) :
                new ObjectParameter("VezaDokumenta", typeof(int));
    
            var tipDokumentaParameter = tipDokumenta.HasValue ?
                new ObjectParameter("TipDokumenta", tipDokumenta) :
                new ObjectParameter("TipDokumenta", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_ZatvaranjeDokumenata", vezaDokumentaParameter, tipDokumentaParameter);
        }
    }
}