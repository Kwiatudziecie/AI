using System.IO;
using System.Text;

namespace FinancialMarketPredictor.Utilities
{
    public class CSVWriter
    {
        private const char Separator = ',';

        private readonly string _pathToFile;

        private StreamWriter _writer;

        public CSVWriter(string pathToFile)
        {
            _pathToFile = pathToFile;
        }

        [System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.Demand)]
        public void Write(object[,] data)
        {
            using (_writer = new StreamWriter(_pathToFile))
            {
                int cols = data.GetLength(1);
                for (int i = 0, n = data.GetLength(0); i < n; i++)
                {
                    StringBuilder builder = new StringBuilder();
                    for (int j = 0; j < cols; j++)
                    {
                        builder.Append(data[i, j]);
                        if (j != cols - 1)
                            builder.Append(Separator);
                    }
                    _writer.WriteLine(builder.ToString());
                }
                _writer.Close();
            }
        }
    }

}