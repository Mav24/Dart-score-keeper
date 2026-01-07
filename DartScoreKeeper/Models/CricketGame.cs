namespace DartScoreKeeper.Models;

public class CricketGame : Game
{
    public CricketGame(List<Player> players) : base(GameType.Cricket, players)
    {
    }
    
    public override void ProcessDartScore(int score, int multiplier)
    {
        // Cricket only counts 15-20 and Bulls (25)
        if ((score >= 15 && score <= 20) || score == 25)
        {
            int hits = multiplier;
            int previousHits = CurrentPlayer.CricketScores[score];
            CurrentPlayer.CricketScores[score] += hits;
            CurrentPlayer.DartsThrown++;
            
            // If player now has more than 3 hits and this number is not closed by all other players,
            // they score points for the extra hits from THIS turn only
            if (CurrentPlayer.CricketScores[score] > 3 && previousHits >= 3)
            {
                // Already had 3+, so all new hits score
                int extraHits = hits;
                bool allOthersClosed = true;
                
                foreach (var player in Players)
                {
                    if (player != CurrentPlayer && player.CricketScores[score] < 3)
                    {
                        allOthersClosed = false;
                        break;
                    }
                }
                
                if (!allOthersClosed)
                {
                    CurrentPlayer.Score += score * extraHits;
                }
            }
            else if (CurrentPlayer.CricketScores[score] > 3 && previousHits < 3)
            {
                // Just closed this turn, score only the excess
                int extraHits = CurrentPlayer.CricketScores[score] - 3;
                bool allOthersClosed = true;
                
                foreach (var player in Players)
                {
                    if (player != CurrentPlayer && player.CricketScores[score] < 3)
                    {
                        allOthersClosed = false;
                        break;
                    }
                }
                
                if (!allOthersClosed)
                {
                    CurrentPlayer.Score += score * extraHits;
                }
            }
        }
        else
        {
            CurrentPlayer.DartsThrown++;
        }
        
        if (CheckWinCondition())
        {
            IsGameOver = true;
            Winner = CurrentPlayer;
        }
        
        NextDart();
    }
    
    public override bool CheckWinCondition()
    {
        // Check if current player has closed all numbers (15-20 and 25)
        bool allClosed = true;
        foreach (var key in CurrentPlayer.CricketScores.Keys)
        {
            if (CurrentPlayer.CricketScores[key] < 3)
            {
                allClosed = false;
                break;
            }
        }
        
        if (!allClosed) return false;
        
        // Check if current player has highest or equal score
        foreach (var player in Players)
        {
            if (player != CurrentPlayer && player.Score > CurrentPlayer.Score)
            {
                return false;
            }
        }
        
        return true;
    }
}
