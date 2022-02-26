using prepData.Core;
using prepData.Support.Service;
using System;
using System.IO;
using System.Windows;

namespace prepData.Support.ViewModels
{
    public class SupportListViewModel : ATreatmentPhase
    {
        private AFileTreatmentServiceSupport _supportTreatmentService;

        public SupportListViewModel(AFileTreatmentServiceSupport supportTreatmentService)
        {
            this.TreatmentHeader = "Support";
            this._supportTreatmentService = supportTreatmentService;
        }
        public override void ExecuteBrowseInputDataPathCommand()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DataFilePath = openFileDialog.FileName;
                this.OutputPathName = Path.GetFullPath(FilePathManager.getInstance().getPathName(DataType.Support, DataFilePath));
            }
        }

        public override void TreatmentLaunch()
        {
            try
            {
                //SupportTreatmentService supportFile = new SupportTreatmentService();// (this.DataFilePath, worker);
                _supportTreatmentService.Initialize(this.DataFilePath);
                DataLogs log = _supportTreatmentService.ExportFile();
                worker.ReportProgress(1, log);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
