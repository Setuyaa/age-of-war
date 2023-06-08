using System;
namespace age_of_war
{
    [Serializable]
    public class Knight : Unit
    {
        public Knight()
        {
            constHp = 20;
            hp = constHp;
            attack = 10;
            defence = 10;
            cost = attack + defence + hp;
            name = "Knight";
        }
        public override string ToString()
        {
            return $"{name}";
        }
        public override void PrintResultAttack(int i, Army army1, Army army2, int j)
        {
            Console.WriteLine($"{i} ход: {army1.ToString()}: {ToString()} атаковал с силой {Attack}");
        }

        public override void PrintResultDefence(int i, Army army2)
        {
            if (hp <= 0)
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} был убит");
            else
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} остался жив");
        }
    }
}