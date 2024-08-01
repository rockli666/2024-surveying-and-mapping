using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class POINT
    {
        public string ID;
        public double X;
        public double Y;
    }
    class MyCuve
    {
        public double E0, E1, E2, E3;
        public double F0, F1, F2, F3;
        public POINT startp = new POINT();
        public POINT endp = new POINT();
    }
}
