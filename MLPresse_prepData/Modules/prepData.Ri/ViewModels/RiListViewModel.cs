using prepData.Core;
using prepData.Ri.Service;
using Prism.Commands;
using System.IO;

namespace prepData.Ri.ViewModels
{
    public class RiListViewModel : ATreatmentPhase
    {
        private string _descrFilePath;
        public string DescrFilePath
        {
            get { return _descrFilePath; }
            set { SetProperty(ref _descrFilePath, value); }
        }

        private DelegateCommand _browseInputDescrFilePathCommand;
        public DelegateCommand BrowseInputDescrFilePathCommand =>
            _browseInputDescrFilePathCommand ?? (_browseInputDescrFilePathCommand = new DelegateCommand(ExecuteBrowseInputDescrFilePathCommand));

        public RiListViewModel()
        {
            this.TreatmentHeader = "Ri";
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
            RiTreatmentService file = new RiTreatmentService (this.DescrFilePath, this.DataFilePath, worker);
            //file.Initialize(this.DescrFilePath, this.DataFilePath, worker);
            file.ExportFile();
        }
    }
}
