#load "../tools.csx"
var lines = File.ReadAllLines("day4/input.txt");
var sectionPairs = lines.Select(l => l.Split('-', ',').Select(n => n.Parse()).ToArray())
.Select(n => new SectionPair(new Section(n[0], n[1]), new Section(n[2], n[3]))).ToList();

public record Section(int Start, int End);
public record SectionPair(Section FirstElvSection, Section SecondElvSection);

var fullyContainedCount = sectionPairs.Where(sp => sp.FirstElvSection.FullyContains(sp.SecondElvSection) || sp.SecondElvSection.FullyContains(sp.FirstElvSection)).Count();
var overlappingCount = sectionPairs.Where(sp => sp.FirstElvSection.Overlaps(sp.SecondElvSection)).Count();

WriteLine($"Number of assignment pairs fully containing the other: {fullyContainedCount}");
WriteLine($"Number of overlapping assignment pairs: {overlappingCount}");

static bool FullyContains(this Section section, Section other)
    => section.Start >= other.Start && section.End <= other.End;

static bool Overlaps(this Section section, Section other)
    => section.Start <= other.End && other.Start <= section.End;

