#load "../tools.csx"

var lines = File.ReadAllLines("day8/sample.txt");
var gridSize = lines.Length;

public record Tree(int Height, int Row, int Column);
HashSet<Tree> visibleTrees = new HashSet<Tree>();

Tree[,] map = new Tree[gridSize, gridSize];

for (int r = 0; r < gridSize; r++)
{
    for (int c = 0; c < gridSize; c++)
    {
        map[r, c] = new Tree(lines[r][c].ToString().Parse(), r, c);
        if (r == 0 || r == gridSize - 1 || c == 0 || c == gridSize - 1)
        {
            visibleTrees.Add(map[r, c]);
        }
    }
}

visibleTrees.Count.Dump();




map.Dump(t => t.Height.ToString());