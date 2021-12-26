using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IZalihaRepository
    {
        bool AddToZalihaArtikla(ZalihaArtiklaDTO zaliha);
        IEnumerable<ZalihaArtiklaDTO> GetAllZaliheArtikla(int psl_sifra);

        IEnumerable<ZalihaArtiklaDTO> FindZalihaArtikla(int psl_sifra, string parametar);
        bool AddToRaf(int art_sifra, int psl_sifra, int rad_sifra);
        IEnumerable<ZalihaArtiklaDTO> GetAllArtikalInRaf(int raf_sifra);

    }
}
