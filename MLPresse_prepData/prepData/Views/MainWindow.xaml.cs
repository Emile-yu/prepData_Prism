using prepData.Core;
using Prism.Regions;
using System.Windows;
using Telerik.Windows.Controls;

namespace prepData.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        private readonly IApplicationCommands _applicationCommands;

        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();
            this._applicationCommands = applicationCommands;
        }

        private void RadOutlookBar_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            //RadOutlookBarItem newSelectedItem = (sender as RadOutlookBar).SelectedItem as RadOutlookBarItem;
            var item = ((RadOutlookBar)sender).SelectedItem as IBarItem;

            if (item != null)
            {
                _applicationCommands.NavigateCommand.Execute(item.DefaultNavigationPath);
                //todo
            }
        }
    }
}
