using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day3
    {
        public int GetPart1Solution()
        {
            var aocData = File.ReadAllLines("C:\\Users\\freddie.freeston\\source\\repos\\AdventOfCode\\AdventOfCode\\Solutions\\day3_data.txt")
               .ToList();

            var partNumbers = new List<int>();
            for (var i = 0; i < aocData.Count; i++)
            {
                var line = aocData[i];
                var symbols = Regex.Matches(line, @"[^\d.]");
                var numbers = Regex.Matches(line, @"\d+");
                for (var y = 0; y < numbers.Count; y++)
                {
                    var numberMatch = numbers[y];
                    var indexAfterNumber = numberMatch.Index + numberMatch.Length;

                    for (var x = 0; x < symbols.Count; x++)
                    {
                        var symbol = symbols[x];

                        if (symbol.Index + 1 == numberMatch.Index || indexAfterNumber == symbol.Index)
                        {
                            partNumbers.Add(Convert.ToInt32(numberMatch.Value));
                        }
                    }

                    if (i != 0)
                    {
                        this.HandleAdjacentLine(aocData[i - 1], numberMatch, partNumbers, indexAfterNumber);
                    }
                    if (i != aocData.Count - 1)
                    {
                        this.HandleAdjacentLine(aocData[i + 1], numberMatch, partNumbers, indexAfterNumber);
                    }

                }
            };
            return partNumbers.Sum();
        }

        public int GetPart2Solution()
        {
            var aocData = File.ReadAllLines("C:\\Users\\freddie.freeston\\source\\repos\\AdventOfCode\\AdventOfCode\\Solutions\\day3_data.txt")
               .ToList();
            var gearRatios = new List<int>();
            for (var i = 0; i < aocData.Count; i++)
            {
                var line = aocData[i];
                var stars = Regex.Matches(line, @"\*");
                var numbers = Regex.Matches(line, @"\d+");

                for (var x = 0; x < stars.Count; x++)
                {
                    var symbol = stars[x];
                    var adjacentNumbers = new List<int>();
                    for (var y = 0; y < numbers.Count; y++)
                    {
                        var numberMatch = numbers[y];
                        var indexAfterNumber = numberMatch.Index + numberMatch.Length;

                        if (symbol.Index + 1 == numberMatch.Index || indexAfterNumber == symbol.Index)
                        {
                            adjacentNumbers.Add(Convert.ToInt32(numberMatch.Value));
                        }
                    }

                    if (i != 0)
                    {
                        this.HandleAdjacentLineSymbol(aocData[i - 1], symbol, adjacentNumbers);
                    }
                    if (i != aocData.Count - 1)
                    {
                        this.HandleAdjacentLineSymbol(aocData[i + 1], symbol, adjacentNumbers);
                    }

                    if (adjacentNumbers.Count == 2)
                    {
                        gearRatios.Add(adjacentNumbers.FirstOrDefault() * adjacentNumbers[1]);
                    }
                }
            };
            return gearRatios.Sum();
        }

        private void HandleAdjacentLineSymbol(string adjacentLine, Match symbolMatch, List<int> adjacentNumbers)
        {
            var adjacentNumbersOnOtherLine = Regex.Matches(adjacentLine, @"\d+");
            for (var z = 0; z < adjacentNumbersOnOtherLine.Count; z++)
            {
                var adjacentNumber = adjacentNumbersOnOtherLine[z];
                var indexAfterNumber = adjacentNumber.Index + adjacentNumber.Length;
                if (symbolMatch.Index + 1 == adjacentNumber.Index || indexAfterNumber == symbolMatch.Index)
                {
                    adjacentNumbers.Add(Convert.ToInt32(adjacentNumber.Value));
                }
                else
                {
                    for (var p = adjacentNumber.Index; p < indexAfterNumber; p++)
                    {
                        if (symbolMatch.Index == p)
                        {
                            adjacentNumbers.Add(Convert.ToInt32(adjacentNumber.Value));
                        }
                    }
                }
            }
        }

        private void HandleAdjacentLine(string adjacentLine, Match numberMatch, List<int> partNumbers, int indexAfterNumber)
        {
            var adjacentSymbols = Regex.Matches(adjacentLine, @"[^\d.]");
            for (var z = 0; z < adjacentSymbols.Count; z++)
            {
                var adjacentSymbol = adjacentSymbols[z];

                if (adjacentSymbol.Index + 1 == numberMatch.Index || indexAfterNumber == adjacentSymbol.Index)
                {
                    partNumbers.Add(Convert.ToInt32(numberMatch.Value));
                }
                else
                {
                    for (var p = numberMatch.Index; p < indexAfterNumber; p++)
                    {
                        if (adjacentSymbol.Index == p)
                        {
                            partNumbers.Add(Convert.ToInt32(numberMatch.Value));
                        }
                    }
                }
            }
        }
    }
}
