using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public interface IApplicationCommands
    {
        CompositeCommand NavigateCommand { get; }
    }
    public class ApplicationCommands : IApplicationCommands
    {
        private CompositeCommand _navigateCommand = new CompositeCommand();
        public CompositeCommand NavigateCommand
        {
            get { return _navigateCommand; }
        }

    }
}
