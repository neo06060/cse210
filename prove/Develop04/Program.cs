using System;
using System.Threading;

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
        Console.WriteLine($"Duration set to: {duration} seconds");
        PrepareToBegin();
        PerformActivity(duration);
        EndActivity(duration);
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("\nGet ready to begin...");
        ShowSpinner(3);
    }

    protected void EndActivity(int duration)
    {
        Console.WriteLine("\nGood job! You have completed the activity.");
        Console.WriteLine($"You have completed {name} for {duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected abstract void PerformActivity(int duration);
}

class BreathingActivity : BaseActivity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding you through slow breathing.")
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
        ShowSpinner(3);
    }

    private void BreatheOut()
    {
        Console.WriteLine("\nBreathe out...");
        ShowSpinner(3);
    }
}

class ReflectionActivity : BaseActivity
{
    private static readonly string[] Prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] Questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        Random random = new Random();
        Console.WriteLine("\n" + Prompts[random.Next(Prompts.Length)]);
        var startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            Console.WriteLine(Questions[random.Next(Questions.Length)]);
            ShowSpinner(5);
        }
    }
}

class ListingActivity : BaseActivity
{
    private static readonly string[] Prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        Random random = new Random();
        Console.WriteLine("\n" + Prompts[random.Next(Prompts.Length)]);
        Console.WriteLine("You have a few seconds to start thinking...");
        ShowSpinner(5);

        int count = 0;
        var startTime = DateTime.Now;
        while (DateTime.Now - startTime < TimeSpan.FromSeconds(duration))
        {
            Console.Write("Enter an item: ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou have listed {count} items.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        BreathingActivity breathingActivity = new BreathingActivity();
        ReflectionActivity reflectionActivity = new ReflectionActivity();
        ListingActivity listingActivity = new ListingActivity();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

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
                Console.Write("Enter duration of the activity in seconds: ");
                int duration = int.Parse(Console.ReadLine());
                reflectionActivity.StartActivity(duration);
            }
            else if (choice == "3")
            {
                Console.Write("Enter duration of the activity in seconds: ");
                int duration = int.Parse(Console.ReadLine());
                listingActivity.StartActivity(duration);
            }
            else if (choice == "4")
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
