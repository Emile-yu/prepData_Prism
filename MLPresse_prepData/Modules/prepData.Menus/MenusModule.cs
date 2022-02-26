using prepData.Core;
using prepData.Menus.ViewModels;
using prepData.Menus.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace prepData.Menus
{
    public class MenusModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.OutlookGroupRegion,typeof(MenuList));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

    }
}