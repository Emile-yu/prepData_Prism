using prepData.Core;
using prepData.Support.Service;
using System.IO;

namespace prepData.Support.ViewModels
{
    public class SupportListViewModel : ATreatmentPhase
    {
        public SupportListViewModel()
        {
            this.TreatmentHeader = "Support";
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
            SupportTreatmentService supportFile = new SupportTreatmentService(this.DataFilePath, worker);
            //supportFile.Initialize(this.DataFilePath, worker);
            supportFile.ExportFile();
        }
    }
}
