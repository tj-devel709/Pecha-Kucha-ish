using System.Timers;

namespace RapidFire;

public partial class MainPage : ContentPage
{
	System.Timers.Timer timer;
	List<string> imageNames;

	List<int> usedIndexes = new List<int>();

	int timerCount = 20;

	public MainPage()
	{
		InitializeComponent();
		timer = new System.Timers.Timer(1000); // Set the timer interval to 20 seconds
		timer.Elapsed += OnTimedEvent;
		timer.AutoReset = true;
		timer.Enabled = false; // Initially, the timer is disabled
		imageNames = new List<string> ();
		for (int i = 1; i <= 36; i++){
			imageNames.Add("image" + i + ".jpg");
		}
	}

    private void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
		timerCount--;
		Console.WriteLine($"{timerCount}");
		if (timerCount == 0)
		{
			timerCount = 20;
			AssignNewImage();
		}
		Dispatcher.Dispatch(() =>
		{
			TimerLabel.Text = timerCount.ToString();
		});
    }

	void AssignNewImage()
	{

		var rando = new Random().Next(0, imageNames.Count);
		while (usedIndexes.Contains(rando)){
			rando = new Random().Next(0, imageNames.Count);
		}
		usedIndexes.Add(rando);

		Dispatcher.Dispatch(() =>
		{
			MainImage.Source = ImageSource.FromFile(imageNames[rando]);
		});
		Console.WriteLine($"{rando}");
		Console.WriteLine($"{MainImage.Source}");
	}

    private void OnStartClicked(object sender, EventArgs e)
	{
		timer.Enabled = true; // Start the timer
		StartButton.IsEnabled = false; // Disable the Start button
		StopButton.IsEnabled = true; // Enable the Stop button
		AssignNewImage();

		Dispatcher.Dispatch(() =>
		{
			InstructionsLabel.IsVisible = false;
		});
	}
	
	private void OnStopClicked(object sender, EventArgs e)
	{
		timer.Enabled = false; // Stop the timer
		StartButton.IsEnabled = true; // Enable the Start button
		StopButton.IsEnabled = false; // Disable the Stop button

		MainImage.Source = null;
		usedIndexes.Clear();
		timerCount = 20;

		Dispatcher.Dispatch(() =>
		{
			TimerLabel.Text = timerCount.ToString();
			InstructionsLabel.IsVisible = true;
		});
	}
}
