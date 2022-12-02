#load "../tools.csx"

var lines = File.ReadAllLines("day2/input.txt");
List<Game> games = new();

var rock = new Shape("Rock", 1, element => element.Name is "Scissors");
var paper = new Shape("Paper", 2, element => element.Name is "Rock");
var scissors = new Shape("Scissors", 3, element => element.Name is "Paper");

Dictionary<string, Shape> Map = new()
{
    {"A", rock},
    {"B", paper},
    {"C", scissors},
    {"X", rock},
    {"Y", paper},
    {"Z", scissors},
};

foreach (var line in lines)
{
    var opponentShape = Map[line.Substring(0, 1)];
    var yourShape = Map[line.Substring(2, 1)];
    var game = new Game(opponentShape, yourShape);
    games.Add(game);
}

public record Game(Shape OpponentShape, Shape YourShape);

public record Shape(string Name, int Score, Func<Shape, bool> Beats);

public class Player
{
    public int TotalScore { get; set; }
}

var opponent = new Player();
var you = new Player();

foreach (var game in games)
{
    if (game.OpponentShape == game.YourShape)
    {
        opponent.TotalScore += game.OpponentShape.Score + 3;
        you.TotalScore += game.YourShape.Score + 3;
    }

    else if (game.OpponentShape.Beats(game.YourShape))
    {
        opponent.TotalScore += game.OpponentShape.Score + 6;
        you.TotalScore += game.YourShape.Score;
    }
    else
    {
        you.TotalScore += game.YourShape.Score + 6;
        opponent.TotalScore += game.OpponentShape.Score;
    }
}

WriteLine(you.TotalScore);