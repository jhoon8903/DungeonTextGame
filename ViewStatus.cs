using static DungeonTextGame.Character;

namespace DungeonTextGame;

public static class ViewStatus
{
    public static void Status()
    {
        while (true)
        {
            Clear();
            WriteLine("상태 보기");
            WriteLine("케릭터의 정보가 표시됩니다.");
            WriteLine();


            int characterLevel = Level;
            string chad = Job switch
            {
                Jobs.전사 => "전사",
                Jobs.마법사 => "마법사",
                Jobs.궁수 => "궁수",
                Jobs.도적 => "도적",
                Jobs.성직자 => "성직자",
                Jobs.무직 => "무직",
            };
            float damage = Damage;
            float defence = Defence;
            float hp = HealthPoint;
            float gold = Gold;

            WriteLine($"LV : {characterLevel: 00}");
            WriteLine($"Chad : {chad}");
            WriteLine($"공격력 : {damage + ViewInventory.additonalDamage} (+{ViewInventory.additonalDamage})");
            WriteLine($"방어력 : {defence + ViewInventory.additonalDefence} (+{ViewInventory.additonalDefence})");
            WriteLine($"체력 : {hp}");
            WriteLine($"Gold : {gold} G");
            WriteLine();
            WriteLine("0. 나가기");

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