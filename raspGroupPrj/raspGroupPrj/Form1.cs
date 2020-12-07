using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace raspGroupPrj
{
    public partial class Form1 : Form
    {
        int startProcessorsCount = 5;
        int startTasksCount = 12;
        int maxTaskComplexity = 2000;
        public Form1()
        {
            InitializeComponent();
            Balancer balancer = new Balancer(RandomProcessors(), startProcessorsCount);
        }

        private Processor[] RandomProcessors()
        {
            Random rnd = new Random();
            List<Processor> processors = new List<Processor>();
            int i = 0;
            do
            {
                Thread.Sleep(25);
                processors.Add(new Processor(ref i, startProcessorsCount, rnd));
            } while (i < startProcessorsCount);
            return processors.ToArray();
        }
    }
}
