using System;
namespace age_of_war
{
    [Serializable]
    public class Proxy: Unit
    {
        private Unit unit;
        public Proxy(Unit unit)
        {
            this.unit = unit;
        }
        public new int Attack
        {
            get { return unit.Attack; }
            set { unit.Attack = value; }
        }
        public new int Defence
        {
            get { return unit.Defence; }
            set { unit.Defence = value; }
        }

        public new int Hp
        {
            get { return unit.Hp; }
            set { unit.Hp = value; }
        }
        public new int ConstHp
        {
            get { return unit.ConstHp; }
        }
        public new string Name
        {
            get { return unit.Name; }
            set { unit.Name = value; }
        }

        public bool BuffOn
        {
            get { return unit.BuffOn; }
            set { unit.BuffOn = value; }
        }
        public int Range() {
            if (unit is ISpecialAbility)
                return ((ISpecialAbility)unit).range;
            return 0;
        }
        public void GetHeal(int power) {
            if (unit is IHealable)
                ((IHealable)unit).GetHeal(power);
        }
        public new void GetHit(int oppAtt, int ArmyPrice, int i, Army army2) {
            if (Access()) // доступ
            {
                unit.GetHit(oppAtt, ArmyPrice);
                PrintResultDefence(i, army2);
            }
        }
        public override void PrintResultAttack(int i, Army army1, Army army2, int j)
        {
            unit.PrintResultAttack(i, army1, army2, j);
            //log
        }
        public override void PrintResultDefence(int i, Army army2)
        {
            unit.PrintResultDefence(i, army2);
            //log
        }
        public new bool IsStillAlive()
        {
            return unit.IsStillAlive();
        }
        public void Action(Proxy SA, Army army1, ArrayOfArmies army2, int i, int j)
        {
            if (unit is ISpecialAbility)
            {
                int hplace = 0;
                if (unit is Cloner) hplace = GetClonerPlace(army1);
                ((ISpecialAbility)unit).Action((ISpecialAbility)unit, army1, army2, i, j, hplace);
            }
        }
        public new bool CheckSA()
        {
            if (unit is ISpecialAbility)
                return true;
            return false;
        }
        public bool IsIHealable()
        {
            if (unit is IHealable)
                return true;
            return false;
        }
        public bool IsIClonable()
        {
            if (unit is IClonable)
                return true;
            return false;
        }
        public bool Probability() {
            if (unit is Cloner)
            {
                Cloner cloner = (Cloner)unit;
                return cloner.Probability();
            }
            return false;
        }
        public int GetClonerPlace(Army army)
        {
            if (unit is Cloner)
            {
                return army.army.IndexOf(this);
            }
            return -1;
        }
        public IClonable Clone()
        {
            if (unit is IClonable)
                return ((IClonable)unit).Clone;
            return null;
        } 
        private bool Access() {
            return true;
        }
        public override string ToString() {
            return unit.ToString();
        }
    }
}

