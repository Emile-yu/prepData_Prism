using prepData.Core;
using prepData.Core.Region;
using prepData.Individual;
using prepData.Menus;
using prepData.Ri;
using prepData.Support;
using prepData.Views;
using prepData_Business;
using prepData_Business.Configuration;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Configuration;
using System.Windows;
using Telerik.Windows.Controls;

namespace prepData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            
            //StyleManager.ApplicationTheme = new Expression_DarkTheme();
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IModulesCollections, ModulesCollections>();
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            var modules = this.Container.Resolve<IModulesCollections>();
            ManagerConfiguration section = (ManagerConfiguration)ConfigurationManager.GetSection("managerConfiguration");
            if (section != null)
            {
                if (section.Modules.GetModuleByName(Helper.SupportModule) != null)
                {
                    moduleCatalog.AddModule<SupportModule>();
                    modules.Modules.Add(new NavigationItem() { Caption = Helper.GetCaptionName(Helper.SupportModule), NavigationPath = Helper.GetNavigationPath(Helper.SupportModule) });
                }
                if (section.Modules.GetModuleByName(Helper.RiModule) != null)
                {
                    moduleCatalog.AddModule<RiModule>();
                    modules.Modules.Add(new NavigationItem() { Caption = Helper.GetCaptionName(Helper.RiModule), NavigationPath = Helper.GetNavigationPath(Helper.RiModule) });
                }
                if (section.Modules.GetModuleByName(Helper.IndividualModule) != null)
                {
                    moduleCatalog.AddModule<IndividualModule>();
                    modules.Modules.Add(new NavigationItem() { Caption = Helper.GetCaptionName(Helper.IndividualModule), NavigationPath = Helper.GetNavigationPath(Helper.IndividualModule) });
                }
            }
            moduleCatalog.AddModule<MenusModule>();
            
        }
        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
