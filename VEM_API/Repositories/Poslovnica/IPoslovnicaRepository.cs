using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IPoslovnicaRepository
    {
        IEnumerable<LPoslovnica> GetAllPoslovnica();
        IEnumerable<LPoslovnica> GetPoslovnicaByParametar(string parametar);
        bool AddNewPoslovnica(LPoslovnica poslovnica);
        bool ActivateDeactivatePoslovnica(int psl_sifra);
        bool UpdatePoslovnica(int id, LPoslovnica psl);

    }
}
