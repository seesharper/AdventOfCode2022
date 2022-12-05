#load "../tools.csx"

using System.Text.RegularExpressions;

var lines = File.ReadAllLines("day5/input.txt");
var emptyLineIndex = Array.IndexOf(lines, string.Empty);
var stacks = Regex.Matches(lines[emptyLineIndex - 1], @"\d+").Select(m => m.Groups[0].Value.Parse()).Select(s => new Stack<string>()).ToArray();
InitializeStacks();

var moves = lines[(emptyLineIndex + 1)..].Select(ml => ml.MatchNumbers()).Select(n => new Move(n[0], n[1], n[2])).ToArray();
public record Move(int numberOfCratesToMove, int fromStack, int toStack);

foreach (var move in moves)
{
    stacks[move.toStack - 1].PushMany(stacks[move.fromStack - 1].PopMany(move.numberOfCratesToMove).ToArray());
}

var message = stacks.Select(s => s.Peek()).Aggregate((current, next) => $"{current}{next}");
WriteLine($"Message after rearrangement: {message}");

ClearStacks();
InitializeStacks();

foreach (var move in moves)
{
    stacks[move.toStack - 1].PushMany(stacks[move.fromStack - 1].PopMany(move.numberOfCratesToMove).Reverse().ToArray());
}

message = stacks.Select(s => s.Peek()).Aggregate((current, next) => $"{current}{next}");
WriteLine($"Message after rearrangement using CrateMover 9001: {message}");

void InitializeStacks()
{
    for (int l = emptyLineIndex - 2; l >= 0; l--)
    {
        for (int s = 0; s < stacks.Length; s++)
        {
            var crate = lines[l].Substring((s * 4) + 1, 1).Trim();
            if (!string.IsNullOrWhiteSpace(crate))
            {
                stacks[s].Push(crate);
            }
        }
    }
}

void ClearStacks()
{
    foreach (var stack in stacks)
    {
        stack.Clear();
    }
}
