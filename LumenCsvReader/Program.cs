using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace LumenCsvReader
{
    /// <summary>
    /// this program uses the LumenWorks CSV reader to determine how long it takes
    /// to parse a CSV file.  The results are used to compare other methods of
    /// reading CSV files.
    /// </summary>
    class Program
    {
        static string filePath = @"..\..\..\TestData\";
        //static string fileName = @"..\..\..\TestData\simple5.csv";
        static string fileName = "test1.csv";

        static void Main(string[] args)
        {
            DateTime tStart = DateTime.Now;
            Console.WriteLine("Using LumenWorks CSV Reader");
            Console.WriteLine("File: " + fileName);
            Console.WriteLine("Starting - " + tStart);

            // open the file "data.csv" which is a CSV file with headers
            using (CsvReader csv = new CsvReader(new StreamReader(filePath + fileName), true))
            {
                int fieldCount = csv.FieldCount;

                string[] headers = csv.GetFieldHeaders();
                while (csv.ReadNextRecord()) { }
            }

            DateTime tDone = DateTime.Now;
            Console.WriteLine("    Done - " + tDone);
            TimeSpan ET = tDone.Subtract(tStart);
            Console.WriteLine("ET: " + ET.ToString());
            Console.ReadLine();
        }
    }
}
