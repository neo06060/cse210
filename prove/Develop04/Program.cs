using System;
using System.Threading;
using System.Transactions;

abstract class BaseActivity
{
    protected string name;
    protected string description;
    
    public BaseActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
    public void StartActivity(int duration)
    {
        Console.WriteLine($"\nStarting {name}...");
        Console.WriteLine(description);
        Console.WriteLine($"duration set to: {duration} seconds");
        PerpareToBegin();
        PerformActivity(duration);
        EndActivity();
    }

    protected void PerpareToBegin()
    {
        Console.WriteLine("\nGet ready to begin...");
        Thread.Sleep(3000);
    }
    protected void  EndActivity()
    {
        Console.WriteLine("\nGood Job! you have completed the activity.");
        Console.WriteLine($"you have completed {name}.");
        Thread.Sleep(3000);
    }
    protected abstract void PerformActivity(int duration);
}
class BreathingActivity : BaseActivity
{
    public BreathingActivity() : base("breathing activity", "this activity will help you relax and focus on yourself")
    {
    }

    protected override void PerformActivity(int duration)
    {
        var startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            BreatheIn();
            BreatheOut();
        }
    }

    private void BreatheIn()
    {
        Console.WriteLine("\nBreathe in...");
        Thread.Sleep(3000);
    }

    private void BreatheOut()
    {
        Console.WriteLine("\nBreathe out...");
        Thread.Sleep(3000);
    }
}

class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathingActivity = new BreathingActivity();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Quit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter duration of the activity in seconds: ");
                int duration = int.Parse(Console.ReadLine());
                breathingActivity.StartActivity(duration);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Exiting program...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter again.");
            }
        }
    }
}