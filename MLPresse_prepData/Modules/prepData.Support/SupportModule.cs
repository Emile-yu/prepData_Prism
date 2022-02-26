using prepData.Core;
using prepData.Support.Service;
using prepData.Support.ViewModels;
using prepData.Support.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace prepData.Support
{
    public class SupportModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            //regionManager.RegisterViewWithRegion(RegionNames.RibbonRegion, typeof(HomeTab));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<AFileTreatmentServiceSupport, SupportTreatmentService>();
            containerRegistry.RegisterForNavigation<SupportList, SupportListViewModel>();
            containerRegistry.RegisterDialog<MessageView, MessageViewModel>();
        }
    }
}