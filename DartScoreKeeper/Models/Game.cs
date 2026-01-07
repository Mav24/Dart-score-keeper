namespace DartScoreKeeper.Models;

public enum GameType
{
    Game501,
    Cricket,
    AroundTheClock,
    Killer
}

public abstract class Game
{
    public GameType Type { get; protected set; }
    public List<Player> Players { get; set; } = new();
    public int CurrentPlayerIndex { get; set; }
    public int CurrentDartInTurn { get; set; } // 1, 2, or 3
    public bool IsGameOver { get; set; }
    public Player? Winner { get; set; }
    
    protected Game(GameType type, List<Player> players)
    {
        Type = type;
        Players = players;
        CurrentPlayerIndex = 0;
        CurrentDartInTurn = 1;
        IsGameOver = false;
    }
    
    public Player CurrentPlayer => Players[CurrentPlayerIndex];
    
    public abstract void ProcessDartScore(int score, int multiplier);
    public abstract bool CheckWinCondition();
    
    public void NextDart()
    {
        CurrentDartInTurn++;
        if (CurrentDartInTurn > 3)
        {
            NextPlayer();
        }
    }
    
    public void NextPlayer()
    {
        CurrentDartInTurn = 1;
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
    }
}
