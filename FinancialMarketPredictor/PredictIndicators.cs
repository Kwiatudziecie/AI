using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using FinancialMarketPredictor.Entities;
using FinancialMarketPredictor.Utilities;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks.Training.LMA;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Neural.Networks.Training.Anneal;
using Encog.Neural.Networks.Training.Strategy;
using Encog.Neural.Activation;
using Encog.Persist.Persistors;

namespace FinancialMarketPredictor
{

    public enum TrainingAlgorithm
    {
        Resilient,
        Annealing,
        Evolutionary
    }

    public delegate void TrainingStatus(int iteration, double error, TrainingAlgorithm algorithm);

    public sealed class PredictIndicators
    {
        #region Constants
        private const int IndexesToConsider = 3;
        private const int InputTuples = 10;
        private const int OutputSize = 3;
        private const double MaxError = 0.0001;
        #endregion

        #region Private Members
        private BasicNetwork _network;
        private double[][] _input;
        private double[][] _ideal;
        private FinancialPredictorManager _manager;
        private Thread _trainThread;
        private string _pathtosp;
        private string _pathtorates;
        private string _pathToOrlen;
        private int _trainingSize = 1000;
        #endregion
        public bool Loaded { get; private set; }

        public int HiddenLayers { get; private set; }

        public int HiddenUnits { get; private set; }

        public DateTime MaxIndexDate => _manager?.MaxDate ?? DateTime.MinValue;

        public DateTime MinIndexDate => _manager?.MinDate ?? DateTime.MaxValue;

        #region Constructors
        public PredictIndicators(string pathToLotos, string pathToPrimeRates, string pathToOrlen, int hiddenUnits, int hiddenLayers)
        {
            if (!File.Exists(pathToLotos))
                throw new ArgumentException("pathToLotos targets an invalid file");
            if (!File.Exists(pathToPrimeRates))
                throw new ArgumentException("pathToPrimeRates targets an invalid file");
            if (!File.Exists(pathToOrlen))
                throw new ArgumentException("pathToOrlen targets an invalid file");

            _pathtosp = pathToLotos;
            _pathtorates = pathToPrimeRates;
            _pathToOrlen = pathToOrlen;

            CreateNetwork(hiddenUnits, hiddenLayers);  
            _manager = new FinancialPredictorManager(InputTuples, OutputSize);     
            _manager.Load(_pathtosp, _pathtorates, _pathToOrlen); 
            Loaded = true;
            HiddenLayers = hiddenLayers;
            HiddenUnits = hiddenUnits;
        }
        #endregion

        public void ReloadFiles(string pathToSp500, string pathToPrimeRates, string pathToNasdaq)
        {
            if (!File.Exists(pathToSp500))
                throw new ArgumentException("pathToLotos targets an invalid file");
            if (!File.Exists(pathToPrimeRates))
                throw new ArgumentException("pathToPrimeRates targets an invalid file");
            if (!File.Exists(pathToNasdaq))
                throw new ArgumentException("pathToOrlen targets an invalid file");
            Loaded = false;
            _pathtosp = pathToSp500;
            _pathtorates = pathToPrimeRates;
            _pathToOrlen = pathToNasdaq;
            _manager = new FinancialPredictorManager(InputTuples, OutputSize); 
            _manager.Load(_pathtosp, _pathtorates, _pathToOrlen);    
            _ideal = _input = null;
            Loaded = true;
        }

        private void CreateNetwork(int hiddenUnits, int hiddenLayers)
        {
            _network = new BasicNetwork {Name = "Financial Predictor", Description = "Network for prediction analysis"};
            _network.AddLayer(new BasicLayer(InputTuples * IndexesToConsider));                             /*Input*/
            for (int i = 0; i < hiddenLayers; i++)
                _network.AddLayer(new BasicLayer(new ActivationTANH(), true, hiddenUnits));                 /*Hidden layer*/
            _network.AddLayer(new BasicLayer(new ActivationTANH(), true, OutputSize));                      /*Output of the network*/
            _network.Structure.FinalizeStructure();                                                         /*Finalize network structure*/
            _network.Reset();                                                                               /*Randomize*/
        }
        public void CreateTrainingSets(DateTime trainFrom, DateTime trainTo)
        {
            int startIndex = -1;
            int endIndex = -1;
            foreach (FinancialIndexes sample in _manager.Samples)
            {
                if (sample.Date.CompareTo(trainFrom) < 0)
                    startIndex++;
                if (sample.Date.CompareTo(trainTo) < 0)
                    endIndex++;
            }

            _trainingSize = endIndex - startIndex;
            _input = new double[_trainingSize][];
            _ideal = new double[_trainingSize][];
            //grab point
            for (int i = startIndex; i < endIndex; i++)
            {
                _input[i - startIndex] = new double[InputTuples * IndexesToConsider];
                _ideal[i - startIndex] = new double[OutputSize];
                _manager.GetInputData(i, _input[i - startIndex]);
                _manager.GetOutputData(i, _ideal[i - startIndex]);
            }

        }

