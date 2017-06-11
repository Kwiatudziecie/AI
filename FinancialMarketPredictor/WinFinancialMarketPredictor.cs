// ciumac.sergiu@gmail.com
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using FinancialMarketPredictor.Entities;
using FinancialMarketPredictor.Properties;
using FinancialMarketPredictor.Utilities;
using System.Configuration;
using System.Security.Permissions;

namespace FinancialMarketPredictor
{
    public partial class WinFinancialMarketPredictor : Form
    {
        #region Private member fields

        private string _pathToOrlen = "Orlen.csv";

        private string _pathToPrimeRates = "Prime.csv";

        private string _pathToLotos = "Lotos.csv";

        private PredictIndicators _predictor;

        private readonly DateTime _predictFrom = CSVReader.ParseDate("2017-01-01");

        private readonly DateTime _predictTo = CSVReader.ParseDate("2017-05-01");

        private readonly DateTime _learnFrom = CSVReader.ParseDate("2005-06-09");

        private readonly DateTime _learnTo = CSVReader.ParseDate("2017-01-01");

        private int _hiddenLayers = 2;

        private int _hiddenUnits = 41;

        private bool _reloadFiles = false;

        #endregion

        /// <summary>
        /// Public parameter less constructor
        /// </summary>
        public WinFinancialMarketPredictor()
        {
            DateTime minDate;
            DateTime maxDate;
            InitializeComponent();
            _btnStop.Enabled = false;
            _btnExport.Enabled = false;
            try
            {
                maxDate = CSVReader.ParseDate(ConfigurationManager.AppSettings["MaxDate"]);
                minDate = CSVReader.ParseDate(ConfigurationManager.AppSettings["MinDate"]);
            }
            catch
            {
                maxDate = DateTime.Now;
                minDate = CSVReader.ParseDate("1971-02-05");   
            }
            _dtpTrainFrom.Value = _learnFrom;
            _dtpTrainUntil.Value = _learnTo;
            _dtpPredictFrom.Value = _predictFrom;
            _dtpPredictTo.Value = _predictTo;

            _dtpTrainFrom.MaxDate = _dtpTrainUntil.MaxDate = _dtpPredictFrom.MaxDate = _dtpPredictTo.MaxDate = maxDate;
            _dtpTrainFrom.MinDate = _dtpTrainUntil.MinDate = _dtpPredictFrom.MinDate = _dtpPredictTo.MinDate = minDate;

            _nudHiddenLayers.Value = _hiddenLayers;
            _nudHiddenUnits.Value = _hiddenUnits;

        }

        private void WinFinancialMarketPredictorLoad(object sender, EventArgs e)
        {
            SetPathsInTextBoxes();
        }

        private void SetPathsInTextBoxes()
        {
            if (File.Exists(Path.GetFullPath(_pathToOrlen)))
                _tbPathToOrlen.Text = Path.GetFileName(_pathToOrlen);
            if (File.Exists(Path.GetFullPath(_pathToPrimeRates)))
                _tbPathToPR.Text = Path.GetFileName(_pathToPrimeRates);
            if (File.Exists(Path.GetFullPath(_pathToLotos)))
                _tbPathToLotos.Text = Path.GetFileName(_pathToLotos);
        }
        
        private void TrainingCallback(int epoch, double error, TrainingAlgorithm algorithm)
        {
            Invoke(addAction, new object [] {epoch, error, algorithm, _dgvTrainingResults});

        }
        
