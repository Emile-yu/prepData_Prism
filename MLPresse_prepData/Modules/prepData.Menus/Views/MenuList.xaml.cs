using prepData.Core;
using prepData_Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace prepData.Menus.Views
{
    /// <summary>
    /// Interaction logic for MenuList.xaml
    /// </summary>
    public partial class MenuList : RadOutlookBarItem, IBarItem
    {
        public MenuList()
        {
            InitializeComponent();
            _dataTree.Loaded += _DataTree_Loaded;
        }

        private void _DataTree_Loaded(object sender, RoutedEventArgs e)
        {
            _dataTree.Loaded -= _DataTree_Loaded;
            foreach (var item in _dataTree.Items)
            {
                var val = item as NavigationItem;
                val.Children[0].IsSelected = true;
            }


        }

        public string DefaultNavigationPath => "SupportList";

    }
}
