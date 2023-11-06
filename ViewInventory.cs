using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using ConsoleTables;

namespace DungeonTextGame;

public static class ViewInventory
{
    public static int additonalDamage { get; set; }
    public static int additonalDefence { get; set; }

    public static void Inventory()
    {
        while (true)
        {
            Clear();
            WriteLine("인벤토리");
            WriteLine("보유 중인 아이템을 관리할 수 있습니다."); 
            WriteLine();
            WriteLine("[아이템 목록]");
            ShowItemList(GameManager.ItemList, GameManager.SelectCommand.Show);
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

    private static void ShowItemList(List<Item> itemsList, GameManager.SelectCommand command)
    {
        itemsList.Sort((x,y) => y.ItemName.CompareTo(x.ItemName));
        var table = new ConsoleTable("---아이템 이름---", "---효과---", "----------------------아이템 설명----------------------");
        int itemIndex = 1;
        foreach (var item in itemsList)
        {
            if (item.Buying)
            {
                string indexDisplay = command == GameManager.SelectCommand.Equip ? $"{itemIndex++}. " : string.Empty;
                string equipment = item.Equipment ? "[E]" : "";
                string itemType = item.ItemType switch
                {
                    Item.ItemTypes.Weapon => "공격력",
                    Item.ItemTypes.Defender => "방어력",
                    Item.ItemTypes.Position => "회복량", 
                    Item.ItemTypes.Consumable => "수량",
                    _ => ""
                };
                table.AddRow($"{indexDisplay}{equipment}{item.ItemName}", $"{itemType} +{item.ItemStatus}", item.ItemDesc);
            }
        }
        table.Write();
    }

    public static void EquipmentManger()
    {
        Clear();
        while (true)
        {
            WriteLine("인벤토리 - [장착 관리]"); 
            WriteLine("보유 중인 아이템을 관리할 수 있습니다."); 
            WriteLine();
            ShowItemList(GameManager.ItemList, GameManager.SelectCommand.Equip);
            WriteLine(); 
            WriteLine("아이템을 장착 또는 해제하려면 아이템 번호를 입력하세요");
            WriteLine("0. 나가기");
            WriteLine();
            WriteLine("원하시는 행동을 입력해주세요."); 
            Write(">>");
            string? command = ReadLine();
            if (int.TryParse(command, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= GameManager.ItemList.Count)
            {
                Item selectedItem = GameManager.ItemList[selectedIndex - 1];
                selectedItem.Equipment = !selectedItem.Equipment;
                if (selectedItem.Equipment)
                {
                    switch (selectedItem.ItemType)
                    {
                        case Item.ItemTypes.Weapon:
                            additonalDamage += selectedItem.ItemStatus;
                            break;
                        case Item.ItemTypes.Defender:
                            additonalDefence += selectedItem.ItemStatus;
                            break;
                    }
                }
                else
                {
                    switch (selectedItem.ItemType)
                    {
                        case Item.ItemTypes.Weapon:
                            additonalDamage -= selectedItem.ItemStatus;
                            break;
                        case Item.ItemTypes.Defender:
                            additonalDefence -= selectedItem.ItemStatus;
                            break;
                    }
                }
                GameManager.Save();
            }
            else if (command == "0")
            {
                Inventory();
                break;
            }
            else
            {
                GameManager.WrongCommand();
            }
        }
    }
}