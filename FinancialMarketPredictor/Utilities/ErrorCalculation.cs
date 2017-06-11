using System;

namespace FinancialMarketPredictor.Utilities
{
    public class ErrorCalculation
    {
        private double _globalError;
        private int _setSize;
        public double CalculateRMS()
        {
            return Math.Sqrt(_globalError/(_setSize));
        }

        public void UpdateError(double[] actual, double[] ideal)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                double delta = ideal[i] - actual[i];
                _globalError += delta * delta;
            }
            _setSize += ideal.Length;
        }


    }
}
