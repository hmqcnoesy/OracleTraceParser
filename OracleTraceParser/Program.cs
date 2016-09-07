using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OracleTraceParser
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sr = new StreamReader(args[0]))
            {
                var line = string.Empty;
                var parsingSqlHex = false;
                var timestamp = string.Empty;
                var parsedSql = string.Empty;
                var patternStart = @"nsbasic_bsd\: packet dump";
                var patternTimeStamp = @"\[\d{2}-[A-Z]{3}-\d{4} (\d\d\:){3}\d{3}\]";
                var patternHex = @"nsbasic_bsd\: ([0-9A-F][0-9A-F] ){8}";
                var patternEnd = @"nsbasic_bsd\: exit \(0\)$";

                while (line != null)
                {
                    if (Regex.IsMatch(line, patternStart))
                    {
                        timestamp = Regex.Match(line, patternTimeStamp).Value;
                        parsingSqlHex = true;
                    }
                    else if (parsingSqlHex)
                    {
                        if (Regex.IsMatch(line, patternEnd))
                        {
                            if (!string.IsNullOrEmpty(parsedSql))
                            {
                                Console.WriteLine(timestamp);
                                Console.WriteLine(parsedSql + "\r\n");
                            }
                            parsedSql = string.Empty;
                            parsingSqlHex = false;
                        }
                        else if (Regex.IsMatch(line, patternHex))
                        {
                            parsedSql += HexToString(line.Substring(line.Length - 35, 23));
                        }
                    }

                    line = sr.ReadLine();
                }
            }
        }


        static string HexToString(string hexValues)
        {
            var hexCodeArray = hexValues.Split(" ".ToCharArray());
            var n = 0;
            var s = string.Empty;

            for (var i = 0; i < hexCodeArray.Length; i++)
            {
                n = Convert.ToInt32(hexCodeArray[i], 16);
                if (n > 31 && n < 127) s += Convert.ToChar(Convert.ToUInt32(hexCodeArray[i], 16));
            }

            return s;
        }
    }
}
