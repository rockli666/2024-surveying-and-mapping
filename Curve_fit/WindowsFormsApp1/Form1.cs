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

namespace WindowsFormsApp1
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
                    MessageBox.Show("请导入正确的文件");
                }
            }
        }

        private void 闭合曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("请先打开文件");
            }
            else
            {
                Cal.caculate(1);
                richTextBox2.Clear();
                richTextBox2.Text += "起点ID" + "\t" + "起点X" + "\t" + "起点Y" + "\t" + "终点ID" + "\t" + "终点X" + "\t" + "终点Y"
                    + "\t" + "E0" + "\t" + "E1" + "\t" + "E2" + "\t" + "E3"
                    + "\t" + "F0" + "\t" + "F1" + "\t" + "F2" + "\t" + "F3" + "\n";
                foreach (MyCuve p in Cal.mycuver)
                {
                    richTextBox2.Text += p.startp.ID + "\t" + p.startp.X.ToString("f3") + "\t" + p.startp.Y.ToString("f3")
                        + "\t" + p.endp.ID + "\t" + p.endp.X.ToString("f3") + "\t" + p.endp.Y.ToString("f3")
                        + "\t" + p.E0.ToString("f3") + "\t" + p.E1.ToString("f3") + "\t" + p.E2.ToString("f3") + "\t" + p.E3.ToString("f3")
                        + "\t" + p.F0.ToString("f3") + "\t" + p.F1.ToString("f3") + "\t" + p.F2.ToString("f3") + "\t" + p.F3.ToString("f3") + "\n";
                }
                updatachart();
                tabControl1.SelectedIndex = 2;
            }
        }

        private void 不闭合曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("请先点击打开文件");
            }
            else
            {
                Cal.caculate(0);
                richTextBox2.Clear();
                richTextBox2.Text += "起点ID" + "\t" + "起点X" + "\t" + "起点Y" + "\t" + "终点ID" + "\t" + "终点X" + "\t" + "终点Y"
                    + "\t" + "E0" + "\t" + "E1" + "\t" + "E2" + "\t" + "E3"
                    + "\t" + "F0" + "\t" + "F1" + "\t" + "F2" + "\t" + "F3" + "\n";
                foreach (MyCuve p in Cal.mycuver)
                {
                    richTextBox2.Text += p.startp.ID + "\t" + p.startp.X.ToString("f3") + "\t" + p.startp.Y.ToString("f3")
                        + "\t" + p.endp.ID + "\t" + p.endp.X.ToString("f3") + "\t" + p.endp.Y.ToString("f3")
                        + "\t" + p.E0.ToString("f3") + "\t" + p.E1.ToString("f3") + "\t" + p.E2.ToString("f3") + "\t" + p.E3.ToString("f3")
                        + "\t" + p.F0.ToString("f3") + "\t" + p.F1.ToString("f3") + "\t" + p.F2.ToString("f3") + "\t" + p.F3.ToString("f3") + "\n";
                }
            }
            updatachart();
            tabControl1.SelectedIndex = 2;
        }

        private void 保存文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
            {
                MessageBox.Show("请先点击计算");
            }
            else
            {
                SaveFileDialog sa = new SaveFileDialog();
                sa.Filter = "文本文件|*.txt";
                sa.FileName = "reslut";

                if (sa.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sa.FileName, richTextBox2.Text);
                }
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.打开文件" +
                "\n" +
                "2.点击闭合曲线或不闭合曲线进行曲线拟合" +
                "\n" +
                "3.查看报告和图形结果");
        }

        public void updatachart()
        {
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = Cal.suplyP.Min(p=>p.X)-10 ;
            chart1.ChartAreas[0].AxisY.Minimum = Cal.suplyP.Min(p => p.Y) - 10;
            chart1.ChartAreas[0].AxisX.Maximum = Cal.suplyP.Max(p => p.X) + 10;
            chart1.ChartAreas[0].AxisY.Maximum = Cal.suplyP.Max(p => p.Y) + 10;
            foreach (MyCuve p in Cal.mycuver)
            {
                double i = 0.01;
                double z = 0;
                while(z<1)
                {
                    double x = p.E0 + p.E1 * z + p.E2 * z * z + p.E3 * z * z * z;
                    double y = p.F0 + p.F1 * z + p.F2 * z * z + p.F3 * z * z * z;
                    chart1.Series[0].Points.AddXY(x, y);
                    z += i;
                }
            }
        }
    }
}

