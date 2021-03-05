using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core
{
    public abstract class ATreatmentPhase : BindableBase
    {

        #region properties

        protected readonly BackgroundWorker worker = new BackgroundWorker();

        private string treatmentHeader;
        public string TreatmentHeader
        {
            get { return treatmentHeader; }
            set { SetProperty(ref treatmentHeader, value); }
        }

        private string _phasesLoggerTitle;
        public string PhasesLoggerTitle
        {
            get { return _phasesLoggerTitle; }
            set { SetProperty(ref _phasesLoggerTitle, value); }
        }

        private string _dataFilePath;
        public string DataFilePath
        {
            get { return _dataFilePath; }
            set { 
                SetProperty(ref _dataFilePath, value);
                TreatmentLaunchCommand.RaiseCanExecuteChanged();
            }
        }

        private string _outputPathName;
        public string OutputPathName
        {
            get { return _outputPathName; }
            set { SetProperty(ref _outputPathName, value); }
        }

        private DelegateCommand _browseInputDataPathCommand;
        public DelegateCommand BrowseInputDataPathCommand =>
            _browseInputDataPathCommand ?? (_browseInputDataPathCommand = new DelegateCommand(ExecuteBrowseInputDataPathCommand));

        private DelegateCommand _treatmentLaunchCommand;
        public DelegateCommand TreatmentLaunchCommand =>
            _treatmentLaunchCommand ?? (_treatmentLaunchCommand = new DelegateCommand(ExecuteTreatmentLaunchCommand, IsValidPath));

        private ObservableCollection<LogItem> _phaseLogs = new ObservableCollection<LogItem>();
        public ObservableCollection<LogItem> PhaseLogs
        {
            get { return _phaseLogs; }
            set { SetProperty(ref _phaseLogs, value); }
        }
        #endregion

        #region constructor

        public ATreatmentPhase()
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        #endregion

        #region abstract function

        public abstract void TreatmentLaunch();

        public abstract void ExecuteBrowseInputDataPathCommand();
        
        #endregion

        #region functions


        void ExecuteTreatmentLaunchCommand()
        {
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PhasesLoggerTitle = "Le traitement est terminé";
            PhaseLogs.Add(new LogItem(PhasesLoggerTitle));
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DataLogs logData = e.UserState as DataLogs;

            if (logData != null)
            {
                switch (logData.Type)
                {
                    case LogType.None:
                        this.PhaseLogs.Add(new LogItem(logData.Message));
                        break;
                    case LogType.Success:
                        if (this.PhaseLogs.Count > 0)
                            this.PhaseLogs[PhaseLogs.Count - 1] = new LogItem(logData.Message);
                        break;
                    default:
                        break;
                }

            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            PhasesLoggerTitle = "Le traitement est en cours";
            TreatmentLaunch();
        }

        bool IsValidPath()
        {
            return !String.IsNullOrEmpty(this.DataFilePath);
        }
        #endregion
    }
}
