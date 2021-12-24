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
        IEnumerable<LKorisnik> GetAllKorisnik();
        IEnumerable<LKorisnik> GetKorisnikByParametar(string parametar);
        bool CreateNewKorisnik(LKorisnik korisnik);
        bool UpdatePasswordKorisnik(int id, LKorisnik korisnik);
        bool ChangePoslovnica(int id, LKorisnik korisnik);
        bool UpdateKorisnik(int id, LKorisnik korisnik);
        IEnumerable<Autorizacija_Korisnika> GetAllRola();


    }
}
