using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace raspGroupPrj
{
    class Processor
    {
        int id;
        private float Load;
        float load { 
            get { return Load; } 
            set {
                if (value < 0)
                    Load = 0;
                else
                    if (value > 100)
                    Load = 100;
                    else
                    Load = value;
            }
        }
        int anotherCount;
        List<Processor> another= new List<Processor>();
        public Processor(ref int i,int count,Random rnd)
        {
            id = i++;
            if(i<count)
                if (rnd.Next(2) == 0)
                {
                    Thread.Sleep(25);
                    another.Add(new Processor(ref i, count, rnd));
                }
            anotherCount = i - id-1;
        }
    }
}
