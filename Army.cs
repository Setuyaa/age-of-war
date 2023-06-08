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
        //public int count;
        public Army(string name, int i)
        {
            this.name = name;
            if (i == 1)
            {
                if (name == "Computer")
                {
                    var hf = new HIFactory();
                    Proxy pr = new Proxy(hf.Create());
                    var kf = new KFactory();
                    Proxy pr0 = new Proxy(kf.Create());
                    var lf = new LIFactory();
                    Proxy pr1 = new Proxy(lf.Create());
                    Proxy pr2 = new Proxy(lf.Create());
                    Proxy pr3 = new Proxy(lf.Create());
                    Proxy pr4 = new Proxy(lf.Create());
                    army = new List<Proxy>() { pr, pr0, pr4, pr2 };
                    //count = 4;
                }
                else
                    army = BuyArmy.BuyArmyMain();
            }
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