using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Konscious.Security.Cryptography;

namespace DungeonTextGame;

public class Account
{
    public static Character LoginCharacter { get; set; } = null!;
    public static List<Character>? Users = new ();

    public static void LoadAccountData()
    {
        string jsonFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/User.json";
        string jsonString = File.ReadAllText(jsonFilePath);
        if (jsonString != null)
        {
            Users = JsonSerializer.Deserialize<List<Character>>(jsonString);
        }
    }
    
    public static void LogIn()
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

            if (id == "0" || pw == "0") Title.MainTitle();

            Character? user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine("유저 정보가 존재하지 않습니다.\n");
                ResetColor();
                continue;
            }
            if (VerifyPassword(pw, user.Pw))
            {
                LoginCharacter = user;
                GameManager.GameStart();
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

    public static void CreateAccount()
    {
        while (true)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("Create Account");
            WriteLine("\n");
            ResetColor();
            Write("생성할 ID를 입력하세여 (영) >>  ");
            string id = ReadLine();
            Character? user = Users.FirstOrDefault(u => u.Id == id);
            if (user!= null)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("이미 존재하는 계정입니다.");
                ResetColor();
                continue;
            }
            var pw = InputPassword();
            SaveAccount(id, pw);
            break;
        }
    }

    private static string InputPassword()
    {
        string finalPassword = "";
        Write("비밀번호를 입력하세요 (영/숫자) >>  ");
        string pw = ReadLine();

        Write("(확인)동일한 비밀번호를 다시 입력해주세요 >> ");
        string checkPw = ReadLine();

        if (pw != checkPw)
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("비밀번호가 일치하지 않습니다. 다시 확인해주세요.");
            ResetColor();
            InputPassword();
        }
        else
        {
            finalPassword = pw;
        }
        return finalPassword;
    }

    private static void SaveAccount(string id, string pw)
    {
        string hashedPassword = HashPassword(pw);
        const string accountFilePath = "/Users/daniel/Documents/GitHub/DungeonTextGame/User.json";
        string jsonString = File.ReadAllText(accountFilePath, Encoding.UTF8);
        List<Character> accounts = JsonSerializer.Deserialize<List<Character>>(jsonString) ?? new List<Character>();
        Character newAccount = new Character
        {
            Id = id,
            Pw = hashedPassword,
            Level = 1,
            Exp = 0.0f,
            Job = Character.Jobs.무직,
            Damage = 10.0f,
            ItemDamage = 0,
            Defence = 5.0f,
            ItemDefence = 0,
            HealthPoint = 100.0f,
            Gold = 1500,
            Inventory = new Dictionary<string, InventoryItem>
            {
                { "무쇠갑옷", new InventoryItem { ToOwn = true, Equipment = false } },
                { "낡은 검", new InventoryItem { ToOwn = true, Equipment = false } }
            }
        };
        accounts.Add(newAccount);
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        jsonString = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(accountFilePath, jsonString, Encoding.UTF8);
        WriteLine("계정이 생성 되었습니다. 로그인 해 주세요");
        while (true) 
        {
            WriteLine("Enter를 입력하면 타이틀 화면으로 돌아갑니다.");
            ConsoleKeyInfo inputKey = ReadKey();
            if (inputKey.Key == ConsoleKey.Enter)
            {
               Program.Main();
            }
            else
            {
                WriteLine("입력값이 올바르지 않습니다.");
                continue;
            }
            break;
        }
    }

    private static bool VerifyPassword(string inputPassword, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);
    
        // Salt는 Hash에서 추출하거나 별도로 저장해야 합니다.
        // 예를 들어, 첫 16바이트가 Salt라고 가정합니다.
        byte[] salt = hashBytes.Take(16).ToArray();
    
        // Hash 부분을 추출합니다.
        byte[] storedHashBytes = hashBytes.Skip(16).ToArray();
    
        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(inputPassword)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; 
            argon2.MemorySize = 65536;
            argon2.Iterations = 4;

            // 입력된 비밀번호로부터 생성된 해시
            byte[] newHashBytes = argon2.GetBytes(storedHashBytes.Length);

            // 저장된 해시와 입력된 비밀번호의 해시를 비교
            return newHashBytes.SequenceEqual(storedHashBytes);
        }
    }
    
    private static string HashPassword(string password)
    {
        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            byte[] salt = CreateSalt();
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; 
            argon2.MemorySize = 65536;
            argon2.Iterations = 4;

            // 해시 생성
            byte[] hashBytes = argon2.GetBytes(128);

            // 해시 앞에 솔트를 붙여서 전체를 Base64 인코딩으로 변환
            byte[] hashWithSaltBytes = new byte[salt.Length + hashBytes.Length];
            Array.Copy(salt, 0, hashWithSaltBytes, 0, salt.Length);
            Array.Copy(hashBytes, 0, hashWithSaltBytes, salt.Length, hashBytes.Length);

            return Convert.ToBase64String(hashWithSaltBytes);
        }
    }

    private static byte[] CreateSalt()
    {
        var buffer = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(buffer);
        }
        return buffer;
    }
}