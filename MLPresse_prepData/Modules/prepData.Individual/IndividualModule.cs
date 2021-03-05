using prepData.Core;
using prepData.Individual.ViewModels;
using prepData.Individual.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace prepData.Individual
{
    public class IndividualModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndividualList, IndividualListViewModel>();
        }
    }
}