using System;
using System.Threading;

public class Stopwatch
{
    public TimeSpan TimeElapsed { get; private set; } = TimeSpan.Zero;
    public bool IsRunning { get; private set; } = false;

    // Define the event handler delegate
    public delegate void StopwatchEventHandler(string message);

    // Events
    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    private Timer _timer;

    // Start Method
    public void Start()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
            _timer = new Timer(Tick, null, 0, 1000); // Timer to call Tick every second
        }
        else
        {
            Console.WriteLine("Stopwatch is already running!");
        }
    }

    // Stop Method
    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            _timer?.Dispose();
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
        else
        {
            Console.WriteLine("Stopwatch is not running!");
        }
    }

    // Reset Method
    public void Reset()
    {
        TimeElapsed = TimeSpan.Zero;
        OnReset?.Invoke("Stopwatch Reset!");
        Console.WriteLine("Time Elapsed: 00:00:00");
    }

    // Tick method: increments the time
    private void Tick(object state)
    {
        TimeElapsed = TimeElapsed.Add(TimeSpan.FromSeconds(1));
        Console.Clear();
        Console.WriteLine($"Time Elapsed: {TimeElapsed}");
        Console.WriteLine("Press S to Start | T to Stop | R to Reset | Q to Quit");
    }
}

class Program
{
    static void Main()
    {
        // Stopwatch instance
        Stopwatch stopwatch = new Stopwatch();

        // Event Handlers
        stopwatch.OnStarted += message => Console.WriteLine(message);
        stopwatch.OnStopped += message => Console.WriteLine(message);
        stopwatch.OnReset += message => Console.WriteLine(message);

        // Console User Interface
        Console.WriteLine("Stopwatch Application");
        Console.WriteLine("Press S to Start | T to Stop | R to Reset | Q to Quit");

        while (true)
        {
            var input = Console.ReadKey(true).Key; // Reads key press
            switch (input)
            {
                case ConsoleKey.S:
                    stopwatch.Start();
                    break;
                case ConsoleKey.T:
                    stopwatch.Stop();
                    break;
                case ConsoleKey.R:
                    stopwatch.Reset();
                    break;
                case ConsoleKey.Q:
                    stopwatch.Stop(); // Stops the timer before quitting
                    Console.WriteLine("Exiting application...");
                    return;
                default:
                    Console.WriteLine("Invalid key. Use S, T, R, or Q.");
                    break;
            }
        }
    }
}


