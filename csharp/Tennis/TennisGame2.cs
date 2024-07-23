namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private int _p1point;
        private int _p2point;

        private string _p1res = "";
        private string _p2res = "";
        private string _player1Name;
        private string _player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this._player1Name = player1Name;
            this._p1point = 0;
            this._player2Name = player2Name;
        }

        public string GetScore()
        {
            var score = "";
            if (_p1point == _p2point && _p1point < 3)
            {
                score += $"{Score(this._p1point)}-All";
            }
            if (_p1point == _p2point && _p1point > 2)
            {
                score = "Deuce";
            }

            if (_p1point > 0 && _p2point == 0)
            {
                // this._p1res = Score(this._p1point);
                if (_p1point == 1)
                {
                    _p1res = "Fifteen";
                }
                else if (_p1point == 2)
                {
                    _p1res = "Thirty";
                }
                else if (_p1point == 3)
                {
                    _p1res = "Forty";
                }


                _p2res = "Love";
                score = $"{this._p1res}-{this._p2res}";
            }
            if (_p2point > 0 && _p1point == 0)
            {
                if (_p2point == 1)
                {
                    _p2res = "Fifteen";
                }
                if (_p2point == 2)
                {
                    _p2res = "Thirty";
                }
                if (_p2point == 3)
                {
                    _p2res = "Forty";
                }

                _p1res = "Love";
                score = $"{this._p1res}-{this._p2res}";
            }

            if (_p1point > _p2point && _p1point < 4)
            {
                this._p1res = Score(_p1point);
                this._p2res = Score(_p2point);

                score = $"{this._p1res}-{this._p2res}";
            }
            if (_p2point > _p1point && _p2point < 4)
            {
                _p2res = Score(this._p2point);
                _p1res = Score(this._p1point);

                score = $"{this._p1res}-{this._p2res}";
            }

            if (_p1point > _p2point && _p2point >= 3)
            {
                score = "Advantage player1";
            }

            if (_p2point > _p1point && _p1point >= 3)
            {
                score = "Advantage player2";
            }

            if (_p1point >= 4 && _p2point >= 0 && (_p1point - _p2point) >= 2)
            {
                score = "Win for player1";
            }
            if (_p2point >= 4 && _p1point >= 0 && (_p2point - _p1point) >= 2)
            {
                score = "Win for player2";
            }
            return score;
        }

        public void SetP1Score(int number)
        {
            PlayerScore(ref this._p2point, number);
        }

        public void SetP2Score(int number)
        {
            PlayerScore(ref this._p2point, number);
        }

        public void WonPoint(string player)
        {
            if (player == this._player1Name)
            {
                PlayerScore(ref this._p1point, 1);
            }
            else
            {
                PlayerScore(ref this._p2point, 1);
            }
        }
        private static string Score(int playerScore)
              => playerScore switch
              {
                  0 => "Love",
                  1 => "Fifteen",
                  2 => "Thirty",
                  3 => "Forty"
              };
        private static void PlayerScore(ref int playerScore, int number)
        {
            playerScore += number;
        }
    }
}