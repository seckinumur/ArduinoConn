using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduinoConn.AMDB.DBCLASS;
using ArduinoConn.AMDB;
using ArduinoConn.LOCALDB;

namespace ArduinoConn
{
    public partial class Form1 : Form
    {
        public bool CONTROL;
        public string value;
        public string value1;
        public Form1()
        {
            InitializeComponent();
            comboBox3.SelectedItem = "9600";
            serialPort1.BaudRate = int.Parse(comboBox3.SelectedItem.ToString());
            serialPort2.BaudRate = int.Parse(comboBox3.SelectedItem.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                comboBox1.DataSource = SerialPort.GetPortNames();
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort2.PortName = comboBox1.SelectedItem.ToString();
                comboBox2.SelectedItem = "PASSIVE";
            }
            catch
            {
                MessageBox.Show("ERR. Serial Port NOT FOUND");
            }
            try
            {
                using (DBEntities2 db = new DBEntities2())
                {
                    var bul = db.Active.Where(p => p.State == 1).FirstOrDefault();
                    if (bul.State == 1)
                    {
                        CONTROL = true;
                    }
                }
            }
            catch
            {
                CONTROL = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
                serialPort1.Open();
                serialPort1.Write("1");
                button1.BackColor = Color.White;
                await Task.Delay(60);
                button1.BackColor = Color.DarkRed;
                toolStripStatusLabel2.Text = "TEMPERATURE";
            }
            catch
            {
                MessageBox.Show("ERR. Serial Port NOT FOUND");
                toolStripStatusLabel2.Text = "ERR. Serial Port NOT FOUND";
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string a = serialPort1.ReadLine();
            value = a;
            listBox1.Items.Add(a);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            serialPort1.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.BaudRate = int.Parse(comboBox3.SelectedItem.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox3.SelectedItem = "9600";
            comboBox2.SelectedItem = "PASSIVE";
            serialPort1.BaudRate = int.Parse(comboBox3.SelectedItem.ToString());
            toolStripStatusLabel1.Text = "| Timer : OFF |";
            timer1.Stop();
            numericUpDown1.Value = 5000;
            listBox1.Items.Clear();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort2.Close();
                serialPort2.Open();
                serialPort2.Write("0");
                button2.BackColor = Color.White;
                await Task.Delay(60);
                button2.BackColor = Color.Gold;
                toolStripStatusLabel2.Text = "LIGHT SENSOR";
            }
            catch
            {
                MessageBox.Show("ERR. Serial Port NOT FOUND");
                toolStripStatusLabel2.Text = "ERR. Serial Port NOT FOUND";
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "ACTIVE")
            {
                if (CONTROL == true)
                {
                    DialogResult Uyari = new DialogResult();
                    Uyari = MessageBox.Show("Artificial intelligence activated?", "Attention!", MessageBoxButtons.YesNo);
                    if (Uyari == DialogResult.Yes)
                    {
                        timer1.Interval = Convert.ToInt32(numericUpDown1.Value);
                        timer1.Start();
                        toolStripStatusLabel1.Text = "| Timer : " + numericUpDown1.Value.ToString() + " |";
                    }
                }
                else
                {
                    Form3 ac = new Form3();
                    ac.Show();
                    ac.button1.Visible = true;
                    ac.label2.Visible = false;
                    ac.pictureBox1.Visible = false;
                    MessageBox.Show("LOCAL DATABASE NOT ACTIVE!");
                }
            }
            else
            {
                timer1.Stop();
                toolStripStatusLabel1.Text = "| Timer : OFF |";
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.Open();
            serialPort1.Write("1");
            await Task.Delay(100);
            serialPort2.Open();
            serialPort2.Write("0");
            await Task.Delay(100);
            try
            {
                using (DBEntities2 db = new DBEntities2())
                {
                    MainLog Record = new MainLog();
                    Record.Days = DateTime.Now.Day.ToString();
                    Record.Month = DateTime.Now.Month.ToString();
                    Record.Time = DateTime.Now.ToShortTimeString();
                    Record.Years = DateTime.Now.Year.ToString();
                    Record.Light = value1;
                    Record.Temperature = value;
                    Record.Damp = "NO DATA";
                    db.MainLog.Add(Record);
                    db.SaveChanges();
                    value1 = "";
                    value = "";
                }
            }
            catch
            {
                MessageBox.Show("ERR. Serial Port NOT FOUND");
                toolStripStatusLabel2.Text = "ERR. Serial Port NOT FOUND";
            }

        }

        private void pLANToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cORECODEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 ac = new Form2();
            ac.Show();
        }

        private void dATABASESETTINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 ac = new Form3();
            if (CONTROL == true)
            {
                ac.Show();
                ac.button1.Visible = false;
                ac.label2.Visible = true;
                ac.pictureBox1.Visible = true;
            }
            else
            {
                ac.Show();
                ac.button1.Visible = true;
                ac.label2.Visible = false;
                ac.pictureBox1.Visible = false;
            }
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string a = serialPort2.ReadLine();
            value1 = a;
            listBox1.Items.Add(a);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            serialPort2.Close();
        }
    }
}
