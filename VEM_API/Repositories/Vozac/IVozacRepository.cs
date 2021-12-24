using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IVozacRepository
    {
        IEnumerable<LVozaci> GetAllVozace();
        IEnumerable<LVozaci> GetVozaceBySifra(int sifra);
        bool CreateNewVozac(LVozaci vozac);
        bool UpdateVozac(int id, LVozaci vozac);

    }
}
