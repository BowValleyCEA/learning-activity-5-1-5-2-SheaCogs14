using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<(string Initials, int Score)> highScores = new List<(string, int)>();

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");



        // LearningActivity5_1(); 
        LearningActivity5_2();   
    }


    static void LearningActivity5_1()
    {
        Random randomScore = new Random();
        while (true)
        {
            GameSelection choice = ChooseGameOption();

            switch (choice)
            {
                case GameSelection.Play:
                    int newHighestScore = randomScore.Next(1000, 1000000);
                    Console.WriteLine("Please enter your initials (exactly 3 characters):");
                    string initials = Console.ReadLine().ToUpper();

                    if (initials.Length != 3)
                    {
                        Console.WriteLine("Invalid input. Please use initials limited to 3 characters.");
                        break;
                    }

                    AddHighScore(initials, newHighestScore);
                    Console.WriteLine($"You finished with a score of {newHighestScore}");
                    break;

                case GameSelection.SeeHighScore:
                    DisplayHighScores();
                    break;

                case GameSelection.Exit:
                    return;
            }
        }
    }

    static void AddHighScore(string initials, int score)
    {
        highScores.Add((initials, score));
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (highScores.Count > 10)
        {
            highScores.RemoveAt(10);
        }
    }

    static void DisplayHighScores()
    {
        Console.WriteLine("Top 10 High Scores:");
        foreach (var (initials, score) in highScores)
        {
            Console.WriteLine($"{initials}: {score}");
        }
    }

    static GameSelection ChooseGameOption()
    {
        bool validSelection = false;
        int selection = 0;
        while (!validSelection)
        {
            Console.WriteLine("Would you like to:\n\t 1: Play\n\t 2: See the list of high scores\n\t 3: Exit the game?");
            if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= 3)
                validSelection = true;
        }

        return (GameSelection)selection;
    }

    enum GameSelection
    {
        Play = 1, SeeHighScore = 2, Exit = 3
    }

    // POS System Logic
    static void LearningActivity5_2()
    {
        VideoRentalStore store = new VideoRentalStore();

        store.AddVideo("The Godfather", "Crime", 175);

        store.AddUser("Shea");


        while (true)
        {
            VideoSelection choice = ChooseVideoOption();

            switch (choice)
            {
                case VideoSelection.Rent:
                    Console.WriteLine("Please input the customer's name:");
                    string userName = Console.ReadLine();
                    User? user = store.Users.FirstOrDefault(u => u.Name == userName);

                    if (user == null)
                    {
                        Console.WriteLine("No user found. Do you want to create an account? (yes/no)");
                        string response = Console.ReadLine().ToLower();
                        if (response == "yes")
                        {
                            store.AddUser(userName);
                            user = store.Users.FirstOrDefault(u => u.Name == userName);
                            Console.WriteLine($"User {userName} was added!");
                        }
                        else
                        {
                            break;
                        }
                    }

                    Console.WriteLine("Below is a list of available movies:");
                    foreach (var video in store.SearchVideos(isAvailable: true))
                    {
                        Console.WriteLine($"{video.VideoId}: {video.Title} ({video.Genre}) - {video.Duration} minutes");
                    }

                    Console.WriteLine("Please enter the video ID:");
                    if (int.TryParse(Console.ReadLine(), out int videoId))
                    {
                        store.RentVideo(user, videoId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid video ID.");
                    }
                    break;

                case VideoSelection.List:
                    Console.WriteLine("Available movies:");
                    foreach (var video in store.SearchVideos(isAvailable: true))
                    {
                        Console.WriteLine($"{video.VideoId}: {video.Title} ({video.Genre}) - {video.Duration} minutes");
                    }
                    break;

                case VideoSelection.Return:
                    Console.WriteLine("Please input the customer's name:");
                    userName = Console.ReadLine();
                    user = store.Users.FirstOrDefault(u => u.Name == userName);

                    if (user == null)
                    {
                        Console.WriteLine("No user found.");
                        break;  
                    }

                    Console.WriteLine("Below is a list of currently rented videos:");
                    foreach (var video in user.CurrentRentals)
                    {
                        Console.WriteLine($"{video.VideoId}: {video.Title} ({video.Genre}) - {video.Duration} minutes");
                    }

                    Console.WriteLine("Please enter the video ID to return:");
                    if (int.TryParse(Console.ReadLine(), out videoId))
                    {
                        store.ReturnVideo(user, videoId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid video ID.");
                    }
                    break;

                case VideoSelection.Exit:
                    return;
            }
        }
    }

    static VideoSelection ChooseVideoOption()
    {
        bool validSelection = false;
        int selection = 0;
        while (!validSelection)
        {
            Console.WriteLine("Would you like to:\n\t 1: Rent a Video\n\t 2: See the list of videos for rent\n\t 3: Return movie \n\t 4: Exit the program?");
            if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= 4)
                validSelection = true;
        }

        return (VideoSelection)selection;
    }

    enum VideoSelection
    {
        Rent = 1, List = 2, Return = 3, Exit = 4
    }
}

