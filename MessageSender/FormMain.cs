using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageSender
{
    public partial class FormMain : Form
    {
        #region Private variables
        private const double UPPERLIMITX = 0.6;
        private const double LOWERLIMITX = -0.6;
        private const double UPPERLIMITY = 0.6;
        private const double LOWERLIMITY = -0.6;
        private const double UPPERLIMITZ = 0.6;
        private const double LOWERLIMITZ = -0.6;
        private const int PINNUM = 25;
        private BackgroundWorker bgwFinsTotalResultSenderSt2;
        private OmronFinsHelper omronFinsSt2 = new OmronFinsHelper();
        private bool isOmronFinsConnectedSt2 = false;
        private string strReceivedData = "";
        #endregion

        #region Public variables
        public static string strPartNoName = "";
        public static string strBaseDirectory;
        public static string strConfigFilePath;
        public static string strLogFilePath;
        public static IniFile ConfigIniFile;
        public static SettingHelper mSettingHelper = new SettingHelper();
        #endregion

        #region Load, initialize and close form
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ReadSettingConfigFile();
            //初始化Fins连接
            isOmronFinsConnectedSt2 = omronFinsSt2.InitializeOmronFins(omronFinsSt2.mPLCIP, omronFinsSt2.mPLCPort);
            // 更新状态栏
            UpdateStatusLabel();
            InitFinsTotalResultBackWorker();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            omronFinsSt2.CloseOmronFins();
        }

        // 更新状态栏
        private void UpdateStatusLabel()
        {
            this.StatusLabelStartupPath.Text = strBaseDirectory + Application.ProductName + ".exe";

            // Fins connection status
            if (isOmronFinsConnectedSt2)
            {
                StatusLabel_SerialPort.BackColor = Color.Lime;
                StatusLabel_SerialPort.Text = "TCP 已连接";
            }
            else
            {
                StatusLabel_SerialPort.BackColor = Color.Red;
                StatusLabel_SerialPort.Text = "TCP 断开";
            }
        }

        /// <summary>
        /// 读取配置文件 config.ini 中的各项信息
        /// </summary>
        private void ReadSettingConfigFile()
        {
            strBaseDirectory = Utility.GetThisExecutableDirectory();
            strConfigFilePath = strBaseDirectory + "Config\\Config.ini";
            ConfigIniFile = new IniFile(strConfigFilePath);

            // 读取PLC地址
            mSettingHelper.PLCIP2 = ConfigIniFile.IniReadValue("PLC Parameter", "IP2");
            mSettingHelper.PLCPort2 = Convert.ToInt16(ConfigIniFile.IniReadValue("PLC Parameter", "Port2"));
            omronFinsSt2.mPLCIP = mSettingHelper.PLCIP2;
            omronFinsSt2.mPLCPort = mSettingHelper.PLCPort2;
        }
        #endregion

        #region Receive message from another application
        //WM_COPYDATA消息所要求的数据结构
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        private const int WM_COPYDATA = 0x004A;
        //接收消息方法
        protected override void WndProc(ref System.Windows.Forms.Message e)
        {
            if (e.Msg == WM_COPYDATA)
            {
                CopyDataStruct cds = (CopyDataStruct)e.GetLParam(typeof(CopyDataStruct));
                strReceivedData = cds.lpData.ToString();
                string[] strDataAll;
                short[] outputData = new short[PINNUM * 3];
                char[] charSeparatorsOnePin = new char[] { ';' };
                strDataAll = strReceivedData.Split(charSeparatorsOnePin, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < PINNUM * 3; i++)
                {
                    outputData[i] = Convert.ToInt16(strDataAll[i]);
                }
                bgwFinsTotalResultSenderSt2.RunWorkerAsync(outputData);
            }
            base.WndProc(ref e);
        }
        #endregion

        #region Backgroundworker to send result through Omron Fins
        /// <summary>
        /// 初始化TwinCat重新连接的后台线程
        /// </summary>
        private void InitFinsTotalResultBackWorker()
        {
            bgwFinsTotalResultSenderSt2 = new BackgroundWorker();
            bgwFinsTotalResultSenderSt2.DoWork += BgwFinsTotalResultSenderSt2_DoWork;
        }

        /// <summary>
        /// Fins发送数据到第二工站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgwFinsTotalResultSenderSt2_DoWork(object sender, DoWorkEventArgs e)
        {
            short[] mData = (short[])e.Argument;
            short[] outputData = new short[mData.Length + 1];

            // 复制mData到outputData
            for (int i = 0; i < mData.Length; i++)
            {
                outputData[i] = mData[i];
            }

            // 数组最后附加1，表示反馈数据有更新
            outputData[mData.Length] = 1;

            // 如果Fins通讯连接状态正常，发送反馈数据
            if (isOmronFinsConnectedSt2)
            {
                omronFinsSt2.mFinsConnStatus = true;
                // 发送Fins数据，默认情况下失效模式信息以位的形式保存在D4500起始的地址里面
                omronFinsSt2.FinsSendData(outputData, PINNUM * 3 + 1);
            }
        }
        #endregion
    }
}
