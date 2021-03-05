using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace prepData_Business
{
    public class NavigationItem
    {
        public string Caption { get; set; }

        public string NavigationPath { get; set; }

        public bool IsExpanded { get; set; }

        public bool IsSelected { get; set; }

        public ObservableCollection<NavigationItem> Items { get; set; } = new ObservableCollection<NavigationItem>();
    }
}
