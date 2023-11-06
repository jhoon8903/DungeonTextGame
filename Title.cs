using System.Text;

namespace DungeonTextGame;

public class Title
{
    public static void MainTitle()
    {
        OutputEncoding = Encoding.UTF8;
        PrintLine("                                                                                                                      ", ConsoleColor.Gray);
        PrintLine("                                                                                              ,-.----.               ", ConsoleColor.Gray);
        PrintLine("    ,---,                                                                           ,-.----.  \\    /  \\   ,----..    ", ConsoleColor.Gray);
        PrintLine("  .'  .' `\\                                                                         \\    /  \\ |   :    \\ /   /   \\   ", ConsoleColor.Gray);
        PrintLine(",---.'     \\         ,--,      ,---,                      ,---.       ,---,         ;   :    \\|   |  .\\ |   :     :  ", ConsoleColor.Cyan);
        PrintLine("|   |  .`\\  |      ,'_ /|  ,-+-. /  | ,----._,.          '   ,'\\  ,-+-. /  |        |   | .\\ :.   :  |: .   |  ;. /  ", ConsoleColor.Cyan);
        PrintLine(":   : |  '  | .--. |  | : ,--.'|'   |/   /  ' /  ,---.  /   /   |,--.'|'   |        .   : |: ||   |   \\ .   ; /--`   ", ConsoleColor.Yellow);
        PrintLine("|   ' '  ;  :'_ /| :  . ||   |  ,\"' |   :     | /     \\ .   ; ,. |   |  ,\"' |        |   |  \\ :|   : .   ;   | ;  __  ", ConsoleColor.Yellow);
        PrintLine("'   | ;  .  |  ' | |  . .|   | /  | |   | .\\  ./    /  '   | |: |   | /  | |        |   : .  /;   | |`-'|   : |.' .' ", ConsoleColor.Green);
        PrintLine("|   | :  |  |  | ' |  | ||   | |  | .   ; ';  .    ' / '   | .; |   | |  | |        ;   | |  \\|   | ;   .   | '_.' : ", ConsoleColor.Green);
        PrintLine("'   : | /  ;:  | : ;  ; ||   | |  |/   .   . '   ;   /|   :    |   | |  |/         |   | ;\\  \\   ' |   '   ; : \\  | ", ConsoleColor.Magenta);
        PrintLine("|   | '` ,/ '  :  `--'   |   | |--'  `---`-'| '   |  / |\\   \\  /|   | |--'          :   ' | \\.:   : :   '   | '/  .' ", ConsoleColor.Magenta);
        PrintLine(";   :  .'   :  ,      .-.|   |/      .'__/_/: |   :    | `----' |   |/              :   : '- ' |   | :   |   :    /   ", ConsoleColor.Red);
        PrintLine("|   ,.'      `--`----'   '---'       |   :    :\\   \\  /         '---'               |   |.'   `---'.|    \\   \\ .'    ", ConsoleColor.Red);
        PrintLine("'---'                                 \\   \\  /  `----'                              `---'       `---`     `---`      ", ConsoleColor.Gray);
        PrintLine("                                       `--`-'                                                                        ", ConsoleColor.Gray);
        
        WriteLine("\n\n");
        ForegroundColor = ConsoleColor.Yellow;
        WriteLine("1. 로그인");
        WriteLine("2. 아이디생성");
        WriteLine("3. 종료");
        ResetColor();

        // 사용자 입력을 받습니다.
        WriteLine("\n옵션을 선택하고 Enter 키를 누르세요.");
        string userInput = ReadLine();
        // 사용자 입력에 따라 로직을 추가할 수 있습니다.
    }

    static void PrintLine(string line, ConsoleColor color)
    {
        ForegroundColor = color;
        WriteLine(line);
        ResetColor();
    }
}