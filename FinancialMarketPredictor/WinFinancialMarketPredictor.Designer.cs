using System;
using System.Windows.Forms;

namespace FinancialMarketPredictor
{
    partial class WinFinancialMarketPredictor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._labPathToLotos = new System.Windows.Forms.Label();
            this._tbPathToOrlen = new System.Windows.Forms.TextBox();
            this._labPathToPR = new System.Windows.Forms.Label();
            this._tbPathToPR = new System.Windows.Forms.TextBox();
            this._tbMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._gbPredict = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this._dtpPredictTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this._dtpPredictFrom = new System.Windows.Forms.DateTimePicker();
            this._btnSaveResults = new System.Windows.Forms.Button();
            this._btnPredict = new System.Windows.Forms.Button();
            this._dgvPredictionResults = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualLotos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PredictedLotos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualOrlen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PredictedOrlen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualPIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PredictedPIR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._gbTrain = new System.Windows.Forms.GroupBox();
            this._dtpTrainUntil = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._dtpTrainFrom = new System.Windows.Forms.DateTimePicker();
            this._btnExport = new System.Windows.Forms.Button();
            this._btnStop = new System.Windows.Forms.Button();
            this._btnStartTraining = new System.Windows.Forms.Button();
            this._dgvTrainingResults = new System.Windows.Forms.DataGridView();
            this.Epoch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrainingAlgorithm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._btnLoad = new System.Windows.Forms.Button();
            this._tbPathToLotos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._nudHiddenUnits = new System.Windows.Forms.NumericUpDown();
            this._nudHiddenLayers = new System.Windows.Forms.NumericUpDown();
            this._labHIddenUnits = new System.Windows.Forms.Label();
            this._labHiddenLayers = new System.Windows.Forms.Label();
            this._tbMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this._gbPredict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).BeginInit();
            this.tabPage2.SuspendLayout();
            this._gbTrain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // _labPathToLotos
            // 
            this._labPathToLotos.AutoSize = true;
            this._labPathToLotos.Location = new System.Drawing.Point(23, 24);
            this._labPathToLotos.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labPathToLotos.Name = "_labPathToLotos";
            this._labPathToLotos.Size = new System.Drawing.Size(397, 25);
            this._labPathToLotos.TabIndex = 0;
            this._labPathToLotos.Text = "Path to Lotos indexes (double click to select)";
            // 
            // _tbPathToOrlen
            // 
            this._tbPathToOrlen.Location = new System.Drawing.Point(23, 56);
            this._tbPathToOrlen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._tbPathToOrlen.Name = "_tbPathToOrlen";
            this._tbPathToOrlen.Size = new System.Drawing.Size(455, 29);
            this._tbPathToOrlen.TabIndex = 1;
            this._tbPathToOrlen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathTbOrlenMouseDoubleClick);
            // 
            // _labPathToPR
            // 
            this._labPathToPR.AutoSize = true;
            this._labPathToPR.Location = new System.Drawing.Point(568, 24);
            this._labPathToPR.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labPathToPR.Name = "_labPathToPR";
            this._labPathToPR.Size = new System.Drawing.Size(381, 25);
            this._labPathToPR.TabIndex = 3;
            this._labPathToPR.Text = "Path to Prime Rates (double click to select)";
            // 
            // _tbPathToPR
            // 
            this._tbPathToPR.Location = new System.Drawing.Point(571, 56);
            this._tbPathToPR.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._tbPathToPR.Name = "_tbPathToPR";
            this._tbPathToPR.Size = new System.Drawing.Size(455, 29);
            this._tbPathToPR.TabIndex = 4;
            this._tbPathToPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToPrMouseDoubleClick);
            // 
            // _tbMain
            // 
            this._tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbMain.Controls.Add(this.tabPage1);
            this._tbMain.Controls.Add(this.tabPage2);
            this._tbMain.Location = new System.Drawing.Point(23, 176);
            this._tbMain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._tbMain.Name = "_tbMain";
            this._tbMain.SelectedIndex = 0;
            this._tbMain.Size = new System.Drawing.Size(1420, 567);
            this._tbMain.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this._gbPredict);
            this.tabPage1.Controls.Add(this._btnSaveResults);
            this.tabPage1.Controls.Add(this._btnPredict);
            this.tabPage1.Controls.Add(this._dgvPredictionResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Size = new System.Drawing.Size(1412, 530);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Predict";
            // 
            // _gbPredict
            // 
            this._gbPredict.Controls.Add(this.label6);
            this._gbPredict.Controls.Add(this._dtpPredictTo);
            this._gbPredict.Controls.Add(this._btnLoad);
            this._gbPredict.Controls.Add(this.label5);
            this._gbPredict.Controls.Add(this._dtpPredictFrom);
            this._gbPredict.Location = new System.Drawing.Point(11, 10);
            this._gbPredict.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._gbPredict.Name = "_gbPredict";
            this._gbPredict.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._gbPredict.Size = new System.Drawing.Size(1385, 111);
            this._gbPredict.TabIndex = 13;
            this._gbPredict.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(527, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Predict To";
            // 
            // _dtpPredictTo
            // 
            this._dtpPredictTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictTo.Location = new System.Drawing.Point(532, 58);
            this._dtpPredictTo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dtpPredictTo.Name = "_dtpPredictTo";
            this._dtpPredictTo.Size = new System.Drawing.Size(455, 29);
            this._dtpPredictTo.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Predict From";
            // 
            // _dtpPredictFrom
            // 
            this._dtpPredictFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictFrom.Location = new System.Drawing.Point(11, 58);
            this._dtpPredictFrom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dtpPredictFrom.Name = "_dtpPredictFrom";
            this._dtpPredictFrom.Size = new System.Drawing.Size(455, 29);
            this._dtpPredictFrom.TabIndex = 14;
            // 
            // _btnSaveResults
            // 
            this._btnSaveResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSaveResults.Location = new System.Drawing.Point(1045, 465);
            this._btnSaveResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnSaveResults.Name = "_btnSaveResults";
            this._btnSaveResults.Size = new System.Drawing.Size(204, 42);
            this._btnSaveResults.TabIndex = 4;
            this._btnSaveResults.Text = "Export Results";
            this._btnSaveResults.UseVisualStyleBackColor = true;
            this._btnSaveResults.Click += new System.EventHandler(this.BtnSaveResultsClick);
            // 
            // _btnPredict
            // 
            this._btnPredict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPredict.Location = new System.Drawing.Point(1260, 465);
            this._btnPredict.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnPredict.Name = "_btnPredict";
            this._btnPredict.Size = new System.Drawing.Size(138, 42);
            this._btnPredict.TabIndex = 2;
            this._btnPredict.Text = "Predict";
            this._btnPredict.UseVisualStyleBackColor = true;
            this._btnPredict.Click += new System.EventHandler(this.BtnPredictClick);
            // 
            // _dgvPredictionResults
            // 
            this._dgvPredictionResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvPredictionResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvPredictionResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.ActualLotos,
            this.PredictedLotos,
            this.ActualOrlen,
            this.PredictedOrlen,
            this.ActualPIR,
            this.PredictedPIR,
            this.ErrorDifference});
            this._dgvPredictionResults.Location = new System.Drawing.Point(11, 134);
            this._dgvPredictionResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dgvPredictionResults.Name = "_dgvPredictionResults";
            this._dgvPredictionResults.Size = new System.Drawing.Size(1385, 321);
            this._dgvPredictionResults.TabIndex = 1;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // ActualLotos
            // 
            this.ActualLotos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualLotos.HeaderText = "Actual Lotos";
            this.ActualLotos.Name = "ActualLotos";
            // 
            // PredictedLotos
            // 
            this.PredictedLotos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PredictedLotos.HeaderText = "Predicted Lotos";
            this.PredictedLotos.Name = "PredictedLotos";
            // 
            // ActualOrlen
            // 
            this.ActualOrlen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualOrlen.HeaderText = "Actual Orlen";
            this.ActualOrlen.Name = "ActualOrlen";
            // 
            // PredictedOrlen
            // 
            this.PredictedOrlen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PredictedOrlen.HeaderText = "Predicted Orlen";
            this.PredictedOrlen.Name = "PredictedOrlen";
            // 
            // ActualPIR
            // 
            this.ActualPIR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualPIR.HeaderText = "Actual PIR";
            this.ActualPIR.Name = "ActualPIR";
            // 
            // PredictedPIR
            // 
            this.PredictedPIR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PredictedPIR.HeaderText = "PredictedPIR";
            this.PredictedPIR.Name = "PredictedPIR";
            // 
            // ErrorDifference
            // 
            this.ErrorDifference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorDifference.HeaderText = "RMS Error";
            this.ErrorDifference.Name = "ErrorDifference";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this._gbTrain);
            this.tabPage2.Controls.Add(this._btnExport);
            this.tabPage2.Controls.Add(this._btnStop);
            this.tabPage2.Controls.Add(this._btnStartTraining);
            this.tabPage2.Controls.Add(this._dgvTrainingResults);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Size = new System.Drawing.Size(1412, 530);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Train";
            // 
            // _gbTrain
            // 
            this._gbTrain.Controls.Add(this._dtpTrainUntil);
            this._gbTrain.Controls.Add(this.label4);
            this._gbTrain.Controls.Add(this.label3);
            this._gbTrain.Controls.Add(this._dtpTrainFrom);
            this._gbTrain.Location = new System.Drawing.Point(16, 10);
            this._gbTrain.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._gbTrain.Name = "_gbTrain";
            this._gbTrain.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._gbTrain.Size = new System.Drawing.Size(1375, 111);
            this._gbTrain.TabIndex = 12;
            this._gbTrain.TabStop = false;
            // 
            // _dtpTrainUntil
            // 
            this._dtpTrainUntil.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpTrainUntil.Location = new System.Drawing.Point(522, 58);
            this._dtpTrainUntil.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dtpTrainUntil.Name = "_dtpTrainUntil";
            this._dtpTrainUntil.Size = new System.Drawing.Size(455, 29);
            this._dtpTrainUntil.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(517, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Train Until";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Train From";
            // 
            // _dtpTrainFrom
            // 
            this._dtpTrainFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpTrainFrom.Location = new System.Drawing.Point(11, 58);
            this._dtpTrainFrom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dtpTrainFrom.Name = "_dtpTrainFrom";
            this._dtpTrainFrom.Size = new System.Drawing.Size(455, 29);
            this._dtpTrainFrom.TabIndex = 11;
            // 
            // _btnExport
            // 
            this._btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExport.Location = new System.Drawing.Point(1258, 465);
            this._btnExport.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(138, 42);
            this._btnExport.TabIndex = 3;
            this._btnExport.Text = "Save";
            this._btnExport.UseVisualStyleBackColor = true;
            this._btnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // _btnStop
            // 
            this._btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStop.Location = new System.Drawing.Point(1110, 465);
            this._btnStop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnStop.Name = "_btnStop";
            this._btnStop.Size = new System.Drawing.Size(138, 42);
            this._btnStop.TabIndex = 2;
            this._btnStop.Text = "Stop";
            this._btnStop.UseVisualStyleBackColor = true;
            this._btnStop.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // _btnStartTraining
            // 
            this._btnStartTraining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStartTraining.Location = new System.Drawing.Point(961, 465);
            this._btnStartTraining.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnStartTraining.Name = "_btnStartTraining";
            this._btnStartTraining.Size = new System.Drawing.Size(138, 42);
            this._btnStartTraining.TabIndex = 1;
            this._btnStartTraining.Text = "Start";
            this._btnStartTraining.UseVisualStyleBackColor = true;
            this._btnStartTraining.Click += new System.EventHandler(this.BtnStartTrainingClick);
            // 
            // _dgvTrainingResults
            // 
            this._dgvTrainingResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvTrainingResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvTrainingResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Epoch,
            this.Error,
            this.TrainingAlgorithm});
            this._dgvTrainingResults.Location = new System.Drawing.Point(16, 134);
            this._dgvTrainingResults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._dgvTrainingResults.Name = "_dgvTrainingResults";
            this._dgvTrainingResults.Size = new System.Drawing.Size(1379, 321);
            this._dgvTrainingResults.TabIndex = 0;
            // 
            // Epoch
            // 
            this.Epoch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Epoch.HeaderText = "Epoch";
            this.Epoch.Name = "Epoch";
            // 
            // Error
            // 
            this.Error.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            // 
            // TrainingAlgorithm
            // 
            this.TrainingAlgorithm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TrainingAlgorithm.HeaderText = "Training Algorithm";
            this.TrainingAlgorithm.Name = "TrainingAlgorithm";
            // 
            // _btnLoad
            // 
            this._btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnLoad.Location = new System.Drawing.Point(1075, 45);
            this._btnLoad.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(268, 42);
            this._btnLoad.TabIndex = 3;
            this._btnLoad.Text = "Load Neural Network";
            this._btnLoad.UseVisualStyleBackColor = true;
            this._btnLoad.Click += new System.EventHandler(this.BtnLoadClick);
            // 
            // _tbPathToLotos
            // 
            this._tbPathToLotos.Location = new System.Drawing.Point(23, 126);
            this._tbPathToLotos.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._tbPathToLotos.Name = "_tbPathToLotos";
            this._tbPathToLotos.Size = new System.Drawing.Size(455, 29);
            this._tbPathToLotos.TabIndex = 8;
            this._tbPathToLotos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToLotosMouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(397, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Path to Orlen indexes (double click to select)";
            // 
            // _nudHiddenUnits
            // 
            this._nudHiddenUnits.Location = new System.Drawing.Point(1140, 57);
            this._nudHiddenUnits.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._nudHiddenUnits.Name = "_nudHiddenUnits";
            this._nudHiddenUnits.Size = new System.Drawing.Size(297, 29);
            this._nudHiddenUnits.TabIndex = 11;
            this._nudHiddenUnits.ValueChanged += new System.EventHandler(this.NudHiddenUnitsValueChanged);
            // 
            // _nudHiddenLayers
            // 
            this._nudHiddenLayers.Location = new System.Drawing.Point(1140, 128);
            this._nudHiddenLayers.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._nudHiddenLayers.Name = "_nudHiddenLayers";
            this._nudHiddenLayers.Size = new System.Drawing.Size(297, 29);
            this._nudHiddenLayers.TabIndex = 12;
            this._nudHiddenLayers.ValueChanged += new System.EventHandler(this.NudHiddenLayersValueChanged);
            // 
            // _labHIddenUnits
            // 
            this._labHIddenUnits.AutoSize = true;
            this._labHIddenUnits.Location = new System.Drawing.Point(1134, 24);
            this._labHIddenUnits.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labHIddenUnits.Name = "_labHIddenUnits";
            this._labHIddenUnits.Size = new System.Drawing.Size(123, 25);
            this._labHIddenUnits.TabIndex = 13;
            this._labHIddenUnits.Text = "Hidden Units";
            // 
            // _labHiddenLayers
            // 
            this._labHiddenLayers.AutoSize = true;
            this._labHiddenLayers.Location = new System.Drawing.Point(1134, 99);
            this._labHiddenLayers.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this._labHiddenLayers.Name = "_labHiddenLayers";
            this._labHiddenLayers.Size = new System.Drawing.Size(138, 25);
            this._labHiddenLayers.TabIndex = 14;
            this._labHiddenLayers.Text = "Hidden Layers";
            // 
            // WinFinancialMarketPredictor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 765);
            this.Controls.Add(this._labHiddenLayers);
            this.Controls.Add(this._labHIddenUnits);
            this.Controls.Add(this._nudHiddenLayers);
            this.Controls.Add(this._nudHiddenUnits);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._tbPathToLotos);
            this.Controls.Add(this._tbMain);
            this.Controls.Add(this._tbPathToPR);
            this.Controls.Add(this._labPathToPR);
            this.Controls.Add(this._tbPathToOrlen);
            this.Controls.Add(this._labPathToLotos);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "WinFinancialMarketPredictor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFinancialMarketPredictorFormClosing);
            this.Load += new System.EventHandler(this.WinFinancialMarketPredictorLoad);
            this._tbMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this._gbPredict.ResumeLayout(false);
            this._gbPredict.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this._gbTrain.ResumeLayout(false);
            this._gbTrain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labPathToLotos;
        private System.Windows.Forms.TextBox _tbPathToOrlen;
        private System.Windows.Forms.Label _labPathToPR;
        private System.Windows.Forms.TextBox _tbPathToPR;
        private System.Windows.Forms.TabControl _tbMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnStop;
        private System.Windows.Forms.Button _btnStartTraining;
        private System.Windows.Forms.DataGridView _dgvTrainingResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn Epoch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrainingAlgorithm;
        private System.Windows.Forms.DataGridView _dgvPredictionResults;
        private System.Windows.Forms.TextBox _tbPathToLotos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker _dtpTrainFrom;
        private System.Windows.Forms.GroupBox _gbTrain;
        private System.Windows.Forms.DateTimePicker _dtpTrainUntil;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox _gbPredict;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker _dtpPredictFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker _dtpPredictTo;
        private System.Windows.Forms.Button _btnPredict;
        private System.Windows.Forms.Button _btnLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualLotos;
        private System.Windows.Forms.DataGridViewTextBoxColumn PredictedLotos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualOrlen;
        private System.Windows.Forms.DataGridViewTextBoxColumn PredictedOrlen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActualPIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PredictedPIR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorDifference;
        private System.Windows.Forms.Button _btnSaveResults;
        private System.Windows.Forms.NumericUpDown _nudHiddenUnits;
        private System.Windows.Forms.NumericUpDown _nudHiddenLayers;
        private System.Windows.Forms.Label _labHIddenUnits;
        private System.Windows.Forms.Label _labHiddenLayers;

        private readonly Action<int, double, TrainingAlgorithm, DataGridView> addAction = new Action<int, double, TrainingAlgorithm, DataGridView>((epoch, error, algorithm, dgvTrainingResults) =>
                                                                                                                                                   {
                                                                                                                                                       int rowIndex = dgvTrainingResults.Rows.Add(epoch, error, algorithm.ToString());
                                                                                                                                                       dgvTrainingResults.FirstDisplayedScrollingRowIndex = rowIndex;
                                                                                                                                             });
    }
}

