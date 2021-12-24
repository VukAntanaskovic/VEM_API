using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IKomitentRepository
    {
        IEnumerable<KomitentDTO> GetAllKomitent();
        IEnumerable<KomitentDTO> GetKomitentByParameter(string parametar);
        bool CreateNewKomitent(KomitentDTO komitent);
        bool UpdateKomitent(int id, KomitentDTO komitent);
        bool ActivateDeactivateKomitent(int id);
        IEnumerable<KomitentDTO> GetAllDobavljac();
        IEnumerable<KomitentDTO> GetDobavljacByParametar(string parametar);


    }
}
