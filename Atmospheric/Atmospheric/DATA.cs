using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmospheric
{
    public class POINT
    {
        public string ID;
        public double X;
        public double Y;
        public double Z;
        public double A;
        public double E;
        public double delay;
    }
    public class DataEntity
    {
        public List<POINT> p = new List<POINT>();
        public List<InonPOINT> Inp = new List<InonPOINT>();
        public double T;//储存时间
    }

    public class InonPOINT
    {
        public string ID;
        public double B, L, H, E, ds;
        public double ZWD = 0.1, ZHD;
        public double MwE, MdE;
        public int time;
    }
}
