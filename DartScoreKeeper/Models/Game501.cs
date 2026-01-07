namespace DartScoreKeeper.Models;

public class Game501 : Game
{
    private int _turnScore;
    
    public Game501(List<Player> players) : base(GameType.Game501, players)
    {
        // Initialize each player with 501 points
        foreach (var player in Players)
        {
            player.Score = 501;
        }
        _turnScore = 0;
    }
    
    public override void ProcessDartScore(int score, int multiplier)
    {
        int points = score * multiplier;
        _turnScore += points;
        CurrentPlayer.DartsThrown++;
        
        // Check if this would take player below 0 or to exactly 1
        int newScore = CurrentPlayer.Score - _turnScore;
        
        if (CurrentDartInTurn == 3 || newScore == 0)
        {
            // End of turn
            if (newScore >= 0 && newScore != 1)
            {
                // Valid turn
                CurrentPlayer.Score = newScore;
                CurrentPlayer.TurnScores.Add(_turnScore);
                
                if (CheckWinCondition())
                {
                    IsGameOver = true;
                    Winner = CurrentPlayer;
                }
            }
            // If invalid (below 0 or exactly 1), turn score is reset and not applied
            
            _turnScore = 0;
            NextPlayer();
        }
        else
        {
            // Continue turn
            NextDart();
        }
    }
    
    public override bool CheckWinCondition()
    {
        return CurrentPlayer.Score == 0;
    }
}
