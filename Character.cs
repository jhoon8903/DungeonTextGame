using System.Text.Json.Serialization;

namespace DungeonTextGame;

public class Character
{
    public static int Level = 1;
    public  enum Jobs { 전사, 마법사, 궁수, 도적, 성직자 }

    public static Jobs Job = Jobs.전사;
    public static float Damage = 10f;
    public static float Defence = 5f;
    public static float HealthPoint = 100f;
    public static int Gold = 1500;
}

public class Item
{
    public enum itemTypes
    {
        Weapon, Defender, Consumable
    }
    [JsonPropertyName("itemName")]
    public string itemName { get; set; }
    [JsonPropertyName("itemType")]
    public itemTypes itemType { get; set; }
    [JsonPropertyName("itemStatus")]
    public int itemStatus { get; set; }
    [JsonPropertyName("equipment")]
    public bool equipment { get; set; }     
    [JsonPropertyName("itemDesc")]
    public string itemDesc { get; set; }
}