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
        IEnumerable<VozaciDTO> GetAllVozace();
        IEnumerable<VozaciDTO> GetVozaceBySifra(int sifra);
        bool CreateNewVozac(VozaciDTO vozac);
        bool UpdateVozac(int id, VozaciDTO vozac);

    }
}
