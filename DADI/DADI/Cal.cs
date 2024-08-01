using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADI
{
    class Cal
    {
        public static List<POINT> p;
        public static double a;
        public static double f;
        public static string text = "";//输出程序正确性
        public static double b;
        public static double e2;
        public static double e_2;
        /// <summary>
        /// 计算椭球参数
        /// </summary>
        /// <returns></returns>
        public static string Caltuoqiu()
        {
            string text1 = "";
            b = a * (1 - f);
            e2 = (a * a - b * b) / (a * a);
            e_2 = e2 / (1 - e2);
            text1 += "椭球短半轴," + b.ToString("f3") + "\n";
            text1 += "第一偏心率平方," + e2.ToString("f8") + "\n";
            text1 += "第二偏心率平方," + e_2.ToString("f8") + "\n";
            return text1;
        }

        public static string CalD(POINT p, ref double S)
        {
            double B1 = DMStoRAD(p.B1);
            double B2 = DMStoRAD(p.B2);
            double L1 = DMStoRAD(p.L1);
            double L2 = DMStoRAD(p.L2);

            string text2 = "";
            double u1 = Math.Atan(Math.Sqrt(1 - e2) * Math.Tan(B1));
            double u2 = Math.Atan(Math.Sqrt(1 - e2) * Math.Tan(B2));
            double l = L2 - L1;

            double a1 = Math.Sin(u1) * Math.Sin(u2);
            double a2 = Math.Cos(u1) * Math.Cos(u2);
            double b1 = Math.Cos(u1) * Math.Sin(u2);
            double b2 = Math.Sin(u1) * Math.Cos(u2);
            text2 += "u1," + u1.ToString("f8") + "\n";
            text2 += "u2," + u2.ToString("f8") + "\n";

            double lamda = l;
            text2 += "l," + l.ToString("f8") + "\n";
            text2 += "a1," + a1.ToString("f8") + "\n";
            text2 += "a2," + a2.ToString("f8") + "\n";
            text2 += "b1," + b1.ToString("f8") + "\n";
            text2 += "b2," + b2.ToString("f8") + "\n";
            double theta = 0;
            double theta1 = 0;
            double alpha = 0;
            double beta = 0;
            double gama = 0;
            double A1 = 0;
            double sinA0 = 0;
            double sigma = 0;
            double sigma1 = 0;
            double cosA02 = 0;
            do
            {
                double P = Math.Cos(u2) * Math.Sin(lamda);
                double q = b1 - b2 * Math.Cos(lamda);
                A1 = Math.Atan2(P, q);
                if (A1 < 0)
                    A1 += 2 * Math.PI;
                if (a1 > Math.PI * 2)
                    A1 -= 2 * Math.PI;

                double sin_sigma = P * Math.Sin(A1) + q * Math.Cos(A1);
                double cos_sigma = a1 + a2 * Math.Cos(lamda);
                sigma = Math.Atan(sin_sigma / cos_sigma);
                if (cos_sigma > 0)
                    sigma = Math.Abs(sigma);
                if (cos_sigma < 0)
                    sigma = Math.PI - Math.Abs(sigma);

                sinA0 = Math.Cos(u1) * Math.Sin(A1);
                sigma1 = Math.Atan(Math.Tan(u1) / Math.Cos(A1));
                cosA02 = 1 - sinA0 * sinA0;

                alpha = (e2 / 2.0 + e2 * e2 / 8.0 + e2 * e2 * e2 / 16.0) - (e2 * e2 / 16.0 + e2 * e2 * e2 / 16.0) * cosA02 + (3 * e2 * e2 * e2 / 128.0) * cosA02 * cosA02;
                beta = (e2 * e2 / 16.0 + e2 * e2 * e2 / 16.0) * cosA02 - (e2 * e2 * e2 / 32.0) * cosA02 * cosA02;
                gama = (e2 * e2 * e2 / 256.0) * cosA02 * cosA02;

                theta1 = (alpha * sigma +
                    beta * Math.Cos(2 * sigma1 + sigma) * Math.Sin(sigma) +
                    gama * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma)) * sinA0;

                if (Math.Abs(theta - theta1) < 1e-10)
                    break;
                theta = theta1;
                lamda = l + theta;
            } while (true);

            text2 += "alpha," + alpha.ToString("f8") + "\n";
            text2 += "beta," + beta.ToString("f8") + "\n";
            text2 += "gama," + beta.ToString("f8") + "\n";
            text2 += "A1," + A1.ToString("f8") + "\n";
            text2 += "lamda," + lamda.ToString("f8") + "\n";
            text2 += "sigma," + sigma.ToString("f8") + "\n";
            text2 += "sinA0," + sinA0.ToString("f8") + "\n";

            double k2 = e_2 * cosA02;
            double A = (1 - k2 / 4.0 + 7 * k2 * k2 / 64.0 - 15 * k2 * k2 * k2 / 256.0) / b;
            double B = (k2 / 4.0 - k2 * k2 / 8.0 + 37 * k2 * k2 * k2 / 512.0);
            double C = (k2 * k2 / 128.0 - k2 * k2 * k2 / 128.0);


            sigma1 = Math.Atan(Math.Tan(u1) / Math.Cos(A1));
            double Xs = C * Math.Sin(2 * sigma) * Math.Cos(4 * sigma1 + 2 * sigma);
            S = (sigma - B * Math.Sin(sigma) * Math.Cos(2 * sigma1 + sigma) - Xs) / A;

            text2 += "A," + A.ToString("f8") + "\n";
            text2 += "B," + B.ToString("f8") + "\n";
            text2 += "C," + C.ToString("f8") + "\n";
            text2 += "sigma1," + sigma1.ToString("f8") + "\n";
            return text2;
        }

        public static void CalDADI()
        {
            text += "椭球长半轴," + a.ToString("f0") + "\n";
            text += "扁率倒数," + (1/f).ToString("f3") + "\n";
            text += "扁率," + f.ToString("f8") + "\n";
            text += Caltuoqiu();
            for(int i=0;i<p.Count();i++)
            {
                if (i == 0)
                    text += CalD(p[i], ref p[i].S);
                else
                    CalD(p[i], ref p[i].S);
            }
            text += "第一条大地线长度," + p[0].S.ToString("f3") + "\n";
            text += "第二条大地线长度," + p[1].S.ToString("f3") + "\n";
            text += "第三条大地线长度," + p[2].S.ToString("f3") + "\n";
            text += "第四条大地线长度," + p[3].S.ToString("f3") + "\n";
            text += "第五条大地线长度," + p[4].S.ToString("f3") + "\n";
        }
        /// <summary>
        /// 角度转换
        /// </summary>
        /// <param name="DMS"></param>
        /// <returns></returns>
        public static double DMStoRAD(double DMS)
        {
            double angle = 0;
            double D = (int)DMS;
            double M = (int) ((DMS - D) * 100);
            double S = ((DMS - D) * 100 - M) * 100;
            angle = D + M / 60.0 + S / 3600.0;

            return angle / 180.0 * Math.PI;
        }
    }
}
