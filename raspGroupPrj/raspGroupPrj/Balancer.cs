using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raspGroupPrj
{
    class Balancer
    {
        List<Task> tasks = new List<Task>();
        Processor[] processors;
        int procCount;
        public Balancer(Processor[] Processors,int allProcCount)
        {
            processors = Processors;
            procCount = allProcCount;
        }
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void AddTasks(List<Task> tasks)
        {
            this.tasks.AddRange(tasks);
        }
        public void Balance()
        {

        }
    }
}
