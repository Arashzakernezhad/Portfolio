using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assign7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) { HockeyPlayer.sortBy = 0; } // by Name
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) { HockeyPlayer.sortBy = 1; } // by Number
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) { HockeyPlayer.sortBy = 2; } // by Goals
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) { HockeyPlayer.revSort = 1; }
            else { HockeyPlayer.revSort = 0; }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please Enter all the fields"); return;
            }
            HockeyPlayer a;
            a = new HockeyPlayer(textBox1.Text, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text));
            listBox1.Items.Add(a);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HockeyPlayer u = new HockeyPlayer(textBox1.Text, Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            listBox1.Items[listBox1.SelectedIndex] = u;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            button3.Enabled = true;
            HockeyPlayer temp = (HockeyPlayer)listBox1.SelectedItem;
            textBox1.Text = temp.Name;
            textBox2.Text = temp.Number.ToString();
            textBox3.Text = temp.Goals.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HockeyPlayer[] list = new HockeyPlayer[listBox1.Items.Count];
            listBox1.Items.CopyTo(list, 0);
            Array.Sort(list);

            if (HockeyPlayer.revSort == 1)
            {
                Array.Reverse(list);
            }

            switch (HockeyPlayer.sortBy)
            {
                case 0:
                    label4.Font = new Font(label4.Font, FontStyle.Bold);
                    label5.Font = new Font(label5.Font, FontStyle.Regular);
                    label6.Font = new Font(label6.Font, FontStyle.Regular);
                    break;
                case 1:
                    label5.Font = new Font(label5.Font, FontStyle.Bold);
                    label4.Font = new Font(label4.Font, FontStyle.Regular);
                    label6.Font = new Font(label6.Font, FontStyle.Regular);
                    break;
                case 2:
                    label6.Font = new Font(label6.Font, FontStyle.Bold);
                    label4.Font = new Font(label4.Font, FontStyle.Regular);
                    label5.Font = new Font(label5.Font, FontStyle.Regular);
                    break;
            }
            listBox1.Items.Clear();
            listBox1.Items.AddRange(list);
        }

        private void loadTeamFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.ShowDialog();

            string name = OpenFileDialog1.FileName;

            if (!File.Exists(name))
            {
                MessageBox.Show("Cannot find the file:" + name);
                return;
            }

            FileStream fileIn = new FileStream(name, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fileIn);

            string line = reader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                HockeyPlayer temp = new HockeyPlayer(fields[0], Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]));

                listBox1.Items.Add(temp);
                line = reader.ReadLine();
            }

            reader.Close(); 
            fileIn.Close();
        }

        private void saveTeamFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.ShowDialog();
            saveFileDialog1.FileName = "HockeyNew.csv";
            FileStream fileout = new FileStream(saveFileDialog1.FileName , FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileout);

            foreach (object obj in listBox1.Items)
            {
                HockeyPlayer x = (HockeyPlayer)obj;

                writer.Write(x.Name + ",");
                writer.Write(x.Number + ",");
                writer.WriteLine(x.Goals);
            }
            writer.Close();
            fileout.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
