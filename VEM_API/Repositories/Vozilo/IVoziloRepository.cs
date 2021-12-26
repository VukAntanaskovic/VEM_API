using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IVoziloRepository
    {
        IEnumerable<VoziloDTO> GetAllVozila();
        IEnumerable<VoziloDTO> GetVoziloByParametar(string parametar);
        bool CreateNewVozilo(VoziloDTO vozilo);
        bool UpdateVozilo(int id, VoziloDTO vozilo);

    }
}
