using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DelegateCsvReader
{
    /// <summary>
    /// This program uses an array of delegates to dispatch to scanning functions
    /// where each character offset into the dispatch array is used to dispatch
    /// to a function that determines the next action
    /// this prototype is used to determine best case timing so all characters
    /// so each charactrer dispatchs to the same 'scanNextAlpha' routine.
    /// </summary>
    class Program
    {
        const bool DEBUG = false;
        static string filePath = @"..\..\..\TestData\";
        //static string fileName = "simple5.csv";
        static string fileName = "test1.csv";
        const int nFields = 6;
        static int cbCharsRemaining = 0;
        static int oNextCh = 0;
        static int oFieldStarts = 0;
        static int nLines = 0;
        static bool eol = false;
        static string line;
        static string[] fields;
        static scanState ScanState = scanState.alpha;

        enum scanState
        {
            alpha
        }

        public delegate bool ScanNextChar();
        static public ScanNextChar[,] Scan = new ScanNextChar[2, 256];

        static void Main(string[] args)
        {
            DateTime tStart = DateTime.Now;
            Console.WriteLine("Using delegate dispatching CSV Reader");
            Console.WriteLine("File: " + fileName);
            Console.WriteLine("Starting - " + tStart);
            InitAlphaScan();

            using (StreamReader reader = new StreamReader(filePath + fileName, Encoding.Default, true))
            {
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    nLines++;
                    oFieldStarts = oNextCh = 0;
                    cbCharsRemaining = line.Length;
                    eol = false;
                    fields = GetFields(line);
                    if (false)
                    {
                        Console.WriteLine(line);
                        for (int i = 0; i < fields.Length; i++)
                        {
                            Console.WriteLine("{0}:{1}", i, fields[i]);
                        }
                    }
                }
            }
            DateTime tDone = DateTime.Now;
            Console.WriteLine("    Done - " + tDone);
            TimeSpan ET = tDone.Subtract(tStart);
            Console.WriteLine("ET: " + ET.ToString());
            Console.ReadLine();
        }

        static string[] GetFields(string line)
        {
            string[] fields = new string[nFields];
            int nextField = 0;
            while (cbCharsRemaining > 0)
            {
                fields[nextField++] = GetNextField();
            }
            return fields;
        }

        static string GetNextField()
        {
            oFieldStarts = oNextCh;
            if (DEBUG)
            {
                bool notDone = true;
                while (notDone)
                {
                    int lineLen = line.Length;
                    char ch = line[oNextCh];
                    int oCh = line[oNextCh];
                    if (nLines == 6)
                    {
                        if (oNextCh > 64)
                        {
                            int foo = 0;  //place to set breakpoint
                        }
                    }
                    notDone = Scan[(int)ScanState, oCh]();
                }
            }
            else
            {
                while (Scan[(int)ScanState, line[oNextCh]]()) { }
            }
            return line.Substring(oFieldStarts, oNextCh - oFieldStarts);
        }

        static bool scanNextAlpha()
        {
            oNextCh++;
            eol = --cbCharsRemaining <= 0;
            return !eol;
        }

        static void InitAlphaScan()
        {
            ScanNextChar scanAlpha = scanNextAlpha;
            for (var i = 0; i < 256; i++)
            {
                Scan[(int)scanState.alpha, i] = scanNextAlpha;
            }
        }
    }
}
