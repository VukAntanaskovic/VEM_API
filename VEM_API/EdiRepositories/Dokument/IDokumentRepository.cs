using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.DbModel;
using VEM_API.Models;

namespace VEM_API.EdiRepositories
{
    public interface IDokumentRepository
    {
        #region "Tip dokumenta"
        /// <summary>
        /// Kolekcija tipova dokumenata
        /// </summary>
        /// <returns></returns>
        IEnumerable<TipDokumentaDTO> GetAllTipDokumenta();
        #endregion "Tip dokumenta"

        #region "Dokumenti"
        /// <summary>
        /// Kolekcija svih dokumenata
        /// </summary>
        /// <returns></returns>
        IEnumerable<DokumentDTO> GetAllDokument();

        /// <summary>
        /// Kolekcija dokumenata pod kriterijumom parametra
        /// </summary>
        /// <param name="parametar">Sifra ili broj dokumenta</param>
        /// <returns></returns>
        IEnumerable<DokumentDTO> GetDokumentById(string parametar);

        /// <summary>
        /// Kolekcija svih potvrdjenih dokumenata
        /// </summary>
        /// <returns></returns>
        IEnumerable<DokumentDTO> GetAllPotvrdjenDokument();

        /// <summary>
        /// Kolekcija potvrdjenih dokumenata pod kriterijumom parametra
        /// </summary>
        /// <param name="parametar">Sifra ili broj dokumenta</param>
        /// <returns></returns>
        IEnumerable<DokumentDTO> GetPotvrdjeniDokumentById(string parametar);

        /// <summary>
        /// Unos req dokumenta
        /// </summary>
        /// <param name="dokument"></param>
        /// <param name="na_poslovnicu"></param>
        /// <returns></returns>
        bool CreateRequestDocument(DokumentDTO dokument, int? na_poslovnicu);

        /// <summary>
        /// Unos resp dokumenta
        /// </summary>
        /// <param name="resp_tip_dokumenta"></param>
        /// <param name="resp_komitent"></param>
        /// <param name="resp_dok_veza"></param>
        /// <param name="resp_primalac"></param>
        /// <param name="resp_poslovnica"></param>
        /// <param name="resp_datum_isporuke"></param>
        /// <param name="resp_napomena"></param>
        /// <param name="korisnik"></param>
        void CreateResponseDocument(int resp_tip_dokumenta, int resp_komitent, int resp_dok_veza, int resp_primalac, int resp_poslovnica, DateTime resp_datum_isporuke, string resp_napomena, int korisnik);
        
        /// <summary>
        /// Potvrdjivanje dokumenta
        /// </summary>
        /// <param name="dok_sifra_dokumenta"></param>
        /// <param name="korisnik"></param>
        /// <returns></returns>
        bool ConfirmDocument(int dok_sifra_dokumenta, int korisnik);

        /// <summary>
        /// Kreiranje dokumenta 'Licno preuzimanje'
        /// </summary>
        /// <param name="dok_sifra_dokumenta"></param>
        /// <param name="korisnik"></param>
        /// <returns></returns>
        bool CreateLicnoPreuzimanje(int dok_sifra_dokumenta, int korisnik);

        /// <summary>
        /// Kolekcija dokumenata sa istom vezom
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<DokumentDTO> GetAllVezniDokumenti(int id);

        /// <summary>
        /// Zatvaranje dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CloseDocument(int id);
        #endregion "Dokumenti"

        #region "Stavke dokumenata"
        /// <summary>
        /// Kolekcija stavki jednog dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<StavkeDokumentaDTO> GetAllStavkeDokument(int id);

        /// <summary>
        /// Kreiranje nove stavke dokumenta
        /// </summary>
        /// <param name="stavka"></param>
        /// <returns></returns>
        bool CreateNewStavkaDokumenta(StavkeDokumentaDTO stavka);

        /// <summary>
        /// Brisanje stavke sa dokumenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteStavka(int id);

        /// <summary>
        /// Azuriranje rezervisane kolicine nakon manipulacije sa stavkama
        /// </summary>
        /// <param name="sifra_artikla"></param>
        /// <param name="kolicina"></param>
        /// <param name="broj_dokumenta"></param>
        void UpdateRezervisanaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta);

        /// <summary>
        /// Azuriranje dostupne kolicine nakon manipulacije sa stavkama
        /// </summary>
        /// <param name="sifra_artikla"></param>
        /// <param name="kolicina"></param>
        /// <param name="broj_dokumenta"></param>
        void UpdateDostupnaKolicina(int sifra_artikla, int kolicina, int broj_dokumenta);

        /// <summary>
        /// Provera dostupnosti kolicine
        /// </summary>
        /// <param name="sifra_artikla"></param>
        /// <param name="kolicina"></param>
        /// <param name="broj_dokumenta"></param>
        /// <returns></returns>
        bool CheckDostupnaKolicina(int sifra_artikla, int? kolicina, int? broj_dokumenta);
        #endregion "Stavke dokumenata"

        #region "Manipulacija zalihom"
        /// <summary>
        /// Kreiranje prijema robe, azuriranje zaliha
        /// </summary>
        /// <param name="br_narudzbe"></param>
        void CreatePrijemRobe(int br_narudzbe);

        /// <summary>
        /// Provera zalihe za dokument
        /// </summary>
        /// <param name="sifra"></param>
        /// <param name="kolicina"></param>
        /// <param name="poslovnica"></param>
        /// <returns></returns>
        bool CheckZalihaForDocument(int sifra, int kolicina, int poslovnica);
        #endregion "Manipulacija zalihom"

        #region "Prenosi"
        /// <summary>
        /// Kolekcija otvorenih internih prenosa
        /// </summary>
        /// <returns></returns>
        IEnumerable<InterniPrenosDTO> GetAllOtvoreniPrenos();

        /// <summary>
        /// Kreira novu internu otpremnicu
        /// </summary>
        /// <param name="dokument"></param>
        /// <returns></returns>
        bool CreateNewPrenos(int dokument, int sa_poslovnice, int na_poslovnicu);

        /// <summary>
        /// Kreiranje novog internog prijema i zatvaranje prenosa
        /// </summary>
        /// <param name="prenos"></param>
        /// <returns></returns>
        bool CreateNewInterniPrijem(int? prenos);
        #endregion "Prenosi"


    }
}
