namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private int _player1Point;
        private int _player2Point;

        private string _player1Result;
        private string _player2Result;

        private string _player1Name;
        private string _player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this._player1Name = player1Name;
            this._player2Name = player2Name;
            this._player1Point = 0;
            this._player2Point = 0;
            this._player1Result = "";
            this._player2Result = "";
        }

        public string GetScore()
        {
            if (IsWinner(this._player1Point, this._player2Point))
            {
                return "Win for player1";
            }
            else if (IsWinner(this._player2Point, this._player1Point))
            {
                return "Win for player2";
            }

            if (IsAdvantage(this._player1Point, this._player2Point))
            {
                return "Advantage player1";
            }
            else if (IsAdvantage(this._player2Point, this._player1Point))
            {
                return "Advantage player2";
            }

            if (this._player1Point == this._player2Point && this._player1Point < 3)
            {
                return $"{Score(this._player1Point)}-All";
            }
            else if (this._player1Point == this._player2Point && this._player1Point > 2)
            {
                return "Deuce";
            }

            if (this._player1Point > 0 && this._player2Point == 0)
            {
                this._player1Result = Score(this._player1Point);

                this._player2Result = "Love";
            }
            else 
            {
                this._player2Result = Score(this._player2Point);

                this._player1Result = "Love";
            }

            this._player1Result = Score(this._player1Point);
            this._player2Result = Score(this._player2Point);

            return $"{this._player1Result}-{this._player2Result}";
        }

        public void WonPoint(string player)
        {
            if (player == this._player1Name)
            {
                PlayerScore(ref this._player1Point);
            }
            else
            {
                PlayerScore(ref this._player2Point);
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
        private static void PlayerScore(ref int playerScore)
        {
            playerScore++;
        }
        private static bool IsAdvantage(int firstPlayerPoints, int secondPlayerPoints)
            => firstPlayerPoints > secondPlayerPoints && secondPlayerPoints >= 3;
        private static bool IsWinner(int firstPlayerPoints, int secondPlayerPoints)
            => firstPlayerPoints >= 4 && secondPlayerPoints >= 0
            && (firstPlayerPoints - secondPlayerPoints) >= 2;
    }
}