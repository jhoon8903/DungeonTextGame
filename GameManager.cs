global using static System.Console;
namespace DungeonTextGame;

public static class GameManager
{
    public static void GameStart()
    {
        Clear();
        while (true)
        {
            WriteLine("스파르타 마을에 오신 여러분 환영합니다."); 
            WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            WriteLine();
            WriteLine("1. 상태 보기"); 
            WriteLine("2. 인벤토리");
        
            WriteLine("원하시는 행동을 입력해주세요"); 
            Write(">>");
            string? command = ReadLine();

            if (command == "1")
            {
                ViewStatus.Status();
                break;
            }
            else if (command == "2")
            {
                ViewInventory.Inventory();
                break;
            }
            else
            {
                Clear(); 
                WrongCommand();
            }
        }
    }

    public static void WrongCommand()
    {
        WriteLine("입력 값이 잘못 되었습니다. 다시 입력해주세요");
        WriteLine();
    }
}