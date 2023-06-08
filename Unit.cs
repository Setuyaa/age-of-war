using System;
namespace age_of_war
{
    [Serializable]
    public abstract class Unit
    {
        protected int hp; // Максимальное HP
        protected int constHp; // Текущее HP
        protected int attack; // Атака
        protected int defence; // Защита
        protected int cost; // Стоимость юнита
        protected string name;
        public bool BuffOn = false; // false - баффы не надеты, true - надеты или были надеты (надеть больше нельзя)
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        public int Defence
        {
            get { return defence; }
            set { defence = value; }
        }
        public int ConstHp
        {
            get { return constHp; }
            set { constHp = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = attack + defence + hp; }
        }
        public string Name { get { return name; } set {name = value ;} }
        public void GetHit(int oppAtt, int ArmyPrice)
        {
            // oppAtt - сила атаки
            var minus = (int)Math.Round((decimal)((ArmyPrice - defence) * oppAtt / 100));
            hp -= minus;
        }
        public bool IsStillAlive()
        {
            bool t = true;
            if (hp <= 0) t = false;
            return t;
        }
        public abstract void PrintResultAttack(int i, Army army1, Army army2, int j);
        //   {
        // Console.WriteLine($"{i} ход: {army1.ToString()}: {ToString()} атаковал с силой {Attack}");
        //    }
        public abstract void PrintResultDefence(int i, Army army2);
        //{
        //    if (hp <= 0)
        //        Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} был убит");
        //    else
        //        Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} остался жив");
        //}
        internal IHealable GetHeal(int power)
        {
            throw new NotImplementedException();
        }
        public void CheckSA() { }
    }
}

