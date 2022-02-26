using prepData.Core;
using prepData_Business;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Menus.ViewModels
{
    public class MenuListViewModel : BindableBase
    {
        private static string s_caption = "ML Folder";

        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private DelegateCommand<NavigationItem> _selectedItemChanged;
        private IApplicationCommands _applicationCommands;
        private IModulesCollections _modulesCollections;

        public DelegateCommand<NavigationItem> SelectedItemChanged =>
            _selectedItemChanged ?? (_selectedItemChanged = new DelegateCommand<NavigationItem>(ExecuteSelectedItemChanged));

        void ExecuteSelectedItemChanged(NavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                this._applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }

        public MenuListViewModel(IApplicationCommands applicationCommands, IModulesCollections modulesCollections)
        {
            this._applicationCommands = applicationCommands;
            this._modulesCollections = modulesCollections;
            GenerateMenu();
        }
        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();
            if (_modulesCollections.Modules != null)
            {
                var root = new NavigationItem() { Caption = s_caption, NavigationPath = _modulesCollections.Modules.First().NavigationPath, IsExpanded = true };
                root.Children = new ObservableCollection<NavigationItem>();
                foreach (var item in _modulesCollections.Modules)
                {
                    root.Children.Add(item);
                }
                Items.Add(root);
            }
            

           

            //root.Items.Add(new NavigationItem() { Caption = "Support", NavigationPath = "SupportList" });
            //root.Items.Add(new NavigationItem() { Caption = "Ri", NavigationPath = "RiList" });
            //root.Items.Add(new NavigationItem() { Caption = "Individu", NavigationPath = "IndividualList" });

        }
    }
}
