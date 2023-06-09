namespace age_of_war
{
    [Serializable]
    class Adapter : Unit
    {
        private GulyayGorod gulyayGorod;

        public Adapter()
        {
            gulyayGorod = new GulyayGorod(10, 30, 40);
            //name = ";
            attack = gulyayGorod.GetStrength();
            defence = gulyayGorod.GetDefence();
            cost = gulyayGorod.GetCost();
            constHp = gulyayGorod.GetHealth();
            hp = constHp;
            name = "GulyayGorod";
    }

        public new bool IsStillAlive()
        {
            return gulyayGorod.IsDead;
        }

        public new void GetHit(int oppAtt, int ArmyPrice, int i, Army army)
        {
            gulyayGorod.TakeDamage(oppAtt, ArmyPrice, i, army);
            hp = gulyayGorod.GetCurrentHealth();
        }

        public override void PrintResultAttack(int i, Army army1, Army army2, int j)
        {
            Console.WriteLine($"{i} ход: {army1.ToString()}: {name} не атакует, атака {Attack}");
        }

        public override void PrintResultDefence(int i, Army army2)
        {
            if (hp <= 0)
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} был убит");
            else
                Console.WriteLine($"{i} ход: {army2.ToString()}: {ToString()} с защитой {Defence} остался жив");
        }
        public override string ToString()
        {
            return $"{name}";
        }
    }
}