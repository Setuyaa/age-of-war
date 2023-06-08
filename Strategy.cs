using System;
using static System.Collections.Specialized.BitVector32;

namespace age_of_war
{
    public interface IStrategy
    {
        void Algorithm(ArrayOfArmies ar1, ArrayOfArmies ar2, Action action, int f);
    }
    public class MainStrategy : IStrategy
    {
        public void Algorithm(ArrayOfArmies ar1, ArrayOfArmies ar2, Action action, int f)
        {
            var tempStrategy = new TempStrategy();
            //создаем промежуточные армии 
            ArrayOfArmies temp1 = tempStrategy.CreateArrayOfArmies(1, ar1);
            ArrayOfArmies temp2 = tempStrategy.CreateArrayOfArmies(1, ar2);
            //записываем в промежуточную армию армию правильного построения
            tempStrategy.MakeNewTemp(ar1, temp1, 1);
            tempStrategy.MakeNewTemp(ar2, temp2, 1);
            // записываем новое построение
            tempStrategy.MakeTempToReal(ar1, temp1, 1);
            tempStrategy.MakeTempToReal(ar2, temp2, 1);
            // Вызываем бой
            tempStrategy.Play(ar1, ar2, action, 0, f, 1);
            // Пишем состояние армий
            action.PrintAfterSteps(ar1, f, ar2);
        }
    }
    public class ThreeInRowStrategy : IStrategy
    {
        public void Algorithm(ArrayOfArmies ar1, ArrayOfArmies ar2, Action action, int f)
        {
            var tempStrategy = new TempStrategy();
            // Создаем промежуточные армии 
            ArrayOfArmies temp1 = tempStrategy.CreateArrayOfArmies(3, ar1);
            ArrayOfArmies temp2 = tempStrategy.CreateArrayOfArmies(3, ar2);
            // Заполняем промежутчные армии
            tempStrategy.MakeNewTemp(ar1, temp1, 3);
            tempStrategy.MakeNewTemp(ar2, temp2, 3);
            // Заполняем номинальные
            tempStrategy.MakeTempToReal(ar1, temp1, 3);
            tempStrategy.MakeTempToReal(ar2, temp2, 3);
            // Вызываем бой
            tempStrategy.Play(ar1, ar2, action, 0, f, 3);
            // Пишем состояние армий
            action.PrintAfterSteps(ar1, f, ar2);
        }
    }
    public class SquadStrategy : IStrategy
    {
        public void Algorithm(ArrayOfArmies ar1, ArrayOfArmies ar2, Action action, int f)
        {
            var tempStrategy = new TempStrategy();
            // Создаем промежуточные армии 
            ArrayOfArmies temp1 = tempStrategy.CreateArrayOfArmies(ar1.size, ar1);
            ArrayOfArmies temp2 = tempStrategy.CreateArrayOfArmies(ar2.size, ar2);
            // Заполняем промежутчные армии
            tempStrategy.MakeNewTemp(ar1, temp1, ar1.size);
            tempStrategy.MakeNewTemp(ar2, temp2, ar2.size);
            // Заполняем номинальные
            tempStrategy.MakeTempToReal(ar1, temp1, ar1.size);
            tempStrategy.MakeTempToReal(ar2, temp2, ar2.size);
            // Вызываем бой
            tempStrategy.Play(ar1, ar2, action, 0, f, Math.Max(ar1.size, ar2.size));
            // Пишем состояние армий
            action.PrintAfterSteps(ar1, f, ar2);
        }
    }
    public class TempStrategy
    {
        public ArrayOfArmies CreateArrayOfArmies(int size, ArrayOfArmies ar)
        {
            ArrayOfArmies temp = new ArrayOfArmies(ar.name, size);
            for (int i = 0; i < size; i++)
            {
                temp.array[i] = new Army(temp.name, 2);
                temp.array[i].army = new List<Proxy>();
            }
            return temp;
        }
        public void MakeNewTemp(ArrayOfArmies ar, ArrayOfArmies temp, int size)
        {
            int flag = ar.size;
            while (flag >= 0)
            {
                for (int i = 0; i < ar.armyNumber; i++)
                {
                    int j = 0;
                    while (j != size)
                    {
                        int r;
                        try { r = ar.array[i].army.Count(); }
                        catch { r = 0; }
                        if (r == 0)
                        {
                            flag--;
                            if (flag <= 0)
                                break;
                        }
                        else
                        {
                            flag = ar.armyNumber;
                            temp.array[j].army.Add(ar.array[i].army[0]);
                            ar.array[i].army.RemoveAt(0);
                            j++;
                        }
                    }
                }
            }
        }
        public void MakeTempToReal(ArrayOfArmies ar, ArrayOfArmies temp, int size)
        {
            ar.array[0] = temp.array[0];
            ar.armyNumber = 1;
            Recursion(1, ar, temp, size);
        }
        public void Recursion(int recStep, ArrayOfArmies ar, ArrayOfArmies temp, int size)
        {
            if (recStep < size)
            {
                if (ar.size > recStep)
                {
                    ar.array[recStep] = temp.array[recStep];
                    if (recStep == size - 1)
                        ar.armyNumber = size;
                    else
                        Recursion(recStep + 1, ar, temp, size);
                }
                else
                    ar.armyNumber = recStep;
            }
        }
        public void Play(ArrayOfArmies ar1, ArrayOfArmies ar2, Action action, int i, int f, int size)
        {
            if (i < Math.Min(ar1.size, ar2.size))
            {
                if (ar1.array[i].army.Count() > 0 && ar2.array[i].army.Count() > 0)
                    action.OnlyOneStep(ar1.array[i], ar2.array[i], f, ar1, ar2);
                if (i + 1 < size)
                    Play(ar1, ar2, action, i + 1, f, size);
            }
        }
    }
}
