using prepData.Core;
using prepData.Ri.Service;
using Prism.Commands;
using System;
using System.IO;
using System.Windows;

namespace prepData.Ri.ViewModels
{
    public class RiListViewModel : ATreatmentPhase
    {
        private AFileTreatmentServiceRi _riTreatmentService;
        private string _descrFilePath;
        public string DescrFilePath
        {
            get { return _descrFilePath; }
            set { SetProperty(ref _descrFilePath, value); }
        }

        private DelegateCommand _browseInputDescrFilePathCommand;
        

        public DelegateCommand BrowseInputDescrFilePathCommand =>
            _browseInputDescrFilePathCommand ?? (_browseInputDescrFilePathCommand = new DelegateCommand(ExecuteBrowseInputDescrFilePathCommand));

        public RiListViewModel(AFileTreatmentServiceRi riTreatmentService)
        {
            this.TreatmentHeader = "Ri";
            this._riTreatmentService = riTreatmentService;
        }

        void ExecuteBrowseInputDescrFilePathCommand()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DescrFilePath = openFileDialog.FileName;
                this.OutputPathName = Path.GetFullPath(FilePathManager.getInstance().getPathName(DataType.Ri, this.DescrFilePath));
            }
        }
        public override void ExecuteBrowseInputDataPathCommand()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DataFilePath = openFileDialog.FileName;

            }
        }

        public override void TreatmentLaunch()
        {
            try
            {
                _riTreatmentService.Initialize(this.DescrFilePath, this.DataFilePath);
                DataLogs log = _riTreatmentService.ExportFile();
                worker.ReportProgress(1, log);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
