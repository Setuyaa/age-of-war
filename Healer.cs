using System;
using System.Text;

namespace age_of_war
{
    [Serializable]
    public class Healer : Unit, ISpecialAbility
    {
        public int power = 5;
        public int range { get; }
        public Healer()
        {
            constHp = 3;
            hp = constHp;
            attack = 2;
            range = 3;
            defence = 1;
            cost = attack + defence + hp + (power + range) * 2;
            name = "Healer";
        }

        public new int Cost
        {
            get { return cost; }
            set { cost = attack + defence + hp + (power + range) * 2; }
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
        public void Action(ISpecialAbility healer1, Army army1, ArrayOfArmies army, int i, int j, int hplace)
        {
            Healer healer = (Healer)healer1;
            if (army1.army[0].IsIHealable()) //  for proxy
            {
                if (army1.army[0].Hp < army1.army[0].ConstHp && army1.army[0].IsStillAlive())
                {
                    (army1.army[0]).GetHeal(healer.power);
                    Console.WriteLine($"{i} ход: {army1.ToString()}: {healer.ToString()} вылечил {army1.army[0].ToString()} с хп {army1.army[0].Hp}");
                }
            }
        }
    }
}