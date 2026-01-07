namespace DartScoreKeeper.Models;

public class DartScore
{
    public int Number { get; set; }
    public int Multiplier { get; set; } // 1 = single, 2 = double, 3 = triple
    public int TotalScore => Number * Multiplier;
    
    public DartScore(int number, int multiplier)
    {
        Number = number;
        Multiplier = multiplier;
    }
    
    public override string ToString()
    {
        string multiplierStr = Multiplier switch
        {
            2 => "D",
            3 => "T",
            _ => ""
        };
        
        return $"{multiplierStr}{Number}";
    }
}
