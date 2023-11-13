using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace DungeonTextGame
{
    public static class ChatSocket
    {
        private static ClientWebSocket _client;
        private static string ID { get; set; }

        public static async Task ChatRoomAsync()
        {
            ClearScreen();
            WriteToConsole("[Chat Room]");
            ID = Account.LoginCharacter.Id;
            await ConnectToServerAsync();
            var receiveTask = ReceiveMessagesAsync(); // 메시지 수신 작업 시작
            while (_client.State == WebSocketState.Open)
            {
                string input = ReadInput(); // 사용자 입력 받기
                if (string.IsNullOrEmpty(input))
                    break;
                await SendMessageAsync(input);
            }
            await receiveTask; // 메시지 수신 작업 완료 대기
        }
        private static async Task SendMessageAsync(string message)
        {
            ClearCurrentConsoleLine(); // 메시지 보내기 전에 현재 콘솔 줄을 지웁니다.

            ChatMessage chatMessage = new ChatMessage { User = ID, Message = message };
            string json = JsonSerializer.Serialize(chatMessage);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            await _client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = CursorTop - 1; // 현재 줄에서 한 줄 위로 이동
            SetCursorPosition(0, currentLineCursor);
            Write(new string(' ', WindowWidth)); 
            SetCursorPosition(0, currentLineCursor);
        }

        private static async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[1024];
            try
            {
                while (_client.State == WebSocketState.Open)
                {
                    var result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    string receivedMessageJson = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    ChatMessage receivedMessage = JsonSerializer.Deserialize<ChatMessage>(receivedMessageJson);
                    WriteToConsole($"{receivedMessage.User} : {receivedMessage.Message}");
                }
            }
            catch (Exception ex)
            {
                WriteToConsole($"Error in receiving messages: {ex.Message}");
            }
        }

        private static async Task ConnectToServerAsync()
        {
            _client = new ClientWebSocket();
            await _client.ConnectAsync(new Uri("ws://127.0.0.1:3000"), CancellationToken.None);
            WriteToConsole("Connected!");
        }
        private static void ClearScreen() => Clear();
        private static void WriteToConsole(string message) => WriteLine(message);
        private static string ReadInput() => ReadLine();
    }


    public class ChatMessage
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
}