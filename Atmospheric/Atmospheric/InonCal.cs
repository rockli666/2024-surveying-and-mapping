using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmospheric
{
    class InonCal
    {
        public static DataEntity data = new DataEntity();

        /// <summary>
        /// 湿分量的计算
        /// </summary>
        /// <param name="E"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static double CalMwE(double E, double B)
        {
            double MwE = 0;
            double[,] table = new double[5, 3];
            table[0, 0] = 0.00058021897; table[0, 1] = 0.0014275268; table[0, 2] = 0.043472961;
            table[1, 0] = 0.00056794847; table[1, 1] = 0.0015138625; table[1, 2] = 0.046729510;
            table[2, 0] = 0.00058118019; table[2, 1] = 0.0014572752; table[2, 2] = 0.043908931;
            table[3, 0] = 0.00059727542; table[3, 1] = 0.0015007428; table[3, 2] = 0.044626982;
            table[4, 0] = 0.00061641693; table[4, 1] = 0.0017599082; table[4, 2] = 0.054736038;
            double aw = 0, bw = 0, cw = 0;
            double sinE = Math.Sin(E * Math.PI / 180.0);

            if (Math.Abs(B) < 15.0)
            {
                aw = table[0, 0];
                bw = table[0, 1];
                cw = table[0, 2];
                double on, down;
                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));
                MwE = down / on;
            }
            else if (Math.Abs(B) > 75.0)
            {
                aw = table[4, 0];
                bw = table[4, 1];
                cw = table[4, 2];
                double on, down;
                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));
                MwE = down / on;
            }
            else
            {
                int i = 0, m = 15;
                if (Math.Abs(B) >= 15.0 && Math.Abs(B) < 30.0) { i = 0; m = 15; }
                if (Math.Abs(B) >= 30.0 && Math.Abs(B) < 45.0) { i = 1; m = 30; }
                if (Math.Abs(B) >= 45.0 && Math.Abs(B) < 60.0) { i = 2; m = 45; }
                if (Math.Abs(B) >= 60.0 && Math.Abs(B) < 75.0) { i = 3; m = 60; }
                aw = table[i, 0] + (table[i + 1, 0] - table[i, 0]) * (B - m) / 15.0;
                bw = table[i, 1] + (table[i + 1, 1] - table[i, 1]) * (B - m) / 15.0;
                cw = table[i, 2] + (table[i + 1, 2] - table[i, 2]) * (B - m) / 15.0;
                double on, down;
                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));
                MwE = down / on;
            }
            return MwE;
        }
        /// <summary>
        /// 干分量的计算
        /// </summary>
        /// <param name="E"></param>
        /// <param name="B"></param>
        /// <param name="H"></param>
        /// <returns></returns>
        public static double CalMdE(double E, double B, double H)
        {
            double MdE = 0;
            double[,] table1 = new double[5, 3];
            double[,] table2 = new double[5, 3];
            table1[0, 0] = 0.0012769934; table1[0, 1] = 0.0029153695; table1[0, 2] = 0.062610505;
            table1[1, 0] = 0.0012683230; table1[1, 1] = 0.0029152299; table1[1, 2] = 0.062837393;
            table1[2, 0] = 0.0012465397; table1[2, 1] = 0.0029288445; table1[2, 2] = 0.063721774;
            table1[3, 0] = 0.0012196049; table1[3, 1] = 0.0029022565; table1[3, 2] = 0.063824265;
            table1[4, 0] = 0.0012045996; table1[4, 1] = 0.0029024912; table1[4, 2] = 0.064258455;

            table2[0, 0] = 0.0; table2[0, 1] = 0.0; table2[0, 2] = 0.0;
            table2[1, 0] = 0.000012709626; table2[1, 1] = 0.000021414979; table2[1, 2] = 0.000090128400;
            table2[2, 0] = 0.000026523662; table2[2, 1] = 0.000030160779; table2[2, 2] = 0.000043497037;
            table2[3, 0] = 0.000034000452; table2[3, 1] = 0.000072562722; table2[3, 2] = 0.00084795348;
            table2[4, 0] = 0.000041202191; table2[4, 1] = 0.00011723375; table2[4, 2] = 0.0017037206;

            double aw = 0, bw = 0, cw = 0;
            double aht = 0, bht = 0, cht = 0;
            double sinE = Math.Sin(E * Math.PI / 180.0);
            double t = 31 + 28 + 31 + 30 + 31 + 30 + 28;
            double cos = Math.Cos(2 * Math.PI * (t - 28) / 365.25);

            if (Math.Abs(B) < 15.0)
            {
                aw = table1[0, 0] + table2[0, 0] * cos;
                bw = table1[0, 1] + table2[0, 1] * cos;
                cw = table1[0, 2] + table2[0, 2] * cos;

                double on, down;
                double on1, down1;
                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));
                on1 = 1.0 / (1 + (aht / (1 + bht / (1 + cht))));
                down1 = 1.0 / (sinE + (aht / (sinE + bht / (sinE + cht))));
                MdE = down / on + (1.0 / sinE - down1 / on1) * H / 1000.0;
            }
            else if (Math.Abs(B) > 75.0)
            {
                aw = table1[4, 0] + table2[4, 0] * cos;
                bw = table1[4, 1] + table2[4, 1] * cos;
                cw = table1[4, 2] + table2[4, 2] * cos;

                double on, down;
                double on1, down1;

                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));
                on1 = 1.0 / (1 + (aht / (1 + bht / (1 + cht))));
                down1 = 1.0 / (sinE + (aht / (sinE + bht / (sinE + cht))));
                MdE = down / on + (1.0 / sinE - down1 / on1) * H / 1000.0;
            }
            else
            {
                int i = 0, m = 15;
                if (Math.Abs(B) >= 15.0 && Math.Abs(B) < 30.0) { i = 0; m = 15; }
                if (Math.Abs(B) >= 30.0 && Math.Abs(B) < 45.0) { i = 1; m = 30; }
                if (Math.Abs(B) >= 45.0 && Math.Abs(B) < 60.0) { i = 2; m = 45; }
                if (Math.Abs(B) >= 60.0 && Math.Abs(B) < 75.0) { i = 3; m = 60; }
                aw = table1[i, 0] + (table1[i + 1, 0] - table1[i, 0]) * (B - m) / 15.0 + table2[i, 0] + (table2[i + 1, 0] - table2[i, 0]) * (B - m) / 15.0 * cos;
                bw = table1[i, 1] + (table1[i + 1, 1] - table1[i, 1]) * (B - m) / 15.0 + table2[i, 1] + (table2[i + 1, 1] - table2[i, 1]) * (B - m) / 15.0 * cos;
                cw = table1[i, 2] + (table1[i + 1, 2] - table1[i, 2]) * (B - m) / 15.0 + table2[i, 2] + (table2[i + 1, 2] - table2[i, 2]) * (B - m) / 15.0 * cos;

                double on, down;
                double on1, down1;
                on = 1.0 / (1 + (aw / (1 + bw / (1 + cw))));
                down = 1.0 / (sinE + (aw / (sinE + bw / (cw + sinE))));

                on1 = 1.0 / (1 + (aht / (1 + bht / (1 + cht))));
                down1 = 1.0 / (sinE + (aht / (sinE + bht / (sinE + cht))));
                MdE = down / on + (1.0 / sinE - down1 / on1) * H / 1000.0;
            }
            return MdE;
        }
        /// <summary>
        /// 延迟量的计算
        /// </summary>
        /// <param name="P"></param>
        public static string calDs()
        {
            string text = "";
            text += "测站名".PadRight(5) + "高度角".PadRight(8) + "ZHD".PadRight(8) + "m_d(E)".PadRight(12) + "ZWD".PadRight(8) + "m_w(E)".PadRight(12) + "延迟改正".PadRight(8) + "\n";
            foreach (InonPOINT P in data.Inp)
            {
                double ZHD = 2.29951 * Math.Pow((Math.E), -0.000116 * P.H);
                double ZWD = 0.1;
                P.MwE = CalMwE(P.E, P.B);
                P.MdE = CalMdE(P.E, P.B, P.H);
                P.ZHD = ZHD;
                P.ds = ZHD * CalMdE(P.E, P.B, P.H) + ZWD * CalMwE(P.E, P.B);
                text += string.Format("{0,-8}{1,-10:N2}{2,-10:N3}{3,-10:N3}{4,-10:N3}{5,-10:N3}{6,-10:N3}\n",
        P.ID, P.E, P.ZHD, P.MdE, P.ZWD, P.MwE, P.ds);
            }
            return text;
        }
    }
}
