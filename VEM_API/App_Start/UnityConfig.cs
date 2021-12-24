using System.Web.Http;
using Unity;
using Unity.WebApi;
using VEM_API.EdiRepositories;
using VEM_API.LogProvider;
using VEM_API.Repositories;



namespace VEM_API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //Repositories start
            container.RegisterType<IArtikalRepository, ArtikalRepository>();
            container.RegisterType<ILogProvider, LogProvider.LogProvider>();
            container.RegisterType<IEntityRepository, EntityRepository>();
            container.RegisterType<IPoslovnicaRepository, PoslovnicaRepository>();
            container.RegisterType<IZalihaRepository, ZalihaRepository>();
            container.RegisterType<IRafRepository, RafRepository>();
            container.RegisterType<IKomitentRepository, KomitentRepository>();
            container.RegisterType<IKorisnikRepository, KorisnikRepository>();
            container.RegisterType<IPrimalacRepository, PrimalacRepository>();
            container.RegisterType<IAdresarRepository, AdresarRepository>();
            container.RegisterType<IMernaJedinicaRepository, MernaJedinicaRepository>();
            container.RegisterType<IVoziloRepository, VoziloRepository>();
            container.RegisterType<IVozacRepository, VozacRepository>();
            container.RegisterType<IAnalitikaRepository, AnalitikaRepository>();
            //Repositories end

            //EdiRepositories start
            container.RegisterType<IDokumentRepository, DokumentRepository>();
            container.RegisterType<IIsporukaRepository, IsporukaRepository>();
            //EdiRepositories end

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}