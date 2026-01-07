using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DartScoreKeeper.Models;
using System.Collections.ObjectModel;

namespace DartScoreKeeper.ViewModels;

public partial class GameViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private Game? currentGame;
    
    [ObservableProperty]
    private ObservableCollection<Player> players = new();
    
    [ObservableProperty]
    private Player? currentPlayer;
    
    [ObservableProperty]
    private string currentDartText = "Dart 1 of 3";
    
    [ObservableProperty]
    private bool isGameOver;
    
    [ObservableProperty]
    private string gameTitle = "";
    
    [ObservableProperty]
    private ObservableCollection<DartScore> currentTurnScores = new();
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Players") && query.ContainsKey("GameType"))
        {
            var playerList = (List<Player>)query["Players"];
            var gameType = (GameType)query["GameType"];
            
            Players = new ObservableCollection<Player>(playerList);
            
            // Create the appropriate game
            CurrentGame = gameType switch
            {
                GameType.Game501 => new Game501(playerList),
                GameType.Cricket => new CricketGame(playerList),
                GameType.AroundTheClock => new AroundTheClockGame(playerList),
                GameType.Killer => new KillerGame(playerList),
                _ => new Game501(playerList)
            };
            
            GameTitle = gameType switch
            {
                GameType.Game501 => "501",
                GameType.Cricket => "Cricket",
                GameType.AroundTheClock => "Around the Clock",
                GameType.Killer => "Killer",
                _ => "Dart Game"
            };
            
            CurrentPlayer = CurrentGame.CurrentPlayer;
            UpdateDartText();
        }
    }
    
    [RelayCommand]
    private void ProcessDartHit(DartScore dartScore)
    {
        if (CurrentGame == null || IsGameOver) return;
        
        CurrentTurnScores.Add(dartScore);
        CurrentGame.ProcessDartScore(dartScore.Number, dartScore.Multiplier);
        
        // Update observable properties
        CurrentPlayer = CurrentGame.CurrentPlayer;
        IsGameOver = CurrentGame.IsGameOver;
        UpdateDartText();
        
        // Refresh player scores
        OnPropertyChanged(nameof(Players));
        
        // Clear turn scores if new turn
        if (CurrentGame.CurrentDartInTurn == 1)
        {
            CurrentTurnScores.Clear();
        }
        
        if (IsGameOver && CurrentGame.Winner != null)
        {
            Shell.Current.DisplayAlert("Game Over", 
                $"{CurrentGame.Winner.Name} wins!", "OK");
        }
    }
    
    [RelayCommand]
    private void RegisterDartboardHit(string position)
    {
        // This will be called from the dartboard UI
        // Parse the position and create a DartScore
        var dartScore = ParseDartboardPosition(position);
        if (dartScore != null)
        {
            ProcessDartHit(dartScore);
        }
    }
    
    private DartScore? ParseDartboardPosition(string position)
    {
        // This is a placeholder - actual implementation will depend on dartboard UI
        // Format expected: "T20", "D16", "S5", "BULL", "OUTER"
        if (position == "MISS")
            return new DartScore(0, 1);
        
        if (position == "BULL")
            return new DartScore(25, 2);
        
        if (position == "OUTER")
            return new DartScore(25, 1);
        
        // Parse format like "T20", "D16", "S5"
        if (position.Length >= 2)
        {
            char multiplierChar = position[0];
            string numberStr = position.Substring(1);
            
            if (int.TryParse(numberStr, out int number))
            {
                int multiplier = multiplierChar switch
                {
                    'T' => 3,
                    'D' => 2,
                    'S' => 1,
                    _ => 1
                };
                
                return new DartScore(number, multiplier);
            }
        }
        
        return null;
    }
    
    private void UpdateDartText()
    {
        if (CurrentGame != null)
        {
            CurrentDartText = $"Dart {CurrentGame.CurrentDartInTurn} of 3";
        }
    }
    
    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
