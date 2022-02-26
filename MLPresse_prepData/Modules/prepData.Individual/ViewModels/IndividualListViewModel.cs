using prepData.Core;
using prepData.Individual.Service;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace prepData.Individual.ViewModels
{
    public class IndividualListViewModel : ATreatmentPhase
    {
        #region private properties 
        private string _begin;
        private string _end;
        private AFileTreatmentServiceIndividual _individuFileTreatment;
        #endregion

        #region public properties
        public string Begin
        {
            get { return _begin; }
            set { SetProperty(ref _begin, value); }
        }

        public string End
        {
            get { return _end; }
            set { SetProperty(ref _end, value); }
        }
        #endregion

        #region constructor
        public IndividualListViewModel(AFileTreatmentServiceIndividual IndividuFileTreatment)
        {
            this.TreatmentHeader = "Individu";
            _individuFileTreatment = IndividuFileTreatment;
        }

        #endregion

        #region operations override
        public override void TreatmentLaunch()
        {
            _individuFileTreatment.Initialize(this.DataFilePath, Int32.Parse(this.Begin), Int32.Parse(this.End));

            try
            {
                _individuFileTreatment.ImportFile();
                DataLogs log = _individuFileTreatment.ExportFile();
                worker.ReportProgress(1, log);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
