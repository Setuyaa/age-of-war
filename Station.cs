using System;
namespace age_of_war
{
    public class Station
    {

        public ArrayOfArmies ar1;
        public ArrayOfArmies ar2;
        int j;
        public int strategy;
        public Station(ArrayOfArmies ar1, ArrayOfArmies ar2, int j, int str)
        {
            strategy = str;
            this.j = j;
            this.ar1 = new ArrayOfArmies(ar1.name, ar1.size);
            this.ar2 = new ArrayOfArmies(ar2.name, ar2.size);
            for (int i = 0; i < ar1.size; i++)
            {
                this.ar1.array[i] = new Army(ar1.name, 2);
                if (i < ar1.armyNumber)
                    this.ar1.array[i] = ar1.array[i].DeepCopy() as Army;
            }
            for (int i = 0; i < ar2.size; i++)
            {
                this.ar2.array[i] = new Army(ar2.name, 2);
                if (i < ar2.armyNumber)
                    this.ar2.array[i] = ar2.array[i].DeepCopy() as Army;
            }
        }
    }
}