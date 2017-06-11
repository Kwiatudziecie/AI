using System;
using System.Collections.Generic;
using System.IO;
using FinancialMarketPredictor.Utilities;

namespace FinancialMarketPredictor.Entities
{
    public sealed class FinancialPredictorManager
    {
        #region Private Members
   
        private List<InterestRate> _rates = new List<InterestRate>();
        
        private List<Orlen> _orlenIndex = new List<Orlen>();
        
        private List<Lotos> _lotosIndexes = new List<Lotos>();
        
        private readonly List<FinancialIndexes> _samples = new List<FinancialIndexes>();
        
        private readonly int _inputSize;
        
        private readonly int _outputSize;

        #endregion

        #region Properties
        
        public double MaxOrlen { get; private set; }
        
        public double MinOrlen { get; private set; }
        
        public double MaxLotos { get; private set; }
        
        public double MinLotos { get; private set; }

        public double MaxPrimeRate { get; private set; }

        public double MinPrimeRate { get; private set; }

        public DateTime MaxDate { get; private set; }

        public DateTime MinDate { get; private set; }
        #endregion

        private const string DateHeader = "Date";
        private const string CloseHeader = "Close";

        #region Constructors
        
        public FinancialPredictorManager(int inputSize, int outputSize)
        {
            if (inputSize <= 0)
                throw new ArgumentException("inputSize cannot be less than 0");
            if (outputSize <= 0)
                throw new ArgumentException("outputSize cannot be less than 0");
            _inputSize = inputSize;
            _outputSize = outputSize;
            MaxOrlen = MaxPrimeRate = MaxLotos = Double.MinValue;
            MinOrlen = MinPrimeRate = MinLotos = Double.MaxValue;
            MaxDate = DateTime.MaxValue;
            MinDate = DateTime.MinValue;
        }
        #endregion
        
        public void GetInputData(int offset, double[] input)
        {
            for (int i = 0; i < _inputSize; i++)
            {
                FinancialIndexes sample = _samples[offset + i];
                input[i*3]       = sample.Lotos;
                input[i*3 + 1]   = sample.PrimeInterestRate;
                input[i*3 + 2]   = sample.Orlen;
            }
        }
        
        public void GetOutputData(int offset, double[] output)
        {
            FinancialIndexes sample = _samples[offset + _inputSize];
            output[0]     = sample.Lotos;
            output[1] = sample.PrimeInterestRate;
            output[2] = sample.Orlen;
            
        }

        #region Get indexes

        public double GetSpIndex(DateTime date)
        {
            double currentsp = 0;

            foreach (Lotos item in _lotosIndexes)
            {
                if (item.Date.CompareTo(date) >= 0)
                {
                    return currentsp;
                }
                currentsp = item.SpIndex;
            }
            return currentsp;
        }
       
        public double GetPrimeRate(DateTime date)
        {
            double currentRate = 0;

            foreach (InterestRate rate in _rates)
            {
                if (rate.Date.CompareTo(date) >= 0)
                {
                    return currentRate;
                }
                currentRate = rate.Rate;
            }
            return currentRate;
        }

       public double GetNasdaqIndex(DateTime date)
        {
            double currentAmount = 0;

            foreach (Orlen index in _orlenIndex)
            {
                if (index.Date.CompareTo(date) >= 0)
                {
                    return currentAmount;
                }
                currentAmount = index.Amount;
            }
            return currentAmount;
        }

        #endregion
        
        public IList<FinancialIndexes> Samples => _samples;
       
        public void Load(String pathToLotos, String primeFilename, String pathToOrlen)
        {
            if (!File.Exists(pathToLotos))
                throw new ArgumentException("pathToLotos targets an invalid file");
            if (!File.Exists(primeFilename))
                throw new ArgumentException("primeFilename targets an invalid file");
            if (!File.Exists(pathToOrlen))
                throw new ArgumentException("pathToOrlen targets an invalid file");
            try
            {
                LoadLotos(pathToLotos);
                LoadPrimeInterestRates(primeFilename);
                LoadOrlenIndexes(pathToOrlen);
            }
            catch
            {
                throw new NotSupportedException("Loading file failed. Not supported file format. Make sure column headers are written in the file");
            }
           MaxDate = MaxDate.Subtract(new TimeSpan(_inputSize, 0, 0, 0)); 
            StitchFinancialIndexes();
            _samples.Sort();            
            NormalizeData();
        }