public class User
{
    private static int nextId = 100;
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<Video> CurrentRentals { get; private set; }
    public List<Video> RentalHistory { get; private set; }

    public User(string name)
    {
        UserId = nextId++;
        Name = name;
        CurrentRentals = new List<Video>();
        RentalHistory = new List<Video>();
    }

    public void RentVideo(Video video)
    {
        CurrentRentals.Add(video);
    }

    public void ReturnVideo(Video video)
    {
        CurrentRentals.Remove(video);
        RentalHistory.Add(video);
    }

    public override string ToString()
    {
        return $"User ID: {UserId}, Name: {Name}";
    }
}

public class Video
{
    private static int nextId = 1;
    public int VideoId { get; private set; }
    public string Title { get; private set; }
    public string Genre { get; private set; }
    public int Duration { get; private set; }
    public bool IsRented { get; private set; }
    public User? RentedBy { get; private set; } 

    public Video(string title, string genre, int duration)
    {
        VideoId = nextId++;
        Title = title;
        Genre = genre;
        Duration = duration;
        IsRented = false;
        RentedBy = null;  
    }

    public void Rent(User user)
    {
        IsRented = true;
        RentedBy = user;
    }

    public void Return()
    {
        IsRented = false;
        RentedBy = null;  
    }

    public override string ToString()
    {
        return $"Video ID: {VideoId}, Title: {Title}, Genre: {Genre}, Duration: {Duration} mins, Is Rented: {IsRented}";
    }
}

public class VideoRentalStore
{
    public List<User> Users { get; private set; }
    public List<Video> Videos { get; private set; }

    public VideoRentalStore()
    {
        Users = new List<User>();
        Videos = new List<Video>();
    }

    public void AddUser(string name)
    {
        Users.Add(new User(name));
    }

    public void AddVideo(string title, string genre, int duration)
    {
        Videos.Add(new Video(title, genre, duration));
    }

    public List<Video> SearchVideos(bool isAvailable = false)  
    {
        if (isAvailable)
        {
            return Videos.Where(v => !v.IsRented).ToList(); 
        }
        return Videos;
    }

    public void RentVideo(User user, int videoId)
    {
        Video? video = Videos.FirstOrDefault(v => v.VideoId == videoId && !v.IsRented);
        if (video != null)
        {
            video.Rent(user);
            user.RentVideo(video);
            Console.WriteLine($"{user.Name} successfully rented {video.Title}.");
        }
        else
        {
            Console.WriteLine("Video is either not available or does not exist.");
        }
    }

    public void ReturnVideo(User user, int videoId)
    {
        Video? video = user.CurrentRentals.FirstOrDefault(v => v.VideoId == videoId);
        if (video != null)
        {
            video.Return();
            user.ReturnVideo(video);
            Console.WriteLine($"{user.Name} successfully returned {video.Title}.");
        }
        else
        {
            Console.WriteLine("The user has not rented this video.");
        }
    }
}