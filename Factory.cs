using System;
namespace age_of_war
{
    interface IFactory
    {
        public Unit Create();
    }

    class LIFactory : IFactory
    {
        public Unit Create()
        {
            return new LightInfantry();
        }
    }
    class HIFactory : IFactory
    {
        public Unit Create()
        {
            return new HeavyInfantry();
        }
    }
    class KFactory : IFactory
    {
        public Unit Create()
        {
            return new Knight();
        }
    }
    class ArrowFactory : IFactory
    {
        public Unit Create()
        {
            return new Arrow();
        }
    }
    class HealerFactory : IFactory
    {
        public Unit Create()
        {
            return new Healer();
        }
    }
    class ClonerFactory : IFactory
    {
        public Unit Create()
        {
            return new Cloner();
        }
    }

    class GGFactory : IFactory
    {
        public Unit Create()
        {
            return new Adapter();

        }
    }
}