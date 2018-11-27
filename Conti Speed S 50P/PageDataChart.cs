using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Conti_Speed_S_50P
{
    enum Algorithm
    {
        Average,
        Median,
    }

    public partial class PageDataChart : UserControl
    {
        private const int PINNUM = 25;
        private const int DATANUM = 30;
        private Chart[] chart;
        private int mSelectedDataIndex = 0;
        private double xNominal, xLowerLimit, xUpperLimit, 
            yNominal, yLowerLimit, yUpperLimit, 
            zNominal, zLowerLimit, zUpperLimit;
        private List<AllPinDataXYZ> allPinDataXYZSt1 = new List<AllPinDataXYZ>();
        private List<AllPinDataXYZ> allPinDataXYZSt2 = new List<AllPinDataXYZ>();
        private DataTable dtResultDetails;
        public DataTable DtResultDetails { get => dtResultDetails; set => dtResultDetails = value; }
        public int comboSelectedIndex { get; set; }
        public double XNominal { get => xNominal; set => xNominal = value; }
        public double XLowerLimit { get => xLowerLimit; set => xLowerLimit = value; }
        public double XUpperLimit { get => xUpperLimit; set => xUpperLimit = value; }
        public double YNominal { get => yNominal; set => yNominal = value; }
        public double YLowerLimit { get => yLowerLimit; set => yLowerLimit = value; }
        public double YUpperLimit { get => yUpperLimit; set => yUpperLimit = value; }
        public double ZNominal { get => zNominal; set => zNominal = value; }
        public double ZLowerLimit { get => zLowerLimit; set => zLowerLimit = value; }
        public double ZUpperLimit { get => zUpperLimit; set => zUpperLimit = value; }

        public PageDataChart()
        {
            InitializeComponent();
            chart = new Chart[3] { chartX, chartY, chartZ };
            // 添加series到图表中
            Series[] series = new Series[PINNUM];
            for (int index = 0; index < 3; index++)
            {
                for (int i = 0; i < PINNUM; i++)
                {
                    series[i] = new Series();
                    series[i].ChartArea = "ChartArea1";
                    series[i].ChartType = SeriesChartType.Line;
                    series[i].Name = "Pin" + (i + 1);
                    series[i].MarkerSize = 6;
                    series[i].MarkerStyle = MarkerStyle.Square;
                    chart[index].Series.Add(series[i]);
                }

                // 添加上下限到图表中
                Series[] seriesLimit = new Series[3];
                for (int i = 0; i < 3; i++)
                {
                    seriesLimit[i] = new Series();
                    seriesLimit[i].ChartArea = "ChartArea1";
                    seriesLimit[i].ChartType = SeriesChartType.Line;
                    seriesLimit[i].Name = "Limit" + (i + 1);
                    if (i == 0) seriesLimit[i].Color = Color.Black;
                    else seriesLimit[i].Color = Color.Red;
                    seriesLimit[i].BorderDashStyle = ChartDashStyle.Dash;
                    List<double> yLimit = new List<double>();
                    for (int j = 1; j <= DATANUM; j++)
                    {
                        if (i == 0) yLimit.Add(0);
                        else if (i == 1) yLimit.Add(-0.25);
                        else if (i == 2) yLimit.Add(0.25);
                    }
                    seriesLimit[i].Points.DataBindY(yLimit);
                    chart[index].Series.Add(seriesLimit[i]);
                }
            }

        }

        private void PageHome_Load(object sender, System.EventArgs e)
        {
            for (int i = 0; i < PINNUM; i++)
            {
                checkedListBoxPinName.Items.Add("Pin No. " + (i + 1));
            }

            checkedListBoxPinName.SetItemChecked(0, true);

            mSelectedDataIndex = 0;
            // 默认选择St1工站
            cmbStationSelect.Items.Add("工站St1");
            cmbStationSelect.Items.Add("工站St2");
            cmbStationSelect.SelectedIndex = 0;

            //默认让Pin1数据显示
            for (int i = 0; i < 3; i++)
            {
                SetSeriesVisable(i, mSelectedDataIndex);
            }
        }

        /// <summary>
        /// 添加一条数据记录
        /// </summary>
        /// <param name="dataXYZ"></param>
        public void AddDataRecordToChartSt1(AllPinDataXYZ dataXYZ)
        {
            if (allPinDataXYZSt1.Count < DATANUM)
            {
                allPinDataXYZSt1.Add(dataXYZ);
            }
            else
            {
                allPinDataXYZSt1.RemoveAt(0);
                allPinDataXYZSt1.Add(dataXYZ);
            }
        }

        /// <summary>
        /// 更新工站1图表
        /// </summary>
        public void UpdateChartSt1()
        {
            // 如果选择是的St2，则不更新
            if (cmbStationSelect.SelectedIndex == 1) return;

            // 清除数据
            for (int i = 0; i < PINNUM; i++)
            {
                chart[0].Series["Pin" + (i + 1)].Points.Clear();
                chart[1].Series["Pin" + (i + 1)].Points.Clear();
                chart[2].Series["Pin" + (i + 1)].Points.Clear();
            }
            // 添加数据
            for (int i = 0; i < PINNUM; i++)
            {
                List<double> yDataPosZ = new List<double>();
                List<double> yDataPosX = new List<double>();
                List<double> yDataPosY = new List<double>();

                for (int j = 0; j < allPinDataXYZSt1.Count; j++)
                {
                    if (allPinDataXYZSt1[j].PosXOrigData[i] != null)
                    {
                        yDataPosX.Add((double)(allPinDataXYZSt1[j].PosXOrigData[i]));
                    }
                    if (allPinDataXYZSt1[j].PosYOrigData[i] != null)
                    {
                        yDataPosY.Add((double)(allPinDataXYZSt1[j].PosYOrigData[i]));
                    }
                    if (allPinDataXYZSt1[j].PosZOrigData[i] != null)
                    {
                        yDataPosZ.Add((double)(allPinDataXYZSt1[j].PosZOrigData[i]));
                    }
                }
                chart[0].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosX);
                chart[1].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosY);
                chart[2].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosZ);
            }
        }

        /// <summary>
        /// 添加一条数据记录
        /// </summary>
        /// <param name="dataXYZ"></param>
        public void AddDataRecordToChartSt2(AllPinDataXYZ dataXYZ)
        {
            if (allPinDataXYZSt2.Count < DATANUM)
            {
                allPinDataXYZSt2.Add(dataXYZ);
            }
            else
            {
                allPinDataXYZSt2.RemoveAt(0);
                allPinDataXYZSt2.Add(dataXYZ);
            }
        }
        /// <summary>
        /// 更新工站2图表
        /// </summary>
        /// <param name="index">表格序号：0,1,2</param>
        public void UpdateChartSt2()
        {
            // 如果选择是的St1，则不更新
            if (cmbStationSelect.SelectedIndex == 0) return;

            // 清除数据
            for (int i = 0; i < PINNUM; i++)
            {
                chart[0].Series["Pin" + (i + 1)].Points.Clear();
                chart[1].Series["Pin" + (i + 1)].Points.Clear();
                chart[2].Series["Pin" + (i + 1)].Points.Clear();
            }
            // 添加数据
            for (int i = 0; i < PINNUM; i++)
            {
                List<double> yDataPosZ = new List<double>();
                List<double> yDataPosX = new List<double>();
                List<double> yDataPosY = new List<double>();

                for (int j = 0; j < allPinDataXYZSt2.Count; j++)
                {
                    if (allPinDataXYZSt2[j].PosXOrigData[i] != null)
                    {
                        yDataPosX.Add((double)(allPinDataXYZSt2[j].PosXOrigData[i]));
                    }
                    if (allPinDataXYZSt2[j].PosYOrigData[i] != null)
                    {
                        yDataPosY.Add((double)(allPinDataXYZSt2[j].PosYOrigData[i]));
                    }
                    if (allPinDataXYZSt2[j].PosZOrigData[i] != null)
                    {
                        yDataPosZ.Add((double)(allPinDataXYZSt2[j].PosZOrigData[i]));
                    }
                }
                chart[0].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosX);
                chart[1].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosY);
                chart[2].Series["Pin" + (i + 1)].Points.DataBindY(yDataPosZ);
            }
        }

        /// <summary>
        /// 设置折线可见
        /// </summary>
        /// <param name="index"></param>
        /// <param name="seriesNo"></param>
        private void SetSeriesVisable(int index, int seriesNo)
        {
            for (int i = 0; i < PINNUM; i++)
            {
                if (i == seriesNo)
                {
                    chart[index].Series[i].Enabled = true;
                }
                else chart[index].Series[i].Enabled = false;
            }
        }

        /// <summary>
        /// 清除折线图中的所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearStatistics_Click(object sender, EventArgs e)
        {
            allPinDataXYZSt1.Clear();
            for (int i = 0; i < PINNUM; i++)
            {
                chart[0].Series["Pin" + (i + 1)].Points.Clear();
                chart[1].Series["Pin" + (i + 1)].Points.Clear();
                chart[2].Series["Pin" + (i + 1)].Points.Clear();
            }
        }

        /// <summary>
        /// 选择工站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStationSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStationSelect.SelectedIndex == 0)
            {
                UpdateChartSt1();
            }
            else
            {
                UpdateChartSt2();
            }
        }

        /// <summary>
        /// 选择Pin针
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxPinName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox listBox = sender as CheckedListBox;
            listBox.SetItemChecked(mSelectedDataIndex, false);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < listBox.Items.Count; j++)
                {
                    if (listBox.GetItemChecked(j))
                    {
                        mSelectedDataIndex = j;
                        chart[i].Series[j].Enabled = true;
                    }
                    else
                        chart[i].Series[j].Enabled = false;
                }
            }
        }
    }
}
