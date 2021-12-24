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
        IEnumerable<LVozilo> GetAllVozila();
        IEnumerable<LVozilo> GetVoziloByParametar(string parametar);
        bool CreateNewVozilo(LVozilo vozilo);
        bool UpdateVozilo(int id, LVozilo vozilo);

    }
}
