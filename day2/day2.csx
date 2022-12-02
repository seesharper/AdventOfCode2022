#load "../tools.csx"

var lines = File.ReadAllLines("day2/input.txt");
List<Game> games = new();

var rock = new Shape("Rock", 1, element => element.Name is "Scissors");
var paper = new Shape("Paper", 2, element => element.Name is "Rock");
var scissors = new Shape("Scissors", 3, element => element.Name is "Paper");

public record Game(string OpponentMove, string YourMove);

public record Shape(string Name, int Score, Func<Shape, bool> Beats);

Dictionary<string, Shape> Map = new()
{
    {"A", rock},
    {"B", paper},
    {"C", scissors},
    {"X", rock},
    {"Y", paper},
    {"Z", scissors},
};

games = lines.Select(line => new Game(line.Substring(0, 1), line.Substring(2, 1))).ToList();

public class Player
{
    public int TotalScore { get; set; }
}

Dictionary<Shape, Shape> LoosesToMap = new()
{
    {rock, paper},
    {paper, scissors},
    {scissors, rock},
};

Dictionary<Shape, Shape> WinsOverMap = new()
{
    {rock, scissors},
    {paper, rock},
    {scissors, paper},
};

public Shape SimpleStrategy(Game game) => Map[game.YourMove];

public Shape SmartStrategy(Game game) => game.YourMove switch
{
    "X" => WinsOverMap[Map[game.OpponentMove]],
    "Z" => LoosesToMap[Map[game.OpponentMove]],
    _ => Map[game.OpponentMove]
};

public (Player You, Player Opponent) Play(Func<Game, Shape> strategy)
{
    var opponent = new Player();
    var you = new Player();

    foreach (var game in games)
    {
        var opponentShape = Map[game.OpponentMove];
        var yourShape = strategy(game);

        if (opponentShape == yourShape)
        {
            opponent.TotalScore += opponentShape.Score + 3;
            you.TotalScore += yourShape.Score + 3;
        }

        else if (opponentShape.Beats(yourShape))
        {
            opponent.TotalScore += opponentShape.Score + 6;
            you.TotalScore += yourShape.Score;
        }
        else
        {
            you.TotalScore += yourShape.Score + 6;
            opponent.TotalScore += opponentShape.Score;
        }
    }
    return (you, opponent);
}

WriteLine($"Total score using simple strategy: {Play(SimpleStrategy).You.TotalScore}");
WriteLine($"Total score using smart strategy: {Play(SmartStrategy).You.TotalScore}");
