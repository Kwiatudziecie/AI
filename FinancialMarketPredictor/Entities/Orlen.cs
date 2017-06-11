using System;

namespace FinancialMarketPredictor.Entities
{
    public class Orlen : IComparable<Orlen>
    {
        public Orlen(double amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        #region Properties
        
        public double Amount {get; set;}
        
        public DateTime Date {get; set;}

        #endregion

        #region IComparable<Orlen> Members
        
        public int CompareTo(Orlen other)
        {
            return Date.CompareTo(other.Date);
        }

        #endregion
    }
}
