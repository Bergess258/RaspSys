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
        public float load;
        List<Processor> another= new List<Processor>();
        public delegate void procLoadChange(int id, float load);
        procLoadChange del;
        public Processor(ref int i,int count,Random rnd, procLoadChange func)
        {
            id = i++;
            load = rnd.Next(101);
            del = func;
            del(id,load);
            Thread.Sleep(25);
            if (i<count)
                if (rnd.Next(2) == 0)
                {
                    Thread.Sleep(25);
                    another.Add(new Processor(ref i, count, rnd, func));
                }
        }
        public Processor(Processor[] processors)
        {
            another = processors.ToList();
            load = Balancer.maxLoad;
        }
        public async Task<float> Balance(float complexity)
        {
            int maxLoad = Balancer.maxLoad;
            if (load> maxLoad)
            {
                complexity += load - maxLoad;
                load = maxLoad;
                del(id, load);
            }
            else
                if (load < maxLoad)
            {
                float diff = maxLoad - load;
                if (complexity >= 0)
                {
                    if (complexity - diff >= 0)
                    {
                        load = maxLoad;
                        complexity -= diff;
                    }
                    else
                    {
                        load += complexity;
                        complexity -= diff;
                    }
                    del(id, load);
                }
                else
                    complexity -= diff;
            }
            if (another.Count > 0)
            {
                float compForProc = complexity / another.Count;
                Task<float>[] tasks = new Task<float>[another.Count];
                Queue<int> q = new Queue<int>();
                for (int i = 0; i < another.Count; i++)
                {
                    q.Enqueue(i);
                    int help = q.Dequeue();
                    tasks[help] = Task<float>.Run(() => another[help].Balance(compForProc));
                }
                await Task.WhenAll(tasks);
                float[] res = new float[another.Count];
                List<int> negative = new List<int>();
                List<int> positive = new List<int>();
                for (int i = 0; i < another.Count; ++i)
                {
                    q.Enqueue(i);
                    int help = q.Dequeue();
                    res[help] = tasks[help].Result;
                    if (res[help] > 0)
                        positive.Add(help);
                    else
                        if(res[help] < 0)
                            negative.Add(help);
                }
                while (negative.Count > 0 && positive.Count > 0)
                {
                    for (int i = 0; i < positive.Count; ++i)
                    {
                        q.Enqueue(i);
                        int gg = q.Dequeue();
                        float compNeed = res[positive[gg]];
                        float toSend = 0;
                        for (int j = 0; j < negative.Count && toSend != compNeed; j++)
                        {
                            q.Enqueue(i);
                            int bb = q.Dequeue();
                            if (compNeed >= res[negative[bb]] * -1)
                            {
                                res[positive[gg]] += res[negative[bb]];
                                toSend -= res[negative[bb]];
                                tasks[negative[bb]] = Task<int>.Run(() => another[positive[gg]].Balance(res[negative[bb]]));
                            }
                            else
                            {
                                res[positive[gg]] = 0;
                                tasks[negative[bb]] = Task<int>.Run(() => another[positive[gg]].Balance(compNeed));
                            }
                        }
                    }
                    await Task.WhenAll(tasks);
                    negative.Clear();
                    positive.Clear();
                    for (int i = 0; i < another.Count; ++i)
                    {
                        q.Enqueue(i);
                        int help = q.Dequeue();
                        res[help] = tasks[help].Result;
                        if (res[help] > 0)
                            positive.Add(help);
                        else
                            if (res[help] < 0)
                            negative.Add(help);
                    }
                }
                complexity = 0;
                if (positive.Count > 0)
                    for (int i = 0; i < positive.Count; ++i)
                    {
                        q.Enqueue(i);
                        int capture = q.Dequeue();
                        complexity += res[positive[capture]];
                    }
                       
                else
                    for (int i = 0; i < negative.Count; ++i)
                    {
                        q.Enqueue(i);
                        int capture = q.Dequeue();
                        complexity += res[negative[capture]];
                    }
            }
            return complexity;
        }
        public float BalanceNoAsunc(float complexity)
        {
            int maxLoad = Balancer.maxLoad;
            if (load > maxLoad)
            {
                complexity += load - maxLoad;
                load = maxLoad;
                del(id, load);
            }
            else
                if (load < maxLoad)
                {
                float diff = maxLoad - load;
                    if (complexity >= 0)
                    {
                        if (complexity - diff >= 0)
                        {
                            load = maxLoad;
                            complexity -= diff;
                        }
                        else
                        {
                            load += complexity;
                            complexity -= diff;
                        }
                        del(id, load);
                    }
                    else
                        complexity -= diff;
                }
            if (another.Count > 0)
            {
                float compForProc = complexity / another.Count;
                float[] res = new float[another.Count];
                List<int> negative = new List<int>();
                List<int> positive = new List<int>();
                for (int i = 0; i < another.Count; ++i)
                {
                    res[i] = another[i].BalanceNoAsunc(compForProc);
                    if (res[i] > 0)
                        positive.Add(i);
                    else
                        if (res[i] < 0)
                        negative.Add(i);
                }
                while (negative.Count > 0 && positive.Count > 0)
                {
                    for (int i = 0; i < positive.Count; ++i)
                    {
                        float compNeed = res[positive[i]];
                        float toSend = 0;
                        for (int j = 0; j < negative.Count && toSend != compNeed; j++)
                            if (compNeed >= res[negative[j]] * -1)
                            {
                                compNeed += res[negative[j]];
                                toSend -= res[negative[j]];
                                res[negative[j]] = 0;
                            }
                            else
                            {
                                toSend = compNeed;
                                res[negative[j]] += compNeed;
                            }
                        res[positive[i]] = another[positive[i]].BalanceNoAsunc(toSend);
                    }
                    negative.Clear();
                    positive.Clear();
                    for (int i = 0; i < another.Count; ++i)
                    {
                        if (res[i] > 0)
                            positive.Add(i);
                        else
                            if (res[i] < 0)
                            negative.Add(i);
                    }
                }
                complexity = 0;
                if (positive.Count > 0)
                    for (int i = 0; i < positive.Count; ++i)
                        complexity += res[positive[i]];
                else
                    for (int i = 0; i < negative.Count; ++i)
                        complexity += res[negative[i]];
            }
            return complexity;
        }
        public IEnumerable<Processor> Levelorder()
        {
            var queue = new Queue<Processor>();
            for (int i = 0; i < another.Count; ++i)
            {
                queue.Enqueue(another[i]);
            }
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;
                if (node.another.Count != 0)
                    for (int i = 0; i < node.another.Count; ++i)
                        queue.Enqueue(node.another[i]);
            }
        }
    }
}
