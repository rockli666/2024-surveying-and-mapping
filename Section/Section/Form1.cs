using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Section
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文本文件|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.Text = FileHelp.READ(op.FileName);
                }
                catch
                {
                    MessageBox.Show("文件打开失败");
                }
            }
        }

        private void 一键计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
                MessageBox.Show("请先导入文件");
            else
            {
                Cal a=new Cal();
                Cal.Caculate();
                richTextBox2.Text = Cal.text;
                richTextBox2.Text += "=========纵断面结果==========="+"\n";
                richTextBox2.Text += "ID"+"\t"+"X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.V1)
                    richTextBox2.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3")+"\n";
                richTextBox2.Text += "=========横断面1结果===========" + "\n";
                richTextBox2.Text += "ID" + "\t" + "X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.VH[0])
                    richTextBox2.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3") + "\n";
                richTextBox2.Text += "=========横断面2结果===========" + "\n";
                richTextBox2.Text += "ID" + "\t" + "X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.VH[1])
                    richTextBox2.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3") + "\n";
            }
            tabControl1.SelectedIndex = 2;
        }

        private void 保存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.FileName = "reslut";
            sa.Filter = "文本文件|*.txt";
            if (richTextBox2.Text == "")
                MessageBox.Show("请先导入文件并计算");
            else
            {
                if (sa.ShowDialog() == DialogResult.OK)
                {              
                    File.WriteAllText(sa.FileName, richTextBox2.Text);
                }
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.导入文件" +
                "\n" +
                "2.点击一键计算" +
                "\n" +
                "3.计算结果包含总断面数据和横断面数据");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文本文件|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.Text = FileHelp.READ(op.FileName);
                }
                catch
                {
                    MessageBox.Show("文件打开失败");
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.FileName = "reslut";
            sa.Filter = "文本文件|*.txt";
            if (richTextBox2.Text == "")
                MessageBox.Show("请先导入文件并计算");
            else
            {
                if (sa.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sa.FileName, richTextBox2.Text);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
                MessageBox.Show("请先导入文件");
            else
            {
                richTextBox3.Clear();
                richTextBox2.Clear();
                Cal a = new Cal();
                Cal.Caculate();
                richTextBox2.Text = Cal.text;
                richTextBox3.Text += "=========纵断面结果===========" + "\n";
                richTextBox3.Text += "ID" + "\t" + "X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.V1)
                    richTextBox3.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3") + "\n";
                richTextBox3.Text += "=========横断面1结果===========" + "\n";
                richTextBox3.Text += "ID" + "\t" + "X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.VH[0])
                    richTextBox3.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3") + "\n";
                richTextBox3.Text += "=========横断面2结果===========" + "\n";
                richTextBox3.Text += "ID" + "\t" + "X" + "\t" + "Y" + "\t" + "H" + "\n";
                foreach (POINT p in Cal.VH[1])
                    richTextBox3.Text += p.ID + "\t" + p.X.ToString("F3") + "\t" + p.Y.ToString("F3") + "\t" + p.H.ToString("F3") + "\n";
            }
            tabControl1.SelectedIndex = 2;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.导入文件" +
               "\n" +
               "2.点击一键计算" +
               "\n" +
               "3.计算结果包含总断面数据和横断面数据");
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
