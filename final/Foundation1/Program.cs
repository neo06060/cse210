using System;
using System.Collections.Generic;
class Video
{
    private string title;
    private string author;
    private int length; 
    private List<Comment> comments;
    public Video(string title, string author, int length)
    {
        this.title = title;
        this.author = author;
        this.length = length;
        this.comments = new List<Comment>();
    }
    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }
    public int GetNumberOfComments()
    {
        return comments.Count;
    }
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"Length: {length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            comment.DisplayComment();
        }
    }
}
class Comment
{
    private string commenterName;
    private string text;
    public Comment(string commenterName, string text)
    {
        this.commenterName = commenterName;
        this.text = text;
    }
    public void DisplayComment()
    {
        Console.WriteLine($"{commenterName}: {text}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("How to breath", "Paco Jones", 300);
        Video video2 = new Video("How to fall aesleep", "Dolores Delano", 450);
        Video video3 = new Video("How to wake up from sleepìng", "Alba sura", 600);
        video1.AddComment(new Comment("Tomas Turbado", "Great video!"));
        video1.AddComment(new Comment("Elver Galarga", "Very informative."));
        video1.AddComment(new Comment("Thua Wela", "Thanks for sharing."));
        video2.AddComment(new Comment("Jorge Nitales", "Loved it!"));
        video2.AddComment(new Comment("Keko Ñete", "Good job."));
        video2.AddComment(new Comment("Elpi Tokorto", "Nice explanation."));
        video3.AddComment(new Comment("Kepo Jhon", "Awesome!"));
        video3.AddComment(new Comment("Alex Cremento", "Very helpful."));
        video3.AddComment(new Comment("Paco Gerte", "Keep it up."));
        List<Video> videos = new List<Video> { video1, video2, video3 };
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
            Console.WriteLine();
        }
    }
}
