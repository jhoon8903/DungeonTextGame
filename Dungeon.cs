using ConsoleTables;
using static DungeonTextGame.Account;

namespace DungeonTextGame;

public static class Dungeon
{
    private static readonly Random Random = new Random();
    private static float _fullDamage;
    private static int _fullDefence;
    private static int _requireDefence;

    public static void DungeonLobby()
    {
        Clear();
        ForegroundColor = ConsoleColor.Magenta;
        WriteLine("던전입장"); 
        ResetColor();
        WriteLine("이곳은 던전 로비 입니다.");
        Write("케릭터 HP : ");
        ForegroundColor = ConsoleColor.Red;
        Write(LoginCharacter.HealthPoint);
        WriteLine();
        Write("케릭터 레벨 : ");
        ForegroundColor = ConsoleColor.Yellow;
        Write(LoginCharacter.Level);
        ResetColor();
        WriteLine();
        Write("케릭터 공격력 : ");
        ForegroundColor = ConsoleColor.Green;
        _fullDamage = LoginCharacter.Damage + LoginCharacter.ItemDamage;
        Write(_fullDamage);
        ResetColor();
        WriteLine();
        Write("케릭터 방어력 : ");
        ForegroundColor = ConsoleColor.Yellow;
        _fullDefence = LoginCharacter.Defence + LoginCharacter.ItemDefence;
        Write(_fullDefence);
        ResetColor();
        WriteLine();
        ForegroundColor = ConsoleColor.Cyan;
        WriteLine($"다음 레벨업 까지 남은 던전 클리어 횟수 : { LoginCharacter.Level - LoginCharacter.dungeonCleatCount}회");
        ResetColor();
        switch (LoginCharacter.HealthPoint)
        {
            case > 0:
            {
                var table = new ConsoleTable("--던전 난이도--", "---입장 권장사항---");
                table.AddRow("1. 쉬운 던전","방어력 5 이상 권장");
                table.AddRow("2. 일반 던전","방어력 11 이상 권장");
                table.AddRow("3. 어려운 던전", "방어력 17 이상 권장");
                table.AddRow("0. 나가기", "메인 로비");
                table.Write();
                while (true)
                {
                    WriteLine("\n입장 던전의 번호를 입력해주세요");
                    ForegroundColor = ConsoleColor.Yellow;
                    Write(">>");
                    ResetColor();
                    if (int.TryParse(ReadLine(), out int selectCommand))
                    {
                        WriteLine(selectCommand);
                        switch (selectCommand)
                        {
                            case 0:
                             GameManager.GameStart();
                                return;
                            case 1:
                            case 2:
                            case 3:
                                DungeonEntrance(selectCommand);
                                break;
                            default:
                                WriteLine("올바르지 않은 입력 값 입니다. 확인해주세요");
                                continue;
                        }
                    }
                }
            }
            case <= 0:
                ForegroundColor = ConsoleColor.Red;
                WriteLine("HP가 0 입니다. 키를 입력하시면 Main 화면으로 복귀합니다.");
                ResetColor();
                ReadKey(); 
                GameManager.GameStart();
                break;
        }
    }

    private static void DungeonEntrance(int dungeonLevel)
    {
        switch (dungeonLevel)
        {
            case 1:
                _requireDefence = 5;
                DungeonClearProcess(_fullDefence <= _requireDefence ? 60 : 100, 1);
                break;
            case 2:
                _requireDefence = 11;
                DungeonClearProcess(_fullDefence <= _requireDefence ? 60 : 100, 2);
                break;
            case 3:
                _requireDefence = 17;
                DungeonClearProcess(_fullDefence <= _requireDefence ? 60 : 100, 2);
                break;
        }
    }

    private static void DungeonClearProcess(int chance, int dungeonLevel)
    {
        if (Random.Next(100) > chance)
        {
            FailDungeon(dungeonLevel);
        }
        else
        {
            ClearDungeon(dungeonLevel);
        }
    }
                                                                                                          
    private static void FailDungeon(int dungeonLevel)
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine("던전 탐험 실패");
        ResetColor();
        WriteLine($"{MappingDungeonLevel(dungeonLevel)} 던전 탐험에 실패 하였습니다.\n");
        WriteLine("[탐험 결과]"); 
        WriteLine($"체력 {LoginCharacter.HealthPoint} => {LoginCharacter.HealthPoint /=2 }\n");
        GameManager.Save();
        WriteLine("아무키나 입력하세요");
        ReadKey();
        DungeonLobby();
    }

    private static void ClearDungeon(int dungeonLevel)
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine("던전 탐험 성공");
        ResetColor();
        WriteLine($"{MappingDungeonLevel(dungeonLevel)} 던전 탐험에 성공 하였습니다.\n");
        WriteLine("[탐험 결과]");
        int[] hp = DecreaseCharacterHp();
        WriteLine($"체력 {hp[0]} => {hp[1]}");
        LoginCharacter.HealthPoint = hp[1];
        int[] gold = IncreaseGold(dungeonLevel);
        WriteLine($"Gold {gold[0]} => {gold[1]}");
        LoginCharacter.Gold = gold[1];
        LoginCharacter.dungeonCleatCount++;
        if (LoginCharacter.Level == LoginCharacter.dungeonCleatCount)
        {
            LoginCharacter.Level++;
            LoginCharacter.dungeonCleatCount = 0;
            LoginCharacter.HealthPoint = 100;
        }
        GameManager.Save();
        WriteLine("아무키나 입력하시면 로비로 돌아갑니다.");
        ReadKey();
        DungeonLobby();
    }

    private static string MappingDungeonLevel(int dungeonLevel)
    {
        return dungeonLevel switch
        {
            1 => "쉬움",
            2=> "보통",
            3=> "어려운",
        };
    }

    private static int[] DecreaseCharacterHp()
    {
        int[] hp = new int[2];
        int decreaseValue = _fullDefence - _requireDefence;
        int minHp = 20 + decreaseValue;
        int maxHp = 35 + decreaseValue;
        var decreaseHp = Random.Next(minHp, maxHp+1);
        int originHp = LoginCharacter.HealthPoint;
        hp[0] = originHp;
        hp[1] = decreaseHp;
        return hp;
    }

    private static int[] IncreaseGold(int dungeonLevel)
    {
        int[] gold = new int[2];
        float fullDamage = LoginCharacter.Damage + LoginCharacter.ItemDamage;
        int bonusRate = Random.Next((int)fullDamage, (int)fullDamage * 2 + 1) / 100;
        int increaseGold = dungeonLevel switch
        {
           1 => 1000 + 1000 * bonusRate,
           2 => 1700 + 1700 * bonusRate,
           3 => 2500 + 2500 * bonusRate
        };
        gold[0] = LoginCharacter.Gold;
        gold[1] = LoginCharacter.Gold + increaseGold;
        return gold;
    }
}