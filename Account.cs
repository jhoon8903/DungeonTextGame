using System.Text;
using System.Text.Json;

namespace DungeonTextGame;

public static class Account
{
    public static Character LoginCharacter { get; set; } = null!;
    public static List<Character>? Users = new();
    private static readonly HttpClient Client = new HttpClient();
    private const string LoginHost = "http://52.78.28.10:3000/login";
    private const string CreateHost = "http://52.78.28.10:3000/createAccount";

    public static async Task LogIn()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("L O G I N\n");
            ResetColor();
            WriteLine("0. 나가기");
            Write("ID를 입력하세요 >>  ");
            string? id = ReadLine();
            Write("Password를 입력하세요 >>  ");
            string? pw = ReadLine();

            if (id == "0" || pw == "0") await Title.MainTitle();

            // Character? user = Users.FirstOrDefault(u => u.Id == id);
            var loginResult = await LoginRequest(id, pw);

            if (loginResult.Item1)
            {
                if (id != null)
                    if (loginResult.Character != null)
                        LoginCharacter = loginResult.Character;
                await GameManager.GameStart();
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Id 또는 Pw가 올바르지 않습니다.\n");
                ResetColor();
                continue;
            }
            break;
        }
    }

    private static async Task<(bool, Character? Character, string)> LoginRequest(string? id, string? pw)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { id, pw }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(LoginHost, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<LoginResponse>(responseContent);
            return (true, result?.Character, "");
        }
        var errorResult = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
        return (false, null, errorResult?.Message ?? "Unknown Error");
    }

    internal class LoginResponse
    {
        public Character? Character { get; set; }
    }

    public static async Task CreateAccount()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Create Account");
            WriteLine("0. 나가기");
            WriteLine("\n");
            ResetColor();
            Write("생성할 ID를 입력하세여 (영) >>  ");
            string id = ReadLine();
            if (id == "0")
            {
                await Title.MainTitle();
                break;
            }
            string pw = InputPassword();
            var createAccountResult = await CreateAccountRequest(id, pw);

            if (createAccountResult.success)
            {
                WriteLine("계정이 생성 되었습니다. 로그인 해 주세요");
                break;
            }

            ForegroundColor = ConsoleColor.Red;
            WriteLine(createAccountResult.message);
            ResetColor();
        }
    }

    private static async Task<(bool success, string message)> CreateAccountRequest(string id, string pw)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { id, pw }), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(CreateHost, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (true, "Account created successfully");
        }

        var errorResult = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
        return (false, errorResult?.Message ?? "Unknown Error");
    }

    private static string InputPassword()
    {
        string pw, checkPw;
        do
        {
            Write("비밀번호를 입력하세요 (영/숫자) >>  ");
            pw = ReadLine();
            Write("(확인)동일한 비밀번호를 다시 입력해주세요 >> ");
            checkPw = ReadLine();

            if (pw != checkPw)
            {
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("비밀번호가 일치하지 않습니다. 다시 확인해주세요.");
                ResetColor();
            }
        } while (pw != checkPw);

        return pw;
    }
}

internal class ErrorResponse
{
    public ErrorResponse(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}