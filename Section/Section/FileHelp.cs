using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Section
{
    class FileHelp
    {
        public static string READ(string path)
        {
            Cal.p = new List<POINT>();
            Cal.K = new List<POINT>();
            Cal.ALL = new List<POINT>();
            string text = File.ReadAllText(path);
            StreamReader re = new StreamReader(path);
            string line = re.ReadLine();
            string[] lines = line.Split(',');
            Cal.H0 = double.Parse(lines[1]);
            line = re.ReadLine();
            line = re.ReadLine();
            lines = line.Split(',');
            Cal.A.ID = lines[0];
            Cal.A.X = double.Parse(lines[1]);
            Cal.A.Y = double.Parse(lines[2]);
            line = re.ReadLine();
            lines = line.Split(',');
            Cal.B.ID = lines[0];
            Cal.B.X = double.Parse(lines[1]);
            Cal.B.Y = double.Parse(lines[2]);
            line = re.ReadLine();

            while (!re.EndOfStream)
            {
                line = re.ReadLine();
                lines = line.Split(',');
                POINT p = new POINT();
                p.ID = lines[0];
                p.X = double.Parse(lines[1]);
                p.Y = double.Parse(lines[2]);
                p.H = double.Parse(lines[3]);
                if (p.ID.Contains("K"))
                {
                    Cal.K.Add(p);
                }
                else
                    Cal.p.Add(p);
                Cal.ALL.Add(p);
            }
            return text;
        }
    }
}
