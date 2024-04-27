using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = int.Parse(Console.ReadLine());
        string letterGrade = "";
        if (gradePercentage >= 90)
        {
            letterGrade = "A";
        }
        else if (gradePercentage >= 80)
        {
            letterGrade = "B";
        }
        else if (gradePercentage >= 70)
        {
            letterGrade = "C";
        }
        else if (gradePercentage >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }
        bool passed = gradePercentage >= 70;
        string sign = "";
        int lastDigit = gradePercentage % 10;
        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3 || gradePercentage == 100)
        {
            sign = "-";
        }
        if (letterGrade == "A" && gradePercentage == 100)
        {
            letterGrade = "A+";
            sign = "";
        }
        else if (letterGrade == "F")
        {
            sign = "";
        }
        Console.WriteLine($"Your grade is: {letterGrade}{sign}");
        if (passed)
        {
            Console.WriteLine("Congrats! You passed the course, keep it up!");
        }
        else
        {
            Console.WriteLine("Better luck next time! Im sure you will do it.");
        }
    }
}
