using System.Text.Json;
using System.Text.RegularExpressions;

public static void ShouldBe<T>(this T value, T expected)
{
    if (!value.Equals(expected))
    {
        throw new Exception($"Actual value {value} should be {expected}");
    }
}

public static void Dump<T>(this T value)
{
    WriteLine(JsonSerializer.Serialize(value, new JsonSerializerOptions() { WriteIndented = true }));
    WriteLine();
}

public static void Dump<T>(this T value, string whatIsThis)
{
    WriteLine(whatIsThis);
    WriteLine(JsonSerializer.Serialize(value, new JsonSerializerOptions() { WriteIndented = true }));
    WriteLine();
}

public static void Dump<T>(this T[,] value, Func<T, string> formatter = null)
{
    formatter ??= v => v.ToString();

    StringBuilder rowBuilder = new StringBuilder();
    for (int r = 0; r < value.GetLength(0); r++)
    {
        for (int c = 0; c < value.GetLength(1); c++)
        {
            rowBuilder.Append(formatter(value[r, c]));
        }
        WriteLine(rowBuilder.ToString());
        rowBuilder.Clear();
    }
}


public static bool IsSet(this int value, int bitPosition)
{
    return (value & (1 << bitPosition)) != 0;
}

public static int SetBit(this int value, int bitPosition)
{
    return value |= 1 << bitPosition;
}


public static int Parse(this string value)
{
    return int.Parse(value);
}

public static int[] MatchNumbers(this string value)
    => Regex.Matches(value, @"\d+").Select(m => m.Groups[0].Value.Parse()).ToArray();

public static T[] PopMany<T>(this Stack<T> stack, int count)
{
    List<T> result = new();
    for (int i = 0; i < count; i++)
    {
        result.Add(stack.Pop());
    }
    return result.ToArray();
}

public static void PushMany<T>(this Stack<T> stack, T[] values)
{
    foreach (var value in values)
    {
        stack.Push(value);
    }
}


public static int GetMostCommonBits(this int[] values, int bitCount, int whenEqual = 0)
{
    int[] bitsSet = new int[bitCount];
    int[] bitsNotSet = new int[bitCount];
    foreach (var value in values)
    {
        for (int i = 0; i < bitCount; i++)
        {
            if (value.IsSet(i))
            {
                bitsSet[i]++;
            }
            else
            {
                bitsNotSet[i]++;
            }
        }
    }

    int mostCommonBits = 0;

    for (int i = 0; i < bitCount; i++)
    {
        if (bitsSet[i] > bitsNotSet[i])
        {
            mostCommonBits = mostCommonBits.SetBit(i);
        }

        if (bitsSet[i] == bitsNotSet[i] && whenEqual == 1)
        {
            mostCommonBits = mostCommonBits.SetBit(i);
        }
    }

    return mostCommonBits;
}