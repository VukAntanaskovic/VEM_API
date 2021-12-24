using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IArtikalRepository
    {
        IEnumerable<ArtikalDTO> GetAllArtikal();
        IEnumerable<ArtikalDTO> GetArtikalBySifra(string parametar);
        bool CreateNewArtikal(ArtikalDTO artikal);
        void AddToZaliha(int art_sifra, int psl_sifra);
        bool UpdateArtikal(int art_sifra, string art_naziv, string art_proizvodjac, string art_ean, int kom_dobavljac, string art_tip, bool? art_aktivan);
        bool ActivateDeactivateArtikal(int art_sifra);
    }
}
