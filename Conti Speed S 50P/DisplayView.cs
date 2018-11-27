using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Conti_Speed_S_50P
{
    public partial class DisplayView : UserControl
    {
        private static readonly Random random = new Random();
        private const int PINNUM = 25;
        private const int DATANUM = 30;
        private Color _backgroundColor;
        private int _viewIndex;
        // 历史数据，允许为Null，当Pin针没有时，不显示数据
        private List<double?> _posX = new List<double?>();
        private List<double?> _posY = new List<double?>();
        private List<double?> _posZ = new List<double?>();
        private double _posXUpperLimit;
        private double _posYUpperLimit;
        private double _posZUpperLimit;
        private double _posXLowerLimit;
        private double _posYLowerLimit;
        private double _posZLowerLimit;
        private bool _isPinExist = true;

        // 试图的状态，用于控制动画
        private int _status = 0;

        public Color BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }
        public int ViewIndex { get => _viewIndex; set => _viewIndex = value; }
        public double PosXUpperLimit { get => _posXUpperLimit; set => _posXUpperLimit = value; }
        public double PosYUpperLimit { get => _posYUpperLimit; set => _posYUpperLimit = value; }
        public double PosZUpperLimit { get => _posZUpperLimit; set => _posZUpperLimit = value; }
        public double PosXLowerLimit { get => _posXLowerLimit; set => _posXLowerLimit = value; }
        public double PosYLowerLimit { get => _posYLowerLimit; set => _posYLowerLimit = value; }
        public double PosZLowerLimit { get => _posZLowerLimit; set => _posZLowerLimit = value; }
        public List<double?> PosX { get => _posX; set => _posX = value; }
        public List<double?> PosY { get => _posY; set => _posY = value; }
        public List<double?> PosZ { get => _posZ; set => _posZ = value; }
        public int Status { get => _status; set => _status = value; }
        public bool IsPinExist { get => _isPinExist; set => _isPinExist = value; }

        public DisplayView()
        {
            InitializeComponent();
        }

        private void DisplayView_Load(object sender, EventArgs e)
        {
            picBoxView.BackColor = BackgroundColor;
            picBoxView.Paint += PicBoxView_Paint;
        }

        private void PicBoxView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // 消除锯齿
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // 第一步：画标签
            //Brush brushBack = new SolidBrush(Color.MintCream);
            //g.FillRectangle(brushBack, 0, 0, 15, 15);
            //g.DrawString((ViewIndex + 1).ToString(),
            //    new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
            //    new SolidBrush(Color.Black),
            //    0,
            //    0);

            // 第二步：画中心线
            Pen penCenterLine = new Pen(Color.LightGray);
            penCenterLine.Width = 1.5F;
            penCenterLine.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            Point pointMiddleLeft = new Point(0, picBoxView.Height / 2);
            Point pointMiddleRight = new Point(picBoxView.Width, picBoxView.Height / 2);
            Point pointMiddleTop = new Point(picBoxView.Width / 2, 0);
            Point pointMiddleBot = new Point(picBoxView.Width / 2, picBoxView.Height);
            g.DrawLine(penCenterLine, pointMiddleLeft, pointMiddleRight);
            g.DrawLine(penCenterLine, pointMiddleTop, pointMiddleBot);

            // 第三步：画刻度尺X轴
            Pen penRuleLine = new Pen(Color.DarkSlateGray);
            for (int i = 0; i <= 25; i++)
            {
                Point point1 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 * i / 200, picBoxView.Height / 2);
                Point point2;
                Point point3 = new Point(picBoxView.Width / 2 - picBoxView.Width * 3 * i / 200, picBoxView.Height / 2);
                Point point4;
                if (i % 5 == 0 && i % 10 != 0)
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 * i/ 200, picBoxView.Height / 2 - picBoxView.Height * 6 / 200);
                    point4 = new Point(picBoxView.Width / 2 - picBoxView.Width * 3 * i / 200, picBoxView.Height / 2 - picBoxView.Height * 6 / 200);
                }
                else if (i % 10 == 0)
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 * i/ 200, picBoxView.Height / 2 - picBoxView.Height * 9 / 200);
                    point4 = new Point(picBoxView.Width / 2 - picBoxView.Width * 3 * i / 200, picBoxView.Height / 2 - picBoxView.Height * 9 / 200);
                }
                else
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 * i/ 200, picBoxView.Height / 2 - picBoxView.Height * 3 / 200);
                    point4 = new Point(picBoxView.Width / 2 - picBoxView.Width * 3 * i / 200, picBoxView.Height / 2 - picBoxView.Height * 3 / 200);
                }
                g.DrawLine(penRuleLine, point1, point2);
                g.DrawLine(penRuleLine, point3, point4);
            }

            // 第四步：画X刻度标记+/-0.25
            g.DrawString("0.25",
                new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                new SolidBrush(Color.DarkSlateGray),
                picBoxView.Width / 2 + picBoxView.Width * 3 * 20 / 200,
                picBoxView.Height / 2);

            g.DrawString("-0.25",
                new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                new SolidBrush(Color.DarkSlateGray),
                picBoxView.Width / 2 - picBoxView.Width * 3 * 30 / 200,
                picBoxView.Height / 2);

            // 第五步：画刻度尺Y轴
            for (int i = 0; i <= 25; i++)
            {
                Point point1 = new Point(picBoxView.Width / 2, picBoxView.Height / 2 + picBoxView.Height * 3 * i / 200);
                Point point2;
                Point point3 = new Point(picBoxView.Width / 2, picBoxView.Height / 2 - picBoxView.Height * 3 * i / 200);
                Point point4;
                if (i % 5 == 0 && i % 10 != 0)
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 6 / 200, picBoxView.Height / 2 + picBoxView.Height * 3 * i / 200);
                    point4 = new Point(picBoxView.Width / 2 + picBoxView.Width * 6 / 200, picBoxView.Height / 2 - picBoxView.Height * 3 * i / 200);
                }
                else if (i % 10 == 0)
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 9 / 200, picBoxView.Height / 2 + picBoxView.Height * 3 * i / 200);
                    point4 = new Point(picBoxView.Width / 2 + picBoxView.Width * 9 / 200 , picBoxView.Height / 2 - picBoxView.Height * 3 * i / 200);
                }
                else
                {
                    point2 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 / 200, picBoxView.Height / 2 + picBoxView.Height * 3 * i / 200);
                    point4 = new Point(picBoxView.Width / 2 + picBoxView.Width * 3 / 200, picBoxView.Height / 2 - picBoxView.Height * 3 * i / 200);
                }
                g.DrawLine(penRuleLine, point1, point2);
                g.DrawLine(penRuleLine, point3, point4);
            }

            // 第六步：画Y刻度标记+/-0.25
            g.DrawString("0.25",
                new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                new SolidBrush(Color.DarkSlateGray),
                picBoxView.Width / 2 - 15,
                picBoxView.Height / 2 - picBoxView.Height * 3 * 31 / 200);

            g.DrawString("-0.25",
                new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                new SolidBrush(Color.DarkSlateGray),
                picBoxView.Width / 2 - 15, 
                picBoxView.Height / 2 + picBoxView.Height * 3 * 26 / 200);

            // 第七步：画边界线，边界线左右上下四边各留picBox宽度的1/8间隙，方便用于显示超出范围的Pin针
            Pen penLimitLine = new Pen(Color.OrangeRed);
            penLimitLine.Width = 1.0F;
            penLimitLine.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawRectangle(penLimitLine, (float)picBoxView.Width / 8,
                (float)picBoxView.Height / 8,
                (float)picBoxView.Width * 3 / 4,
                (float)picBoxView.Height * 3 / 4);

            // 第八步;画数据点，如果该Pin针存在，则画出所有数据的坐标，如果Pin针不存在，直接显示文字提示
            if (!IsPinExist)
            {
                g.DrawString("No Pin Here",
                    new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                    new SolidBrush(Color.LightGray),
                    picBoxView.Width / 6,
                    picBoxView.Height / 3);
            }
            else
            {
                Brush[] brushDataPoint = new Brush[10];
                // 定义不同等级的颜色，由浅到深，共10各等级
                // 远离中心线越远的数据点颜色越深
                brushDataPoint[0] = new SolidBrush(Color.FromArgb(200, 200, 200));
                brushDataPoint[1] = new SolidBrush(Color.FromArgb(180, 180, 180));
                brushDataPoint[2] = new SolidBrush(Color.FromArgb(160, 160, 160));
                brushDataPoint[3] = new SolidBrush(Color.FromArgb(140, 140, 140));
                brushDataPoint[4] = new SolidBrush(Color.FromArgb(120, 120, 120));
                brushDataPoint[5] = new SolidBrush(Color.FromArgb(100, 100, 100));
                brushDataPoint[6] = new SolidBrush(Color.FromArgb(90, 90, 90));
                brushDataPoint[7] = new SolidBrush(Color.FromArgb(80, 80, 80));
                brushDataPoint[8] = new SolidBrush(Color.FromArgb(70, 70, 70));
                brushDataPoint[9] = new SolidBrush(Color.FromArgb(60, 60, 60));

                Brush brushDataOutofSpec = new SolidBrush(Color.OrangeRed);
                Brush brushDataPerfect = new SolidBrush(Color.Lime);
                int colorLevel;
                int x, y;
                // -------------|----------------------|-----------|-----------|----------------------|-------------
                //      PosZLowerLimit    valuePerfectLow         Zero    valuePerfectHigh      PosZUpperLimit
                //double valuePerfectLow = PosZLowerLimit + (PosZUpperLimit - PosZLowerLimit) * 0.4;
                //double valuePerfectHigh = PosZLowerLimit + (PosZUpperLimit - PosZLowerLimit) * 0.6;
                double valuePerfectLow = PosZLowerLimit + (PosZUpperLimit - PosZLowerLimit) * 0.45;
                double valuePerfectHigh = PosZLowerLimit + (PosZUpperLimit - PosZLowerLimit) * 0.55;

                for (int i = 0; i < PosX.Count; i++)
                {
                    // 画布的坐标原点为左上角，计算y值时应该取原始数据PosY[i]的负值
                    x = (int)(picBoxView.Width / 8 + (PosX[i] - PosXLowerLimit) / (PosXUpperLimit - PosXLowerLimit) * 0.75 * picBoxView.Width);
                    y = (int)(picBoxView.Height / 8 + (-PosY[i] - PosYLowerLimit) / (PosYUpperLimit - PosYLowerLimit) * 0.75 * picBoxView.Height);

                    // 如果x, y偏出整个PicBox，则将其显示在最边界上，防止点超出画图区域
                    if (x < 0) x = 0;
                    if (x > picBoxView.Width) x = picBoxView.Width;
                    if (y < 0) y = 0;
                    if (y > picBoxView.Height) x = picBoxView.Height;

                    // 计算画点的区域
                    Rectangle r;
                    if (i < PosX.Count - 1)
                    {
                        r = new Rectangle(x - 3,
                            y - 3,
                            6,
                            6);
                    }
                    else
                    {
                        r = new Rectangle(x - 3 - Status / 2,
                            y - 3 - Status / 2,
                            6 + Status,
                            6 + Status);
                    }

                    // 判断坐标是否在pictureBox的范围内
                    if (x >= 0 && x <= picBoxView.Width && y >= 0 && y <= picBoxView.Width)
                    {
                        // 判断数值是否超出spec
                        //if (PosX[i] < PosXLowerLimit || PosX[i] > PosXUpperLimit ||
                        //    PosY[i] < PosYLowerLimit || PosY[i] > PosYUpperLimit ||
                        //    PosZ[i] < PosZLowerLimit || PosZ[i] > PosZUpperLimit)
                        if (PosZ[i] < PosZLowerLimit || PosZ[i] > PosZUpperLimit)
                        {
                            // 圆形中填充颜色
                            g.FillEllipse(brushDataOutofSpec, r);
                        }
                        else if (PosZ[i] >= valuePerfectLow && PosZ[i] <= valuePerfectHigh)
                        {
                            // 圆形中填充颜色
                            g.FillEllipse(brushDataPerfect, r);
                        }
                        else
                        {
                            colorLevel = (int)((Math.Abs((double)PosZ[i]) -
                                (valuePerfectHigh - valuePerfectLow) / 2) * 20 /
                                ((PosZUpperLimit - PosZLowerLimit) - (valuePerfectHigh - valuePerfectLow)));
                            if (colorLevel < 0) colorLevel = 0;
                            if (colorLevel > 9) colorLevel = 9;
                            // 圆形中填充颜色
                            g.FillEllipse(brushDataPoint[colorLevel], r);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加一条新的数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void AddOneDataRecord(double x, double y, double z)
        {
            if (PosX.Count < DATANUM)
            {
                PosX.Add(x);
                PosY.Add(y);
                PosZ.Add(z);
            }
            else
            {
                PosX.RemoveAt(0);
                PosX.Add(x);
                PosY.RemoveAt(0);
                PosY.Add(y);
                PosZ.RemoveAt(0);
                PosZ.Add(z);
            }
        }

        private double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();
            return Math.Round(minValue + (next * (maxValue - minValue)), 3);
        }

        private int RandomNumberBetween(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
