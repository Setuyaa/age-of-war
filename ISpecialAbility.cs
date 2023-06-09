using System;
namespace age_of_war
{
    public interface ISpecialAbility
    {
        public int range { get; }
        public void Action(ISpecialAbility special, Army army1, ArrayOfArmies army, int i, int j, int hplace) { }
    }
}

