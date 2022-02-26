using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace prepData.Core
{
    public abstract class ATreatmentPhase : BindableBase, INavigationAware
    {

        #region properties

        protected readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

        private string NavUri { get; set; }

        private string _treatmentHeader;
        public string TreatmentHeader
        {
            get { return _treatmentHeader; }
            set { SetProperty(ref _treatmentHeader, value); }
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

        public DelegateCommand CloseCommand => new DelegateCommand(() =>
        {
            //根据uri获取对应的已注册对象名称
            var obj = _unityContainer.Registrations.FirstOrDefault(o => o.Name == NavUri);
            string name = obj.MappedToType.Name;
            //根据对象名称从region的views里面找到对象
            if (!string.IsNullOrEmpty(name))
            {
                var region = _regionManager.Regions[RegionNames.LeftMenuTreeContentRegion];
                var view = region.Views.FirstOrDefault(v => v.GetType().Name == name);

                //把这个对象从region的views里移除
                if (view != null)
                {
                    region.Remove(view);
                }
            }
        });

        private ObservableCollection<LogItem> _phaseLogs = new ObservableCollection<LogItem>();
        public ObservableCollection<LogItem> PhaseLogs
        {
            get { return _phaseLogs; }
            set { SetProperty(ref _phaseLogs, value); }
        }
        #endregion

        #region constructor

        public ATreatmentPhase(/*IUnityContainer unityContainer, IRegionManager regionManager*/)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //this._unityContainer = unityContainer;
            //this._regionManager = regionManager;
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
                        else
                            this.PhaseLogs.Add(new LogItem(logData.Message));
                        break;
                    case LogType.Error:
                        this.PhaseLogs.Add(new LogItem(logData.Message));
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavUri = navigationContext.Uri.ToString();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
        #endregion
    }
}
