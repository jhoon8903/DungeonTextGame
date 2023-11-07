using System.Text.Json.Serialization;

namespace DungeonTextGame;

public class Character
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("pw")]public string Pw { get; set; }
    [JsonPropertyName("level")]public int Level { get; set; }
    [JsonPropertyName("exp")]public float Exp { get; set; }
    public  enum Jobs { 전사, 마법사, 궁수, 도적, 성직자, 무직 }
    [JsonPropertyName("job")]public Jobs Job { get; set; }
    [JsonPropertyName("damage")]public  float Damage { get; set; }
    [JsonPropertyName("itemDamage")]public  float ItemDamage { get; set; }
    [JsonPropertyName("defence")]public  float Defence { get; set; }
    [JsonPropertyName("itemDefence")]public  float ItemDefence { get; set; }
    [JsonPropertyName("hp")]public  float HealthPoint { get; set; }
    [JsonPropertyName("gold")]public  int Gold { get; set; }
    [JsonPropertyName("inventory")] public Dictionary<string, InventoryItem> Inventory { get; set; }
}

public class InventoryItem
{
    [JsonPropertyName("toOwn")] public bool ToOwn { get; set; }
    [JsonPropertyName("equipment")] public bool Equipment { get; set; }
}
