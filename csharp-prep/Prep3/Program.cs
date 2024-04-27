using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int magicNumber = random.Next(1, 101); 
        int guess;
        int attempts = 0;
        Console.WriteLine("Welcome to Guess My Number Game!");
        Console.WriteLine("I've picked a number between 1 and 100. Will you have the luck to find it in one try?");
        do
        {
            Console.Write("Enter your guess: ");
            guess = int.Parse(Console.ReadLine());
            attempts++;
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine($"Good job! The magic number was {magicNumber}.");
            }
        } while (guess != magicNumber);
        Console.WriteLine($"You tried really hard this ammount of times {attempts}.");
        Console.Write("Do you want to play again? (yes/no): ");
        string playAgain = Console.ReadLine().ToLower();

        if (playAgain == "yes")
        {
            Main(args);
        }
        else
        {
            Console.WriteLine("Thank you for playing! See you next time!");
        }
    }
}
