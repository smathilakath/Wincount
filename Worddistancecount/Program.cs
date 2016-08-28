using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Worddistancecount
{
    class Program
    {
        static void Main(string[] args)
        {
            //TypeA();
            TypeB();

        }

        private static void TypeB()
        {
            var words = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            countWordsInFile("C:\\log.txt", words);
            var Distance = new CalculateDistance("Sachin","centuries");
            var result = Distance.ReadSpit("C:\\log.txt");
            var final = result.Aggregate((l, r) => l.Min < r.Min ? l : r);
            Console.WriteLine("Line : {0} Min : {1} Index {2} Index 2 {1}", final.LineNumber, final.Min, final.IndexOne, final.IndexTwo);
            
        }

        private static void TypeA()
        {
            var total = 0;
            using (StreamReader sr = new StreamReader("C:\\log.txt"))
            {

                while (!sr.EndOfStream)
                {
                    var counts = sr
                        .ReadLine()
                        .Split(' ')
                        .GroupBy(s => s)
                        .Select(g => new { Word = g.Key, Count = g.Count() });
                    var wc = counts.SingleOrDefault(c => c.Word == "Sachin");
                    var wc2 = counts.SingleOrDefault(c => c.Word == "centuries");
                    total += (wc == null) ? 0 : wc.Count;
                }
            }
        }
        
        private static void countWordsInFile(string file, Dictionary<string, int> words)
        {
            var content = File.ReadAllText(file);

            var wordPattern = new Regex(@"\w+");

            foreach (Match match in wordPattern.Matches(content))
            {
                int currentCount = 0;
                words.TryGetValue(match.Value, out currentCount);

                currentCount++;
                words[match.Value] = currentCount;
            }
        }
    }
}
