using System;
namespace age_of_war
{
    public class StacksOfStations
    {
        public Stack<Station> stForUndo;
        public Stack<Station> stForRedo;
        public StacksOfStations()
        {
            stForUndo = new Stack<Station>();
            stForRedo = new Stack<Station>();
        }
        public void ClearRedo()
        {
            stForRedo.Clear();
        }
    }
}