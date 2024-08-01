using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DADI
{
    class FileHelp
    {
        public static string READ(string path)
        {
            Cal.p = new List<POINT>();
            string text = File.ReadAllText(path);
            StreamReader re = new StreamReader(path);
            string line = re.ReadLine();
            string[] lines = line.Split(',');
            Cal.a = double.Parse(lines[0]);
            Cal.f = 1 / double.Parse(lines[1]);

            re.ReadLine();
            while(!re.EndOfStream)
            {
                line=re.ReadLine();
                lines = line.Split(',');
                POINT p = new POINT();
                p.SID = lines[0];
                p.B1 = double.Parse(lines[1]);
                p.L1 = double.Parse(lines[2]);
                p.EID = lines[3];
                p.B2 = double.Parse(lines[4]);
                p.L2 = double.Parse(lines[5]);
                Cal.p.Add(p);
            }
            return text;
        }

    }
}
