using System;
using System.Collections.Generic;
using System.IO;
using FinancialMarketPredictor.Utilities;

namespace FinancialMarketPredictor.Entities
{
    public sealed class FinancialPredictorManager
    {

        public enum type
        {
            rate, orlen, lotos
        }
        #region Private Members

        private List<Idx> _rates = new List<Idx>();
        private List<Idx> _orlenIndexes = new List<Idx>();
        private List<Idx> _lotosIndexes = new List<Idx>();

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
                input[i * 3] = sample.Lotos;
                input[i * 3 + 1] = sample.PrimeInterestRate;
                input[i * 3 + 2] = sample.Orlen;
            }
        }

        public void GetOutputData(int offset, double[] output)
        {
            FinancialIndexes sample = _samples[offset + _inputSize];
            output[0] = sample.Lotos;
            output[1] = sample.PrimeInterestRate;
            output[2] = sample.Orlen;

        }

        public double GetIndex(DateTime date, List<Idx> list)
        {
            double current = 0;

            foreach (var index in list)
            {
                if (index.Date.CompareTo(date) >= 0)
                {
                    return current;
                }
                current = index.Rate;
            }
            return current;
        }

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
                LoadIndexes(pathToLotos, _lotosIndexes, type.lotos);
                LoadIndexes(primeFilename, _rates, type.rate);
                LoadIndexes(pathToOrlen, _orlenIndexes, type.orlen);
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

        public void LoadIndexes(String filename, List<Idx> list, type types)
        {
            if (list == null) list = new List<Idx>();
            else if (list.Count > 0) list.Clear();

            double max, min;
            switch (types)
            {
                case type.rate:
                    max = MaxPrimeRate;
                    min = MinPrimeRate;
                    break;
                case type.orlen:
                    max = MaxOrlen;
                    min = MinOrlen;
                    break;
                default:
                    min = MinLotos;
                    max = MaxLotos;
                    break;
            }

            using (CSVReader csv = new CSVReader(filename))
            {
                while (csv.Next())
                {
                    DateTime date = csv.GetDate(DateHeader);
                    double rate = csv.GetDouble(CloseHeader);
                    Idx sample = new Idx(rate, date);
                    list.Add(sample);
                    if (rate > max) max = rate;
                    if (rate < min) min = rate;
                }
                csv.Close();
                list.Sort();
            }
            switch (types)
            {
                case type.rate:
                    MaxPrimeRate=max;
                    MinPrimeRate = min;
                    break;
                case type.orlen:
                    MaxOrlen = max;
                    MinOrlen = min;
                    break;
                case type.lotos:
                    MinLotos = min;
                    MaxLotos = max;
                    break;
            }
            if (list.Count <= 0) return;
            if (MinDate < list[0].Date)
                MinDate = list[0].Date;
            if (MaxDate > list[list.Count - 1].Date)
                MaxDate = list[list.Count - 1].Date;
        }


        public void StitchFinancialIndexes()
        {
            foreach (Idx item in _lotosIndexes)
            {
                _samples.Add(new FinancialIndexes(GetIndex(item.Date, _orlenIndexes), GetIndex(item.Date, _lotosIndexes),
                    GetIndex(item.Date, _rates), item.Date));
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

