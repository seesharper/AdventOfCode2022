#load "../tools.csx"
var lines = File.ReadAllLines("day3/input.txt");

Dictionary<char, int> priorityMap = new();

InitializePriorityMap('a', 1);
InitializePriorityMap('A', 27);

int prioritySum = 0;

foreach (var line in lines)
{
    var itemsInFirstCompartment = line.Select(c => new Item(c, priorityMap[c])).Take(line.Length / 2);
    var itemsInSecondCompartment = line.Select(c => new Item(c, priorityMap[c])).Skip(line.Length / 2);
    var itemsInBothCompartments = itemsInFirstCompartment.Intersect(itemsInSecondCompartment);
    prioritySum += itemsInBothCompartments.Select(i => i.Priority).Sum();
}

prioritySum.Dump();

public record Item(char Id, int Priority);

public void InitializePriorityMap(char startLetter, int priority)
{
    var startIndex = (int)startLetter;
    for (int i = startIndex; i <= startIndex + 25; i++)
    {
        char character = (char)i;
        priorityMap.Add(character, priority);
        priority++;
    }
}