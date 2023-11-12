global using static System.Console;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using static DungeonTextGame.Account;

namespace DungeonTextGame;

public static class GameManager
{
    public static List<Item> ItemList = new List<Item>();
    public enum SelectCommand { Show, Equip, Buying, Selling }
    public const string CharacterFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/User.json"; 
    private const string ItemFilePath =  "/Users/daniel/Documents/GitHub/DungeonTextGame/Item.json";

    public static async Task GameStart()
    {
        Clear();
        InitItem();
        while (true)
        {
            WriteLine("스파르타 마을에 오신 여러분 환영합니다."); 
            WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            WriteLine("1. 상태 보기"); 
            WriteLine("2. 인벤토리");
            WriteLine("3. 상점");
            WriteLine("4. 던전입장");
            WriteLine("5. 휴식하기");
            WriteLine("6. 채팅하기");
            WriteLine("7. 로그아웃\n");
            WriteLine("원하시는 행동을 입력해주세요"); 
            Write(">>");
            string? command = ReadLine();

            if (int.TryParse(command, out int selectNum))
            {
                switch (selectNum)
                {
                    case 1:
                        ViewStatus.Status();
                        return;
                    case 2:
                        ViewInventory.Inventory();
                        return;
                    case 3:
                        Store.StoreShop();
                        return;
                    case 4:
                        Dungeon.DungeonLobby();
                        return;
                    case 5:
                        Rest();
                        return;
                    case 6:
                       await ChatSocket.ChatRoomAsync();
                       WriteLine("채팅방");
                       return;
                    case 7:
                        Title.MainTitle();
                        return;
                    default:
                        WrongCommand();
                        continue;
                }                                    
            }
            else
            {
                continue;
            }
        }
    }
    private static void InitItem()
    {
        string read;
        using (StreamReader r = new StreamReader(ItemFilePath, Encoding.UTF8))
        {
            read = r.ReadToEnd();
        }
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        ItemList = JsonSerializer.Deserialize<List<Item>>(read, options);
    }

    public static void Save()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        var loginCharacters = JsonSerializer.Serialize(Users, options);
        File.WriteAllText(CharacterFilePath, loginCharacters);
    }

    public static void DeleteItemToInventory(string inventoryItem)
    {
        foreach (var character in Users.Where(character => character.Id == LoginCharacter.Id))
        {
            character.Inventory.Remove(inventoryItem);
        }
    }

    public static void WrongCommand()
    {
        Clear();
        WriteLine("입력 값이 잘못 되었습니다. 다시 입력해주세요\n");
    }

    private static void Rest()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("[휴식하기]");
            ResetColor();
            ForegroundColor = ConsoleColor.Yellow;
            Write("500 G");
            ResetColor();
            Write("를 지불 하시면 체력을 회복시킬 수 있습니다.");
            ForegroundColor = ConsoleColor.Yellow;
            Write($"\t보유 골드 : {LoginCharacter.Gold} G");
            ResetColor();
            WriteLine("\n");
            WriteLine("1. 휴식하기");
            WriteLine("0. 나가기\n");
            WriteLine("원하시는 행동을 입력해주세요");
            Write(">> ");
            if (int.TryParse(ReadLine(), out int selectCommand))
            {
                switch (selectCommand)
                {
                    case 1:
                        TakeARest();
                        break;
                    case 0:
                        GameStart();
                        break;
                }
            }
            else
            {
                WrongCommand();
                continue;
            }
            break;
        }
    }

    private static void TakeARest()
    {
        if (LoginCharacter.Gold >= 500)
        {
            LoginCharacter.HealthPoint = 100;
            LoginCharacter.Gold -= 500;
            Save();
            ForegroundColor = ConsoleColor.Green;
            WriteLine("케릭터의 체력이 회복 되었습니다!");

         
        }
        else
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("Gold가 부족합니다!");
            WriteLine("상점에서 아이템을 판매하시거나, 골드가 회복되기를 기다리세요");
            ResetColor();
            WriteLine("아무키나 입력하면 메인화면으로 이동합니다.");
            ReadKey();
        }
        ResetColor();
        WriteLine("아무키나 누르시면 메인화면으로 돌아갑니다.");
        ReadKey();
        GameStart();
    }
}