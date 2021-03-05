using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace prepData.Support.ViewModels
{
    public class MessageViewModel : BindableBase, IDialogAware
    {
        private string _dataFilePath = @"todo";
        public string DataFilePath
        {
            get { return _dataFilePath; }
            set { SetProperty(ref _dataFilePath, value); }
        }

        private string _outputPathName = @"todo";
        public string OutputPathName
        {
            get { return _outputPathName; }
            set { SetProperty(ref _outputPathName, value); }
        }
        public MessageViewModel()
        {

        }
        public string Title => "Support File Message";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
