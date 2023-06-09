using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace age_of_war
{
    [Serializable]
    public abstract class UnitDecorator : HeavyInfantry
    {
        public UnitDecorator(string n, Unit unit, int buffAttack, int buffDefence)
        {
            Name = n;
            Attack = Attack + buffAttack;
            Defence = Defence + buffDefence;
        }
        public override void PrintResultAttack(int i, Army army1, Army army2, int j)
        {
            Console.WriteLine($"{i} ход: {army1.ToString()}: {army1.army[0].ToString()} атаковал с силой {army1.army[0].Attack}");
        }

        public override void PrintResultDefence(int i, Army army2)
        {
            if (hp <= 0)
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} был убит");
            else
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} остался жив");
        }
    }
    [Serializable]
    public class HeavyInfantryHelmet : UnitDecorator //  heavy infantry со шлемом
    {
        static int buff = 5; // +5 к атаке
        public HeavyInfantryHelmet(Unit unit) : base(unit.Name + " со шлемом", unit, buff, 0)
        {
        }
    }
    [Serializable]
    public class HeavyInfantryShield : UnitDecorator //  heavy infantry с щитом
    {
        static int buff = 5; // +5 к защите
        public HeavyInfantryShield(Unit unit) : base(unit.Name + " с щитом", unit, 0, buff)
        {
        }

    }
    [Serializable]
    public class HeavyInfantryHourse : UnitDecorator //  heavy infantry с лошадью
    {
        static int buffAtt = 5; // +5 к атаке
        static int buffDef = 5; // +5 к защите
        public HeavyInfantryHourse(Unit unit) : base(unit.Name + " на лошади", unit, buffAtt, buffDef)
        {
        }
    }
}