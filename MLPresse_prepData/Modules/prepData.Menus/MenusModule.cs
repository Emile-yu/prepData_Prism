using prepData.Core;
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
            regionManager.RegisterViewWithRegion(RegionNames.LeftMenuTreeContentRegion, typeof(MenuList));

            //var moduleCatalog = containerProvider.Resolve<IModuleCatalog>();
            //foreach (var module in moduleCatalog.Modules)
            //{
            //    string name = module.ModuleName;
            //    string typeName = module.ModuleType;
            //}
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}