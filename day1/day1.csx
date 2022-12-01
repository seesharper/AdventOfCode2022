#load "../tools.csx"

var lines = File.ReadAllLines("day1/input.txt");

List<int> totalCaloriesCarriedPerElf = new List<int>();

int caloriesCarriedByElf = 0;
foreach (var line in lines)
{
    if (!string.IsNullOrWhiteSpace(line))
    {
        caloriesCarriedByElf += line.Parse();
    }
    else
    {
        totalCaloriesCarriedPerElf.Add(caloriesCarriedByElf);
        caloriesCarriedByElf = 0;
    }
}
totalCaloriesCarriedPerElf.Add(caloriesCarriedByElf);

var maxCaloriesCarried = totalCaloriesCarriedPerElf.Max();

WriteLine($"The Elf carrying the most calories carries {maxCaloriesCarried} calories");

var caloriesCarriedByTopThreeElves = totalCaloriesCarriedPerElf.OrderDescending().Take(3).Sum();

WriteLine($"The top three Elves carries {caloriesCarriedByTopThreeElves} calories");
