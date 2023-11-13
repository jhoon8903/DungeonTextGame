namespace DungeonTextGame;

public static class Program
{
    public static async Task Main()
    { 
        Account.LoadAccountData();
        await Title.MainTitle();
    }
}