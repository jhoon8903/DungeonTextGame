using System.Text.Json.Serialization;

namespace DungeonTextGame;

public static class Character
{
    [JsonPropertyName("id")] public static string Id;
    [JsonPropertyName("pw")]public static string Pw;
    [JsonPropertyName("level")]public static int Level;
    [JsonPropertyName("exp")]public static float Exp;
    public  enum Jobs { 전사, 마법사, 궁수, 도적, 성직자, 무직 }
    [JsonPropertyName("job")]public static Jobs Job;
    [JsonPropertyName("damage")]public static float Damage;
    [JsonPropertyName("defence")]public static float Defence;
    [JsonPropertyName("hp")]public static float HealthPoint;
    [JsonPropertyName("gold")]public static int Gold;
}

public class Item
{
    public enum ItemTypes
    {
        Weapon, Defender, Position, Consumable
    }
    [JsonPropertyName("itemName")] public string ItemName { get; set; }
    [JsonPropertyName("itemType")] public ItemTypes ItemType { get; set; }
    [JsonPropertyName("itemStatus")] public int ItemStatus { get; set; }
    [JsonPropertyName("equipment")] public bool Equipment { get; set; }     
    [JsonPropertyName("itemDesc")] public string ItemDesc { get; set; }
    [JsonPropertyName("buying")] public bool Buying { get; set; }
    [JsonPropertyName("price")] public int Price { get; set; }
}