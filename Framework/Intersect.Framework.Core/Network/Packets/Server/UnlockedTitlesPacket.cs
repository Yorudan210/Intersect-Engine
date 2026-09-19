using Intersect.Framework.Core.GameObjects.Titles;
using MessagePack;

namespace Intersect.Network.Packets.Server;

[MessagePackObject]
public partial class UnlockedTitlesPacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public UnlockedTitlesPacket()
    {
    }

    public UnlockedTitlesPacket(List<TitleDescriptor> unlockedTitles, Guid equippedTitleId)
    {
        UnlockedTitles = unlockedTitles;
        EquippedTitleId = equippedTitleId;
    }

    [Key(0)]
    public List<TitleDescriptor> UnlockedTitles { get; set; } = [];

    [Key(1)]
    public Guid EquippedTitleId { get; set; }
}
