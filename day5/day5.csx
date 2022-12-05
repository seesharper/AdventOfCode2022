#load "../tools.csx"

using System.Text.RegularExpressions;

var lines = File.ReadAllLines("day5/input.txt");
var emptyLineIndex = Array.IndexOf(lines, string.Empty);
var stacks = Regex.Matches(lines[emptyLineIndex - 1], @"\d+").Select(m => m.Groups[0].Value.Parse()).Select(s => new Stack<string>()).ToArray();

for (int l = emptyLineIndex - 2; l >= 0; l--)
{
    int startIndex = 1;
    for (int s = 0; s < stacks.Length; s++)
    {
        var crate = lines[l].Substring(startIndex, 1).Trim();
        startIndex += 4;
        if (!string.IsNullOrWhiteSpace(crate))
        {
            stacks[s].Push(crate);
        }
    }
}
var moves = lines[(emptyLineIndex + 1)..].Select(ml => ml.MatchNumbers()).Select(n => new Move(n[0], n[1], n[2])).ToArray();
public record Move(int numberOfCratesToMove, int fromStack, int toStack);


foreach (var move in moves)
{
    for (int i = 0; i < move.numberOfCratesToMove; i++)
    {
        stacks[move.toStack - 1].Push(stacks[move.fromStack - 1].Pop());
    }
}

var message = stacks.Select(s => s.Peek()).Aggregate((current, next) => $"{current}{next}");
message.Dump();
