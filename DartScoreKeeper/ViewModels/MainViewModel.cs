using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DartScoreKeeper.Models;
using System.Collections.ObjectModel;

namespace DartScoreKeeper.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private int numberOfPlayers = 2;
    
    [ObservableProperty]
    private string player1Name = "Player 1";
    
    [ObservableProperty]
    private string player2Name = "Player 2";
    
    [ObservableProperty]
    private string player3Name = "Player 3";
    
    [ObservableProperty]
    private string player4Name = "Player 4";
    
    [ObservableProperty]
    private GameType selectedGameType = GameType.Game501;
    
    public ObservableCollection<string> GameTypes { get; } = new()
    {
        "501",
        "Cricket",
        "Around the Clock",
        "Killer"
    };
    
    [RelayCommand]
    private async Task StartGame()
    {
        var players = new List<Player>();
        
        if (numberOfPlayers >= 1 && !string.IsNullOrWhiteSpace(player1Name))
            players.Add(new Player(player1Name));
        if (numberOfPlayers >= 2 && !string.IsNullOrWhiteSpace(player2Name))
            players.Add(new Player(player2Name));
        if (numberOfPlayers >= 3 && !string.IsNullOrWhiteSpace(player3Name))
            players.Add(new Player(player3Name));
        if (numberOfPlayers >= 4 && !string.IsNullOrWhiteSpace(player4Name))
            players.Add(new Player(player4Name));
        
        if (players.Count == 0)
        {
            await Shell.Current.DisplayAlert("Error", "Please enter at least one player name.", "OK");
            return;
        }
        
        var navigationParams = new Dictionary<string, object>
        {
            { "Players", players },
            { "GameType", selectedGameType }
        };
        
        await Shell.Current.GoToAsync(nameof(Views.GamePage), navigationParams);
    }
    
    [RelayCommand]
    private void IncreasePlayerCount()
    {
        if (numberOfPlayers < 4)
            NumberOfPlayers++;
    }
    
    [RelayCommand]
    private void DecreasePlayerCount()
    {
        if (numberOfPlayers > 1)
            NumberOfPlayers--;
    }
}
