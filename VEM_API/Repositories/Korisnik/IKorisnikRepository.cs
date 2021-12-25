using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.DbModel;
using VEM_API.Models;

namespace VEM_API.Repositories
{
    public interface IKorisnikRepository
    {
        bool AuthenticateUser(AppUser appUser, string app);
        IEnumerable<KorisnikDTO> GetAllKorisnik();
        IEnumerable<KorisnikDTO> GetKorisnikByParametar(string parametar);
        bool CreateNewKorisnik(KorisnikDTO korisnik);
        bool UpdatePasswordKorisnik(int id, KorisnikDTO korisnik);
        bool ChangePoslovnica(int id, KorisnikDTO korisnik);
        bool UpdateKorisnik(int id, KorisnikDTO korisnik);
        IEnumerable<AutorizacijaKorisnikaDTO> GetAllRola();
    }
}
