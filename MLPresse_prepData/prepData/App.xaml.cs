using prepData.Core;
using prepData.Core.Region;
using prepData.Individual;
using prepData.Menus;
using prepData.Ri;
using prepData.Support;
using prepData.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
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
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<MenusModule>();
            moduleCatalog.AddModule<SupportModule>();
            moduleCatalog.AddModule<IndividualModule>();
            moduleCatalog.AddModule<RiModule>();
        }
        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
        }
    }
}
