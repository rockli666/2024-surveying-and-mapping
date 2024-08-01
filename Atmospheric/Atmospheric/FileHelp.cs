using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Atmospheric
{
    class FileHelp
    {
        /// <summary>
        /// 导入电离层文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string READ1(string path)
        {
            string text = "";
            StreamReader reader = new StreamReader(path);
            text = File.ReadAllText(path);
            string line = reader.ReadLine();
            string[] lines;
            lines = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double T = double.Parse(lines[4]) * 60 * 60 + double.Parse(lines[5]) * 60 + double.Parse(lines[6]);
            Cal.data.T = T;//获得时间

            while (!reader.EndOfStream)
            {
                POINT pt = new POINT();
                line = reader.ReadLine();
                lines = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                pt.ID = lines[0];
                pt.X = double.Parse(lines[1]) * 1000;
                pt.Y = double.Parse(lines[2]) * 1000;
                pt.Z = double.Parse(lines[3]) * 1000;
                //获得基础的坐标啊信息
                Cal.ConvertToZ(ref pt);
                Cal.data.p.Add(pt);
            }
            return text;
        }
        /// <summary>
        /// 导入对流层文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string READ2(string path)
        {
            string text="";
            var reader = new StreamReader(path);
            string line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                text += line+"\n";
                if (line.Length > 0)
                {
                    InonPOINT pt = new InonPOINT();
                    var buf = line.Split(',');
                    pt.ID = buf[0];
                    pt.time = Convert.ToInt32(buf[1]);
                    pt.B = Convert.ToDouble(buf[2]);
                    pt.L = Convert.ToDouble(buf[3]);
                    pt.H = Convert.ToDouble(buf[4]);
                    pt.E = Convert.ToDouble(buf[5]);
                    InonCal.data.Inp.Add(pt);
                }
            }
            return text;
        }
    }
}

