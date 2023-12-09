using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    internal class Day2
    {
        public int GetSolution()
        {
            var aocData = File.ReadAllLines("C:\\Users\\freddie.freeston\\source\\repos\\AdventOfCode\\AdventOfCode\\Solutions\\day2_data.txt")
               .ToList();
            var colourLimits = new Dictionary<string, int> { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
            List<int> impossibleGames = new List<int>();

            foreach (var game in aocData)
            {
                //Game 1: 7 red, 8 blue; 6 blue, 6 red, 2 green; 2 red, 6 green, 8 blue; 9 green, 2 red, 4 blue; 6 blue, 4 green
                var colonIndex = game.IndexOf(':');
                var numberIndex = game.IndexOf(" ") + 1;
                var gameNumber = Convert.ToInt32(game.Substring(numberIndex, colonIndex - numberIndex));
                var gameWithoutTitle = game.Substring(colonIndex + 1);
                var cubeSets = gameWithoutTitle.Split(';').ToList();
                cubeSets.ForEach(cubeSet =>
                {
                    cubeSet = cubeSet.Trim();
                    var colourCounts = cubeSet.Split(",").ToList();
                    colourCounts.ForEach(colourCount =>
                    {
                        colourCount = colourCount.Trim();
                        var splitColourCount = colourCount.Split(" ");
                        var count = Convert.ToInt32(splitColourCount[0]);
                        var colour = splitColourCount[1];
                        var colourLimit = colourLimits[colour];
                        if (count > colourLimit)
                        {
                            impossibleGames.Add(gameNumber);
                        }
                    });
                });
            }
            int total = 0;
            for (var i = 1; i < 101; i++)
            {
                if (!impossibleGames.Contains(i))
                {
                    total += i;
                }
            }
            Console.WriteLine(total);
            return total;
        }
    }
}
