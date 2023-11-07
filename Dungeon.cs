using ConsoleTables;

namespace DungeonTextGame;

public class Dungeon
{
    public static void DungeonLobby()
    {
        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("던전입장"); 
        ResetColor();
        WriteLine("이곳은 던전 로비 입니다.");
        var table = new ConsoleTable("--던전 난이도--", "---입장 권장사항---");
        table.AddRow("1. 쉬운 던전","방어력 5 이상 권장" );
        table.AddRow("2. 일반 던전","방어력 11 이상 권장");
        table.AddRow("3. 어려운 던전", "방어력 17 이상 권장");
        table.AddRow("0. 나가기", "메인 로비");
        table.Write();
        WriteLine("\n임장 던전의 번호를 입력해주세요");
        ForegroundColor = ConsoleColor.Yellow;
        Write(">>");
        ResetColor();
    }
}