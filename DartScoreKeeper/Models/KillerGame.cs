namespace DartScoreKeeper.Models;

public class KillerGame : Game
{
    private Dictionary<Player, int> _playerTargets = new();
    private Dictionary<Player, int> _playerLives = new();
    
    public KillerGame(List<Player> players) : base(GameType.Killer, players)
    {
        // Each player starts with 3 lives
        // Players need to establish their "killer number" first
        foreach (var player in Players)
        {
            _playerLives[player] = 3;
            _playerTargets[player] = 0; // 0 means no target established yet
        }
    }
    
    public int GetPlayerLives(Player player) => _playerLives[player];
    public int GetPlayerTarget(Player player) => _playerTargets[player];
    
    public override void ProcessDartScore(int score, int multiplier)
    {
        CurrentPlayer.DartsThrown++;
        
        // If player hasn't established their target yet
        if (_playerTargets[CurrentPlayer] == 0)
        {
            // They need to hit a double to establish their number
            if (multiplier == 2 && score >= 1 && score <= 20)
            {
                // Check if this number is not already taken
                bool taken = false;
                foreach (var target in _playerTargets.Values)
                {
                    if (target == score)
                    {
                        taken = true;
                        break;
                    }
                }
                
                if (!taken)
                {
                    _playerTargets[CurrentPlayer] = score;
                    CurrentPlayer.Score = score; // Store in Score for display
                }
            }
        }
        else
        {
            // Player has a target, now they can attack others
            if (multiplier == 2 && score >= 1 && score <= 20)
            {
                // Check if this double belongs to another player
                foreach (var player in Players)
                {
                    if (player != CurrentPlayer && _playerTargets[player] == score)
                    {
                        _playerLives[player]--;
                        
                        if (_playerLives[player] <= 0)
                        {
                            // Player is eliminated
                            _playerLives[player] = 0;
                        }
                        break;
                    }
                }
            }
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
        // Count how many players still have lives
        int playersAlive = 0;
        Player? lastPlayerAlive = null;
        
        foreach (var player in Players)
        {
            if (_playerLives[player] > 0)
            {
                playersAlive++;
                lastPlayerAlive = player;
            }
        }
        
        if (playersAlive == 1)
        {
            Winner = lastPlayerAlive;
            return true;
        }
        
        return false;
    }
}
