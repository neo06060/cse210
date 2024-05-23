using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace GoalTrackingApp
{
    public abstract class Goal
    {
        public string Name { get; private set; }
        public int Points { get; private set; }
        protected Goal(string name, int points)
        {
            Name = name;
            Points = points;
        }
        public abstract bool IsComplete();
        public abstract int RecordEvent();
        public abstract string Display();
    }
    public class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string name, int points) : base(name, points)
        {
            _completed = false;
        }
        public override bool IsComplete()
        {
            return _completed;
        }
        public override int RecordEvent()
        {
            if (!_completed)
            {
                _completed = true;
                return Points;
            }
            return 0;
        }
        public override string Display()
        {
            return $"[{(IsComplete() ? "X" : " ")}] {Name} (Points: {Points})";
        }
    }
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points)
        {
        }
        public override bool IsComplete()
        {
            return false;
        }
        public override int RecordEvent()
        {
            return Points;
        }

        public override string Display()
        {
            return $"[∞] {Name} (Points per record: {Points})";
        }
    }
    public class ChecklistGoal : Goal
    {
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public int BonusPoints { get; private set; }
        public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
        {
            TargetCount = targetCount;
            CurrentCount = 0;
            BonusPoints = bonusPoints;
        }
        public override bool IsComplete()
        {
            return CurrentCount >= TargetCount;
        }
        public override int RecordEvent()
        {
            if (!IsComplete())
            {
                CurrentCount++;
                if (IsComplete())
                {
                    return Points + BonusPoints;
                }
                return Points;
            }
            return 0;
        }
        public override string Display()
        {
            return $"[{(IsComplete() ? "X" : " ")}] {Name} (Completed: {CurrentCount}/{TargetCount}, Points: {Points}, Bonus: {BonusPoints})";
        }
    }
    public class NegativeGoal : Goal
    {
        public NegativeGoal(string name, int penaltyPoints) : base(name, -penaltyPoints)
        {
        }
        public override bool IsComplete()
        {
            return false;
        }
        public override int RecordEvent()
        {
            return Points;
        }
        public override string Display()
        {
            return $"[!] {Name} (Penalty per record: {-Points})";
        }
    }
    public class User
    {
        public string Name { get; private set; }
        public List<Goal> Goals { get; private set; }
        public int Score { get; private set; }
        public int Level { get; private set; }
        public User(string name)
        {
            Name = name;
            Goals = new List<Goal>();
            Score = 0;
            Level = 1;
        }
        public void AddGoal(Goal goal)
        {
            Goals.Add(goal);
        }
        public int RecordEvent(string goalName)
        {
            foreach (var goal in Goals)
            {
                if (goal.Name == goalName)
                {
                    int points = goal.RecordEvent();
                    Score += points;
                    UpdateLevel();
                    return points;
                }
            }
            return 0;
        }
        private void UpdateLevel()
        {
            while (Score >= Level * 1000)
            {
                Level++;
            }
        }
        public string DisplayGoals()
        {
            string display = "";
            foreach (var goal in Goals)
            {
                display += goal.Display() + "\n";
            }
            return display;
        }
        public void SaveToFile(string filename)
        {
            var data = new
            {
                Name = this.Name,
                Score = this.Score,
                Level = this.Level,
                Goals = this.Goals
            };
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            File.WriteAllText(filename, JsonSerializer.Serialize(data, options));
        }
        public void LoadFromFile(string filename)
        {
            var options = new JsonSerializerOptions { IncludeFields = true };
            var data = JsonSerializer.Deserialize<dynamic>(File.ReadAllText(filename), options);
            this.Name = data.GetProperty("Name").GetString();
            this.Score = data.GetProperty("Score").GetInt32();
            this.Level = data.GetProperty("Level").GetInt32();
            this.Goals = new List<Goal>();

            foreach (var goalData in data.GetProperty("Goals").EnumerateArray())
            {
                string goalType = goalData.GetProperty("Type").GetString();
                if (goalType == "SimpleGoal")
                {
                    Goals.Add(new SimpleGoal(goalData.GetProperty("Name").GetString(), goalData.GetProperty("Points").GetInt32()));
                }
                else if (goalType == "EternalGoal")
                {
                    Goals.Add(new EternalGoal(goalData.GetProperty("Name").GetString(), goalData.GetProperty("Points").GetInt32()));
                }
                else if (goalType == "ChecklistGoal")
                {
                    var goal = new ChecklistGoal(
                        goalData.GetProperty("Name").GetString(),
                        goalData.GetProperty("Points").GetInt32(),
                        goalData.GetProperty("TargetCount").GetInt32(),
                        goalData.GetProperty("BonusPoints").GetInt32()
                    );
                    goal.GetType().GetProperty("CurrentCount").SetValue(goal, goalData.GetProperty("CurrentCount").GetInt32());
                    Goals.Add(goal);
                }
                else if (goalType == "NegativeGoal")
                {
                    Goals.Add(new NegativeGoal(goalData.GetProperty("Name").GetString(), -(goalData.GetProperty("Points").GetInt32())));
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            User user = new User("John Doe");
            user.AddGoal(new SimpleGoal("Run a marathon", 1000));
            user.AddGoal(new EternalGoal("Read scriptures", 100));
            user.AddGoal(new ChecklistGoal("Attend temple", 50, 10, 500));
            user.AddGoal(new NegativeGoal("Smoke a cigarette", 50));
            Console.WriteLine(user.RecordEvent("Read scriptures"));  // +100
            Console.WriteLine(user.RecordEvent("Attend temple"));  // +50
            Console.WriteLine(user.RecordEvent("Smoke a cigarette"));  // -50
            Console.WriteLine(user.DisplayGoals());
            Console.WriteLine($"Score: {user.Score}, Level: {user.Level}");
            user.SaveToFile("user_data.json");
            User newUser = new User("New User");
            newUser.LoadFromFile("user_data.json");
            Console.WriteLine($"Loaded User: Score: {newUser.Score}, Level: {newUser.Level}");
        }
    }
}

// Exceeding requirements with additional features:
// - Added leveling system: users level up every 1000 points
// - Added negative goals for tracking and discouraging bad habits
