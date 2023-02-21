using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPlayConsole
{
    public class Score
    {
        private static readonly Score instance = new Score();
        private int playerScore;
        private int compyScore;

        static Score() { }
        private Score() { }

        public static Score Instance
        { 
            get { return instance; } 
        }

        public int PlayerScore
        {
            get { return playerScore; }
        }

        public int CompyScore
        {
            get { return compyScore;}
        }

        public void SetPlayerScore(int score)
        {
            this.playerScore += score;
        }

        public void SetCompyScore(int score)
        {
            this.compyScore += score;
        }
    }
}
