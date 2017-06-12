using System;

namespace FinancialMarketPredictor.Entities
{
    public class Idx : IComparable<Idx>
    {
        public Idx(double rate, DateTime date)
        {
            Rate = rate;
            Date = date;
        }

        #region Properties
        
        public double Rate {get; set;}
        
        public DateTime Date {get; set;}

        #endregion

        #region IComparable<Orlen> Members
        
        public int CompareTo(Idx other)
        {
            return Date.CompareTo(other.Date);
        }

        #endregion
    }
}
