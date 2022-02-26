using prepData.Core;
using prepData.Individual.Service;
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
            containerRegistry.Register<AFileTreatmentServiceIndividual, IndividuFileTreatment>();

            containerRegistry.RegisterForNavigation<IndividualList, IndividualListViewModel>();
        }
    }
}