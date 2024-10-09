// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
List<(string Initials, int Score)> highScores = new List<(string, int)>();
LearningActivity5_1();
//LearningActivity5_2();
return;




void LearningActivity5_1()
{



    Random randomScore = new Random();
    while (true)
    {
        GameSelection choice = ChooseOption();

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

void AddHighScore(string initials, int Score)
{
    highScores.Add((initials, Score));
    highScores.Sort((a, b) => b.Score.CompareTo(a.Score));
    if (highScores.Count > 10)
    {
        highScores.RemoveAt(10);
    }
}

void DisplayHighScores()
{
    Console.WriteLine("Top 10 High Scores:");
    foreach (var (initials, score) in highScores)
    {
        Console.WriteLine($"{initials}: {score}");
    }
}


GameSelection ChooseOption()
{
    bool validSelection = false;
    int selection = 0;
    while (!validSelection)
    {
        Console.WriteLine(
            "Would you like to:\n\t 1: Play\n\t 2: See the list of high scores\n\t 3: Exit the game?");

        if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= 3)
            validSelection = true;

    }

    return (GameSelection)selection;

}

enum GameSelection
{
    Play = 1, SeeHighScore = 2, Exit = 3
}

// POS System below
/*
void LearningActivity5_2()
{

}


VideoSelection ChooseOption()
{
    bool selected = false;
    int selection = 0;
    while (!validSelection)
    {
        Console.WriteLine(
            "Would you like to:\n\t 1: Rent a Video\n\t 2: See the list of videos for rent\n\t 3: Exit the program?");

        if (int.TryParse(Console.ReadLine(), out selection) && selection >= 1 && selection <= 3)
            validSelection = true;
    }

    return (VideoSelection)selection;
}

enum VideoSelection
{
    Rent = 1, list = 2, exit = 3
}
*/