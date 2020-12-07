using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raspGroupPrj
{
    class Balancer
    {
        Processor[] processors;
        int procCount;
        public Balancer(Processor[] Processors,int allProcCount)
        {
            processors = Processors;
            procCount = allProcCount;
        }
        public void Balance()
        {
            int overallComp = (int)tasks.Sum(x => x.complexity);

        }
    }
}
