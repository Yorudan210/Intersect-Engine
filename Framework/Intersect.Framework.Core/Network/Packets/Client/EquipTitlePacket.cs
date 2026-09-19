using MessagePack;

namespace Intersect.Network.Packets.Client;

[MessagePackObject]
public partial class EquipTitlePacket : IntersectPacket
{
    //Parameterless Constructor for MessagePack
    public EquipTitlePacket()
    {
    }

    /// <param name="titleId">The title to equip, or <see cref="Guid.Empty"/> to remove the currently equipped title.</param>
    public EquipTitlePacket(Guid titleId)
    {
        TitleId = titleId;
    }

    [Key(0)]
    public Guid TitleId { get; set; }
}