        public void TrainNetworkAsync(DateTime trainFrom, DateTime trainTo, TrainingStatus status)
        {
            Action<DateTime, DateTime, TrainingStatus> action = TrainNetwork;
            action.BeginInvoke(trainFrom, trainTo, status, action.EndInvoke, action);
        }
        private void TrainNetwork(DateTime trainFrom, DateTime trainTo, TrainingStatus status)
        {
            if(_input == null || _ideal == null)
                CreateTrainingSets(trainFrom, trainTo);
            _trainThread = Thread.CurrentThread;
            int epoch = 1;
            ITrain train = null;
            try
            {
               
                var trainSet = new BasicNeuralDataSet(_input, _ideal);
                train = new ResilientPropagation(_network, trainSet);
                double error;
                do
                {
                    train.Iteration();
                    error = train.Error;
                    status?.Invoke(epoch, error, TrainingAlgorithm.Resilient);
                    epoch++;
                } while (error > MaxError);
            }
            catch (ThreadAbortException) { _trainThread = null; }
            finally
            {
                train?.FinishTraining();
            }
            _trainThread = null;
        }

        public void AbortTraining()
        {
            _trainThread?.Abort();
        }

        [System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.Demand)]
        public void ExportNeuralNetwork(string path)
        {
            if (_network == null)
                throw new NullReferenceException("Network reference is set to null. Nothing to export.");
            Encog.Util.SerializeObject.Save(path, _network);
        }

        public void LoadNeuralNetwork(string path)
        {
            _network = (BasicNetwork)Encog.Util.SerializeObject.Load(path);
            HiddenLayers = _network.Structure.Layers.Count - 2;//in and out
            HiddenUnits = _network.Structure.Layers[1].NeuronCount;
        }

        public List<PredictionResults> Predict(DateTime predictFrom, DateTime predictTo)
        {
            List<PredictionResults> results = new List<PredictionResults>();
            double[] present = new double[InputTuples * IndexesToConsider];
            double[] actualOutput = new double[OutputSize];
            int index = 0;
            foreach (var sample in _manager.Samples)
            {
                if (sample.Date.CompareTo(predictFrom) > 0 && sample.Date.CompareTo(predictTo) < 0)
                {
                    var result = new PredictionResults();
                    _manager.GetInputData(index - InputTuples, present);
                    _manager.GetOutputData(index - InputTuples, actualOutput);
                    var data = new BasicNeuralData(present);
                    var predict = _network.Compute(data);
                    result.ActualLotos = actualOutput[0] * (_manager.MaxLotos - _manager.MinLotos) + _manager.MinLotos;
                    result.PredictedLotos = predict[0] * (_manager.MaxLotos - _manager.MinLotos) + _manager.MinLotos;
                    result.ActualPir = actualOutput[1] * (_manager.MaxPrimeRate - _manager.MinPrimeRate) + _manager.MinPrimeRate;
                    result.PredictedPir = predict[1] * (_manager.MaxPrimeRate - _manager.MinPrimeRate) + _manager.MinPrimeRate;
                    result.ActualOrlen = actualOutput[2] * (_manager.MaxOrlen - _manager.MinOrlen) + _manager.MinOrlen;
                    result.PredictedOrlen = predict[2] * (_manager.MaxOrlen - _manager.MinOrlen) + _manager.MinOrlen;
                    result.Date = sample.Date;
                    var error = new ErrorCalculation();
                    error.UpdateError(actualOutput, predict.Data);
                    result.Error = error.CalculateRMS();
                    results.Add(result);
                }
                index++;
            }
            return results;
        }
    }
}
