using System;
namespace age_of_war
{
    [Serializable]
    public class LightInfantry : Unit, IHealable, IClonable
    {

        public LightInfantry()
        {
            attack = 5;
            defence = 5;
            constHp = 5;
            hp = constHp;
            cost = attack + defence + hp;
            name = "LightInfantry";
        }

        public IClonable Clone
        {
            get { return this.MemberwiseClone() as IClonable; }
        }

        public new void GetHeal(int HealerPower)
        {
            if (hp < ConstHp && hp > 0)
            {
                if (hp + HealerPower >= ConstHp)
                    hp = ConstHp;
                else
                    hp += HealerPower;
            }
        }

        public override void PrintResultAttack(int i, Army army1, Army army2, int j)
        {
            Console.WriteLine($"{i} ход: {army1.ToString()}: {ToString()} атаковал с силой {Attack}");
        }
        public override string ToString()
        {
            return $"{name}";
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