using System;

namespace FinancialMarketPredictor.Entities
{
    public class InterestRate : IComparable<InterestRate>
    {
        public InterestRate(DateTime effectiveDate, double rate)
        {
            Date = effectiveDate;
            Rate = rate;
        }

        #region Properties
        
        public DateTime Date {get; set;}
        
        public double Rate {get; set;}
  
        #endregion

        #region IComparable<InterestRate> Members
        
        public int CompareTo(InterestRate other)
        {
            return Date.CompareTo(other.Date);
        }

        #endregion
    }
}
