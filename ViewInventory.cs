using ConsoleTables;
namespace DungeonTextGame;

public static class ViewInventory
{
    private static Dictionary<string, InventoryItem> _ownedItems = new Dictionary<string, InventoryItem>();
    public static void Inventory()
    {
        while (true)
        {
            Clear();
            WriteLine("인벤토리");
            WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            WriteLine("[아이템 목록]\n");
            ShowItemList(GameManager.ItemList, GameManager.SelectCommand.Show);
            WriteLine("1. 장착 관리"); 
            WriteLine("0. 나가기\n");
            WriteLine("원하시는 행동을 입력해주세요."); 
            Write(">>");
            string? command = ReadLine();
            if (command == "1")
            {
                EquipmentManger(GameManager.ItemList);
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
        _ownedItems = Account.LoginCharacter.Inventory
            .Where(item => item.Value.ToOwn)
            .ToDictionary(item => item.Key, item => item.Value);
        foreach (var ownItem in _ownedItems)
        {
            var characterItem = itemsList.FirstOrDefault(i => i.ItemName == ownItem.Key);  
            string indexDisplay = command == GameManager.SelectCommand.Equip ? $"{itemIndex++}. " : string.Empty;
            string equipment =  ownItem.Value.Equipment ? "[E]" : "";
            string itemType = characterItem.ItemType switch
            {
                Item.ItemTypes.Weapon => "공격력",
                Item.ItemTypes.Defender => "방어력",
                Item.ItemTypes.Position => "회복량",
                _ => ""
            };
            table.AddRow($"{indexDisplay}{equipment}{characterItem.ItemName}", $"{itemType} +{characterItem.ItemStatus}", characterItem.ItemDesc);
        }
        table.Write();
    }

    public static void EquipmentManger(List<Item> itemList)
    {
        while (true)
        {
            Clear();
            WriteLine("인벤토리 - [장착 관리]"); 
            WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n\n");
            ShowItemList(GameManager.ItemList, GameManager.SelectCommand.Equip);
            WriteLine("아이템을 장착 또는 해제하려면 아이템 번호를 입력하세요");
            WriteLine("0. 나가기\n");
            WriteLine("원하시는 행동을 입력해주세요."); 
            Write(">>");
            string? command = ReadLine();
            if (int.TryParse(command, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= _ownedItems.Count)
            {
                var ownItemList = _ownedItems.ToList();
                var selectedItem = ownItemList[selectedIndex-1];
                Item selectedItemInfo = itemList.FirstOrDefault(i => i.ItemName == selectedItem.Key);
                var equippedItems = _ownedItems.Where(equipped => equipped.Value.Equipment).ToList();
                var equippedItemOfSameType = equippedItems
                    .Where(eqi => eqi.Key != selectedItem.Key) // 현재 선택한 아이템을 제외합니다.
                    .FirstOrDefault(eqi => itemList.FirstOrDefault(i => i.ItemName == eqi.Key).ItemType == selectedItemInfo.ItemType);
                if (equippedItemOfSameType.Key != null)
                {
                    equippedItemOfSameType.Value.Equipment = false;
                    var equippedItemInfo = itemList.FirstOrDefault(i => i.ItemName == equippedItemOfSameType.Key);
                    switch (equippedItemInfo.ItemType)
                    {
                        case Item.ItemTypes.Weapon:
                            Account.LoginCharacter.ItemDamage -= equippedItemInfo.ItemStatus;
                           break;
                        case Item.ItemTypes.Defender:
                            Account.LoginCharacter.ItemDefence -= equippedItemInfo.ItemStatus;
                            break;
                    }
                }
                // 선택한 아이템을 토글합니다. (착용 또는 해제)   
                selectedItem.Value.Equipment = !selectedItem.Value.Equipment;
              
                if (selectedItem.Value.Equipment)
                {
                    // 다른 아이템을 해제했다면, 선택한 아이템을 착용합니다.
                    switch (selectedItemInfo.ItemType)
                    {
                        case Item.ItemTypes.Weapon:
                            Account.LoginCharacter.ItemDamage += selectedItemInfo.ItemStatus;
                           break;
                        case Item.ItemTypes.Defender:
                            Account.LoginCharacter.ItemDefence += selectedItemInfo.ItemStatus;
                            break;
                    }
                }
                else
                {
                    // 선택한 아이템을 해제합니다.
                    switch (selectedItemInfo.ItemType)
                    {
                        case Item.ItemTypes.Weapon:
                            Account.LoginCharacter.ItemDamage -= selectedItemInfo.ItemStatus;
                            break;
                        case Item.ItemTypes.Defender:
                            Account.LoginCharacter.ItemDefence -= selectedItemInfo.ItemStatus;
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