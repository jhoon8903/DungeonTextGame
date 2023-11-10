using System.Text.Json.Serialization;

namespace DungeonTextGame;

public class Item
{
    public enum ItemTypes
    {
        Weapon, Defender, Position
    }
    [JsonPropertyName("itemName")] public string ItemName { get; set; }
    [JsonPropertyName("itemType")] public ItemTypes ItemType { get; set; }
    [JsonPropertyName("itemStatus")] public int ItemStatus { get; set; }
    [JsonPropertyName("itemDesc")] public string ItemDesc { get; set; }
    [JsonPropertyName("price")] public int Price { get; set; }
}