global using static System.Console;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DungeonTextGame;

public static class GameManager
{
    public static List<Item> ItemList = new List<Item>();

    public enum SelectCommand { Show, Equip, Buying, Selling }

    public static void GameStart()
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
            WriteLine("4. 던전입장\n");
            WriteLine("원하시는 행동을 입력해주세요"); 
            Write(">>");
            string? command = ReadLine();

            if (command == "1")
            {
                ViewStatus.Status();
                break;
            }
            if (command == "2")
            {
                ViewInventory.Inventory();
                break;
            }
            if (command == "3")
            {
                Store.StoreShop();
                break;
            }

            if (command == "4")
            {
                Dungeon.DungeonLobby();
                break;
            }
            Clear(); 
            WrongCommand();
        }
    }
    private static void Login()
    {
        Write("ID를 입력하세요");
        string id = ReadLine(); 
        Write("Password를 입력하세요");
        string pw = ReadLine();
    }

    private static void InitItem()
    {
        const string itemFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/Item.json";
        string read;
        using (StreamReader r = new StreamReader(itemFilePath, Encoding.UTF8))
        {
            read = r.ReadToEnd();
        }
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        ItemList = JsonSerializer.Deserialize<List<Item>>(read, options);
    }

    public static void WrongCommand()
    {
        Clear();
        WriteLine("입력 값이 잘못 되었습니다. 다시 입력해주세요\n");
    }

    public static void Save()
    {
        const string itemFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/User.json";
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        string json = JsonSerializer.Serialize(Account.Users, options);
        File.WriteAllText(itemFilePath, json);
    }
}