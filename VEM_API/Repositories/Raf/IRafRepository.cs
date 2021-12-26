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
        bool CreateNewRaf(RafDTO raf);
        IEnumerable<ZalihaArtiklaDTO> GetAllArtikalInRaf(int raf_sifra);
        IEnumerable<RafDTO> GetAllRaf();
        bool UpdateRaf(int id, RafDTO lokacija);
        IEnumerable<RafDTO> GetAllRafInPoslovnica(int id, string parametar);


    }
}
