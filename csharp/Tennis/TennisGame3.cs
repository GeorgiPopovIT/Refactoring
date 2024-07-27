namespace Tennis
{
    public class TennisGame3(string player1Name, string player2Name)
        : ITennisGame
    {
        private int player1;
        private int player2;
        private readonly string _player1Name = player1Name;
        private readonly string _player2Name = player2Name;

        public string GetScore()
        {
            if (this.IsNotDraw())
            {
                var score = Score(this.player1);

                return (this.player1 == this.player2)
                    ? $"{score}-All"
                    : $"{score}-{Score(player2)}";
            }
            else
            {
                if (this.player1 == this.player2)
                {
                    return "Deuce";
                }

                string s = this.player1 > this.player2
                     ? this._player1Name
                     : this._player2Name;

                return IsAdvantage()
                    ? $"Advantage {s}"
                    : $"Win for {s}";
            }
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
            {
                this.player1 += 1;
            }
            else
            {
                this.player2 += 1;
            }
        }
        private static string Score(int playerScore)
                => playerScore switch
                {
                    0 => "Love",
                    1 => "Fifteen",
                    2 => "Thirty",
                    3 => "Forty",
                    _ => ""
                };

        private bool IsAdvantage()
            => (this.player1 - this.player2) * (this.player1 - this.player2) == 1;

        private bool IsNotDraw()
            => (this.player1 < 4 && this.player2 < 4)
            && (this.player1 + this.player2 < 6);
    }
}