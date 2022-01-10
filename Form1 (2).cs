using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            time.Hide();
            arrival.Hide();
            priorty_list.Hide();
            tabcnt.AutoSize = true;
            tabcnt.TabPages[0].AutoSize = true;
            quantum.Text = "Q";
            quantum.Enabled = false;
        }
        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }



        Dictionary<int, TextBox> T = new Dictionary<int, TextBox>();
        Dictionary<int, TextBox> A = new Dictionary<int, TextBox>();
        Dictionary<int, TextBox> P = new Dictionary<int, TextBox>();

        List<process> processes = new List<process>();


        private void go_Click(object sender, EventArgs e)
        {

            string arrival_text;
            string burst_text;
            string priorty_text;
            tabPage2.Controls.Clear();
            processes.Clear();
            int number_of_processes = int.Parse(number.Text);
            for (int i = 0; i < number_of_processes; i++)
            {
                arrival_text = A.Values.ElementAt(i).Text;
                burst_text = T.Values.ElementAt(i).Text;
                priorty_text = P.Values.ElementAt(i).Text;
                process p = new process(int.Parse(arrival_text), int.Parse(burst_text), i + 1, int.Parse(priorty_text));
                processes.Add(p);
            }
            tabcnt.SelectTab("tabPage2");
            if (fcfs.Checked == true)
            {
                fcfs_paint();
            }
            if (shortfirst.Checked == true)
            {
                sjf_paint();
            }
            if (priority.Checked == true)
            {
                priorty_paint();
            }
            if (roundrobin.Checked == true)
            {
                roundrobbin_paint();
            }
    
            if (roundrobin.Checked == true)
            {

                //Round robbin

            }
         
        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void processes_Click(object sender, EventArgs e)
        {

        }

        private void config_Click(object sender, EventArgs e)
        {

        }

        private void arrival_Enter(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {


        }

       int scale = 50;
        private void fcfs_paint()
        {

            int number_processes = int.Parse(number.Text);
            Graphics gObject = tabPage2.CreateGraphics();
           Random rnd = new Random();
            tabPage2.Controls.Clear();
            int i = 0;
            tabPage2.AutoScroll = true;
            List<int> departure = new List<int>();



            //sorting according to arrival
            for (int k = 0; k < number_processes; k++)
            {
                for (int l = 0; l < number_processes; l++)
                {
                    if (processes[k].get_arrival() < processes[l].get_arrival())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }

                }

            }
            int time = processes[0].get_arrival();

            //printing
            foreach (process p in processes)
            {

                int arrival = (p.get_arrival());
                int burst = (p.get_burst());
                if (time > arrival)
                    arrival = time;
                if (time < arrival)
                    time = arrival;

                Label draw = new Label();
                draw.Location = new Point(arrival * scale, 150);
                draw.Size = new Size(burst * scale, 20);
                draw.Text = "P" + (p.get_index()).ToString();
                draw.TextAlign = ContentAlignment.MiddleCenter;
                draw.BackColor = (Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                tabPage2.Controls.Add(draw);
                time += burst;

                departure.Add(time);

                Label b = new Label();
                b.Text = (arrival).ToString();
                b.Location = new Point(arrival * scale, 180);

                b.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(b);


                Label c = new Label();
                c.Text = (time).ToString();
                c.Location = new Point(time * scale, 180);
                c.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(c);

                i++;

            }
            float tat = 0;
            float wt = 0;
            for (int iq = 0; iq < number_processes; iq++)
            {
                tat += (departure[iq] - processes[iq].get_arrival());
                wt += departure[iq] - processes[iq].get_arrival() - processes[iq].get_burst();
            }
            tat /= number_processes;
            wt /= number_processes;

            Label d = new Label();
            d.Text = "TAT= " + (tat).ToString() + "   WT= " + (wt).ToString();
            d.Location = new Point(150, 220);
            d.Size = new System.Drawing.Size(200, 50);
            tabPage2.Controls.Add(d);

            Button go_back = new Button();
            go_back.Location = new Point(200, 270);
            go_back.Text = "Back";
            go_back.Click += new EventHandler(go_back_Click);
            tabPage2.Controls.Add(go_back);



        }


        private void sjf_paint()
        {



            int number_processes = int.Parse(number.Text);
            Graphics gObject = tabPage2.CreateGraphics();
            Random rnd = new Random();
            tabPage2.Controls.Clear();
            int i = 0;
            List<int> departure = new List<int>();


            //sorting according to arrival
            for (int k = 0; k < number_processes; k++)
            {
                for (int l = 0; l < number_processes; l++)
                {
                    if (processes[k].get_arrival() < processes[l].get_arrival())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }
                    else if (processes[k].get_arrival() == processes[l].get_arrival() && processes[k].get_burst() < processes[l].get_burst())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }
                }

            }

            int time = processes[0].get_arrival();

            //printing
            for (i = 0; i < number_processes; i++)
            {
                process p = processes[i];
                int arrival = (p.get_arrival());
                int burst = (p.get_burst());
                if (time > arrival)
                    arrival = time;
                if (time < arrival) 
                    time = arrival;



                Label draw = new Label();
                draw.Location = new Point(arrival * scale, 150);
                draw.Size = new Size(burst * scale, 20);
                draw.Text = "P" + (p.get_index()).ToString();
                draw.TextAlign = ContentAlignment.MiddleCenter;
                draw.BackColor = (Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                tabPage2.Controls.Add(draw);
                time += burst;
                departure.Add(time);




                for (int j = i + 1; j < number_processes - 1; j++)
                {
                    if (processes[j].get_arrival() <= time)
                    {
                        for (int k = j + 1; k < number_processes; k++)
                        {
                            if (processes[k].get_arrival() <= time && processes[j].get_burst() > processes[k].get_burst())
                            {
                                process t = processes[j];
                                processes[j] = processes[k];
                                processes[k] = t;
                            }
                        }

                    }


                }





                Label b = new Label();
                b.Text = (arrival).ToString();
                b.Location = new Point(arrival * scale, 180);

                b.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(b);

                Label c = new Label();
                c.Text = (time).ToString();
                c.Location = new Point(time * scale, 180);
                c.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(c);



            }

            float tat = 0;
            float wt = 0;
            for (int iq = 0; iq < number_processes; iq++)
            {
                tat += (departure[iq] - processes[iq].get_arrival());
                wt += departure[iq] - processes[iq].get_arrival() - processes[iq].get_burst();
            }
            tat /= number_processes;
            wt /= number_processes;

            Label d = new Label();
            d.Text = "TAT= " + (tat).ToString() + "   WT= " + (wt).ToString();
            d.Location = new Point(150, 220);
            d.Size = new System.Drawing.Size(200, 50);
            tabPage2.Controls.Add(d);

            Button go_back = new Button();
            go_back.Location = new Point(200, 270);
            go_back.Text = "Back";
            go_back.Click += new EventHandler(go_back_Click);
            tabPage2.Controls.Add(go_back);




        }

        private void priorty_paint()
        {



            int number_processes = int.Parse(number.Text);
            Graphics gObject = tabPage2.CreateGraphics();
            Random rnd = new Random();
            tabPage2.Controls.Clear();
            int i = 0;
            List<int> departure = new List<int>();


            //sorting according to arrival
            for (int k = 0; k < number_processes; k++)
            {
                for (int l = 0; l < number_processes; l++)
                {
                    if (processes[k].get_arrival() < processes[l].get_arrival())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }
                    else if (processes[k].get_arrival() == processes[l].get_arrival() && processes[k].get_priorty() < processes[l].get_priorty())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }
                }

            }

            int time = processes[0].get_arrival();

            //printing
            for (i = 0; i < number_processes; i++)
            {
                process p = processes[i];
                int arrival = (p.get_arrival());
                int burst = (p.get_burst());
                if (time > arrival) 
                    arrival = time;
                if (time < arrival) 
                    time = arrival;



                Label draw = new Label();
                draw.Location = new Point(arrival * scale, 150);
                draw.Size = new Size(burst * scale, 20);
                draw.Text = "P" + (p.get_index()).ToString();
                draw.TextAlign = ContentAlignment.MiddleCenter;
                draw.BackColor = (Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                tabPage2.Controls.Add(draw);
                time += burst;
                departure.Add(time);

                for (int j = i + 1; j < number_processes - 1; j++)
                {
                    if (processes[j].get_arrival() <= time)
                    {
                        for (int k = j + 1; k < number_processes; k++)
                        {
                            if (processes[k].get_arrival() <= time && processes[j].get_priorty() > processes[k].get_priorty())
                            {
                                process t = processes[j];
                                processes[j] = processes[k];
                                processes[k] = t;
                            }
                        }

                    }


                }

                Label b = new Label();
                b.Text = (arrival).ToString();
                b.Location = new Point(arrival * scale, 180);
                b.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(b);

                Label c = new Label();
                c.Text = (time).ToString();
                c.Location = new Point(time * scale, 180);
                c.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(c);

            }


            float tat = 0;
            float wt = 0;
            for (int iq = 0; iq < number_processes; iq++)
            {
                tat += (departure[iq] - processes[iq].get_arrival());
                wt += departure[iq] - processes[iq].get_arrival() - processes[iq].get_burst();
            }
            tat /= number_processes;
            wt /= number_processes;

            Label d = new Label();
            d.Text = "TAT= " + (tat).ToString() + "   WT= " + (wt).ToString();
            d.Location = new Point(150, 220);
            d.Size = new System.Drawing.Size(200, 50);
            tabPage2.Controls.Add(d);

            Button go_back = new Button();
            go_back.Location = new Point(200, 270);
            go_back.Text = "Back";
            go_back.Click += new EventHandler(go_back_Click);
            tabPage2.Controls.Add(go_back);



        }

        private void roundrobbin_paint()
        {

            int number_processes = int.Parse(number.Text);
            Graphics gObject = tabPage2.CreateGraphics();
            Random rnd = new Random();
            tabPage2.Controls.Clear();
            List<int> bursts = new List<int>();
            List<int> arrivals = new List<int>();

            //BDAYT EL ALGORITHM

            for (int j = 0; j < number_processes; j++)
            {
                arrivals.Add(processes[j].get_arrival());
            }
            for (int k = 0; k < number_processes; k++)
            {
                bursts.Add(processes[k].get_burst());
            }
            //sorting according to arrival
            for (int k = 0; k < number_processes; k++)
            {
                for (int l = 0; l < number_processes; l++)
                {
                    if (processes[k].get_arrival() < processes[l].get_arrival())
                    {
                        process t = processes[k];
                        processes[k] = processes[l];
                        processes[l] = t;
                    }
                }

            }

            // algorithm of RR
            int[] remaining = new int[number_processes];
            int[] dyn_arrival = new int[number_processes];
            List<process> seq = new List<process>();
            List<int> seq_burst = new List<int>();
            List<int> seq_start = new List<int>();
            List<int> seq_end = new List<int>();
            for (int i = 0; i < number_processes; i++)
            {
                remaining[i] = processes[i].get_burst();
                dyn_arrival[i] = processes[i].get_arrival();
            }
            int time = processes[0].get_arrival();
            int Q = int.Parse(quantum.Text);
            while (true)
            {
                bool flag = true;
                for (int i = 0; i < number_processes; i++)
                {
                    if (dyn_arrival[i] <= time)
                    {
                        if (dyn_arrival[i] <= Q)
                        {
                            if (remaining[i] > 0)
                            {
                                flag = false;
                                if (remaining[i] > Q)
                                {
                                    time = time + Q;
                                    remaining[i] = remaining[i] - Q;
                                    dyn_arrival[i] = dyn_arrival[i] + Q;
                                    seq.Add(processes[i]);
                                    seq_burst.Add(Q);
                                    seq_start.Add(time - Q);
                                }
                                else
                                {
                                    time = time + remaining[i];
                                    seq.Add(processes[i]);
                                    seq_burst.Add(remaining[i]);
                                    seq_start.Add(time - remaining[i]);
                                    processes[i].set_finish(time);
                                    remaining[i] = 0;
                                }
                            }
                        }
                        else if (dyn_arrival[i] > Q)
                        {
                            for (int j = 0; j < number_processes; j++)
                            {

                                if (dyn_arrival[j] < dyn_arrival[i])
                                {
                                    if (remaining[j] > 0)
                                    {
                                        flag = false;
                                        if (remaining[j] > Q)
                                        {
                                            time = time + Q;
                                            remaining[j] = remaining[j] - Q;
                                            dyn_arrival[j] = dyn_arrival[j] + Q;
                                            seq.Add(processes[j]);
                                            seq_burst.Add(Q);
                                            seq_start.Add(time - Q);
                                        }
                                        else
                                        {
                                            time = time + remaining[j];
                                            seq.Add(processes[j]);
                                            seq_burst.Add(remaining[j]);
                                            seq_start.Add(time - remaining[j]);
                                            processes[i].set_finish(time);
                                            remaining[j] = 0;
                                        }
                                    }
                                }
                            }
                            if (remaining[i] > 0)
                            {
                                flag = false;

                                // Check for greaters 
                                if (remaining[i] > Q)
                                {
                                    time = time + Q;
                                    remaining[i] = remaining[i] - Q;
                                    dyn_arrival[i] = dyn_arrival[i] + Q;
                                    seq.Add(processes[i]);
                                    seq_burst.Add(Q);
                                    seq_start.Add(time - Q);
                                }
                                else
                                {
                                    time = time + remaining[i];
                                    seq.Add(processes[i]);
                                    seq_burst.Add(remaining[i]);
                                    seq_start.Add(time - remaining[i]);
                                    processes[i].set_finish(time);
                                    remaining[i] = 0;
                                }
                            }
                        }
                    }

                   
                }
                // for exit the while loop 
                if (flag)
                {
                    break;
                }

            }
            List<Color> colours = new List<Color>();
            for (int i = 0; i < number_processes; i++)
            {
                colours.Add(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            }

            for (int i = 0; i < seq_burst.Count; i++)
            {
                seq[i].set_burst(seq_burst[i]);
                seq[i].set_arrival(seq_start[i]);
                seq[i].set_color(colours[(seq[i].get_index() - 1)]);
            }

            //printing
            for (int i = 0; i < seq_burst.Count; i++)
            {
                process p = seq[i];
                int arrival = seq_start[i];
                int burst = seq_burst[i];


                Color redColor = Color.FromArgb(255, 0, 0);
                SolidBrush brush = new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));


                Label draw = new Label();
                draw.Location = new Point(arrival * scale, 150);
                draw.Size = new Size(burst * scale, 20);
                draw.Text = "P" + (p.get_index()).ToString();
                draw.TextAlign = ContentAlignment.MiddleCenter;
                draw.BackColor = seq[i].get_color();
                tabPage2.Controls.Add(draw);

                Label b = new Label();
                b.Text = (arrival).ToString();
                b.Location = new Point(arrival * scale, 180);
                b.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(b);


                Label c = new Label();
                c.Text = (time).ToString();
                c.Location = new Point(time * scale, 180);
                c.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(c);


            }
            float tat = 0;
            float wt = 0;
            for (int iq = 0; iq < number_processes; iq++)
            {
                tat += (processes[iq].get_finish() - arrivals[iq]);
                wt += processes[iq].get_finish() - arrivals[iq] - bursts[iq];
            }
            tat /= number_processes;
            wt /= number_processes;



            Label d = new Label();
            d.Text = "TAT= " + (tat).ToString() + "   WT= " + (wt).ToString();
            d.Location = new Point(150, 220);
            d.Size = new System.Drawing.Size(200, 50);
            tabPage2.Controls.Add(d);

            Button go_back = new Button();
            go_back.Location = new Point(200, 300);
            go_back.Text = "Back";
            go_back.Click += new EventHandler(go_back_Click);
            tabPage2.Controls.Add(go_back);



        }


        private void gantt_Paint()
        {
            int number_processes = int.Parse(number.Text);
            Graphics gObject = tabPage2.CreateGraphics();
            Random rnd = new Random();
            tabPage2.Controls.Clear();


            for (int i = 0, j = 10; i < number_processes; i++)
            {

                Color redColor = Color.FromArgb(255, 0, 0);
                SolidBrush brush = new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                gObject.FillRectangle(brush, j, 150, 480 / number_processes, 20); //total width of screen is 480
                j += 480 / number_processes;
                Label a = new Label();
                a.Text = "P" + (i + 1).ToString();
                a.Location = new Point(j - 240 / number_processes, 200);
                a.Name = "P" + (i + 1).ToString();
                a.Size = new System.Drawing.Size(40, 17);
                tabPage2.Controls.Add(a);

            }
            Button go_back = new Button();
            go_back.Location = new Point(200, 250);
            go_back.Text = "Back";
            go_back.Click += new EventHandler(go_back_Click);
            tabPage2.Controls.Add(go_back);



        }
        private void go_back_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            tabcnt.SelectTab("tabPage1");
            tabPage2.Controls.Clear();
            processes.Clear();

        }


        private void arrival_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (number.Text == "")
            {

                time.Hide();
                arrival.Hide();
                priorty_list.Hide();

            }
            else if (number.Text != null)
            {
                A.Clear();
                T.Clear();
                P.Clear();
                processes.Clear();
                try
                {
                    int number_of_processes = int.Parse(number.Text);
                    int pointX = 0;
                    int pointY = 20;

                    arrival.Controls.Clear();
                    time.Controls.Clear();
                    priorty_list.Controls.Clear();
                    tabPage2.Controls.Clear();
                    for (int i = 0; i < number_of_processes; i++)
                    {
                        TextBox a = new TextBox();
                        a.Text = "P" + (i + 1).ToString();
                        a.Location = new Point(pointX, pointY);
                        A.Add(i + 1, a);
                        arrival.Controls.Add(a);
                        arrival.Show();
                        pointY += 25;
                    }
                    pointY = 20;
                    for (int i = 0; i < number_of_processes; i++)
                    {
                        TextBox a = new TextBox();
                        a.Text = "P" + (i + 1).ToString();
                        a.Location = new Point(pointX, pointY);
                        T.Add(i + 1, a);
                        time.Controls.Add(a);
                        time.Show();
                        pointY += 25;
                    }

                    pointY = 20;

                    for (int i = 0; i < number_of_processes; i++)
                    {
                        TextBox a = new TextBox();
                        a.Text = "0";
                        a.Location = new Point(pointX, pointY);
                        P.Add(i + 1, a);
                        priorty_list.Controls.Add(a);
                        priorty_list.Show();
                        pointY += 25;
                    }


                }
                catch (Exception)
                {
                    /*MessageBox.Show(e.ToString());*/
                    MessageBox.Show("ERROR!");
                }


            }


        }

        private void time_Enter(object sender, EventArgs e)
        {

        }

        private void priort_list_Enter(object sender, EventArgs e)
        {

        }

        private void priorty_CheckedChanged(object sender, EventArgs e)
        {
            if ((priority.Checked || prem_priorty.Checked) && number.Text != null)
                priorty_list.Enabled = true;
            else
            {
                priorty_list.Enabled = false;

            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void prem_priorty_CheckedChanged(object sender, EventArgs e)
        {
            if ((priority.Checked || prem_priorty.Checked) && number.Text != null)
                priorty_list.Enabled = true;
            else
            {
                priorty_list.Enabled = false;

            }

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            number.Text = "";
            tabcnt.SelectTab("config");

        }

        private void number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(13))
            {


                string arrival_text;
                string burst_text;
                int number_of_processes = int.Parse(number.Text);
                for (int i = 0; i < number_of_processes; i++)
                {
                    arrival_text = A.Values.ElementAt(i).Text;
                    burst_text = T.Values.ElementAt(i).Text;

                }

                tabcnt.SelectTab("gantt");
                fcfs_paint();
            }

        }

        private void roundrobbin_CheckedChanged(object sender, EventArgs e)
        {
            if (roundrobin.Checked)
                quantum.Enabled = true;
            else
                quantum.Enabled = false;
        }
    }
}

class process
{
    int arrival;
    int burst;
    int index;
    int priorty;
    Color color;
    int finish;



    public process(int arrival, int burst, int index, int priorty)
    {
        this.arrival = arrival;
        this.burst = burst;
        this.index = index;
        this.priorty = priorty;

    }
    public int get_arrival()
    {
        return arrival;
    }
    public int get_burst()
    {
        return burst;
    }
    public int get_index()
    {
        return index;
    }

    public int get_priorty()
    {
        return priorty;
    }
    public void set_burst(int burst)
    {
        this.burst = burst;
    }
    public void set_arrival(int arrival)
    {
        this.arrival = arrival;
    }
    public void set_index(int index)
    {
        this.index = index;
    }
    public void set_priority(int priorty)
    {
        this.priorty = priorty;
    }
    public void set_color(Color color)
    {
        this.color = color;
    }
    public Color get_color()
    {
        return color;
    }
    public int get_finish()
    {
        return finish;
    }
    public void set_finish(int finish)
    {
        this.finish = finish;
    }
}