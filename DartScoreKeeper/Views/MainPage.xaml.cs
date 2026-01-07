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

	private void OnGame501Clicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Game501;
	}

	private void OnCricketClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Cricket;
	}

	private void OnAroundTheClockClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.AroundTheClock;
	}

	private void OnKillerClicked(object sender, EventArgs e)
	{
		_viewModel.SelectedGameType = GameType.Killer;
	}
}
