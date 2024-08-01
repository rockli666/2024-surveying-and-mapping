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

namespace DADI
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

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            richTextBox3.Clear();
            if (richTextBox1.Text == "")
                MessageBox.Show("请先导入文件");
            else
            {
                Cal.text = "";
                Cal.CalDADI();
                richTextBox2.Text = Cal.text;
            }
            tabControl1.SelectedIndex = 2;
            richTextBox3.Text += "起点ID" + "\t" + "起点纬度" + "\t" + "起点经度" + "\t" + "终点ID" + "\t" + "终点纬度" + "\t" + "终点经度" + "\t" + "大地线长度" + "\n";
            foreach (POINT p in Cal.p)
                richTextBox3.Text += p.SID  + "\t" + p.B1.ToString("f3") + "\t" + p.L1.ToString("f3")+
              "\t" + p.EID + "\t" + p.B2.ToString("f3") + "\t" + p.L2.ToString("f3")
                    + "\t" + p.S.ToString("f3") + "\n";
        }

        private void 保存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "文本文件|*.txt";
            sa.FileName = "reslut";
            if (richTextBox2.Text == "")
            {
                MessageBox.Show("请先导入文件并点击计算");
            }
            else
            {
                if (sa.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sa.FileName, richTextBox2.Text);
                }
            }
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
            sa.Filter = "文本文件|*.txt";
            sa.FileName = "reslut";
            if (richTextBox2.Text == "")
            {
                MessageBox.Show("请先导入文件并点击计算");
            }
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
            richTextBox2.Clear();
            richTextBox3.Clear();
            if (richTextBox1.Text == "")
                MessageBox.Show("请先导入文件");
            else
            {
                Cal.text = "";
                Cal.CalDADI();
                richTextBox2.Text = Cal.text;

                tabControl1.SelectedIndex = 2;
                richTextBox3.Text += "起点ID" + "\t" + "起点纬度" + "\t" + "起点经度" + "\t" + "终点ID" + "\t" + "终点纬度" + "\t" + "终点经度" + "\t" + "大地线长度" + "\n";
                foreach (POINT p in Cal.p)
                    richTextBox3.Text += p.SID + "\t" + p.B1.ToString("f3") + "\t" + p.L1.ToString("f3") +
                  "\t" + p.EID + "\t" + p.B2.ToString("f3") + "\t" + p.L2.ToString("f3")
                        + "\t" + p.S.ToString("f3") + "\n";
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("请先打开文件" + "\n" +
                "点击计算" +
                "\n" +
                "计算结果包含程序正确性和报告");
        }
    }
}
