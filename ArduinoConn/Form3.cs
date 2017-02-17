using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArduinoConn.LOCALDB;

namespace ArduinoConn
{
    public partial class Form3 : Form
    {
        public Form1 eris = (Form1)Application.OpenForms["Form1"];
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            pictureBox1.Visible = true;
            using (DBEntities db = new DBEntities())
            {
                Active ekle = new Active();
                ekle.State =1;
                db.Active.Add(ekle);
                db.SaveChanges();
                MessageBox.Show("LOCAL DATABASE ACTIVE");
                eris.CONTROL = true;
            }
        }
    }
}
