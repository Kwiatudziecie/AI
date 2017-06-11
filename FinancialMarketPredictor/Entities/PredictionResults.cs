using System;

namespace FinancialMarketPredictor.Entities
{
    public class PredictionResults
    {
        #region Properties
      
        public DateTime Date { get; set; }
        
        public double ActualLotos {get; set; }
   
        public double PredictedLotos {get; set; }

        public double ActualOrlen { get; set; }

        public double PredictedOrlen { get; set; }

        public double ActualPir { get; set; }

        public double PredictedPir { get; set; }

        public double Error { get; set; }

        #endregion
    }
}
