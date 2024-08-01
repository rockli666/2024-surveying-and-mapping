using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Cal
    {
        public static List<POINT> p;
        public static List<POINT> suplyP;//增加点之后的
        public static List<MyCuve> mycuver;
        /// <summary>
        /// 增加点的函数
        /// </summary>
        /// <param name="if_close"></param>
        public static void  suplyPOINT(int if_close)
        {
            suplyP = new List<POINT>();
            foreach (POINT p in p)
                suplyP.Add(p);
            //先填充已知点
            if(if_close==1)
            {
                int index = suplyP.Count();
                suplyP.Insert(0, suplyP[index - 1]);
                suplyP.Insert(0, suplyP[index - 1]);
                //增加前面两个点
                suplyP.Add(suplyP[2]);
                suplyP.Add(suplyP[3]);
                suplyP.Add(suplyP[4]);
                //增加后面3个点
            }
            if(if_close==0)
            {
                POINT A = new POINT();
                A.X = suplyP[2].X - suplyP[1].X - (suplyP[1].X - suplyP[0].X);
                A.Y = suplyP[2].Y - suplyP[1].Y - (suplyP[1].Y - suplyP[0].Y);
                A.ID = "A";
                suplyP.Insert(0, A);

                POINT B = new POINT();
                B.X = suplyP[2].X - suplyP[1].X - (suplyP[1].X - suplyP[0].X);
                B.Y = suplyP[2].Y - suplyP[1].Y - (suplyP[1].Y - suplyP[0].Y);
                B.ID = "B";
                suplyP.Insert(0, B);

                int index = suplyP.Count()-1;
                POINT C = new POINT();
                C.X = suplyP[index-2].X - suplyP[index-1].X - (suplyP[index-1].X - suplyP[index].X);
                C.Y = suplyP[index-2].Y - suplyP[index-1].Y - (suplyP[index-1].Y - suplyP[index].Y);
                C.ID = "C";
                suplyP.Add(C);

                index = suplyP.Count() - 1;
                POINT D = new POINT();
                D.X = suplyP[index - 2].X - suplyP[index - 1].X - (suplyP[index - 1].X - suplyP[index].X);
                D.Y = suplyP[index - 2].Y - suplyP[index - 1].Y - (suplyP[index - 1].Y - suplyP[index].Y);
                D.ID = "D";
                suplyP.Add(D);
            }
        }
        /// <summary>
        /// 计算梯度的函数
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="p5"></param>
        /// <param name="sin"></param>
        /// <param name="cos"></param>
        public static void CalSinCos(POINT p1,POINT p2,POINT p3,POINT p4,POINT p5,ref double sin,ref double cos)
        {
            double a1 = p2.X - p1.X;
            double a2 = p3.X - p2.X;
            double a3 = p4.X - p3.X;
            double a4 = p5.X - p4.X;
            double b1 = p2.Y - p1.Y;
            double b2 = p3.Y - p2.Y;
            double b3 = p4.Y - p3.Y;
            double b4 = p5.Y - p4.Y;

            double w2 = Math.Abs(a3 * b4 - a4 * b3);
            double w3 = Math.Abs(a1 * b2 - a2 * b1);

            double a0 = w2 * a2 + w3 * a3;
            double b0 = w2 * b2 + w3 * b3;

            cos = a0 / (Math.Sqrt(a0 * a0 + b0 * b0));
            sin= b0 / (Math.Sqrt(a0 * a0 + b0 * b0));
        }
        /// <summary>
        ///总计算 
        /// </summary>
        /// <param name="if_close"></param>
        public static void caculate(int if_close)
        {
            suplyPOINT(if_close);
            mycuver = new List<MyCuve>();

            for(int i=2;i<suplyP.Count()-3;i++)
            {
                double sin = 0,cos=0;
                double sin1 = 0, cos1 = 0;

                CalSinCos(suplyP[i - 2], suplyP[i - 1], suplyP[i], suplyP[i + 1], suplyP[i + 2],ref sin,ref cos);
                CalSinCos(suplyP[i - 1], suplyP[i], suplyP[i+1], suplyP[i + 2], suplyP[i + 3], ref sin1, ref cos1);

                double r = CalD(suplyP[i], suplyP[i + 1]);
                MyCuve my = new MyCuve();
                my.E0 = suplyP[i].X;
                my.E1 = r * cos;
                my.E2 = 3 * (suplyP[i + 1].X - suplyP[i].X) - r * (cos1 + 2 * cos);
                my.E3 = -2 * (suplyP[i + 1].X - suplyP[i].X) + r * (cos1 + cos);
                my.startp = suplyP[i];
                my.endp = suplyP[i + 1];

                my.F0 = suplyP[i].Y;
                my.F1 = r * sin;
                my.F2 = 3 * (suplyP[i + 1].Y - suplyP[i].Y) - r * (sin1 + 2 * sin);
                my.F3 = -2 * (suplyP[i + 1].Y - suplyP[i].Y) + r * (sin1 + sin);

                mycuver.Add(my);
            }
        }
        /// <summary>
        /// 计算距离的函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double CalD(POINT a,POINT b)
        {
            double dis = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            return dis;
        }
    }
}
