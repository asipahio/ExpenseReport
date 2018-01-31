using System.Web.Mvc;
using TherapeuticsMDSample.Models;
using TherapeuticsMDSample.Models.Interfaces;
using Unity;
using Unity.Mvc5;

namespace TherapeuticsMDSample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            container.RegisterType<IPathProvider, ServerPathProvider>();
            container.RegisterType<IConfigurationProvider, ConfigurationProvider>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}