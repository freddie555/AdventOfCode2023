using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Solutions
{
    internal class Day7
    {
        private List<char> cardValues = new List<char> { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };

        public int GetSolution()
        {
            var aocData = File.ReadAllLines("C:\\Users\\freddie.freeston\\source\\repos\\AdventOfCode\\AdventOfCode\\Solutions\\day7_data.txt")
               .ToList();

            var handsList = aocData.Select(x => x.Split(' ')[0]).ToList();
            var scoresList = aocData.Select(x => x.Split(' ')[1]).ToList();

            var handsRanked = new List<string>();

            for (var handIndex = 0; handIndex < handsList.Count; handIndex++)
            {
                var hand = handsList[handIndex];
                if (handIndex == 0)
                {
                    handsRanked.Add(handsList[handIndex]);
                    continue;
                }

                char maxValue;
                int maxOccurrences;
                (maxValue, maxOccurrences) = GetMaxOccurringValueInHand(hand);
                bool twoPair = false;
                bool fullHouse = false;
                (twoPair, fullHouse) = CheckIfTwoPairOrFullHouse(hand, maxValue, maxOccurrences);

                for (var rankedHandIndex = 0; rankedHandIndex < handsRanked.Count; rankedHandIndex++)
                {
                    var rankedHand = handsRanked[rankedHandIndex];
                    char rankedMaxValue;
                    int rankedMaxOccurrences;
                    (rankedMaxValue, rankedMaxOccurrences) = GetMaxOccurringValueInHand(rankedHand);
                    bool rankedIsTwoPair = false;
                    bool rankedIsFullHouse = false;
                    if (rankedHand == "38AJ8")
                    {

                    }
                    (rankedIsTwoPair, rankedIsFullHouse) = CheckIfTwoPairOrFullHouse(rankedHand, rankedMaxValue, rankedMaxOccurrences);

                    if (maxOccurrences > rankedMaxOccurrences)
                    {
                        if (rankedHandIndex == handsRanked.Count - 1)
                        {
                            handsRanked.Add(hand);
                            break;
                        }
                        continue;
                    }
                    else if (rankedMaxOccurrences > maxOccurrences)
                    {
                        handsRanked.Insert(rankedHandIndex, hand);
                        break;
                    }
                    else
                    {
                        if (!rankedIsTwoPair && twoPair)
                        {
                            if (rankedHandIndex == handsRanked.Count - 1)
                            {
                                handsRanked.Add(hand);
                            }
                            continue;
                        }

                        if (rankedIsTwoPair && !twoPair)
                        {
                            handsRanked.Insert(rankedHandIndex, hand);
                            break;
                        }

                        if (!rankedIsFullHouse && fullHouse)
                        {
                            if (rankedHandIndex == handsRanked.Count - 1)
                            {
                                handsRanked.Add(hand);
                            }
                            continue;
                        }

                        if (rankedIsFullHouse && !fullHouse)
                        {
                            handsRanked.Insert(rankedHandIndex, hand);
                            break;
                        }

                        // theyre the same type
                        var ranksHigher = false;
                        var handRanked = false;
                        for (var cardIndex = 0; cardIndex < 5; cardIndex++)
                        {
                            var cardValue = hand[cardIndex];
                            var rankedCardValue = rankedHand[cardIndex];
                            if (cardValue == rankedCardValue)
                            {
                                continue;
                            }

                            if (cardValues.IndexOf(cardValue) > cardValues.IndexOf(rankedCardValue))
                            {
                                ranksHigher = true;
                                break;
                            }

                            if (cardValues.IndexOf(rankedCardValue) > cardValues.IndexOf(cardValue))
                            {
                                handsRanked.Insert(rankedHandIndex, hand);
                                handRanked = true;
                                break;
                            }
                        }

                        if (handRanked)
                        {
                            break;
                        }

                        if (ranksHigher)
                        {
                            if (rankedHandIndex == handsRanked.Count - 1)
                            {
                                handsRanked.Add(hand);
                            }
                            continue;
                        }

                    }
                }
            }

            var handsScores = new List<int>();
            for (var i = 0; i < handsRanked.Count; i++)
            {
                var originalIndex = handsList.IndexOf(handsRanked[i]);
                var handScore = (i + 1) * Convert.ToInt32(scoresList[originalIndex]);
                handsScores.Add(handScore);
            }
            var total = handsScores.Sum();
            return total;
        }

        private (bool, bool) CheckIfTwoPairOrFullHouse(string hand, char maxValue, int maxOccurrences)
        {
            bool twoPair = false;
            bool fullHouse = false;
            if (maxOccurrences == 2 || maxOccurrences == 3)
            {
                var strippedHand = hand.Replace(maxValue.ToString(), "");

                if (hand.IndexOf('J') != -1 && maxValue != 'J')
                {
                    strippedHand = strippedHand.Replace("J", "");
                }
                char otherValue;
                int otherOccurrences = 0;
                (otherValue, otherOccurrences) = GetMaxOccurringValueInHand(strippedHand);

                if (otherOccurrences > 1)
                {
                    if (maxOccurrences == 2)
                    {
                        twoPair = true;
                    }
                    else
                    {
                        fullHouse = true;
                    }
                }
            }
            return (twoPair, fullHouse);
        }

        private (char, int) GetMaxOccurringValueInHand(string hand)
        {
            var maxValue = ' ';
            var maxOccurrences = 1;
            for (var cardIndex = 0; cardIndex < hand.Length; cardIndex++)
            {
                var cardValue = hand[cardIndex];
                var secondOccurrenceIndex = hand.IndexOf(cardValue, cardIndex + 1);
                if (secondOccurrenceIndex >= 0)
                {
                    var thirdOccurrenceIndex = hand.IndexOf(cardValue, secondOccurrenceIndex + 1);
                    if (thirdOccurrenceIndex >= 0)
                    {
                        var fourthOccurrenceIndex = hand.IndexOf(cardValue, thirdOccurrenceIndex + 1);
                        if (fourthOccurrenceIndex >= 0)
                        {
                            var fifthOccurrenceIndex = hand.IndexOf(cardValue, fourthOccurrenceIndex + 1);
                            if (fifthOccurrenceIndex >= 0)
                            {
                                maxValue = cardValue;
                                maxOccurrences = 5;
                            }
                            else
                            {
                                maxValue = cardValue;
                                maxOccurrences = 4;
                            }
                        }
                        else
                        {
                            maxValue = cardValue;
                            maxOccurrences = 3;
                        }
                        break;
                    }
                    else
                    {
                        maxValue = cardValue;
                        maxOccurrences = 2;
                    }
                }
            }

            var jInstances = 0;
            var cardsChecked = 0;

            while (cardsChecked < 5)
            {
                var jIndex = hand.IndexOf('J', cardsChecked);
                if (jIndex != -1)
                {
                    jInstances++;
                    cardsChecked = jIndex + 1;
                }
                else
                {
                    cardsChecked = 5;
                }
            }

            if (maxValue != 'J')
            {
                maxOccurrences += jInstances;
            }
            else if(jInstances > 0)
            {
                var strippedHand = hand.Replace("J", "");
                if (strippedHand.Length > 0)
                {
                    char otherValue;
                    int otherOccurrences = 0;
                    (otherValue, otherOccurrences) = GetMaxOccurringValueInHand(strippedHand);
                    maxOccurrences = otherOccurrences + jInstances;
                }
            }

            return (maxValue, maxOccurrences);
        }
    }
}