        private void BtnStartTrainingClick(object sender, EventArgs e)
        {
            if (_dgvTrainingResults.Rows.Count != 0)
                _dgvTrainingResults.Rows.Clear();

            if (_predictor == null)
            {
                _reloadFiles = false;
                if (!File.Exists(_tbPathToLotos.Text) || !File.Exists(_tbPathToPR.Text) || !File.Exists(_tbPathToOrlen.Text))
                {
                    MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            DateTime trainFrom = _dtpTrainFrom.Value;
            DateTime trainTo = _dtpTrainUntil.Value;

            if (trainFrom > trainTo)
            {
                MessageBox.Show(Resources.TrainFromTrainTo, Resources.BadParameters, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dtpTrainFrom.Focus();
                return;
            }
            FadeControls(true);
            if (_predictor == null)
            {
                _pathToOrlen = _tbPathToOrlen.Text;
                _pathToLotos = _tbPathToLotos.Text;
                _pathToPrimeRates = _tbPathToPR.Text;
                 Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                    _predictor = new PredictIndicators(_pathToOrlen, _pathToPrimeRates, _pathToLotos, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            else if (_reloadFiles)
            {
                _pathToOrlen = _tbPathToOrlen.Text;
                _pathToLotos = _tbPathToLotos.Text;
                _pathToPrimeRates = _tbPathToPR.Text;
                _predictor.ReloadFiles(_pathToOrlen, _pathToPrimeRates, _pathToLotos);
                _dtpTrainFrom.MinDate = _predictor.MinIndexDate;
                _dtpTrainUntil.MaxDate = _predictor.MaxIndexDate;
            }
           
            if (trainFrom < _predictor.MinIndexDate)
                _dtpTrainFrom.MinDate = _dtpTrainFrom.Value = trainFrom = _predictor.MinIndexDate;
            if (trainTo > _predictor.MaxIndexDate)
                _dtpTrainUntil.MaxDate = _dtpTrainUntil.Value = trainTo = _predictor.MaxIndexDate;
            TrainingStatus callback = TrainingCallback;
            _predictor.TrainNetworkAsync(trainFrom, trainTo, callback);
        }

        private void BtnPredictClick(object sender, EventArgs e)
        {
            if (_dgvPredictionResults.Rows.Count != 0)
                _dgvPredictionResults.Rows.Clear();

            if (_predictor == null)
            {
                _reloadFiles = false;
                if (!File.Exists(_tbPathToLotos.Text) || !File.Exists(_tbPathToPR.Text) || !File.Exists(_tbPathToOrlen.Text))
                {
                    MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                switch (MessageBox.Show(Resources.UntrainedPredictorWarning, Resources.NoNetwork, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        this.Cursor = Cursors.WaitCursor;
                        _hiddenLayers = (int)_nudHiddenLayers.Value;
                        _hiddenUnits = (int)_nudHiddenUnits.Value;
                        try
                        {
                            _predictor = new PredictIndicators(_pathToOrlen, _pathToPrimeRates, _pathToLotos, _hiddenUnits, _hiddenLayers);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _predictor = null;
                            return;
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                        using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
                        {
                            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    _predictor.LoadNeuralNetwork(Path.GetFullPath(ofd.FileName));
                                }
                                catch
                                {
                                    MessageBox.Show(Resources.ExceptionMessage, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            DateTime predictFrom = _dtpPredictFrom.Value;
            DateTime predictTo = _dtpPredictTo.Value;
            if (predictFrom > predictTo)
            {
                MessageBox.Show(Resources.PredictFromToWarning, Resources.BadParameters, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dtpPredictFrom.Focus();
                return;
            }

            if (_predictor == null)
            {
                _pathToOrlen = _tbPathToOrlen.Text;
                _pathToLotos = _tbPathToLotos.Text;
                _pathToPrimeRates = _tbPathToPR.Text;
                 Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                    _predictor = new PredictIndicators(_pathToOrlen, _pathToPrimeRates, _pathToLotos, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            List<PredictionResults> results = null;
            try
            {
                if (_reloadFiles)
                {
                    _pathToOrlen = _tbPathToOrlen.Text;
                    _pathToLotos = _tbPathToLotos.Text;
                    _pathToPrimeRates = _tbPathToPR.Text;
                    _predictor.ReloadFiles(_pathToOrlen, _pathToPrimeRates, _pathToLotos);
                }
                results = _predictor.Predict(predictFrom, predictTo);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in results)
            {
                _dgvPredictionResults.Rows.Add(item.Date.ToShortDateString(), item.ActualLotos,
                                               item.PredictedLotos.ToString("F2", CultureInfo.InvariantCulture), item.ActualOrlen, item.PredictedOrlen.ToString("F2", CultureInfo.InvariantCulture),
                                               item.ActualPir, item.PredictedPir.ToString("F2", CultureInfo.InvariantCulture), item.Error.ToString("F4", CultureInfo.InvariantCulture));
            }
        }

        private void WinFinancialMarketPredictorFormClosing(object sender, FormClosingEventArgs e)
        {
            _predictor?.AbortTraining();
        }

        private void BtnStopClick(object sender, EventArgs e)
        {
            FadeControls(false);
            _predictor.AbortTraining();
            _btnExport.Enabled = true;
        }

        private void TbPathToPrMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "pir.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tbPathToPR.Text = Path.GetFullPath(ofd.FileName);
                _reloadFiles = true;
            }
        }
       
        private void FadeControls(bool fade)
        {
            Action<bool> action = (param) =>
                                  {
                                      _tbPathToOrlen.Enabled = param;
                                      _tbPathToPR.Enabled = param;
                                      _tbPathToLotos.Enabled = param;
                                      _btnStartTraining.Enabled = param;
                                      _btnStop.Enabled = !param;
                                      _dtpPredictFrom.Enabled = param;
                                      _dtpPredictTo.Enabled = param;
                                      _dtpTrainFrom.Enabled = param;
                                      _dtpTrainUntil.Enabled = param;
                                      _nudHiddenLayers.Enabled = param;
                                      _nudHiddenUnits.Enabled = param;
                                  };
            Invoke(action, !fade);
        }
        
        private void BtnExportClick(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileIOPermission perm = new FileIOPermission(FileIOPermissionAccess.Write, Path.GetFullPath(sfd.FileName));
                    try
                    {
                        perm.Demand();
                    }
                    catch (System.Security.SecurityException)
                    {
                        MessageBox.Show(Resources.SecurityExceptionMessage, Resources.SecurityException, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _predictor.ExportNeuralNetwork(Path.GetFullPath(sfd.FileName));
                }
            }
        }
        
        private void BtnLoadClick(object sender, EventArgs e)
        {
            if (!File.Exists(_tbPathToLotos.Text) || !File.Exists(_tbPathToPR.Text) || !File.Exists(_tbPathToOrlen.Text))
            {
                MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_predictor == null || _predictor.Loaded == false)
            {
               this.Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                    _predictor = new PredictIndicators(_pathToOrlen, _pathToPrimeRates, _pathToLotos, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _predictor.LoadNeuralNetwork(Path.GetFullPath(ofd.FileName));
                        _nudHiddenLayers.Value = _predictor.HiddenLayers;
                        _nudHiddenUnits.Value = _predictor.HiddenUnits;
                    }
                    catch (System.Security.SecurityException)
                    {
                        MessageBox.Show(Resources.SecurityExceptionFolderLevel, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show(Resources.ExceptionMessage, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        
        private void BtnSaveResultsClick(object sender, EventArgs e)
        {
            var dgvResults = _dgvPredictionResults;
            SaveFileDialog ofd = new SaveFileDialog {Filter = Resources.CsvFilter, FileName = "results.csv"};
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CSVWriter writer = null;
                try
                {
                    writer = new CSVWriter(ofd.FileName);
                }
                catch (System.Security.SecurityException)
                {
                    MessageBox.Show( Resources.SecurityExceptionFolderLevel, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                object[,] values = new object[dgvResults.Rows.Count + 2,dgvResults.Columns.Count];
                int rowIndex = 0;
                int colIndex = 0;
                foreach (DataGridViewColumn col in dgvResults.Columns)
                {
                    values[rowIndex, colIndex] = col.HeaderText;
                    colIndex++;
                }
                rowIndex++;

                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    colIndex = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        values[rowIndex, colIndex] = cell.Value;
                        colIndex++;
                    }
                    rowIndex++;
                }
                
                writer.Write(values);
            }
        }
        
        private void NudHiddenUnitsValueChanged(object sender, EventArgs e)
        {
            if (_predictor == null) return;
            if(MessageBox.Show(Resources.ChangedNetwork, Resources.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _predictor = null;
            }
        }
        
        private void NudHiddenLayersValueChanged(object sender, EventArgs e)
        {
            if (_predictor == null) return;
            if (MessageBox.Show(Resources.ChangedNetwork, Resources.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                _predictor = null;
            }
        }

        private void TbPathTbOrlenMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "Orlen.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            _tbPathToLotos.Text = Path.GetFullPath(ofd.FileName);
            _reloadFiles = true;
        }

        private void TbPathToLotosMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "Lotos.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            _tbPathToLotos.Text = Path.GetFullPath(ofd.FileName);
            _reloadFiles = true;
        }
    }
}
