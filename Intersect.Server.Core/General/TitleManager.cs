using Intersect.Framework.Core.GameObjects.Titles;
using Intersect.Server.Core;
using Newtonsoft.Json;

namespace Intersect.Server.General;

/// <summary>
/// Loads and exposes the server-wide catalog of titles that players can unlock and equip.
/// Titles are defined in resources/titles.json - a new title can be added simply by editing that
/// file and restarting the server (no code changes required).
/// </summary>
public static class TitleManager
{
    private static readonly string TitlesFile = Path.Combine(ServerContext.ResourceDirectory, "titles.json");

    public static Dictionary<Guid, TitleDescriptor> Titles { get; private set; } = new();

    public static void LoadTitles()
    {
        Console.WriteLine(@"Loading titles...");

        var titles = new List<TitleDescriptor>();

        if (File.Exists(TitlesFile))
        {
            titles = JsonConvert.DeserializeObject<List<TitleDescriptor>>(File.ReadAllText(TitlesFile)) ?? titles;
        }
        else
        {
            // First run: seed a couple of example titles so admins have something to look at/edit.
            titles =
            [
                new TitleDescriptor(Guid.NewGuid(), "the Brave", Color.FromArgb(255, 255, 215, 0)),
                new TitleDescriptor(Guid.NewGuid(), "the Wanderer", Color.FromArgb(255, 180, 180, 180)),
            ];

            File.WriteAllText(TitlesFile, JsonConvert.SerializeObject(titles, Formatting.Indented));
        }

        Titles = titles.Where(title => title.Id != Guid.Empty)
            .GroupBy(title => title.Id)
            .ToDictionary(group => group.Key, group => group.First());

        Console.WriteLine($@"Loaded {Titles.Count} title(s).");
    }

    public static TitleDescriptor? Get(Guid titleId) => Titles.GetValueOrDefault(titleId);

    public static TitleDescriptor? FindByName(string name) => Titles.Values.FirstOrDefault(
        title => string.Equals(title.Name, name, StringComparison.OrdinalIgnoreCase)
    );
}
