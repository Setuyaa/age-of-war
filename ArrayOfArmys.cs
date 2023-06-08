using System;
namespace age_of_war
{
    public class ArrayOfArmies
    {
        public Army[] array;
        public string name;
        public int armyNumber;
        public int size;
        public ArrayOfArmies(string name, int size)
        {
            this.name = name;
            array = new Army[size];
            armyNumber = 1;
            this.size = size;
        }
        public int CountUnits() {
            int count = 0;
            for (int i = 0; i < armyNumber; i++)
            { 
                count += array[i].army.Count();
            }
            return count;
        }
    }
}

