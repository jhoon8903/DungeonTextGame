using System.Text;

namespace DungeonTextGame;

public static class Title
{
    public static void MainTitle()
    {

        OutputEncoding = Encoding.UTF8;
        string[] menuItems = { "    LOG IN    ", "CREATE ACCOUNT", "    E X I T   " };
        int selectedMenuItem = 0;
        while (true)
        {        
            Clear();
            PrintLine("  ██████╗ ██╗   ██╗███╗   ██╗ ██████╗ ███████╗ ██████╗ ███╗   ██╗    ██████╗ ██████╗  ██████╗ ", ConsoleColor.Gray);
            PrintLine("  ██╔══██╗██║   ██║████╗  ██║██╔════╝ ██╔════╝██╔═══██╗████╗  ██║    ██╔══██╗██╔══██╗██╔════╝ ", ConsoleColor.Gray);
            PrintLine("  ██║  ██║██║   ██║██╔██╗ ██║██║  ███╗█████╗  ██║   ██║██╔██╗ ██║    ██████╔╝██████╔╝██║  ███╗", ConsoleColor.Cyan);
            PrintLine("  ██║  ██║██║   ██║██║╚██╗██║██║   ██║██╔══╝  ██║   ██║██║╚██╗██║    ██╔══██╗██╔═══╝ ██║   ██║", ConsoleColor.Cyan);
            PrintLine("  ██████╔╝╚██████╔╝██║ ╚████║╚██████╔╝███████╗╚██████╔╝██║ ╚████║    ██║  ██║██║     ╚██████╔╝", ConsoleColor.Yellow);
            PrintLine("  ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝    ╚═╝  ╚═╝╚═╝      ╚═════╝ ", ConsoleColor.Yellow);
            WriteLine();

            for (int i = 0; i < menuItems.Length; i++)
            {
                PrintLine($"\t\t\t\t██████ {menuItems[i]} ██████", i == selectedMenuItem ? ConsoleColor.Yellow : ConsoleColor.Gray);
            }

            ConsoleKeyInfo keyInfo = ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                {
                    selectedMenuItem--;
                    if (selectedMenuItem < 0)
                    {
                        selectedMenuItem = menuItems.Length - 1;
                    }
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    selectedMenuItem++;
                    if (selectedMenuItem >= menuItems.Length)
                    {
                        selectedMenuItem = 0;
                    }
                    break;
                }
                case ConsoleKey.Enter:
                    switch (selectedMenuItem)
                    {
                        case 0: 
                            Account.LogIn();
                            break;
                        case 1:
                            Account.CreateAccount();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                    }
                    break;
            }
        }
    }

    private static void PrintLine(string line, ConsoleColor color)
    {
        ForegroundColor = color;
        WriteLine(line);
        ResetColor();
    }
}