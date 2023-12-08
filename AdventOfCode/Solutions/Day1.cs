using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class Day1
    {
        public int GetSolution()
        {
            var aocData = File.ReadAllLines("C:\\Users\\freddie.freeston\\source\\repos\\AdventOfCode\\AdventOfCode\\Solutions\\day1_data.txt");
            List<int> lineTotals = new List<int>();
            List<string> digitArray = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            var digitAsWordList = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            digitArray.AddRange(digitAsWordList);
            for (int i = 0; i < aocData.Length; i++)
            {
                KeyValuePair<int, string> firstDigit = new KeyValuePair<int, string>(-1, "N/A");
                KeyValuePair<int, string> lastDigit = new KeyValuePair<int, string>(-1, "N/A");
                digitArray.ForEach(x =>
                {
                    var indexOfDigit = aocData[i].IndexOf(x);
                    if (indexOfDigit >= 0)
                    {
                        if (firstDigit.Key == -1 || indexOfDigit < firstDigit.Key)
                        {
                            string intValue = x;
                            if (x.Length > 1)
                            {
                                intValue = digitArray[digitAsWordList.IndexOf(x)];
                            }
                            firstDigit = new KeyValuePair<int, string>(indexOfDigit, intValue);
                        }
                    }

                    var lastIndexOfDigit = aocData[i].LastIndexOf(x);
                    if (lastIndexOfDigit >= 0)
                    {
                        if (lastDigit.Key == -1 || lastIndexOfDigit > lastDigit.Key)
                        {
                            string intValue = x;
                            if (x.Length > 1)
                            {
                                intValue = digitArray[digitAsWordList.IndexOf(x)];
                            }
                            lastDigit = new KeyValuePair<int, string>(lastIndexOfDigit, intValue);
                        }
                    }
                });

                var calibrationValue = $"{firstDigit.Value}{lastDigit.Value}";
                lineTotals.Add(Convert.ToInt32(calibrationValue));
            }

            var total = lineTotals.Sum();
            return total;
        }
    }
}
