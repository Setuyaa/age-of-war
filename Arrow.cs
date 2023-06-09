using System;
namespace age_of_war
{
    [Serializable]
    public class Arrow : Unit, ISpecialAbility
    {
        public int power = 5;
        public int range { get; }
        public Arrow()
        {
            constHp = 10;
            hp = constHp;
            attack = 5;
            defence = 5;
            range = 3;
            cost = attack + defence + hp + (power + range) * 2;
            name = "Archer";
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
        public void Action(ISpecialAbility arrow1, Army army1, ArrayOfArmies army, int i, int j, int hplace)
        {
            for (int k = 0; k < army.armyNumber; k++)
            {
                Army army2 = army.array[k];
                Arrow arrow = (Arrow)arrow1;
                Proxy proxy = army2.army[0];
                Action act = new Action();
                Game game = new Game();
                if (army2.army.Count() > 0)
                {
                    if (proxy.IsStillAlive())
                    {
                        Console.WriteLine($"{i} ход: {army1.ToString()}: {arrow.ToString()} атаковал с силой {arrow.power}");
                        proxy.GetHit(arrow.power, game.ArmyPrice, i, army2);
                    }
                    act.CheckDeath(i, proxy, army2, army1, j);
                }
            }
        }
    }
}