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
        public Balancer(Processor[] Processors)
        {
            processors = Processors;
        }
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void AddTasks(List<Task> tasks)
        {
            this.tasks.AddRange(tasks);
        }
    }
}
