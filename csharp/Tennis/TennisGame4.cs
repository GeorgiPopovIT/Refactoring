namespace Tennis;

public class TennisGame4 : ITennisGame
{
    internal int ServerScore { get; set; }
    internal int ReceiverScore { get; set; }

    internal readonly string _server;
    internal readonly string _receiver;

    public TennisGame4(string player1Name, string player2Name)
    {
        _server = player1Name;
        _receiver = player2Name;
        this.ServerScore = 0;
        this.ReceiverScore = 0;
    }

    public void WonPoint(string playerName)
    {
        if (this._server.Equals(playerName))
        {
            this.ServerScore += 1;
        }
        else
        {
            this.ReceiverScore += 1;
        }
    }

    public string GetScore()
    {
        TennisResult result = new Deuce(
            this, new GameServer(
                this, new GameReceiver(
                    this, new AdvantageServer(
                        this, new AdvantageReceiver(
                            this, new DefaultResult(this)))))).GetResult();
        return result.Format();
    }

    internal bool ReceiverHasAdvantage()
        => this.ReceiverScore >= 4
            && (this.ReceiverScore - this.ServerScore) == 1;

    internal bool ServerHasAdvantage()
      => this.ServerScore >= 4
        && (this.ServerScore - this.ReceiverScore) == 1;

    internal bool ReceiverHasWon()
        => this.ReceiverScore >= 4
        && (this.ReceiverScore - this.ServerScore) >= 2;

    internal bool ServerHasWon()
        => this.ServerScore >= 4
      && (this.ServerScore - ReceiverScore) >= 2;

    internal bool IsDeuce()
        => this.ServerScore >= 3
        && this.ReceiverScore >= 3
        && (this.ServerScore == ReceiverScore);
}

internal class TennisResult
{
    readonly string _serverScore;
    readonly string _receiverScore;

    public TennisResult(string serverScore, string receiverScore)
    {
        this._serverScore = serverScore;
        this._receiverScore = receiverScore;
    }

    internal string Format()
    {
        if (string.IsNullOrWhiteSpace(this._receiverScore))
        {
            return this._serverScore;
        }
        if (this._serverScore.Equals(_receiverScore))
        {
            return $"{this._serverScore}-All";
        }
        return $"{this._serverScore}-{this._receiverScore}";
    }
}

internal interface IResultProvider
{
    TennisResult GetResult();
}

internal class Deuce : IResultProvider
{
    private readonly TennisGame4 _game;
    private readonly IResultProvider _nextResult;

    public Deuce(TennisGame4 game, IResultProvider nextResult)
    {
        this._game = game;
        this._nextResult = nextResult;
    }

    public TennisResult GetResult()
    {
        if (this._game.IsDeuce())
        {
            return new("Deuce", string.Empty);
        }
        return this._nextResult.GetResult();
    }
}

internal class GameServer : IResultProvider
{
    private readonly TennisGame4 _game;
    private readonly IResultProvider _nextResult;

    public GameServer(TennisGame4 game, IResultProvider nextResult)
    {
        this._game = game;
        this._nextResult = nextResult;
    }

    public TennisResult GetResult()
    {
        if (this._game.ServerHasWon())
        {
            return new($"Win for {this._game._server}", string.Empty);
        }
        return this._nextResult.GetResult();
    }
}

internal class GameReceiver : IResultProvider
{
    private readonly TennisGame4 _game;
    private readonly IResultProvider _nextResult;

    public GameReceiver(TennisGame4 game, IResultProvider nextResult)
    {
        _game = game;
        _nextResult = nextResult;
    }

    public TennisResult GetResult()
    {
        if (this._game.ReceiverHasWon())
        {
            return new($"Win for {this._game._receiver}", string.Empty);
        }
        return this._nextResult.GetResult();
    }
}

internal class AdvantageServer : IResultProvider
{
    private readonly TennisGame4 _game;
    private readonly IResultProvider _nextResult;

    public AdvantageServer(TennisGame4 game, IResultProvider nextResult)
    {
        this._game = game;
        this._nextResult = nextResult;
    }

    public TennisResult GetResult()
    {
        if (this._game.ServerHasAdvantage())
        {
            return new($"Advantage {this._game._server}", string.Empty);
        }
        return this._nextResult.GetResult();
    }
}

internal class AdvantageReceiver : IResultProvider
{

    private readonly TennisGame4 _game;
    private readonly IResultProvider _nextResult;

    public AdvantageReceiver(TennisGame4 game, IResultProvider nextResult)
    {
        this._game = game;
        this._nextResult = nextResult;
    }

    public TennisResult GetResult()
    {
        if (this._game.ReceiverHasAdvantage())
        {
            return new TennisResult($"Advantage {this._game._receiver}", string.Empty);
        }
        return this._nextResult.GetResult();
    }
}

internal class DefaultResult : IResultProvider
{

    private readonly string[] _scores;

    private readonly TennisGame4 _game;

    public DefaultResult(TennisGame4 game)
    {
        this._game = game;
        this._scores = ["Love", "Fifteen", "Thirty", "Forty"];
    }

    public TennisResult GetResult()
        => new(_scores[_game.ServerScore], _scores[_game.ReceiverScore]);
}
