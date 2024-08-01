using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atmospheric
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 打开电流层文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文本文件|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string path = op.FileName;
                try
                {
                    richTextBox1.Text = FileHelp.READ1(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("输入文件有误");
                }
            }
        }

        private void 计算电离层改正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Cal.data.p.Count() == 0)
                MessageBox.Show("请输入对流层文件");
            else
                richTextBox2.Text = Cal.CalL();
        }

        private void 打开对流层文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "文本文件|*.txt";
            if (op.ShowDialog() == DialogResult.OK)
            {
                string path = op.FileName;
                try
                {
                    richTextBox1.Text = FileHelp.READ2(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("输入文件有误");
                }
            }
        }

        private void 计算对流层改正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InonCal.data.Inp.Count() == 0)
                MessageBox.Show("请输入对流层文件");
            else
                richTextBox2.Text = InonCal.calDs();
        }
    }
}
