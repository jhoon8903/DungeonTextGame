using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace DungeonTextGame;

public static class ViewInventory
{
    public static void Inventory()
    {
        while (true)
        {
            Clear();
            WriteLine("인벤토리");
            WriteLine("보유 중인 아이템을 관리할 수 있습니다."); 
            WriteLine();

            WriteLine("[아이템 목록]");
            string itemFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/Item.json";
            string read;
            using (StreamReader r = new StreamReader(itemFilePath, Encoding.UTF8))
            {
                read = r.ReadToEnd();
            }
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            List<Item> items = JsonSerializer.Deserialize<List<Item>>(read, options);

            foreach (var item in items)
            {
                string equipment = item.equipment ? "[E]" : string.Empty;
                string itemType = item.itemType switch
                {
                    Item.itemTypes.Weapon => "공격력",
                    Item.itemTypes.Defender => "방어력",
                    Item.itemTypes.Consumable => "수량",
                    _ => ""
                };
                WriteLine($"- {equipment}{item.itemName} | {itemType} +{item.itemStatus} | {item.itemDesc}");
            }
            WriteLine();
            WriteLine("1. 장착 관리"); 
            WriteLine("0. 나가기"); 
            WriteLine();
            WriteLine("원하시는 행동을 입력해주세요."); 
            Write(">>");
            string? command = ReadLine();
            if (command == "1")
            {
                EquipmentManger();
                break;
            }
            if (command == "0")
            {
                GameManager.GameStart();
                break;
            }
            GameManager.WrongCommand();
        }
    }

    public static void EquipmentManger()
    {
        WriteLine("[장착 관리]");
    }
}