using DartScoreKeeper.ViewModels;
using DartScoreKeeper.Models;

namespace DartScoreKeeper.Views;

public partial class GamePage : ContentPage
{
	private readonly GameViewModel _viewModel;

	public GamePage(GameViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
		
		// Set up dartboard interaction
		SetupDartboard();
	}

	private void SetupDartboard()
	{
		// Add tap gesture recognizer to dartboard
		var tapGesture = new TapGestureRecognizer();
		tapGesture.Tapped += OnDartboardTapped;
		DartboardLayout.GestureRecognizers.Add(tapGesture);
	}

	private void OnDartboardTapped(object? sender, TappedEventArgs e)
	{
		// Get tap position relative to dartboard
		var point = e.GetPosition(DartboardLayout);
		
		if (point.HasValue)
		{
			// Calculate which section was hit based on position
			var dartScore = CalculateDartScoreFromPosition(point.Value);
			if (dartScore != null)
			{
				_viewModel.ProcessDartHitCommand.Execute(dartScore);
			}
		}
	}

	private DartScore? CalculateDartScoreFromPosition(Point position)
	{
		// Get dartboard dimensions
		double width = DartboardLayout.Width;
		double height = DartboardLayout.Height;
		
		if (width <= 0 || height <= 0) return null;
		
		// Calculate center and relative position
		double centerX = width / 2;
		double centerY = height / 2;
		double dx = position.X - centerX;
		double dy = position.Y - centerY;
		
		// Calculate distance from center and angle
		double distance = Math.Sqrt(dx * dx + dy * dy);
		double radius = Math.Min(width, height) / 2;
		
		// If outside dartboard
		if (distance > radius * 0.95)
			return new DartScore(0, 1); // Miss
		
		// Calculate angle (0 degrees is at top, increases clockwise)
		double angle = Math.Atan2(dx, -dy) * 180 / Math.PI;
		if (angle < 0) angle += 360;
		
		// Determine which number based on angle
		// Dartboard numbers in clockwise order starting from top
		int[] numbers = { 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5 };
		double sectorAngle = 360.0 / 20;
		int sectorIndex = (int)((angle + sectorAngle / 2) / sectorAngle) % 20;
		int number = numbers[sectorIndex];
		
		// Determine multiplier based on distance from center
		double normalizedDistance = distance / radius;
		
		if (normalizedDistance < 0.05)
			return new DartScore(25, 2); // Double bull
		else if (normalizedDistance < 0.15)
			return new DartScore(25, 1); // Single bull (outer bull)
		else if (normalizedDistance < 0.4)
			return new DartScore(number, 1); // Single inner
		else if (normalizedDistance < 0.5)
			return new DartScore(number, 3); // Triple
		else if (normalizedDistance < 0.85)
			return new DartScore(number, 1); // Single outer
		else if (normalizedDistance < 0.95)
			return new DartScore(number, 2); // Double
		else
			return new DartScore(0, 1); // Miss
	}

	private void OnScoreClicked(object sender, EventArgs e)
	{
		if (sender is Button button && button.Text != null)
		{
			var dartScore = ParseButtonText(button.Text);
			if (dartScore != null)
			{
				_viewModel.ProcessDartHitCommand.Execute(dartScore);
			}
		}
	}

	private void OnMissClicked(object sender, EventArgs e)
	{
		_viewModel.ProcessDartHitCommand.Execute(new DartScore(0, 1));
	}

	private void OnBullClicked(object sender, EventArgs e)
	{
		_viewModel.ProcessDartHitCommand.Execute(new DartScore(25, 2));
	}

	private void OnOuterBullClicked(object sender, EventArgs e)
	{
		_viewModel.ProcessDartHitCommand.Execute(new DartScore(25, 1));
	}

	private DartScore? ParseButtonText(string text)
	{
		if (text.Length < 2) return null;
		
		char multiplierChar = text[0];
		string numberStr = text.Substring(1);
		
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
		
		return null;
	}
}
