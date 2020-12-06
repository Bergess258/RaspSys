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
        float load { 
            get { return load; } 
            set {
                if (value < 0)
                    load = 0;
                else
                    if (value > 100)
                    load = 100;
                    else
                        load = value;
            }
        }
        List<Task> tasks = new List<Task>();
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
