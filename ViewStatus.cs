using static DungeonTextGame.Character;

namespace DungeonTextGame;

public static class ViewStatus
{
    public static void  Status()
    {
        Character character = Account.LoginCharacter;
        while (true)
        {
            Clear();
            WriteLine("상태 보기");
            WriteLine("케릭터의 정보가 표시됩니다.\n");

            int characterLevel = character.Level;
            string chad = character.Job switch
            {
                Jobs.전사 => "전사",
                Jobs.마법사 => "마법사",
                Jobs.궁수 => "궁수",
                Jobs.도적 => "도적",
                Jobs.성직자 => "성직자",
                Jobs.무직 => "무직",
            };
            float damage = character.Damage;
            float itemDamage = character.ItemDamage;
            int defence = character.Defence;
            int itemDefence = character.ItemDefence;
            float hp = character.HealthPoint;
            float gold = character.Gold;

            WriteLine($"LV : {characterLevel: 00}");
            WriteLine($"Chad : {chad}");
            WriteLine($"공격력 : {damage + itemDamage} (+{itemDamage})");
            WriteLine($"방어력 : {defence + itemDefence} (+{itemDefence})");
            WriteLine($"체력 : {hp}");
            WriteLine($"Gold : {gold} G\n");
            WriteLine("0. 나가기\n");
            WriteLine("원하시는 행동을 입력해주세요");
            string? command = ReadLine();
            if (command != "0")
            {
                GameManager.WrongCommand();
                continue;
            }
            GameManager.GameStart();
            break;
        }
    }
}