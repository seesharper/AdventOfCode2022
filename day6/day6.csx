#load "../tools.csx"
var input = File.ReadAllText("day6/input.txt");

public record Buffer(string Content, int Start, int End);

WriteLine($"Number of characters processed before first start-of-packet marker is received: {FirstUniqueBuffer(4).End}");
WriteLine($"Number of characters processed before first start-of-message marker is received: {FirstUniqueBuffer(14).End}");

Buffer FirstUniqueBuffer(int bufferLength)
{
    for (int i = 0; i < input.Length; i++)
    {
        var buffer = new Buffer(input.Substring(i, bufferLength), i, i + bufferLength);
        if (buffer.IsMarker())
        {
            return buffer;
        }
    }
    return new Buffer(string.Empty, 0, 0);
}

static bool IsMarker(this Buffer buffer) => buffer.Content.Distinct().Count() == buffer.Content.Length;