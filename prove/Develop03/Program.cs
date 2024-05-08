using System;
using System.Collections.Generic;

public class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int? VerseEnd { get; }

    public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public string GetDisplayText()
    {
        if (VerseEnd.HasValue)
        {
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        }
        else
        {
            return $"{Book} {Chapter}:{VerseStart}";
        }
    }
}

public class Word
{
    public string Text { get; }
    public bool Hidden { get; set; }

    public Word(string text, bool hidden = false)
    {
        Text = text;
        Hidden = hidden;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public string GetDisplayText()
    {
        return Hidden ? new string('*', Text.Length) : Text;
    }
}

public class Scripture
{
    private readonly Reference reference;
    private readonly List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        foreach (var word in text.Split())
        {
            words.Add(new Word(word));
        }
    }

    public bool HideRandomWords()
    {
        var wordIndices = new List<int>();
        for (int i = 0; i < words.Count; i++)
        {
            if (!words[i].Hidden)
            {
                wordIndices.Add(i);
            }
        }

        if (wordIndices.Count == 0)
        {
            return false; // All words are already hidden
        }

        var randomIndex = new Random().Next(0, wordIndices.Count);
        words[wordIndices[randomIndex]].Hide();
        return true;
    }

    public string GetDisplayText()
    {
        var displayText = "";
        foreach (var word in words)
        {
            displayText += word.GetDisplayText() + " ";
        }
        return displayText.Trim();
    }

    public Reference GetReference()
    {
        return reference;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Reference scriptureReference = new Reference("John", 3, 16);
        string scriptureText = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        Scripture scripture = new Scripture(scriptureReference, scriptureText);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetReference().GetDisplayText());
            Console.WriteLine(scripture.GetDisplayText());
            Console.Write("\nPress Enter to continue or type 'quit' to exit: ");
            string userInput = Console.ReadLine();
            if (userInput.ToLower() == "quit")
            {
                break;
            }
            else
            {
                if (!scripture.HideRandomWords())
                {
                    Console.WriteLine("All words are hidden. Exiting...");
                    break;
                }
            }
        }
    }
}
