using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IAdresarRepository
    {
        IEnumerable<AdresarDTO> GetAllAdresar();
        IEnumerable<AdresarDTO> GetAllAdresarByAdresa(string Adresa);

    }
}
