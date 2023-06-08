using System;
using System.Text;

namespace age_of_war
{
    public class Action
    {
        public IStrategy Strategy { get; set; }
        public readonly int ArmyPrice = 100;
        public void OnlyOneStep(Army army1, Army army2, int i, ArrayOfArmies ar1, ArrayOfArmies ar2)
        {
            CheckBuff(army1);
            CheckBuff(army2);
            if (army2.army.Count > 0 && army1.army.Count > 0)
                Step(i, army1, army2, 0, ArmyPrice);
            if (army2.army.Count > 0 && army1.army.Count > 0)
                Step(i, army2, army1, 0, ArmyPrice);
            if (army2.army.Count > 0 && army1.army.Count > 0)
                DoAction(army1, army2, i, ar2);
            if (army2.army.Count > 0 && army1.army.Count > 0)
                DoAction(army2, army1, i, ar1);
        }
        public void PrintAfterSteps(ArrayOfArmies army1, int f, ArrayOfArmies army2) {
            Console.WriteLine($"Состояние армий после {f} хода");
            PrintArmyState(army1);
            PrintArmyState(army2);
        }
        public void PrintArmyState(ArrayOfArmies army) {
            Console.WriteLine($"{army.name}:");
            for (int i = 0; i < army.armyNumber; i++)
            {
                for (int j = 0; j < army.size; j++)
                {
                    Proxy proxy;
                    if (j < army.array[i].army.Count)
                    {
                        proxy = army.array[i].army[j];
                        Console.Write($"{proxy.ToString()} ");
                    }
                    else
                        break;
                }
            }
            Console.WriteLine();
        }
        public void Step(int i, Army army1, Army army2, int j, int ArmyPrice)
        {
            army1.army[0].PrintResultAttack(i, army1, army2, j);
            army2.army[0].GetHit(army1.army[0].Attack, ArmyPrice, i, army2);
            if (army2.army[0].IsStillAlive() && (army2.army[0].Name == "HeavyInfantry со шлемом" || army2.army[0].Name == "HeavyInfantry с щитом" || army2.army[0].Name == "HeavyInfantry на лошади"))
            {
                PutOffBuff(army2);
            }
            CheckDeath(i, army2.army[0], army2, army1, j); //rename
        }

        public void PutOffBuff(Army army)
        {
            HeavyInfantry heavyInfantry = new HeavyInfantry();
            army.army[0].Attack = heavyInfantry.Attack;
            army.army[0].Defence = heavyInfantry.Defence;
            army.army[0].Name = heavyInfantry.Name;
            Console.WriteLine($"В результате удара баф с {army.army[0].Name} был снят");
        }

        public void CheckDeath(int i, Proxy person2, Army army2, Army army1, int j)
        {
            if (!person2.IsStillAlive()) // если не живой
            { 
                army2.army.RemoveAt(0);
            }
        }

        public void CheckBuff(Army army)
        {
            int armyCount = army.army.Count;
            int indexHI; int indexLI = -1;
            for (int i = 0; i < armyCount; i++) 
            {
                if (army.army[i].Name == "HeavyInfantry")
                {
                    indexHI = i;
                    CheckBuffStepTwo(army, indexHI, indexLI, i - 1);
                    CheckBuffStepTwo(army, indexHI, indexLI, i + 1);
                }
            }
        }
        public void CheckBuffStepTwo(Army army, int indexHI, int indexLI, int k)
        {
            if (!army.army[indexHI].BuffOn)
            {

                try
                {
                    if (army.army[k].Name == "LightInfantry")
                    {
                        indexLI = k;
                    }
                }
                catch { }
                finally
                {
                    if (indexLI > -1)
                    {
                        if (Probability()) // надеваем баффы
                        {
                            var rand = new Random();
                            int buffNumber = rand.Next(1, 3);
                            PutOnBuff(army, army.army[indexHI], indexHI, army.army[indexHI].Hp, buffNumber);
                        }
                    }
                }
                indexLI = -1;
            }
        }

        public void PutOnBuff(Army army, Proxy HI, int indexHI, int hp, int buff)
        {
            var hf = new HIFactory();
            HeavyInfantry heavyInfantry = (HeavyInfantry)hf.Create();
            int tempHP = HI.Hp;
            Console.Write($"{army.ToString()}: на {army.army[indexHI]} надели бафф - ");
            army.army.RemoveAt(indexHI);
            switch (buff)
            {
                case 1:
                    {
                        heavyInfantry = new HeavyInfantryHelmet(heavyInfantry);
                        Console.Write("шлем\n");
                        break;
                    }
                case 2:
                    {
                        heavyInfantry = new HeavyInfantryShield(heavyInfantry);
                        Console.Write("щит\n");
                        break;
                    }
                case 3:
                    {
                        heavyInfantry = new HeavyInfantryHourse(heavyInfantry);
                        Console.Write("конь\n");
                        break;
                    }
            }
            var proxy = new Proxy(heavyInfantry);
            proxy.BuffOn = true; proxy.Hp = tempHP;
            army.army.Insert(indexHI, proxy);
        }
        public bool Probability()
        {
            var rand = new Random();
            int q = rand.Next(0, 100);
            return q < 40 ? true : false;
        }
        static public void DoAction(Army army1, Army army2, int j, ArrayOfArmies  ar2)
        {
            int max = Math.Max(army1.army.Count, army2.army.Count);
            for (int i = 1; i < max; i++)
            {
                if (army1.army.Count > 0 && i < army1.army.Count)
                    if (army1.army[i].CheckSA())
                    {
                        var special = army1.army[i];  // можно запустить цикл для лучника
                        if (i <= special.Range() && i < army1.army.Count())
                        {
                            special.Action(special, army1, ar2, j, i);
                        }
                    }
            }
        }
    }
}

