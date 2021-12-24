using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VEM_API.DbModel;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public class AnalitikaRepository : IAnalitikaRepository
    {
        public List<AnalitikaDTO> GetAnalitika()
        {
            VEMTESTEntities db = new VEMTESTEntities();
            List<AnalitikaDTO> lista = new List<AnalitikaDTO>();

            /*--------------------------Otvoreni dokumenti-----------------------------*/
            var otvoreniDokumenti = (from dok in db.Dokuments
                                     where dok.dok_otvoren == true && dok.tip_dokumenta == 12
                                     select dok).Count();

            /*--------------------------Otvorene isporuke-----------------------------*/
            var isporukaUnetaUSistem = (from isp in db.Isporukas
                                        where isp.sts_status == 5
                                        select isp).Count();

            /*-------------------Ukupna kolicina artikala-----------------------------*/
            var ukupnaKolicinaArtikala = (from zal in db.Zaliha_artikla
                                          select zal.art_dostupna_kolicina).Sum();

            /*--------------------------Ukupno vozila-----------------------------*/
            var ukupnoVozila = (from vzl in db.Voziloes
                                where vzl.vzl_aktivno == true
                                select vzl).Count();

            lista.Add(
              new AnalitikaDTO()
              {
                  narudzbe = otvoreniDokumenti,
                  isporuke = isporukaUnetaUSistem,
                  kolicine = ukupnaKolicinaArtikala,
                  vozila = ukupnoVozila
              });


            return lista;
        }
    }
}