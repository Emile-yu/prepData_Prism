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
        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private DelegateCommand<NavigationItem> _selectedItemChanged;
        private readonly IApplicationCommands _applicationCommands;

        public DelegateCommand<NavigationItem> SelectedItemChanged =>
            _selectedItemChanged ?? (_selectedItemChanged = new DelegateCommand<NavigationItem>(ExecuteSelectedItemChanged));

        void ExecuteSelectedItemChanged(NavigationItem navigationItem)
        {
            if (navigationItem != null)
            {
                this._applicationCommands.NavigateCommand.Execute(navigationItem.NavigationPath);
            }
        }

        public MenuListViewModel(IApplicationCommands applicationCommands)
        {
            GenerateMenu();
            this._applicationCommands = applicationCommands;
        }
        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();
            var root = new NavigationItem() { Caption = "ML Folder", NavigationPath = "SupportList", IsExpanded = true };

            root.Items = new ObservableCollection<NavigationItem>();
            root.Items.Add(new NavigationItem() { Caption = "Support", NavigationPath = "SupportList" });
            root.Items.Add(new NavigationItem() { Caption = "Ri", NavigationPath = "RiList" });
            root.Items.Add(new NavigationItem() { Caption = "Individu", NavigationPath = "IndividualList" });

            Items.Add(root);

        }
    }
}
