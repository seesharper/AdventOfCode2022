#load "../tools.csx"

var lines = File.ReadAllLines("day8/input.txt");
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



for (var r = 1; r <= gridSize - 2; r++)
{
    for (int c = 1; c <= gridSize - 2; c++)
    {
        var maxHeightRight = GetMaxHeighRight(r, c + 1);
        var maxHeightLeft = GetMaxHeighLeft(r, c - 1);
        var maxHeightDown = GetMaxHeightDown(r + 1, c);
        var maxHeightUp = GetMaxHeightUp(r - 1, c);
        var allHeights = new[] { maxHeightRight, maxHeightLeft, maxHeightDown, maxHeightUp };

        if (map[r, c].Height > allHeights.Min())
        {
            visibleTrees.Add(map[r, c]);
        }
    }
}

visibleTrees.Count().Dump();

int GetMaxHeightUp(int row, int column)
{
    int maxValue = 0;
    for (int r = row; r >= 0; r--)
    {
        if (map[r, column].Height > maxValue)
        {
            maxValue = map[r, column].Height;
        }
    }
    return maxValue;
}


int GetMaxHeightDown(int row, int column)
{
    int maxValue = 0;
    for (int r = row; r < gridSize; r++)
    {
        if (map[r, column].Height > maxValue)
        {
            maxValue = map[r, column].Height;
        }
    }
    return maxValue;
}


int GetMaxHeighRight(int row, int column)
{
    int maxValue = 0;
    for (int c = column; c < gridSize; c++)
    {
        if (map[row, c].Height > maxValue)
        {
            maxValue = map[row, c].Height;
        }
    }
    return maxValue;
}

int GetMaxHeighLeft(int row, int column)
{
    int maxValue = 0;
    for (int c = column; c >= 0; c--)
    {
        if (map[row, c].Height > maxValue)
        {
            maxValue = map[row, c].Height;
        }
    }
    return maxValue;
}
