using prepData.Core;
using prepData.Ri.Service;
using prepData.Ri.ViewModels;
using prepData.Ri.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace prepData.Ri
{
    public class RiModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<AFileTreatmentServiceRi, RiTreatmentService>();

            containerRegistry.RegisterForNavigation<RiList, RiListViewModel>();
        }
    }
}