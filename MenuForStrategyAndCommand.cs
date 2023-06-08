using System;
namespace age_of_war
{
    public class MenuForStrategyAndCommand
    {
        public int ShowMenuCom1()
        {
            Console.WriteLine("Выберите действие");
            Console.WriteLine("1. Отменить действие");
            Console.WriteLine("2. Продолжить игру");
            Console.WriteLine("3. Продолжить игру до конца");
            int choice = ChooseChoice1();
            //if (choice == 1)
            //    choice = ShowMenuCom2();
            return choice;  //0 - отмена, 2 продолжить, 3 - до конца, 4 - вернуть действие
        }
        public int ShowMenuCom2()
        {
            Console.WriteLine("Выберите действие");
            Console.WriteLine("1. Отменить действие");
            Console.WriteLine("2. Продолжить игру");
            Console.WriteLine("3. Продолжить игру до конца");
            Console.WriteLine("4. Вернуть действие");
            int choice = ChooseChoice3();
            return choice;
        }
        public int ShowMenuStrategy1()
        {
            Console.WriteLine("Сменить стратегию? 4 - да, 0 - нет");
            int choice = ChooseChoice2();
            if (choice == 4)
                choice = ShowMenuStrategy2(); //0 - продолжить, 1 - 1, 2 - 3, 3 - стенка
            return choice;
        }
        public int ShowMenuStrategy2()
        {
            Console.WriteLine("Выберите стратегию");
            Console.WriteLine("1. Армия в один ряд");
            Console.WriteLine("2. Армия в три ряда");
            Console.WriteLine("3. Стенка на стенку");
            int choice = ChooseChoice1();
            return choice;
        }
        public static int ChooseChoice1()
        {
            int choice = -1;
            while (choice != 1 && choice != 2 && choice != 3)
            {
                try
                {
                    var temp = Console.ReadLine();
                    choice = Convert.ToInt32(temp);
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Введите число!");
                }
            }
            return choice;
        }
        public static int ChooseChoice2()
        {
            int choice = -1;
            while (choice != 4 && choice != 0)
            {
                try
                {
                    var temp = Console.ReadLine();
                    choice = Convert.ToInt32(temp);
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Введите число!");
                }
            }
            return choice;
        }
        public static int ChooseChoice3()
        {
            int choice = -1;
            while (choice != 1 && choice != 2 && choice != 3 && choice != 4)
            {
                try
                {
                    var temp = Console.ReadLine();
                    choice = Convert.ToInt32(temp);
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Введите число!");
                }
            }
            return choice;
        }
    }
}