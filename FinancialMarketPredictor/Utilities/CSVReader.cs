using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FinancialMarketPredictor.Utilities
{
    public class CSVReader : IDisposable
    {
        private readonly TextReader _reader;
        
        private readonly IDictionary<String, int> _columns = new Dictionary<String, int>();
        
        private readonly String[] _data;
        
        public static DateTime ParseDate(String when)
        {
            try
            {
                return DateTime.ParseExact(when, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return default(DateTime);
            }
        }
        
        public CSVReader(String filename)
        {
            _reader = new StreamReader(filename);

            // read the column heads
            String line = _reader.ReadLine();
            string[] tok = line.Split(',');

            for (int index = 0; index < tok.Length; index++)
            {
                String header = tok[index];
                _columns.Add(header.ToLower(), index);
            }

            _data = new String[tok.Length];
        }
        
        public void Close()
        {
            _reader.Close();
        }

        public String Get(String column)
        {
            if (!_columns.ContainsKey(column.ToLower()))
            {
                return null;
            }
            int i = _columns[column.ToLower()];

            return _data[i];
        }
        
        public DateTime GetDate(String column)
        {
            String str = Get(column);
            return DateTime.Parse(str, CultureInfo.InvariantCulture);
        }
        
        public double GetDouble(String column)
        {
            String str = Get(column);
            return double.Parse(str, CultureInfo.InvariantCulture);
        }
        
        public bool Next()
        {
            String line = _reader.ReadLine();
            if (line == null)
            {
                return false;
            }

            string[] tok = line.Split(',');

            for (int i = 0; i < tok.Length; i++)
            {
                String str = tok[i];
                if (i < _data.Length)
                {
                    _data[i] = str;
                }
            }

            return true;
        }

        #region IDisposable Members

        private bool _alreadydisposed = false;

        public void Dispose()
        {
            Dispose(true);
            _alreadydisposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadydisposed) return;
            if (isDisposing)
            {
                _reader.Dispose();
            }
        }

        #endregion
    }
}
