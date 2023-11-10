using ConsoleTables;

namespace DungeonTextGame;

public static class Store
{
    public static void StoreShop()
    {
        while (true)
        {
            StoreDisplay(GameManager.SelectCommand.Show);
            WriteLine();
            WriteLine("1. 아이템 구매"); 
            WriteLine("2. 아이템 판매");
            WriteLine("0. 나가기"); 
            WriteLine();
            WriteLine("원하시는 행동을 입력해주세요"); 
            Write(">>");
            string? command = ReadLine();
            switch (command)
            {
                case "1":
                    StoreDisplay(GameManager.SelectCommand.Buying);
                    break;
                case "2":
                    SellStore();
                    break;
                case "0":
                    GameManager.GameStart();
                    break;
                default:
                    GameManager.WrongCommand();
                    break;
            }
            break;
        }
    }

    private static void StoreDisplay(GameManager.SelectCommand command)
    {
        Clear();
        string storeTitle = command == GameManager.SelectCommand.Show ? "상점" : "상점 - 아이템 구매";
        WriteLine(storeTitle); 
        WriteLine("필요한 아이템을 얻을 수 있는 상점입니다."); 
        WriteLine();
        WriteLine("[보유 골드]"); 
        WriteLine($"{Account.LoginCharacter.Gold} G");
        WriteLine();
        var table = new ConsoleTable("---아이템 이름---", "---효과---", "----------------------아이템 설명----------------------", "--아이템 가격--");
        int itemIndex = 1;

        var characterItems = Account.LoginCharacter.Inventory
            .Where(item => item.Value.ToOwn)
            .Select(item => item.Key)
            .ToList();
        foreach (var item in GameManager.ItemList)
        {
            string itemPurchase =  characterItems.Contains(item.ItemName)  ? "구매완료" : $"{item.Price} G";
            string indexDisplay = command == GameManager.SelectCommand.Buying ? $"{itemIndex++}. " : string.Empty;
            string itemType = item.ItemType switch
            {
                Item.ItemTypes.Weapon => "공격력",
                Item.ItemTypes.Defender => "방어력",
                Item.ItemTypes.Position => "회복량",
                _ => ""
            };
            table.AddRow($"{indexDisplay}{item.ItemName}", $"{itemType} +{item.ItemStatus}", item.ItemDesc, $"{itemPurchase}");
        }
        table.Write();
        if (command == GameManager.SelectCommand.Buying)
        {
            BuyStore();
        }
    }

    private static void BuyStore()
    {
        while (true)
        {
            Write("\n0. 나가기\n");
            WriteLine("구매하려는 아이템의 번호를 입력하세요.");
            Write(">>");
            string? command = ReadLine();
            if (int.TryParse(command, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= GameManager.ItemList.Count)
            {
                Item selectedItem = GameManager.ItemList[selectedIndex - 1];
                var characterItems = Account.LoginCharacter.Inventory;
                var alreadyBought = characterItems.Any(item => item.Value.ToOwn && item.Key == selectedItem.ItemName);
                switch (alreadyBought)
                {
                    case true:
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("이미 구매한 아이템입니다.");
                        ResetColor();
                        continue;
                    case false when Account.LoginCharacter.Gold >= selectedItem.Price:
                        ForegroundColor = ConsoleColor.Yellow;
                        WriteLine("구매를 완료했습니다.");
                        ResetColor();
                       Account.LoginCharacter.Gold -= selectedItem.Price;
                        characterItems.Add(selectedItem.ItemName, new InventoryItem{ToOwn = true, Equipment = false});
                        GameManager.Save();
                        StoreDisplay(GameManager.SelectCommand.Buying);
                        break;
                    case false when Account.LoginCharacter.Gold < selectedItem.Price:
                        ForegroundColor = ConsoleColor.Cyan;
                        WriteLine("Gold 가 부족합니다.");
                        ResetColor();
                        continue;
                }
            }
            else if (command == "0")
            {
                StoreShop();
            }
            else
            {
                GameManager.WrongCommand();
            }
        }
    }

    private static void SellStore()
    {
        while (true)
        {
            WriteLine("상점 - 아이템 판매");
            WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            WriteLine("[보유 골드]"); 
            WriteLine($" {Account.LoginCharacter.Gold} G\n");
            List<Item> sellItemList = new List<Item>();
            var table = new ConsoleTable("---아이템 이름---", "---효과---", "----------------------아이템 설명----------------------", "--아이템 가격--");
            int itemIndex = 1;
            var characterOwnItemList = Account.LoginCharacter.Inventory
                .Where(item => item.Value.ToOwn)
                .ToDictionary(item => item.Key, item => item.Value);
            foreach (var item in GameManager.ItemList)
            {
                if (characterOwnItemList.ContainsKey(item.ItemName))
                {
                    int itemSellPrice = (int)(item.Price * 0.85f);
                    string itemType = item.ItemType switch
                    {
                        Item.ItemTypes.Weapon => "공격력",
                        Item.ItemTypes.Defender => "방어력",
                        Item.ItemTypes.Position => "회복량",
                        _ => ""
                    };
                    table.AddRow($"{itemIndex++}. {item.ItemName}", $"{itemType} +{item.ItemStatus}", item.ItemDesc, $"{itemSellPrice} G");
                    sellItemList.Add(item);
                }
            }
            table.Write();
            WriteLine("\n0. 나가기"); 
            WriteLine("판매사실 아이템의 번호를 입력하세요");
            string? selectCommand = ReadLine();

            if (selectCommand == "0")
            {
                StoreShop();
            }
            else if (int.TryParse(selectCommand, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= characterOwnItemList.Count)
            {
                Item selectedItem = sellItemList[selectedIndex - 1];
                Account.LoginCharacter.Gold+= (int)(selectedItem.Price * 0.85f);
                GameManager.DeleteItemToInventory(selectedItem.ItemName);
                GameManager.Save();
            }
            else
            {
                WriteLine("잘못된 입력입니다.");
            }
        }
    }
}