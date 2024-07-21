namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private int _player1Score = 0;
        private int _player2Score = 0;
        private string player1Name;
        private string player2Name;

        public TennisGame1(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == this.player1Name)
            {
                _player1Score += 1;
            }
            else
            {
                _player2Score += 1;
            }
        }

        public string GetScore()
        {
            string score = "";
            var tempScore = 0;
            if (_player1Score == _player2Score)
            {
                score = _player1Score switch
                {
                    0 => "Love-All",
                    1 => "Fifteen-All",
                    2 => "Thirty-All",
                    _ => "Deuce",
                };
            }
            else if (_player1Score >= 4 || _player2Score >= 4)
            {
                var minusResult = _player1Score - _player2Score;

                score = minusResult switch
                {
                    1 => "Advantage player1",
                    -1 => "Advantage player2",
                    >2 => "Win for player1",
                    _ => "Win for player2"
                };
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    if (i == 1)
                    {
                        tempScore = _player1Score;
                    }
                    else
                    {
                        score += "-";
                        tempScore = _player2Score;
                    }

                    score += tempScore switch
                    {
                        0 => "Love",
                        1 => "Fifteen",
                        2 => "Thirty",
                        3 => "Forty"
                    };
                }
            }
            return score;
        }
    }
}

