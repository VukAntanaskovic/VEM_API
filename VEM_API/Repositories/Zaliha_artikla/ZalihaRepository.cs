using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.LogProvider;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class ZalihaRepository:IZalihaRepository
    {

        private readonly ILogProvider _logProvider;
        private readonly IEntityRepository _entity;
        public static  string OkResponseMessage;
        public static  string ErrorResponseMessage;

        public ZalihaRepository(ILogProvider logProvider, IEntityRepository entity)
        {
            _logProvider = logProvider;
            _entity = entity;
        }
        //Zaliha_Artikala
        public  bool AddToZalihaArtikla(LZalihaArtikla zaliha)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var provera = from ar in db.Zaliha_artikla
                          where ar.art_sifra == zaliha.artikal.art_sifra && ar.psl_poslovnica == zaliha.poslovnica.psl_sifra
                          select ar;
            if (provera.Count() == 0 || provera == null)
            {
                try
                {
                    DbModel.Zaliha_artikla zl = new DbModel.Zaliha_artikla();
                    zl.art_sifra = zaliha.artikal.art_sifra;
                    zl.art_dostupna_kolicina = 0;
                    zl.art_rezervisana_kolicina = 0;
                    zl.psl_poslovnica = zaliha.poslovnica.psl_sifra;
                    zl.raf_sifra = zaliha.raf.raf_sifra;
                    db.Zaliha_artikla.Add(zl);
                    db.SaveChanges();
                    OkResponseMessage = "Uspesno ste dodali artikal na zalihu";
                    _logProvider.AddToLog("AddToZalihaArtikla(LZalihaArtikla zaliha)", OkResponseMessage, false);
                    return true;
                }
                catch (Exception e)
                {
                    ErrorResponseMessage = "Greska prilikom dodavanja artikla na zalihu: " + e.Message;
                    _logProvider.AddToLog("AddToZalihaArtikla(LZalihaArtikla zaliha)", ErrorResponseMessage, true);
                    return false;
                }
            }
            else
            {
                ErrorResponseMessage = "Pogresno prosledjeni podaci";
                _logProvider.AddToLog("AddToZalihaArtikla(LZalihaArtikla zaliha)", ErrorResponseMessage, true);
                return false;
            }
        }//dodavanje artikla na zalihu na poslovnicu

        public  IEnumerable<LZalihaArtikla> GetAllZaliheArtikla(int psl_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<LZalihaArtikla> zaliha = new List<LZalihaArtikla>();
            if (psl_sifra == 0)
            {
                var sveZalihe = from zal in db.Zaliha_artikla
                                join art in db.Artikals on zal.art_sifra equals art.art_sifra
                                join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                                join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                                join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                                join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra
                                select new { zal, art, psl, jed, dob, raf };
                foreach (var art in sveZalihe)
                {
                    zaliha.Add(new LZalihaArtikla(art.zal.zal_sifra,
                      new LArtikal(art.art.art_sifra, art.art.art_naziv, art.art.art_proizvodjac, art.art.art_ean, new LJedinicaMere(art.jed.jed_sifra, art.jed.jed_naziv, art.jed.jed_kratki_naziv),
                      new LKomitent(art.dob.kom_sifra, art.dob.kom_naziv, art.dob.kom_adresa, art.dob.kom_grad, art.dob.kom_ptt, Convert.ToBoolean(art.dob.kom_dobavljac), Convert.ToBoolean(art.dob.kom_aktivan), art.dob.kom_PIB, art.dob.kom_MBR),
                      art.art.art_tip, Convert.ToBoolean(art.art.art_aktivan)),
                      Convert.ToInt32(art.zal.art_dostupna_kolicina), Convert.ToInt32(art.zal.art_rezervisana_kolicina),
                      new LPoslovnica(art.psl.psl_sifra, art.psl.psl_naziv, Convert.ToBoolean(art.psl.psl_aktivna)),
                      new LRaf(art.raf.raf_sifra, art.raf.raf_lokacija, Convert.ToInt32(art.raf.psl_poslovnica)))
                    );
                }

                return zaliha;

            }
            else
            {
                var sveZalihe = from zal in db.Zaliha_artikla
                                join art in db.Artikals on zal.art_sifra equals art.art_sifra
                                join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                                join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                                join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                                join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra
                                where psl.psl_sifra == psl_sifra
                                select new { zal, art, psl, jed, dob, raf };
                foreach (var art in sveZalihe)
                {
                    zaliha.Add(new LZalihaArtikla(art.zal.zal_sifra,
                      new LArtikal(art.art.art_sifra, art.art.art_naziv, art.art.art_proizvodjac, art.art.art_ean, new LJedinicaMere(art.jed.jed_sifra, art.jed.jed_naziv, art.jed.jed_kratki_naziv),
                      new LKomitent(art.dob.kom_sifra, art.dob.kom_naziv, art.dob.kom_adresa, art.dob.kom_grad, art.dob.kom_ptt, Convert.ToBoolean(art.dob.kom_dobavljac), Convert.ToBoolean(art.dob.kom_aktivan), art.dob.kom_PIB, art.dob.kom_MBR),
                      art.art.art_tip, Convert.ToBoolean(art.art.art_aktivan)),
                      Convert.ToInt32(art.zal.art_dostupna_kolicina), Convert.ToInt32(art.zal.art_rezervisana_kolicina),
                      new LPoslovnica(art.psl.psl_sifra, art.psl.psl_naziv, Convert.ToBoolean(art.psl.psl_aktivna)),
                      new LRaf(art.raf.raf_sifra, art.raf.raf_lokacija, Convert.ToInt32(art.raf.psl_poslovnica)))
                    );
                }

                return zaliha;
            }
        }

        public  IEnumerable<LZalihaArtikla> FindZalihaArtikla(int psl_sifra, string parametar)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            int sifra;
            int.TryParse(parametar, out sifra);
            List<LZalihaArtikla> zaliha = new List<LZalihaArtikla>();
            if (psl_sifra == 0)
            {
                var sveZalihe = from zal in db.Zaliha_artikla
                                join art in db.Artikals on zal.art_sifra equals art.art_sifra
                                join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                                join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                                join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                                join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra
                                where art.art_sifra == sifra || art.art_naziv.Contains(parametar) || art.art_proizvodjac.Contains(parametar) || art.art_ean.Equals(parametar)
                                select new { zal, art, psl, jed, dob, raf };
                foreach (var art in sveZalihe)
                {
                    zaliha.Add(new LZalihaArtikla(art.zal.zal_sifra,
                       new LArtikal(art.art.art_sifra, art.art.art_naziv, art.art.art_proizvodjac, art.art.art_ean, new LJedinicaMere(art.jed.jed_sifra, art.jed.jed_naziv, art.jed.jed_kratki_naziv),
                       new LKomitent(art.dob.kom_sifra, art.dob.kom_naziv, art.dob.kom_adresa, art.dob.kom_grad, art.dob.kom_ptt, Convert.ToBoolean(art.dob.kom_dobavljac), Convert.ToBoolean(art.dob.kom_aktivan), art.dob.kom_PIB, art.dob.kom_MBR),
                       art.art.art_tip, Convert.ToBoolean(art.art.art_aktivan)),
                       Convert.ToInt32(art.zal.art_dostupna_kolicina), Convert.ToInt32(art.zal.art_rezervisana_kolicina),
                       new LPoslovnica(art.psl.psl_sifra, art.psl.psl_naziv, Convert.ToBoolean(art.psl.psl_aktivna)),
                       new LRaf(art.raf.raf_sifra, art.raf.raf_lokacija, Convert.ToInt32(art.raf.psl_poslovnica)))
                     );
                }

                return zaliha;

            }
            else
            {
                var sveZalihe = from zal in db.Zaliha_artikla
                                join art in db.Artikals on zal.art_sifra equals art.art_sifra
                                join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                                join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                                join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                                join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra
                                where (psl.psl_sifra == psl_sifra) && (art.art_sifra == sifra || art.art_naziv.Contains(parametar) || art.art_proizvodjac.Contains(parametar) || art.art_ean.Equals(parametar))
                                select new { zal, art, psl, jed, dob, raf };
                foreach (var art in sveZalihe)
                {
                    zaliha.Add(new LZalihaArtikla(art.zal.zal_sifra,
                      new LArtikal(art.art.art_sifra, art.art.art_naziv, art.art.art_proizvodjac, art.art.art_ean, new LJedinicaMere(art.jed.jed_sifra, art.jed.jed_naziv, art.jed.jed_kratki_naziv),
                      new LKomitent(art.dob.kom_sifra, art.dob.kom_naziv, art.dob.kom_adresa, art.dob.kom_grad, art.dob.kom_ptt, Convert.ToBoolean(art.dob.kom_dobavljac), Convert.ToBoolean(art.dob.kom_aktivan), art.dob.kom_PIB, art.dob.kom_MBR),
                      art.art.art_tip, Convert.ToBoolean(art.art.art_aktivan)),
                      Convert.ToInt32(art.zal.art_dostupna_kolicina), Convert.ToInt32(art.zal.art_rezervisana_kolicina),
                      new LPoslovnica(art.psl.psl_sifra, art.psl.psl_naziv, Convert.ToBoolean(art.psl.psl_aktivna)),
                      new LRaf(art.raf.raf_sifra, art.raf.raf_lokacija, Convert.ToInt32(art.raf.psl_poslovnica)))
                    );
                }

                return zaliha;
            }
        }

        public bool AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            DbModel.Zaliha_artikla zal = (from n in db.Zaliha_artikla
                                  where n.art_sifra == art_sifra && n.psl_poslovnica == psl_sifra
                                  select n).FirstOrDefault();
            try
            {
                zal.raf_sifra = rad_sifra;
                db.SaveChanges();
                OkResponseMessage = "Uspesno ste zalihu za artikal " + zal.art_sifra + " na raf: " + zal.raf_sifra;
                _logProvider.AddToLog("AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)", OkResponseMessage, false);
                return true;
            }
            catch (Exception e)
            {
                ErrorResponseMessage = "Greska prilikom dodavanja zalihe na raf: " + e.Message;
                _logProvider.AddToLog("AddToRaf(int art_sifra, int psl_sifra, int rad_sifra)", ErrorResponseMessage, true);
                return false;
            }
        }

        public  IEnumerable<LZalihaArtikla> GetAllArtikalInRaf(int raf_sifra)
        {
            VEMTESTEntities db = new VEMTESTEntities();
            var sveZalihe = from zal in db.Zaliha_artikla
                            join art in db.Artikals on zal.art_sifra equals art.art_sifra
                            join psl in db.Poslovnicas on zal.psl_poslovnica equals psl.psl_sifra
                            join jed in db.Jedinica_Mere on art.art_jedinica_mere equals jed.jed_sifra
                            join dob in db.Komitents on art.kom_dobavljac equals dob.kom_sifra
                            join raf in db.Rafs on zal.raf_sifra equals raf.raf_sifra
                            where zal.raf_sifra == raf_sifra
                            select new { zal, art, psl, jed, dob, raf };
            List<LZalihaArtikla> zaliha = new List<LZalihaArtikla>();
            foreach (var art in sveZalihe)
            {
                zaliha.Add(new LZalihaArtikla(art.zal.zal_sifra,
                      new LArtikal(art.art.art_sifra, art.art.art_naziv, art.art.art_proizvodjac, art.art.art_ean, new LJedinicaMere(art.jed.jed_sifra, art.jed.jed_naziv, art.jed.jed_kratki_naziv),
                      new LKomitent(art.dob.kom_sifra, art.dob.kom_naziv, art.dob.kom_adresa, art.dob.kom_grad, art.dob.kom_ptt, Convert.ToBoolean(art.dob.kom_dobavljac), Convert.ToBoolean(art.dob.kom_aktivan), art.dob.kom_PIB, art.dob.kom_MBR),
                      art.art.art_tip, Convert.ToBoolean(art.art.art_aktivan)),
                      Convert.ToInt32(art.zal.art_dostupna_kolicina), Convert.ToInt32(art.zal.art_rezervisana_kolicina),
                      new LPoslovnica(art.psl.psl_sifra, art.psl.psl_naziv, Convert.ToBoolean(art.psl.psl_aktivna)),
                      new LRaf(art.raf.raf_sifra, art.raf.raf_lokacija, Convert.ToInt32(art.raf.psl_poslovnica)))
                    );
            }
            return zaliha;
        }
    }
}