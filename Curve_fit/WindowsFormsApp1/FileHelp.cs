using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApp1
{
    class FileHelp
    {
        public static string READ(string path)
        {
            string text = File.ReadAllText(path);
            StreamReader re = new StreamReader(path);
            Cal.p = new List<POINT>();
            while(!re.EndOfStream)
            {
                POINT p = new POINT();
                string line = re.ReadLine();
                string[] lines = line.Split(',');
                p.ID = lines[0];
                p.X = double.Parse(lines[1]);
                p.Y = double.Parse(lines[2]);

                Cal.p.Add(p);
            }
            return text;
        }
    }
}
