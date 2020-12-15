using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace raspGroupPrj
{
    public partial class Form1 : Form
    {
        int startProcessorsCount = 5;
        Balancer balancer;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < startProcessorsCount; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
            }
            dataGridView1.Rows.Add();
            Task.Run(() => {
                balancer = new Balancer(RandomProcessors());
            });
        }

        private Processor[] RandomProcessors()
        {
            Random rnd = new Random();
            List<Processor> processors = new List<Processor>();
            int i = 0;
            do
            {
                Thread.Sleep(25);
                processors.Add(new Processor(ref i, startProcessorsCount, rnd, ChangeLoad));
            } while (i < startProcessorsCount);
            return processors.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balancer.BalanceEcho((int)numericUpDown1.Value);
        }
        public void ChangeLoad(int i,float load)
        {
            dataGridView1.BeginInvoke(new MethodInvoker(()=> dataGridView1.Rows[0].Cells[i].Value = load));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            balancer.BalanceEcho();
        }
    }
}
