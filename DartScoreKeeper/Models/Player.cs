namespace DartScoreKeeper.Models;

public class Player
{
    public string Name { get; set; } = string.Empty;
    public int Score { get; set; }
    public int DartsThrown { get; set; }
    public List<int> TurnScores { get; set; } = new();
    public Dictionary<int, int> CricketScores { get; set; } = new(); // For Cricket game
    
    public Player(string name)
    {
        Name = name;
        Score = 0;
        DartsThrown = 0;
        
        // Initialize Cricket scores
        for (int i = 15; i <= 20; i++)
        {
            CricketScores[i] = 0;
        }
        CricketScores[25] = 0; // Bulls
    }
    
    public double GetAverage()
    {
        if (DartsThrown == 0) return 0;
        return TurnScores.Count > 0 ? TurnScores.Average() : 0;
    }
}
