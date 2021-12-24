using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IRafRepository
    {
        bool AddToRaf(int art_sifra, int psl_sifra, int rad_sifra);
        bool CreateNewRaf(LRaf raf);
        IEnumerable<LZalihaArtikla> GetAllArtikalInRaf(int raf_sifra);
        IEnumerable<LRaf> GetAllRaf();
        bool UpdateRaf(int id, LRaf lokacija);
        IEnumerable<LRaf> GetAllRafInPoslovnica(int id, string parametar);


    }
}
