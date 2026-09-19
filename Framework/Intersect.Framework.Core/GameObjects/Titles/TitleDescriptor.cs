using MessagePack;

namespace Intersect.Framework.Core.GameObjects.Titles;

/// <summary>
/// Describes a title that can be unlocked and equipped by a player. When equipped, the title's
/// <see cref="Name"/> and <see cref="Color"/> are shown above the player's head (using the same
/// Header Label mechanism as the "Change Player Label" event command).
/// </summary>
[MessagePackObject]
public partial class TitleDescriptor
{
    // Parameterless constructor for MessagePack/JSON
    public TitleDescriptor()
    {
    }

    public TitleDescriptor(Guid id, string name, Color color)
    {
        Id = id;
        Name = name;
        Color = color;
    }

    [Key(0)]
    public Guid Id { get; set; }

    [Key(1)]
    public string Name { get; set; } = string.Empty;

    [Key(2)]
    public Color Color { get; set; } = Color.White;
}
