using prepData.Core;
using prepData.Individual.Service;
using System;
using System.IO;

namespace prepData.Individual.ViewModels
{
    public class IndividualListViewModel : ATreatmentPhase
    {
        #region properties

        private string _begin;
        public string Begin
        {
            get { return _begin; }
            set { SetProperty(ref _begin, value); }
        }

        private string _end;
        public string End
        {
            get { return _end; }
            set { SetProperty(ref _end, value); }
        }
        #endregion

        #region constructor
        public IndividualListViewModel()
        {
            this.TreatmentHeader = "Individu";
        }

        #endregion

        #region operations override
        public override void TreatmentLaunch()
        {
            IndividuFileTreatment individuFile = new IndividuFileTreatment(this.DataFilePath, worker, Int32.Parse(this.Begin), Int32.Parse(this.End));
            //individuFile.Initialize(this.DataFilePath, worker, Int32.Parse(this.Begin), Int32.Parse(this.End));
            individuFile.ExportFile();
        }

        public override void ExecuteBrowseInputDataPathCommand()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DataFilePath = openFileDialog.FileName;
                this.OutputPathName = Path.GetFullPath(FilePathManager.getInstance().getPathName(DataType.Individu, this.DataFilePath));

            }
        }

        #endregion
    }
}
