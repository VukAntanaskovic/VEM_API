using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IMernaJedinicaRepository
    {
        /// <summary>
        /// Sve jedinice mere
        /// </summary>
        /// <returns>IEnumerable<LJedinicaMere></returns>
        IEnumerable<JedinicaMereDTO> GetAllJedinicaMere();
        bool CreateNewJedinicaMere(JedinicaMereDTO jedinicaMere);
        bool UpdateJedinicaMere(int sifra, JedinicaMereDTO jedinicaMere);
    }
}
