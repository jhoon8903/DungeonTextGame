using SocketIOSharp.Client;
using System.Text.Json;
using EngineIOSharp.Common.Enum;

namespace DungeonTextGame;

public class ChatMessage
{
    public string User { get; set; }
    public string Message { get; set; }
}

public static class ChatSocket
{
    private static SocketIOClient _webSocket;
    private static string ID { get; set; }

    public static async Task ChatRoomAsync()
    {
        Clear();
        ID = Account.LoginCharacter.Id;
        await ConnectToNestServer();

        while (true)
        {
            string inputMessage = ReadLine();
            await SendMessage(inputMessage);

            if (inputMessage.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                LogoutChatRoom();
                break;
            }
        }
    }


    private static Task ConnectToNestServer()
    {
        _webSocket = new SocketIOClient(new SocketIOClientOption(EngineIOScheme.http, "127.0.0.1", 3000, 843,"/chat")); 
        WriteLine($"Socket : {_webSocket}");

        _webSocket.On("connect", () => 
        {
            WriteLine($"{ID}님이 채팅방에 입장하셨습니다.");
            _webSocket.Emit("join", ID); // 예시로 'join' 이벤트를 서버에 보냅니다.
        });

        _webSocket.On("message", data =>
        {
            string json = data[0].ToString();
            // data[0]는 첫 번째 매개변수를 가정합니다. 필요에 따라 인덱스를 조정하십시오.
            ChatMessage message = JsonSerializer.Deserialize<ChatMessage>(json);
            ConsoleMessage(message);
        });

        _webSocket.Connect();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 메세지를 보내는 메서드
    /// </summary>
    private static Task SendMessage(string inputMessage)
    {
        ChatMessage message = new ChatMessage { User = ID, Message = inputMessage };
        _webSocket.Emit("message", JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }

    private static void ConsoleMessage(ChatMessage message)
    {
        if (message.User == ID)
        {
            ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            ResetColor();
        }
        WriteLine($"{message.User} : {message.Message}");
    }


    /// <summary>
    /// 메세지방을 나가는 메서드
    /// </summary>
    private static void LogoutChatRoom()
    {
        _webSocket.Close();
        GameManager.GameStart();
    }
}