using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IPrimalacRepository
    {
        IEnumerable<LPrimalac> GetAllPrimalac();
        IEnumerable<LPrimalac> GetPrimalacById(int id);
        bool InsertNewPrimalac(LPrimalac primalac);

    }
}
