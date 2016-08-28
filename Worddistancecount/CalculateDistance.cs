using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Worddistancecount
{
    public class CalculateDistance
    {
        private string x1 = string.Empty;
        private string x2 = string.Empty;
        public CalculateDistance(string y1, string y2)
        {
            x1 = y1;
            x2 = y2;
        }
        private List<Line> result = new List<Line>();
        public List<Line> ReadSpit(string path)
        {
            var allLines = File.ReadAllLines(path);
            int counter = 0;
            foreach (var line in allLines)
            {
                if (line !="")
                {
                    int[] array = MinimumDistance(x1, x2, SplitWords(line));
                    var x = new Line();
                    x.LineNumber = counter;
                    x.IndexOne = array[1];
                    x.IndexTwo = array[2];
                    x.Min = array[0];
                    result.Add(x);   
                }               
                counter++;   
            }
            return result;
        }
        static string[] SplitWords(string s)
        {
            //
            // Split on all non-word characters.
            // ... Returns an array of all the words.
            //
            return Regex.Split(s, @"\W+");
            // @      special verbatim string syntax
            // \W+    one or more non-word characters together
        }

        private  int[] MinimumDistance(string x1, string x2, string[] inList)
        {
            int index1 = -1;
            int index2 = -1;
            int minDistance = int.MaxValue;
            int tempDistance = 0;

            //for (int x = 0; x < inList.Count(); x++)
            //{
            //    if (inList[x].Equals(x1))
            //    {
            //        index1 = x;
            //    }
            //    if (inList[x].Equals(x2))
            //    {
            //        index2 = x;
            //    }
            //    if (index1 != -1 && index2 != -1)
            //    { // both words have to be found
            //        tempDistance = (int)Math.Abs(index2 - index1);
            //        if (tempDistance < minDistance)
            //        {
            //            minDistance = tempDistance;
            //        }
            //    }
            //}
            index1 = Array.FindIndex(inList, x => x == x1);
            index2 = Array.FindIndex(inList, x => x == x2);

            if (index1 != -1 && index2 != -1)
                { // both words have to be found
                    tempDistance = (int)Math.Abs(index2 - index1);
                    if (tempDistance < minDistance)
                    {
                        minDistance = tempDistance;
                    }
                }
            int[] distance = { minDistance, index1, index2 };
            return distance;
        }

    }
    public class Line
    {
        public int LineNumber { get; set; }
        public int IndexOne { get; set; }
        public int IndexTwo { get; set; }
        public int Min { get; set; }


    }
}
