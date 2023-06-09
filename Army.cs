using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace age_of_war
{
    [Serializable]
    public class Army
    {
        public List<Proxy> army;
        public string name;
        public Army(string name, int i)
        {
            this.name = name;
            if (i == 1)
            {
                if (name == "Computer")
                {
                    //rand
                    //Proxy pr = new Proxy(hf.Create());
                    //Proxy pr0 = new Proxy(kf.Create());
                    //Proxy pr1 = new Proxy(lf.Create());
                    //Proxy pr2 = new Proxy(lf.Create());
                    //Proxy pr3 = new Proxy(lf.Create());
                    //Proxy pr4 = new Proxy(lf.Create());
                    army = RandomArmy();
                    //army = new List<Proxy>() { pr, pr0, pr4, pr2 };
                }
                else
                    army = BuyArmy.BuyArmyMain();
            }
        }
        public List<Proxy> RandomArmy() {

            var lf = new LIFactory();
            var hf = new HIFactory();
            var kf = new KFactory();
            var af = new ArrowFactory();
            var healerf = new HealerFactory();
            var clonerf = new ClonerFactory();
            var GGf = new GGFactory();
            int r = rand();
            switch (r) {
                case 1:
                    {
                        Proxy pr = new Proxy(hf.Create());
                        Proxy pr0 = new Proxy(kf.Create());
                        Proxy pr2 = new Proxy(lf.Create());
                        Proxy pr4 = new Proxy(lf.Create());
                        army = new List<Proxy>() { pr, pr0, pr4, pr2 };
                        break;
                    }
                case 2:
                    {
                        Proxy pr = new Proxy(lf.Create());
                        Proxy pr0 = new Proxy(lf.Create());
                        Proxy pr1 = new Proxy(lf.Create());
                        Proxy pr2 = new Proxy(clonerf.Create());
                        Proxy pr3 = new Proxy(healerf.Create());
                        army = new List<Proxy>() { pr, pr0, pr1, pr2, pr3 };
                        break;
                    }
                case 3:{
                        Proxy pr = new Proxy(GGf.Create());
                        Proxy pr0 = new Proxy(lf.Create());
                        Proxy pr1 = new Proxy(af.Create());
                        army = new List<Proxy>() { pr, pr0, pr1};
                        break;
                    }

                case 4:
                    {
                        Proxy pr = new Proxy(GGf.Create());
                        Proxy pr1 = new Proxy(af.Create());                       
                        Proxy pr2 = new Proxy(healerf.Create());
                        army = new List<Proxy>() { pr, pr1, pr2};
                        break;
                    }
            }
            return army;
        }
        public int rand() {
            var rand = new Random();
            int q = rand.Next(1, 5);
            return q;
        }
        public override string ToString()
        {
            return $"{name}";
        }
        public object DeepCopy()
        {
            object army = null;
            using (MemoryStream tempStream = new MemoryStream())
            {
                BinaryFormatter binFormatter = new BinaryFormatter(null,
                    new StreamingContext(StreamingContextStates.Clone));

                binFormatter.Serialize(tempStream, this);
                tempStream.Seek(0, SeekOrigin.Begin);

                army = binFormatter.Deserialize(tempStream);
            }
            return army;
        }
    }
}