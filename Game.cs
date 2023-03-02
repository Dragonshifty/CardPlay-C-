using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardPlayConsole
{
    public class Game
    {
        private List<Card> deck;
        private List<Card> cardPile = new List<Card>();
        private List<Card> playerHand = new List<Card>();
        private List<Card> compyHand= new List<Card>();
        private Card cardInPlay;
        private Card cardPlayed;

        public void GetNewDeck()
        {
            deck = new List<Card>(Card.GetPack());
            
        }

        public void GetPlayerHand()
        {
            for (int i = 0; i < 5; i++)
            {
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
            }
            playerHand.Sort();
        }

        public void GetCompyHand()
        {
            for (int i = 0; i < 5; i++)
            {
                compyHand.Add(deck[0]);
                deck.RemoveAt(0);
            }
        }

        public void GetNewCard(string whoIsPlaying)
        {
            if (whoIsPlaying == "Player" && deck.Count() > 0)
            {
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
                playerHand.Sort();
            }
            else if (whoIsPlaying == "Compy" && deck.Count() > 0)
            {
                compyHand.Add(deck[0]);
                deck.RemoveAt(0);
                compyHand.Sort();
            }
        }

        public void ShowPlayerHand()
        {
            Console.WriteLine("Player hand:");
            int count = 1;
            foreach (Card card in playerHand)
            {
                Console.WriteLine($"{count}: {card}");
                count++;
            }
            Console.WriteLine();
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
            Console.WriteLine();
        }

        public void ShowCardInPlay()
        {
            cardInPlay = cardPile[0];
            Console.WriteLine($"Card in play: {cardInPlay}\n");
        }

        public void DisplayScores()
        {
            Score score = Score.Instance;
            Console.WriteLine($"Player score: {score.PlayerScore}\nCompy score: {score.CompyScore}");
        }

        public void ResetGame()
        {
            if (deck != null) deck.Clear();
            if (cardPile != null) cardPile.Clear();
            playerHand.Clear();
            compyHand.Clear();

            GetNewDeck();
            GetPlayerHand();
            GetCompyHand();

            cardPile.Insert(0, deck[0]);
            deck.RemoveAt(0);
            cardInPlay = cardPile[0];
        }

        public void Play()
        {
            Console.WriteLine("Let's begin!\n");

            int selection = 0;
            bool proceed = false;
            ResetGame();

            do
            {
                while (!proceed)
                {      
                    ShowPlayerHand();
                    ShowCompyHand();                    
                    ShowCardInPlay();
                    DisplayScores();

                    try
                    {
                        Console.WriteLine("Which card do you wish to play?\n");
                        selection = Convert.ToInt32(Console.ReadLine()) - 1;
                        proceed = (selection > 0 && selection <= playerHand.Count()) ? true : false;
                    } catch
                    {
                        Console.WriteLine("Must be a number between 1 and 5");
                    }

                }
                proceed = false;

                cardInPlay = cardPile[0];

                cardPlayed = playerHand[selection];
                cardPile.Insert(0, playerHand[selection]);
                playerHand.RemoveAt(selection);

                GetNewCard("Player");

                int HighLowDraw = cardPlayed.CompareTo(cardInPlay);
                int difference = Card.GetDifference(cardPlayed, cardInPlay);
                bool isAMatch = Card.CheckSuitMatch(cardPlayed, cardInPlay);

                Score score = Score.Instance;

                switch (HighLowDraw)
                {
                    case 0:
                        Console.WriteLine("No Score change");
                        break;
                    case 1:
                        if (isAMatch)
                        {
                            difference *= 2;
                            Console.WriteLine($"Matching suit, double score: {difference}");
                            score.SetPlayerScore(difference);
                        }
                        else
                        {
                            Console.WriteLine($"Score: {difference}");
                            score.SetPlayerScore(difference);
                        }
                        break;
                    case -1:
                        if (isAMatch)
                        {
                            Console.WriteLine("Suit match, no score change");
                        }
                        else
                        {
                            Console.WriteLine($"Score: -{difference}");
                            score.SetPlayerScore(-difference);
                        }
                        break;
                }

                cardInPlay = cardPile[0];

                Compy compy = new Compy();
                compy.CompyHand = compyHand;
                compy.CardInPlay = cardInPlay;

                int[] compyCardPlayed = compy.PossibleScores();

                cardPile.Insert(0, compyHand[compyCardPlayed[0]]);
                compyHand.RemoveAt(compyCardPlayed[0]);

                GetNewCard("Compy");

                score.SetCompyScore(compyCardPlayed[1]);

            } while (deck.Count > 1);
        }
    }
}
