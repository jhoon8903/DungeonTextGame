
namespace DungeonTextGame;

public class ChatMessage
{
    public string User { get; set; }
    public string Message { get; set; }
}

public static class ChatSocket
{
    private static SocketIOClient.SocketIO _client;
    private static string ID { get; set; }

    public static async Task ChatRoomAsync()
    {
        Clear();
        ID = Account.LoginCharacter.Id;
        await ConnectToNestServer();
    }


    private static async Task ConnectToNestServer()
    {
        _client = new SocketIOClient.SocketIO("http://127.0.0.1:3000/chat");

        _client.On("message", response =>
        {
            string json = response.ToString();
            WriteLine(json);
        });

        await _client.ConnectAsync();
    }
}