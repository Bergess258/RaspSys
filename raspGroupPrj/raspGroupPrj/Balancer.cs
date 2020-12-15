using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raspGroupPrj
{
    class Balancer
    {
        public static int maxLoad = 80;
        public float complexity = 20;
        public Processor mainProcessor;

        public Balancer(Processor[] Processors)
        {
            mainProcessor = new Processor(Processors);
            //BalanceEchoTest();
        }
        public async void BalanceEcho(int addComp)
        {
            complexity += addComp;
            complexity = await mainProcessor.Balance(complexity);
            if (complexity < 0)
                complexity = 0;
        }
        public async void BalanceEcho()
        {
            complexity = await mainProcessor.Balance(complexity);
            if (complexity < 0)
                complexity = 0;
        }

        public void BalanceEchoTest()
        {
            complexity = mainProcessor.BalanceNoAsunc(complexity);
        }

    }
}
