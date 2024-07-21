using static System.Formats.Asn1.AsnWriter;

namespace Tennis
{
    public class TennisGame1 : ITennisGame
    {
        private int _player1Score = 0;
        private int _player2Score = 0;
        private string _player1Name;
        private string _player2Name;

        public TennisGame1(string _player1Name, string _player2Name)
        {
            this._player1Name = _player1Name;
            this._player2Name = _player2Name;
        }

        public void WonPoint(string playerName)
        {
            if (playerName == this._player1Name)
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
            if (_player1Score == _player2Score)
            {
                return _player1Score switch
                {
                    0 => "Love-All",
                    1 => "Fifteen-All",
                    2 => "Thirty-All",
                    _ => "Deuce",
                };
            }
            else if (_player1Score >= 4 || _player2Score >= 4)
            {
                var scoreDifference = _player1Score - _player2Score;

                return scoreDifference switch
                {
                    1 => "Advantage player1",
                    -1 => "Advantage player2",
                    >= 2 => "Win for player1",
                    _ => "Win for player2"
                };
            }
            else
            {
                string score = $"{Score(this._player1Score)}-{Score(this._player2Score)}";
                return score;
            }
        }
        public static string Score(int playerScore)
                => playerScore switch
                {
                    0 => "Love",
                    1 => "Fifteen",
                    2 => "Thirty",
                    3 => "Forty"
                };
    }
}
