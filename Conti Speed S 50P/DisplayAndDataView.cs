using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conti_Speed_S_50P
{
    public partial class DisplayAndDataView : UserControl
    {
        private Timer timerUpdateGUI = new Timer();
        private int statusCount = 0;
        private const int PINNUM = 25;
        private const double POSXLOWERLIMIT = -0.25;
        private const double POSXUPPERLIMIT = 0.25;
        private const double POSYLOWERLIMIT = -0.25;
        private const double POSYUPPERLIMIT = 0.25;
        private const double POSZLOWERLIMIT = -0.25;
        private const double POSZUPPERLIMIT = 0.25;
        private bool _isPinExist = true;

        public bool IsPinExist { get => _isPinExist; set => _isPinExist = value; }

        public DisplayAndDataView(int index)
        {
            InitializeComponent();
            displayView1.BackgroundColor = Color.White;
            displayView1.ViewIndex = index;
            this.lblPinIndex.Text = string.Format("{0}#", index + 1);
            displayView1.PosXLowerLimit = POSXLOWERLIMIT;
            displayView1.PosXUpperLimit = POSXUPPERLIMIT;
            displayView1.PosYLowerLimit = POSYLOWERLIMIT;
            displayView1.PosYUpperLimit = POSYUPPERLIMIT;
            displayView1.PosZLowerLimit = POSZLOWERLIMIT;
            displayView1.PosZUpperLimit = POSZUPPERLIMIT;
            timerUpdateGUI.Interval = 200;
            timerUpdateGUI.Enabled = true;
            timerUpdateGUI.Tick += TimerUpdateGUI_Tick;
        }

        // 定时更新界面，用于显示动画效果
        private void TimerUpdateGUI_Tick(object sender, EventArgs e)
        {
            displayView1.Status = statusCount;
            displayView1.Refresh();
            statusCount += 2;
            if (statusCount > 6)
            {
                statusCount = 0;
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
            this.displayView1.AddOneDataRecord(x, y, z);
        }

        /// <summary>
        /// 设置Pin针是否存在
        /// </summary>
        /// <param name="isExist"></param>
        public void SetPinExist(bool isExist)
        {
            displayView1.IsPinExist = isExist;
        }

        /// <summary>
        /// 更新TextBox中的当前数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void UpdateCurrentDataTextBox(double x, double y, double z)
        {
            txtCurrentX.Text = x.ToString();
            txtCurrentY.Text = y.ToString();
            txtCurrentZ.Text = z.ToString();
            txtCurrentX.ForeColor = InRange(x, POSXLOWERLIMIT, POSXUPPERLIMIT) ? Color.Green : Color.Red;
            txtCurrentY.ForeColor = InRange(y, POSYLOWERLIMIT, POSYUPPERLIMIT) ? Color.Green : Color.Red;
            txtCurrentZ.ForeColor = InRange(z, POSZLOWERLIMIT, POSZUPPERLIMIT) ? Color.Green : Color.Red;
        }

        /// <summary>
        /// 判断上限限
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        private bool InRange(double data, double lower, double upper)
        {
            return (data >= lower && data <= upper) ? true : false;
        }

        /// <summary>
        /// 更新TextBox中的输出数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void UpdateOutputDataTextBox(double x, double y, double z)
        {
            txtOutputX.Text = x.ToString();
            txtOutputY.Text = y.ToString();
            txtOutputZ.Text = z.ToString();
        }
    }
}
