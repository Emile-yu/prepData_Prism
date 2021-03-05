using prepData.Core;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace prepData.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MLPresse prepData";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommands _applicationCommands;

        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(ExecuteCommandName));

        void ExecuteCommandName(string navigationPath)
        {
            if (string.IsNullOrEmpty(navigationPath))
                throw new ArgumentNullException();

            this._regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }

        public MainWindowViewModel(IRegionManager regionManager, IApplicationCommands applicationCommands)
        {
            this._regionManager = regionManager;
            this._applicationCommands = applicationCommands;
            this._applicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
        }
    }
}
