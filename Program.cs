namespace CardPlayConsole
{
    internal class Program
    {
        private static Card one;
        private static Card two;
        static void Main(string[] args)
        {
            
            LinkedList<Card> deck = new LinkedList<Card>(Card.GetPack()); 
            foreach (Card card in deck)
            {
                if (card.GetCardNumber() == 2)
                {
                    one = card;
                } else if (card.GetCardNumber() == 4)
                {
                    two = card;
                }
                
            }

            Console.WriteLine($"{one} vs {two}");
            Console.WriteLine(one.CompareTo(two));
            Console.WriteLine(Card.CheckSuitMatch(one, two));


            
            
        }
    }
}