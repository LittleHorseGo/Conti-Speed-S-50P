using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conti_Speed_S_50P
{
    public class StatisticHelper
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 标准偏差公式：S = Sqrt( ∑( (xi-x_平均)*xi-x_平均) ) /（N-1））) 
        /// 公式中∑代表总和,x_平均代表x的均值,^2代表二次方,Sqrt代表平方根. 　　 
        /// 1.计算总和：sum 
        /// 2.统计个数：count 
        /// 3.计算平均值：avg=sum/count; 
        /// 5.平方累加求和sum2=（x1-avg）^2+（x1-avg）^2+...+（xn-avg）^2 
        /// 6.累加求和结果除以元素的个数 sum2/count
        /// 样本标准偏差除以个数减一 sum2/（count-1） 
        /// 7.总体=sqrt(sum2/count)
        /// 样本=sqrt(sum2/（count-1）);
        /// </summary>
        public static float StDev(float[] arrData)
        {
            float xSum = 0F;
            float xAvg = 0F;
            float sSum = 0F;
            float tmpStDev = 0F;
            int arrNum = arrData.Length;
            for (int i = 0; i < arrNum; i++)
            {
                xSum += arrData[i];
            }
            xAvg = xSum / arrNum;
            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((arrData[j] - xAvg) * (arrData[j] - xAvg));
            }
            tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
            return tmpStDev;
        }

        public static double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return Math.Round(minValue + (next * (maxValue - minValue)), 3);
        }

        public static float Average(float[] arrData)
        {
            float tmpSum = 0F;
            for (int i = 0; i < arrData.Length; i++)
            {
                tmpSum += arrData[i];
            }
            return tmpSum / arrData.Length;
        }

        public static float Max(float[] arrData)
        {
            float tmpMax = arrData[0];
            for (int i = 0; i < arrData.Length; i++)
            {
                if (tmpMax < arrData[i])
                {
                    tmpMax = arrData[i];
                }
            }
            return tmpMax;
        }

        public static float Min(float[] arrData)
        {
            float tmpMin = arrData[0];
            for (int i = 0; i < arrData.Length; i++)
            {
                if (tmpMin > arrData[i])
                {
                    tmpMin = arrData[i];
                }
            }
            return tmpMin;
        }

        public static float CpkU(float upperLimit, float average, float StDev)
        {
            float tmpV = 0F;
            tmpV = upperLimit - average;
            return tmpV / (3 * StDev);
        }

        public static float CpkL(float lowerLimit, float average, float StDev)
        {
            float tmpV = 0F;
            tmpV = average - lowerLimit;
            return tmpV / (3 * StDev);
        }

        public static float Cpk(float CpkU, float CpkL)
        {
            return Math.Abs(Math.Min(CpkU, CpkL));
        }

        /// <summary>
        /// 几何平均数：(x1*x2*...*xn)^(1/n)
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>几何平均数</returns>
        private static double GeometricMean(double[] arr)
        {
            double result = 1;
            foreach (double num in arr)
            {
                result *= Math.Pow(num, 1.0 / arr.Length);
            }
            return result;
        }

        /// <summary>
        /// 调和平均数：n/((1/x1)+(1/x2)+...+(1/xn))
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>调和平均数</returns>
        private static double HarmonicMean(double[] arr)
        {
            double temp = 0;
            foreach (double num in arr)
            {
                temp += (1.0 / num);
            }
            return arr.Length / temp;
        }

        /// <summary>
        /// 平方平均数：((x1*x1+x2*x2+...+xn*xn)/n)^(1/2)
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>平方平均数</returns>
        private static double RootMeanSquare(double[] arr)
        {
            double temp = 0;
            foreach (double num in arr)
            {
                temp += (num * num);
            }
            return Math.Sqrt(temp / arr.Length);
        }

        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns></returns>
        private static double Median(double[] arr)
        {
            //为了不修改arr值，对数组的计算和修改在tempArr数组中进行
            double[] tempArr = new double[arr.Length];
            arr.CopyTo(tempArr, 0);

            //对数组进行排序
            double temp;
            for (int i = 0; i < tempArr.Length; i++)
            {
                for (int j = i; j < tempArr.Length; j++)
                {
                    if (tempArr[i] > tempArr[j])
                    {
                        temp = tempArr[i];
                        tempArr[i] = tempArr[j];
                        tempArr[j] = temp;
                    }
                }
            }

            //针对数组元素的奇偶分类讨论
            if (tempArr.Length % 2 != 0)
            {
                return tempArr[arr.Length / 2 + 1];
            }
            else
            {
                return (tempArr[tempArr.Length / 2] +
                    tempArr[tempArr.Length / 2 + 1]) / 2.0;
            }
        }
    }
}
