using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Conti_Speed_S_50P
{
    public partial class PageDatabaseManage : UserControl
    {
        private const int PINNUMBER = 25;
        private static readonly Random random = new Random();
        private double[] posX = new double[PINNUMBER];
        private double[] posY = new double[PINNUMBER];
        private double[] posZ = new double[PINNUMBER];
        private const double UPPERLIMITX = 0.25;
        private const double LOWERLIMITX = -0.25;
        private const double UPPERLIMITY = 0.25;
        private const double LOWERLIMITY = -0.25;
        private const double UPPERLIMITZ = 0.25;
        private const double LOWERLIMITZ = -0.25;

        private int tableIndex = 0;

        SqlConnection con = new SqlConnection();
        DataSet dataSet = new DataSet();

        public PageDatabaseManage()
        {
            InitializeComponent();
        }

        private void PageDatabaseManage_Load(object sender, EventArgs e)
        {
            cmbDataTableList.Items.Add("St1_PinData");
            cmbDataTableList.Items.Add("St2_PinData");
            cmbDataTableList.SelectedIndex = 0;
            InitializeDataGridViewStyle();
            try
            {
                //连接数据库
                con = SqlHelper.GetConnection(FormMain.mSettingHelper.ConnectionString);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 初始化DataGridView样式
        /// </summary>
        private void InitializeDataGridViewStyle()
        {
            // 设置表格样式
            DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font(
                "Microsoft YaHei",
                7.8F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridView1.GridColor = System.Drawing.SystemColors.ControlLight;
            // 设置表格交替样式
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private double RandomNumberBetween(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            return Math.Round(minValue + (next * (maxValue - minValue)), 3);
        }

        private void cmbDataTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tableIndex = cmbDataTableList.SelectedIndex;
        }

        /// <summary>
        /// 读取数据库中所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadDataSt_Click(object sender, EventArgs e)
        {
            if (tableIndex == 0)
            {
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St1_PinData order by Id desc");

                //也可以直接用DataTable来绑定 
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            else if (tableIndex == 1)
            {
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St2_PinData order by Id desc");

                //也可以直接用DataTable来绑定 
                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }

        /// <summary>
        /// 插入一条新的数据记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadLatestDataSt_Click(object sender, EventArgs e)
        {
            if (tableIndex == 0)
            {
                //查询
                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St1_PinData order by Id desc");
                dataGridView1.DataSource = ds.Tables[0];
            }
            else if (tableIndex == 1)
            {
                //查询
                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St2_PinData order by Id desc");
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        /// <summary>
        /// 读取最新一条数据记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertDataSt_Click(object sender, EventArgs e)
        {
            if (tableIndex == 0)
            {
                string TableName = "St1_PinData";
                StringBuilder sqlQuery = new StringBuilder("INSERT INTO " + TableName);
                sqlQuery.Append(" values(");
                sqlQuery.Append(@"'" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + @"',");
                sqlQuery.Append("'OK',");
                for (int i = 0; i < 25; i++)
                {
                    posX[i] = RandomNumberBetween(LOWERLIMITX, UPPERLIMITX);
                    posY[i] = RandomNumberBetween(LOWERLIMITY, UPPERLIMITY);
                    posZ[i] = RandomNumberBetween(LOWERLIMITZ, UPPERLIMITZ);
                }
                for (int i = 0; i < 24; i++)
                {
                    sqlQuery.Append(string.Format("'{0}',", posX[i]));
                    sqlQuery.Append(string.Format("'{0}',", posY[i]));
                    sqlQuery.Append(string.Format("'{0}',", posZ[i]));
                }
                sqlQuery.Append(string.Format("'{0}',", posX[24]));
                sqlQuery.Append(string.Format("'{0}',", posY[24]));
                sqlQuery.Append(string.Format("'{0}')", posZ[24]));
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sqlQuery.ToString());
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St1_PinData order by Id desc");
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            else if (tableIndex == 1)
            {
                string TableName = "St2_PinData";
                StringBuilder sqlQuery = new StringBuilder("INSERT INTO " + TableName);
                sqlQuery.Append(" values(");
                sqlQuery.Append(@"'" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + @"',");
                sqlQuery.Append("'OK',");
                for (int i = 0; i < 25; i++)
                {
                    posX[i] = RandomNumberBetween(LOWERLIMITX, UPPERLIMITX);
                    posY[i] = RandomNumberBetween(LOWERLIMITY, UPPERLIMITY);
                    posZ[i] = RandomNumberBetween(LOWERLIMITZ, UPPERLIMITZ);
                }
                for (int i = 0; i < 24; i++)
                {
                    sqlQuery.Append(string.Format("'{0}',", posX[i]));
                    sqlQuery.Append(string.Format("'{0}',", posY[i]));
                    sqlQuery.Append(string.Format("'{0}',", posZ[i]));
                }
                sqlQuery.Append(string.Format("'{0}',", posX[24]));
                sqlQuery.Append(string.Format("'{0}',", posY[24]));
                sqlQuery.Append(string.Format("'{0}')", posZ[24]));
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sqlQuery.ToString());
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St2_PinData order by Id desc");
                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }

        private void btnClearAllData_Click(object sender, EventArgs e)
        {
            if (tableIndex == 0)
            {
                string TableName = "St1_PinData";
                StringBuilder sqlQuery = new StringBuilder("Truncate table " + TableName);
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sqlQuery.ToString());
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St1_PinData order by Id desc");
                dataGridView1.DataSource = dataSet.Tables[0];
            }
            else if (tableIndex == 1)
            {
                string TableName = "St2_PinData";
                StringBuilder sqlQuery = new StringBuilder("Truncate table " + TableName);
                SqlHelper.ExecuteNonQuery(con, CommandType.Text, sqlQuery.ToString());
                dataSet = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 100 *  from St2_PinData order by Id desc");
                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }
    }
}
