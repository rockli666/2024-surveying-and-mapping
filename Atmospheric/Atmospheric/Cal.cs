using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmospheric
{
    public class Cal
    {
        public static DataEntity data = new DataEntity();
        /// <summary>
        /// 站点相关数据
        /// </summary>
        static double XP = -2225669.7744;
        static double YP = 4998936.1598;
        static double ZP = 3265908.9678;
        static double Bp = GeodeticConverter.ConvertXYZToLLH_B(XP, YP, ZP);
        static double Lp = GeodeticConverter.ConvertXYZToLLH_L(XP, YP, ZP);
        static double x11 = (-1) * Math.Sin(Bp) * Math.Cos(Lp);
        static double x12 = (-1) * Math.Sin(Bp) * Math.Sin(Lp);
        static double x13 = Math.Cos(Bp);
        static double x21 = (-1) * Math.Sin(Lp);
        static double x22 = Math.Cos(Lp);
        static double x23 = 0;
        static double x31 = Math.Cos(Bp) * Math.Cos(Lp);
        static double x32 = Math.Cos(Bp) * Math.Sin(Lp);
        static double x33 = Math.Sin(Bp);
        /// <summary>
        /// 角度计算
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <returns></returns>
        static double ANGLE_A(double X, double Y, double Z)
        {
            double A;
            A = radToDEG(Math.Atan2(Y, X));
            if (A <= 0) A += 360;
            return A;
        }//计算方位角

        static double ANGLE_E(double X, double Y, double Z)
        {
            double E;
            E = radToDEG(Math.Atan(Z / Math.Sqrt(X * X + Y * Y)));
            return E;
        }//计算高度角

        static double degToRAD(double a)
        {
            double temp;
            temp = a / 180 * Math.PI;
            return temp;
        }//角度转弧度

        static double radToDEG(double a)
        {
            double temp;
            temp = a / Math.PI * 180;
            return temp;
        }//弧度转角度
        /// <summary>
        /// 转换为站心坐标
        /// </summary>
        /// <param name="a"></param>
        public static void ConvertToZ(ref POINT pz)
        {
            double deltaX = pz.X - XP;
            double deltaY = pz.Y - YP;
            double deltaZ = pz.Z - ZP;
            pz.X = x11 * deltaX + x12 * deltaY + x13 * deltaZ;
            pz.Y = x21 * deltaX + x22 * deltaY + x23 * deltaZ;
            pz.Z = x31 * deltaX + x32 * deltaY + x33 * deltaZ;
            pz.A = ANGLE_A(pz.X, pz.Y, pz.Z);
            pz.E = ANGLE_E(pz.X, pz.Y, pz.Z);
        }
        /// <summary>
        /// 计算延迟量
        /// </summary>
        /// <returns></returns>
        public static string CalL()
        {
            string text = "";
            foreach (POINT p in data.p)
            {
                double psi = 0.0137 / ((p.E / 180.0) + 0.11) - 0.022;
                double phi = radToDEG(Bp) / 180 + psi * Math.Cos(degToRAD(p.A));
                double lambda = radToDEG(Lp) / 180 + psi * Math.Sin(degToRAD(p.A)) / Math.Cos(phi * Math.PI);
                double phi_m = phi + 0.064 * Math.Cos((lambda - 1.617) * Math.PI);
                double E = p.E;
                E = E / 180;
                double F = 1 + 16 * Math.Pow((0.53 - E), 3);
                double c = 299792458;
                double a1 = 0.1397 * Math.Pow(10, -7);
                double a2 = -0.7451 * Math.Pow(10, -8);
                double a3 = -0.5960 * Math.Pow(10, -7);
                double a4 = 0.1192 * Math.Pow(10, -6);
                double beta1 = 0.1270 * Math.Pow(10, 6);
                double beta2 = -0.1966 * Math.Pow(10, 6);
                double beta3 = 0.6554 * Math.Pow(10, 5);
                double beta4 = 0.2621 * Math.Pow(10, 6);
                double t = 43200 * lambda + data.T;
                double A1 = 5 * Math.Pow(10, -9);
                double A2 = a1 + a2 * phi_m + a3 * Math.Pow(phi_m, 2) + a4 * Math.Pow(phi_m, 3);
                double A3 = 50400;
                double A4 = beta1 + beta2 * phi_m + beta3 * Math.Pow(phi_m, 2) + beta4 * Math.Pow(phi_m, 3);
                if (Math.Abs(2 * Math.PI * (t - A3) / A4) < 1.57)
                {
                    p.delay = F * (A1 + A2 * Math.Cos(2 * Math.PI * (t - A3) / A4)) * c;
                }
                else if (Math.Abs(2 * Math.PI * (t - A3) / A4) >= 1.57)
                {
                    p.delay = F * A1 * c;
                }
                if (p.E <= 0)
                    p.delay = 0;
                text += p.ID + "\t\t" + p.E.ToString("F3") + "\t\t" + p.A.ToString("F3") + "\t\t" + p.delay.ToString("F4") + "\r\n";
            }
            return text;
        }
        /// <summary>
        /// 经纬度转换
        /// </summary>
        public class GeodeticConverter
        {
            // WGS84椭球参数
            private const double a = 6378137.0; // 长半轴
            private const double f = 1 / 298.257223563; // 扁率
            private const double b = a * (1 - f); // 短半轴
            private const double e2 = f * (2 - f); // 第一偏心率平方
            public static double ConvertXYZToLLH_B(double x, double y, double z)
            {
                double p = Math.Sqrt(x * x + y * y);
                double latitudeInitial = Math.Atan2(z, p);
                double latitude = latitudeInitial;
                return latitude;
            }
            public static double ConvertXYZToLLH_L(double x, double y, double z)
            {
                double longitude = Math.Atan2(y, x);
                return longitude;
            }
        }
    }
}
