using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Conti_Speed_S_50P
{
    public partial class PageHomeSt2 : UserControl
    {
        private DisplayAndDataView[] displayAndDataView;
        private const int PINNUM = 25;
        private const double POSXLOWERLIMIT = -0.25;
        private const double POSXUPPERLIMIT = 0.25;
        private const double POSYLOWERLIMIT = -0.25;
        private const double POSYUPPERLIMIT = 0.25;
        private const double POSZLOWERLIMIT = -0.25;
        private const double POSZUPPERLIMIT = 0.25;

        public PageHomeSt2()
        {
            InitializeComponent();
            displayAndDataView = new DisplayAndDataView[PINNUM];
            for (int i = 0; i < PINNUM; i++)
            {
                displayAndDataView[i] = new DisplayAndDataView(i);
            }
        }

        private void PageHome_Load(object sender, System.EventArgs e)
        {
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            int column, row;
            this.tableLayoutPanel1.Controls.Clear();
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Clear();
            for (int i = 0; i < 5; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    20F));
            }
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < 5; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    20F));
            }
            for (int i = 0; i < PINNUM; i++)
            {
                row = i / 5;
                column = i - row * 5;
                this.tableLayoutPanel1.Controls.Add(displayAndDataView[i], column, row);
            }
        }

        public void UpdateDisplayView(int index, bool isPinExist, double? x, double? y, double? z)
        {
            if (isPinExist)
            {
                displayAndDataView[index].IsPinExist = true;
                displayAndDataView[index].AddOneDataRecord((double)x, (double)y, (double)z);
                displayAndDataView[index].UpdateCurrentDataTextBox((double)x, (double)y, (double)z);
            }
            else
            {
                displayAndDataView[index].IsPinExist = false;
                displayAndDataView[index].SetPinExist(isPinExist);
            }
        }

        public void UpdateOutputData(int index, double x, double y, double z)
        {
            displayAndDataView[index].UpdateOutputDataTextBox(x, y, z);
        }

        public void ReSizeDisplayView()
        {
            for (int i = 0; i < PINNUM; i++)
            {
                displayAndDataView[i].Refresh();
            }
        }
    }
}
