using DartScoreKeeper.ViewModels;
using DartScoreKeeper.Models;

namespace DartScoreKeeper.Views;

public partial class MainPage : ContentPage
{
	private readonly MainViewModel _viewModel;

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	private async void OnGame501Clicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Game501;
		await _viewModel.StartGameCommand.ExecuteAsync(null);
	}

	private async void OnCricketClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Cricket;
		await _viewModel.StartGameCommand.ExecuteAsync(null);
	}

	private async void OnAroundTheClockClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.AroundTheClock;
		await _viewModel.StartGameCommand.ExecuteAsync(null);
	}

	private async void OnKillerClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Killer;
		await _viewModel.StartGameCommand.ExecuteAsync(null);
	}
}
