using System;

namespace FinancialMarketPredictor.Entities
{
    public class FinancialIndexes : IComparable<FinancialIndexes>
    {
        public FinancialIndexes(double orlen, double lotos, double pirIndex, DateTime date)
        {
            Orlen = orlen;
            Lotos = lotos;
            PrimeInterestRate = pirIndex;
            Date = date;
        }
        public double Orlen { get; set; }
        public double Lotos { get; set; }
        public double PrimeInterestRate { get; set; }
        public DateTime Date { get; set; }
        public int CompareTo(FinancialIndexes other)
        {
            return Date.CompareTo(other.Date);
        }

    }
}
