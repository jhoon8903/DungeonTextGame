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
            WriteLine("0. 나가기"); 
            WriteLine();
            WriteLine("원하시는 행동을 입력해주세요"); 
            Write(">>");
            string? command = ReadLine();
            if (command == "1")
            { 
                StoreDisplay(GameManager.SelectCommand.Buying);
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

    private static void StoreDisplay(GameManager.SelectCommand command)
    {
        Clear();
        string storeTitle = command == GameManager.SelectCommand.Show ? "상점" : "상점 - 아이템 구매";
        WriteLine(storeTitle); 
        WriteLine("필요한 아이템을 얻을 수 있는 상점입니다."); 
        WriteLine();
        WriteLine("[보유 골드]"); 
        WriteLine($"{Character.Gold} G");
        WriteLine();
        var table = new ConsoleTable("---아이템 이름---", "---효과---", "----------------------아이템 설명----------------------", "--아이템 가격--");
        int itemIndex = 1;
        foreach (var item in GameManager.ItemList)
        {
            string itemPurchase = item.Buying ? "구매완료" : $"{item.Price}";
            string indexDisplay = command == GameManager.SelectCommand.Buying ? $"{itemIndex++}. " : string.Empty;
            string itemType = item.ItemType switch
            {
                Item.ItemTypes.Weapon => "공격력",
                Item.ItemTypes.Defender => "방어력",
                Item.ItemTypes.Position => "회복량", 
                Item.ItemTypes.Consumable => "수량",
                _ => ""
            };
            table.AddRow($"{indexDisplay}{item.ItemName}", $"{itemType} +{item.ItemStatus}", item.ItemDesc, $"{itemPurchase} G");
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
            WriteLine();
            Write("0. 나가기"); 
            WriteLine();
            WriteLine("구매하려는 아이템의 번호를 입력하세요.");
            Write(">>");
            string? command = ReadLine();
            if (int.TryParse(command, out int selectedIndex) && selectedIndex > 0 && selectedIndex <= GameManager.ItemList.Count)
            {
                Item selectedItem = GameManager.ItemList[selectedIndex - 1];
                if (selectedItem.Buying)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("이미 구매한 아이템입니다.");
                    ResetColor();
                    continue;
                }
                if (!selectedItem.Buying && Character.Gold > selectedItem.Price)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("구매를 완료했습니다.");
                    ResetColor();
                    selectedItem.Buying = true;
                    Character.Gold -= selectedItem.Price;
                    GameManager.Save();
                    continue;
                }
                else if (!selectedItem.Buying && Character.Gold < selectedItem.Price)
                {
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine("Gold 가 부족합니다.");
                    ResetColor();
                    continue;
                }
            }
            else
            {
                WriteLine("잘못된 입력입니다.");
                continue;
            }
            break;
        }
    }
}