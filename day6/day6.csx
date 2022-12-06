#load "../tools.csx"
var input = File.ReadAllText("day6/input.txt");
input.Dump();

public record Buffer(string Content, int Start, int End);

int positionWhereMarkerIsComplete = 0;

for (int i = 0; i < input.Length; i++)
{
    var buffer = new Buffer(input.Substring(i, 4), i, i + 4);
    if (buffer.IsMarker())
    {
        positionWhereMarkerIsComplete = buffer.End;
        break;
    }
}

positionWhereMarkerIsComplete.Dump();


static bool IsMarker(this Buffer buffer)
{
    return buffer.Content.Distinct().Count() == buffer.Content.Length;
}