using System.Web.Http;
using Unity;
using Unity.WebApi;
using IRepository;
using Repository;

namespace WebApi
{
    public static class UnityConfig
    {
        public static IUnityContainer container;
        public static void RegisterComponents()
        {
			container = new UnityContainer();
            container.RegisterType<ICompanyRepo, CompanyRepo>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}