        #region Load .csv files region
        
        public void LoadOrlenIndexes(String filename)
        {
            if (_orlenIndex == null) _orlenIndex = new List<Orlen>();
            else if (_orlenIndex.Count > 0) _orlenIndex.Clear();
            using (CSVReader csv = new CSVReader(filename))
            {
                while (csv.Next())
                {
                    DateTime date = csv.GetDate(DateHeader);
                    double amount = csv.GetDouble(CloseHeader);
                    Orlen sample = new Orlen(amount, date);
                    _orlenIndex.Add(sample);
                    if (amount > MaxOrlen) MaxOrlen = amount;
                    if (amount < MinOrlen) MinOrlen = amount;
                }
                csv.Close();
                _orlenIndex.Sort();
            }
            if (_orlenIndex.Count > 0)
            {
                if (MinDate < _orlenIndex[0].Date)
                    MinDate = _orlenIndex[0].Date;
                if (MaxDate > _orlenIndex[_orlenIndex.Count - 1].Date)
                    MaxDate = _orlenIndex[_orlenIndex.Count - 1].Date;
            }
        }
        
        public void LoadPrimeInterestRates(String primeFilename)
        {
            if (_rates == null) _rates = new List<InterestRate>();
            else if (_rates.Count > 0) _rates.Clear();
            using (CSVReader csv = new CSVReader(primeFilename))
            {
                while (csv.Next())
                {
                    DateTime date = csv.GetDate(DateHeader);
                    double rate = csv.GetDouble(CloseHeader);
                    InterestRate ir = new InterestRate(date, rate);
                    _rates.Add(ir);
                    if (rate > MaxPrimeRate) MaxPrimeRate = rate;
                    if (rate < MinPrimeRate) MinPrimeRate = rate;
                }

                csv.Close();
                _rates.Sort();
            }
            if (_rates.Count > 0)
            {
                if (MinDate < _rates[0].Date)
                    MinDate = _rates[0].Date;
            }

        }
        
        public void LoadLotos(String filename)
        {
            if (_lotosIndexes == null) _lotosIndexes = new List<Lotos>();
            else if (_lotosIndexes.Count > 0) _lotosIndexes.Clear();
            using (CSVReader csv = new CSVReader(filename))
            {
                while (csv.Next())
                {
                    DateTime date = csv.GetDate(DateHeader);
                    double amount = csv.GetDouble(CloseHeader);
                    Lotos sample = new Lotos(amount, date);
                    _lotosIndexes.Add(sample);
                    if (amount > MaxLotos) MaxLotos = amount;
                    if (amount < MinLotos) MinLotos = amount;
                }
                csv.Close();
                _lotosIndexes.Sort();
            }
            if (_lotosIndexes.Count > 0)
            {
                if (MinDate < _lotosIndexes[0].Date)
                    MinDate = _lotosIndexes[0].Date;
                if (MaxDate > _lotosIndexes[_lotosIndexes.Count - 1].Date)
                    MaxDate = _lotosIndexes[_lotosIndexes.Count - 1].Date;
            }
        }
        #endregion
        
        public void StitchFinancialIndexes()
        {
            foreach (Lotos item in _lotosIndexes)
            {
                double rate = GetPrimeRate(item.Date);
                double orlenI = GetNasdaqIndex(item.Date);
                double lotosI = GetSpIndex(item.Date);
                _samples.Add(new FinancialIndexes(orlenI, lotosI, rate, item.Date));
            }          
        }
       public void NormalizeData()
        {
            foreach (FinancialIndexes t in _samples)
            {
                t.Orlen = (t.Orlen - MinOrlen) / (MaxOrlen - MinOrlen);
                t.PrimeInterestRate = (t.PrimeInterestRate - MinPrimeRate) / (MaxPrimeRate - MinPrimeRate);
                t.Lotos = (t.Lotos - MinLotos) / (MaxLotos - MinLotos);
            }
        }
    }
}
