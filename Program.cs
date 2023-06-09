using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace age_of_war
{
    class Game
    {
        int i = 1;
        int str;
        public readonly int ArmyPrice = 100;
        MenuForStrategyAndCommand menuForStrategyAndCommand;
        Invoker invoker;
        StacksOfStations stations;
        public static bool redo = false;
        public static bool undo = false;
        int c;
        Station station;
        public Game()
        {
            menuForStrategyAndCommand = new MenuForStrategyAndCommand();
            invoker = new Invoker();
            stations = new StacksOfStations();
        }
        public void Start()
        {

            bool cont = false;
            var army1 = new Army("Мы победим", i);
            var army2 = new Army("Computer", i);
            ArrayOfArmies ar1 = new ArrayOfArmies(army1.name, army1.army.Count());
            ArrayOfArmies ar2 = new ArrayOfArmies(army2.name, army2.army.Count());
            ar1.array[0] = army1;
            ar2.array[0] = army2;
            station = new Station(ar1, ar2, i, 0);
            stations.stForUndo.Push(station);
            int str = GeneralStep(ar1, ar2);
            c = 0;
            while (ar1.CountUnits() > 0 && ar2.CountUnits() > 0)
            {
                if (!cont && i != 1)
                    while (c != 2 && c != 3)
                        cont = CheckCommand(ar1, ar2, str);
                str = GeneralStep(ar1, ar2);
                c = 0;
                undo = false;
            }
            Congrats(ar1, ar2);
        }
        public bool CheckCommand(ArrayOfArmies ar1, ArrayOfArmies ar2, int str)
        {
            bool cont = false;
            if (i != 1)
            {
                c = 2;
                if (stations.stForRedo.Count == 0 ) c = menuForStrategyAndCommand.ShowMenuCom1();  //1 - отмена, 2 продолжить, 3 - до конца, 4 - вернуть действие
                else c = menuForStrategyAndCommand.ShowMenuCom2();
            }
            else TryCancelFirstStep();
            if (c == 3) cont = true;
            else
            {
                station = new Station(ar1, ar2, i, str);
                stations.stForUndo.Push(station);
                if (c == 2)
                    stations.ClearRedo();
                if (c != 4)
                {
                    if (redo)
                    {
                        stations.ClearRedo();
                        redo = false;
                    }
                    if (c == 1)
                        undo = true;
                }
                if (c != 2)
                {
                    i = invoker.SetUndoRedo(stations, c, ar1, ar2, i, str);
                }
                if (c == 4)
                    redo = true;
            }
            return cont;
        }
        public void TryCancelFirstStep()
        {
            Console.WriteLine("Отмена дальше невозможна, запуск 1 хода");
            c = menuForStrategyAndCommand.ShowMenuCom2();
            if (c == 1)
                TryCancelFirstStep();
        }
        public int GeneralStep(ArrayOfArmies ar1, ArrayOfArmies ar2)
        {
            var action = new Action();
            int str2 = 0;
            if (!undo)
            {
                if (i == 1)
                    str2 = menuForStrategyAndCommand.ShowMenuStrategy2();
                else
                    str2 = menuForStrategyAndCommand.ShowMenuStrategy1(); //0 - продолжить, 1 - 1, 2 - 3, 3 - стенка
            }
            else {
                str2 = menuForStrategyAndCommand.ShowMenuStrategy1(); //0 - продолжить, 1 - 1, 2 - 3, 3 - стенка
            }
            if (str2 != 0)
                str = str2;
            if (str == 1)
                action.Strategy = new MainStrategy();
            if (str == 2)
                action.Strategy = new ThreeInRowStrategy();
            if (str == 3)
                action.Strategy = new SquadStrategy();
            action.Strategy.Algorithm(ar1, ar2, action, i);
            i++;
            return str;
        }
        static public void Congrats(ArrayOfArmies ar1, ArrayOfArmies ar2)
        {
            if (ar1.CountUnits() == 0 && ar2.CountUnits() != 0)
                Console.WriteLine($"{ar2.name} won");
            if (ar1.CountUnits() != 0 && ar2.CountUnits() == 0)
                Console.WriteLine($"{ar1.name} won");
            if (ar1.CountUnits() == 0 && ar2.CountUnits() == 0)
                Console.WriteLine("nobody won");
        }
    }
    class Program
    {
        public static int Main()
        {
            var game = new Game();
            game.Start();
            return 0;
        }
    }
}