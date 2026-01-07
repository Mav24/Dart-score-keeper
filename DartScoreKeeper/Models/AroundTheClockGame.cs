namespace DartScoreKeeper.Models;

public class AroundTheClockGame : Game
{
    public AroundTheClockGame(List<Player> players) : base(GameType.AroundTheClock, players)
    {
        // Players start at 1 and need to hit 1-20 in order
        foreach (var player in Players)
        {
            player.Score = 1; // Current number they need to hit
        }
    }
    
    public override void ProcessDartScore(int score, int multiplier)
    {
        CurrentPlayer.DartsThrown++;
        
        // Check if the score matches the current target number
        if (score == CurrentPlayer.Score)
        {
            CurrentPlayer.Score++; // Move to next number
            CurrentPlayer.TurnScores.Add(score);
            
            if (CheckWinCondition())
            {
                IsGameOver = true;
                Winner = CurrentPlayer;
            }
        }
        
        NextDart();
    }
    
    public override bool CheckWinCondition()
    {
        // Player wins when they reach 21 (hit all numbers 1-20)
        return CurrentPlayer.Score > 20;
    }
}
