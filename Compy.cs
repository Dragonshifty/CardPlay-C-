using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPlayConsole
{
    public class Compy
    {
        private List<Card> compyHand = new List<Card>();
        private Card cardInPlay;

        public List<Card> CompyHand
        {
            set { compyHand = value; }
        }

        public Card CardInPlay
        {
            set { cardInPlay = value; }
        }

        public void ShowCompyHand()
        {
            Console.WriteLine("Compy hand:");
            int count = 1;
            foreach (Card card in compyHand)
            {
                Console.WriteLine($"{count}: {card}");
                count++;
            }
        }

        public int[] PossibleScores()
        {
            // Return array with compy card played index and card score
            int[] returnInfo = new int[2];
            int[] scoreTemp = new int[5];
            int highLowDraw;
            int differenceScore = 0;
            bool checkSuit = false;
            int index = 0;


            for (int i = 0; i < compyHand.Count(); i++)
            {
                highLowDraw = compyHand[i].CompareTo(cardInPlay);
                switch (highLowDraw)
                {
                    case 0:
                        scoreTemp[i] = 0;
                        break;
                    case 1:
                        differenceScore = Card.GetDifference(compyHand[i], cardInPlay);
                        checkSuit = Card.CheckSuitMatch(compyHand[i], cardInPlay);
                        if (checkSuit)
                        {
                            scoreTemp[i] = differenceScore * 2;
                        }
                        else
                        {
                            scoreTemp[i] = differenceScore;
                        }
                        break;
                    case -1:
                        differenceScore = Card.GetDifference(compyHand[i], cardInPlay);
                        checkSuit = Card.CheckSuitMatch(compyHand[i], cardInPlay);
                        if (checkSuit)
                        {
                            scoreTemp[i] = 0;
                        }
                        else
                        {
                            scoreTemp[i] = -differenceScore;
                        }
                        break;
                }
            }
            int max = scoreTemp[0];
            for (int i = 0; i < scoreTemp.Count(); i++)
            {
                if (max < scoreTemp[i])
                {
                    max = scoreTemp[i];
                    index = i;
                }
            }
            Console.WriteLine($"Computer card played: {compyHand[index]}");

            returnInfo[0] = index;
            returnInfo[1] = scoreTemp[index];
            return returnInfo;
        }
    }
}
