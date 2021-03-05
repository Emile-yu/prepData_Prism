using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace prepData.Support.ViewModels
{
    public class HomeTabViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;

        private DelegateCommand<string> _messageCommand;
        public DelegateCommand<string> MessageCommand =>
            _messageCommand ?? (_messageCommand = new DelegateCommand<string>(ExecuteMessageCommand));

        void ExecuteMessageCommand(string parameter)
        {
            _dialogService.Show("MessageView", null, null);
        }
        public HomeTabViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }
    }
}
