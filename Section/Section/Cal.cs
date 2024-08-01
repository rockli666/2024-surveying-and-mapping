using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section
{
    class Cal
    {
        public static List<POINT> V1;
        public static List<List<POINT>> V;
        public static List<List<POINT>> VH;
        public static List<POINT> Vp;
        public static List<POINT> p;
        public static List<POINT> K;
        public static List<POINT> ALL;
        public static POINT A = new POINT();
        public static POINT B = new POINT();
        public static double H0;
        public static string text = "";//输出数据

        /// <summary>
        /// 计算方位角
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double CalAngle(POINT a, POINT b)
        {
            double angle = 0;
            double deltaX = b.X - a.X;
            double deltaY = b.Y - a.Y;
            angle = Math.Atan(deltaY / deltaX);
            if (deltaY > 0 && deltaX > 0) ;
            if (deltaY > 0 && deltaX < 0) angle = Math.PI + angle;
            if (deltaY < 0 && deltaX < 0) angle = Math.PI + angle;
            if (deltaY < 0 && deltaX > 0) angle = Math.PI * 2 + angle;
            if (deltaY > 0 && deltaX == 0) angle = Math.PI / 2.0;
            if (deltaY < 0 && deltaX == 0) angle = Math.PI / 2.0 * 3;

            return angle;
        }
        /// <summary>
        /// 计算内插点高程
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double CalH(POINT a)
        {
            List<POINT> PS = new List<POINT>(ALL);
            foreach (POINT p in PS)
            {
                p.dis = Caldis(a, p);
            }
            PS.Sort((x, y) => x.dis.CompareTo(y.dis));
            double on = 0;
            double down = 0;
            for (int i = 0; i < 20; i++)
            {
                on += PS[i].H / PS[i].dis;
                down += 1 / PS[i].dis;
            }
            return on / down;
        }
        /// <summary>
        /// 计算距离的函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Caldis(POINT a, POINT b)
        {
            double dis = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
            return dis;
        }
        /// <summary>
        /// 计算面积的函数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double CalArea(POINT a, POINT b)
        {
            double S = 0;
            S = (a.H + b.H - 2 * H0) / 2.0 * Caldis(a, b);
            return S;
        }
        /// <summary>
        /// 内插纵断面的点
        /// </summary>
        /// <returns></returns>
        public static string CalZ()
        {
            V = new List<List<POINT>>();
            V1 = new List<POINT>();
            string text1 = "";
            double D0 = Caldis(K[0], K[1]);
            double D1 = Caldis(K[1], K[2]);
            double D = D0 + D1;
            text1 += "D0距离是，" + D0.ToString("F3") + "\n";
            text1 += "D1距离是，" + D1.ToString("F3") + "\n";
            text1 += "总距离是，" + D.ToString("F3") + "\n";

            double[] a = new double[K.Count() - 1];
            for (int i = 0; i < K.Count() - 1; i++)
                a[i] = CalAngle(K[i], K[i + 1]);
            text1 += "方位角01是，" + a[0].ToString("F5") + "\n";
            text1 += "方位角12是，" + a[1].ToString("F5") + "\n";
            //准备k和a
            double Dk = 0, d = 0;//Dk用于求当前总长度，d用于求已经插好的点的长度
            int sump = 0;
            for (int i = 0; i < K.Count() - 1; i++)
            {
                V1.Add(K[i]);
                Vp = new List<POINT>();
                Vp.Add(K[i]);
                Dk += Caldis(K[i], K[i + 1]);
                for (int j = 1; j < Dk / 10 - sump; j++)
                {
                    POINT p = new POINT();
                    p.X = K[i].X + (10 * (j + sump) - d) * Math.Cos(a[i]);
                    p.Y = K[i].Y + (10 * (j + sump) - d) * Math.Sin(a[i]);
                    p.H = CalH(p);
                    if (i == 0)
                        p.ID = "Z" + j;
                    if (i == 1)
                        p.ID = "Y" + j;
                    if (i == 0 && j == 3)
                    {
                        text1 += "Z3的坐标X是，" + p.X.ToString("F3") + "\n";
                        text1 += "Z3的坐标Y是，" + p.Y.ToString("F3") + "\n";
                        text1 += "Z3的坐标H是，" + p.H.ToString("F3") + "\n";
                    }
                    if (i == 1 && j == 3)
                    {
                        text1 += "Y3的坐标X是，" + p.X.ToString("F3") + "\n";
                        text1 += "Y3的坐标Y是，" + p.Y.ToString("F3") + "\n";
                        text1 += "Y3的坐标H是，" + p.H.ToString("F3") + "\n";
                    }//输出报告
                    V1.Add(p);
                    Vp.Add(p);
                }
                Vp.Add(K[i + 1]);
                V.Add(Vp);
                sump += (int)(Dk / 10);
                d += Dk;
            }
            V1.Add(K[K.Count() - 1]);
            return text1;
        }
        /// <summary>
        /// 计算横断面
        /// </summary>
        /// <returns></returns>
        public static string CalHENG()
        {
            string text2 = "";
            VH = new List<List<POINT>>();
            for(int i = 0; i < K.Count() - 1; i++)
            {
                Vp = new List<POINT>();
                double a = CalAngle(K[i], K[i + 1]) + Math.PI / 2;
                POINT M = new POINT();
                M.ID = "M" + i;
                M.X = (K[i].X + K[i + 1].X) / 2;
                M.Y = (K[i].Y + K[i + 1].Y) / 2;
                M.H = CalH(M);
                for (int j = -5; j <= 5; j++)
                {
                    POINT p = new POINT();
                    if (j == 0)
                        Vp.Add(M);
                    else
                    {
                        p.X = M.X + 5 * j * Math.Cos(a);
                        p.Y = M.Y + 5 * j * Math.Sin(a);
                        p.H = CalH(p);
                        if (i == 0)
                            p.ID = "Q" + j;
                        if (i == 1)
                            p.ID = "W" + j;
                        Vp.Add(p);
                    }
                    if (j == -3&&i==0)
                    {
                        text2 += "Q3的坐标X是，" + p.X.ToString("F3") + "\n";
                        text2 += "Q3的坐标Y是，" + p.Y.ToString("F3") + "\n";
                        text2 += "Q3的坐标H是，" + p.H.ToString("F3") + "\n";
                    }
                    if (j == -3 && i == 1)
                    {
                        text2 += "W3的坐标X是，" + p.X.ToString("F3") + "\n";
                        text2 += "W3的坐标Y是，" + p.Y.ToString("F3") + "\n";
                        text2 += "W3的坐标H是，" + p.H.ToString("F3") + "\n";
                    }
                }
                VH.Add(Vp);
            }
            return text2;
        }
        /// <summary>
        /// 总计算
        /// </summary>
        public static void Caculate()
        {
            text += "H0的数值是，" + H0.ToString("F3") + "\n";
            text += "K0的数值是，" + K[0].H.ToString("F3") + "\n";
            text += "K1的数值是，" + K[1].H.ToString("F3") + "\n";
            text += "K2的数值是，" + K[2].H.ToString("F3") + "\n";
            double alphaAB = CalAngle(A, B);
            text += "AB方位角是，" + alphaAB.ToString("F5") + "\n";
            A.H = CalH(A);
            text += "A的高程是，" + A.H.ToString("F3") + "\n";
            B.H = CalH(B);
            text += "B的高程是，" + B.H.ToString("F3") + "\n";
            double areaAB = CalArea(A, B);
            text += "AB的面积是，" + areaAB.ToString("F3") + "\n";
            text += CalZ();
            text += CalHENG();
            double s0 = 0, s1 = 0, s2 = 0, area1 = 0, area2 = 0;

            for (int i = 0; i < V1.Count() - 1; i++)
                s0 += CalArea(V1[i], V1[i + 1]);
            text += "纵断面面积是，" + s0.ToString("F3") + "\n";

            for (int i = 0; i < V[0].Count() - 1; i++)
                s1 += CalArea(V[0][i], V[0][i + 1]);
            text += "第一条纵断面面积是，" + s1.ToString("F3") + "\n";

            for (int i = 0; i < V[1].Count() - 1; i++)
                s2 += CalArea(V[1][i], V[1][i + 1]);
            text += "第二条纵断面面积是，" + s2.ToString("F3") + "\n";

            for (int i = 0; i < VH[0].Count() - 1; i++)
                area1 += CalArea(VH[0][i], VH[0][i + 1]);
            text += "第一条横断面面积是，" + area1.ToString("F3") + "\n";

            for (int i = 0; i < VH[1].Count() - 1; i++)
                area2 += CalArea(VH[1][i], VH[1][i + 1]);
            text += "第二条横断面面积是，" + area2.ToString("F3") + "\n";
        }
    }
}
