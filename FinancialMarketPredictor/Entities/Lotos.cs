using System;

namespace FinancialMarketPredictor.Entities
{
    public class Lotos : IComparable<Lotos>
    {
        public Lotos(double amount, DateTime date)
        {
            SpIndex = amount;
            Date = date;
        }

        #region Properties
      
        public double SpIndex { get; set; }
        
        public DateTime Date { get; set; }

        #endregion
        
        public int CompareTo(Lotos other)
        {
            return Date.CompareTo(other.Date);
        }
    }
}
