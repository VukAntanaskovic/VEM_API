using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEM_API.DbModel;
using VEM_API.Models;

namespace VEM_API.EdiRepositories
{
    public interface IIsporukaRepository
    {

        #region "Isporuka"
        /// <summary>
        /// Kreira novu isporuku
        /// </summary>
        /// <param name="primalac"></param>
        /// <param name="datum"></param>
        /// <param name="veza"></param>
        void CreateNewIsporuka(int? primalac, DateTime? datum, int? veza);

        /// <summary>
        /// Kolekcija isporuka po statusu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IEnumerable<IsporukaDTO> GetAllIsporukaByStatus(int status);

        /// <summary>
        /// Kolekcija isporuka po sifri isporuke ili datumu
        /// </summary>
        /// <param name="isporuka"></param>
        /// <param name="datum"></param>
        /// <returns></returns>
        IEnumerable<IsporukaDTO> CheckStatusIsporuke(int? isporuka, DateTime? datum);

        /// <summary>
        /// Promena statusa isporuke
        /// </summary>
        /// <param name="isporuka"></param>
        /// <param name="status"></param>
        void ChangeStatusIsporuke(int isporuka, int status);

        /// <summary>
        /// Resetovanje statusa isporuke
        /// </summary>
        /// <param name="isporuka"></param>
        /// <param name="tl"></param>
        /// <returns></returns>
        bool ResetIsporuka(int isporuka, int tl);

        /// <summary>
        /// Zahtev za promenu statusa
        /// </summary>
        /// <param name="isporuka"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool StatusRequest(int isporuka, string status);
        #endregion "Isporuka"

        #region "Tovarni list"
        /// <summary>
        /// Kreiranje novog tovarnog lista
        /// </summary>
        /// <param name="tovarni_List"></param>
        /// <returns></returns>
        bool CreateNewTL(Tovarni_List tovarni_List);

        /// <summary>
        /// Kolekcija tovarnih listova
        /// </summary>
        /// <returns></returns>
        IEnumerable<TovarniListDTO> GetAllTovarniList();

        /// <summary>
        /// Dodavanje isporuke na TL
        /// </summary>
        /// <param name="isporuka"></param>
        /// <param name="tl"></param>
        /// <returns></returns>
        bool AddIsporukaToTL(int isporuka, int tl);
        #endregion "Tovarni list"

        #region "Stavke tovarnog lista"
        /// <summary>
        /// Kolekcija stavki TL-a
        /// </summary>
        /// <param name="tl"></param>
        /// <returns></returns>
        IEnumerable<StavkeTovarnogListaDTO> GetAllStavkeTL(int tl);
        #endregion "Stavke tovarnog lista"
    }
}
