using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;

namespace Conti_Speed_S_50P
{
    #region Enum class
    // different access level to access different button or menu,
    // password is required for Supervisor and Administrator level
    public enum AccessLevel { Operator, Supervisor, Administrator }
    // Run state to indicate the status of job running
    public enum RunState { Stopped, RunningContinuous, RunningOnce, RunningLive }
    // capture mode to indicate how to capture the trigger or other signal from PLC,
    // default is scan mode
    public enum CommunicationType { TwinCat, Fins, RS232 }
    public enum TriggerEdge { Rising, Falling }
    public enum IOCaptureMode { Scan, Interrupt }
    // View的类型：DataGrid适合大量数据的显示，如多个Pin的位置度、高度等信息
    // Chart适合数据量比较少时用来显示数据的动态变化趋势
    public enum ViewType { DataGrid, Chart }
    // View图表的排版结构
    // Horizontal, //图表水平布局
    // Vertical,   //图表垂直布局，默认情况下是垂直布局
    public enum ViewLayout { Horizontal, Vertical }
    public enum ResultType { OK, NG, Error }

    #endregion

    public partial class FormMain : Form
    {
        #region Page
        public PageHomeSt1 _homeSt1 = new PageHomeSt1();
        public PageHomeSt2 _homeSt2 = new PageHomeSt2();
        public PageDatabaseManage _databaseManage = new PageDatabaseManage();
        public PageDataChart _dataChart = new PageDataChart();
        public PageSetting _setting = new PageSetting();
        #endregion

        #region Private variable
        public const int PINNUM = 25;
        private int totalCount1 = 0; //总计数
        private int totalCount2 = 0; //总计数

        private AllPinDataXYZ allPins1 = new AllPinDataXYZ(); // 当次测量数据
        private short[] outputData1;
        private short[] currentOutputData1; // 当前工位St1反馈数据
        private List<AllPinDataXYZ> pinOrigDataSt1 = new List<AllPinDataXYZ>(); // 原始数据

        private AllPinDataXYZ allPins2 = new AllPinDataXYZ(); // 当次测量数据
        private short[] outputData2;
        private short[] currentOutputData2; // 当前工位St2反馈数据
        private List<AllPinDataXYZ> pinOrigDataSt2 = new List<AllPinDataXYZ>(); // 原始数据

        private int dataIndex1;
        private int dataIndex2;
        private OmronFinsHelper omronFinsSt1 = new OmronFinsHelper();
        //private OmronFinsHelper omronFinsSt2 = new OmronFinsHelper();
        private bool isOmronFinsConnectedSt1 = false;
        private bool isOmronFinsConnectedSt2 = false;
        private bool isOmronFinsConnected = false;
        private BackgroundWorker bgwFinsTotalResultSenderSt1;
        private BackgroundWorker bgwFinsTotalResultSenderSt2;
        private BackgroundWorker bgwRetriveDatabaseData;
        private SqlConnection con = new SqlConnection();
        private DataSet dataSet = new DataSet();
        private string strReceivedData = "";
        #endregion

        #region Public variable
        public static string strPartNoName = "";
        public static string strBaseDirectory;
        public static string strConfigFilePath;
        public static string strMessageSenderConfigFilePath;
        public static string strLogFilePath;
        public static AccessLevel mCurrentAccessLevel = AccessLevel.Operator;
        public static IniFile ConfigIniFile;
        public static IniFile MessageSenderConfigIniFile;
        public static SettingHelper mSettingHelper = new SettingHelper();
        #endregion

        #region Form load, close, initialization
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void FormMain_Resize(object sender, EventArgs e)
        {
            //_homeSt1.ReSizeDisplayView();
            //_homeSt2.ReSizeDisplayView();
        }
        /// <summary>
        /// FormMain的构造函数，主界面显示前，先显示SplashForm界面，
        /// 先处理耗时程序：如加载vpp文件，初始化通讯，检查通讯状态等
        /// </summary>
        public FormMain()
        {
            // Creat and initialize MainForm components
            Splasher.Status = "Creating MainForm...";
            Splasher.Progress = 10;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            // Load setting file from the folder
            Splasher.Status = "Loading setting file...";
            Splasher.Progress = 20;
            Thread.Sleep(100);
            strBaseDirectory = Utility.GetThisExecutableDirectory();
            this.StatusLabelStartupPath.Text = strBaseDirectory + Application.ProductName + ".exe";
            ReadSettingConfigFile();
            InitVariable();

            // Load Vpp file from the folder
            Splasher.Status = "Loading Vpp Files...";
            Splasher.Progress = 25;

            // Delete obsolete data and image from the local folder
            Splasher.Status = "Delete obsolete data and image...";
            Splasher.Progress = 35;
            //DeleteObsoleteDataImage();

            Splasher.Status = "Initialize home page ...";
            Splasher.Progress = 40;
            Thread.Sleep(200);

            // 初始化状态栏
            InitStatusLabel();

            Splasher.Status = "Initialize IO board ...";
            Splasher.Progress = 60;
            Thread.Sleep(200);
            // 初始化IO板卡
            // if (!isIOCardDisabled) ioHelper.InitialIOCard();

            Splasher.Status = "Initialize TwinCat connection ...";
            Splasher.Progress = 80;

            //初始化Fins连接
            isOmronFinsConnectedSt1 = omronFinsSt1.InitializeOmronFins(omronFinsSt1.mPLCIP, omronFinsSt1.mPLCPort);
            //isOmronFinsConnectedSt2 = omronFinsSt2.InitializeOmronFins(omronFinsSt2.mPLCIP, omronFinsSt2.mPLCPort);
            isOmronFinsConnected = isOmronFinsConnectedSt1 && isOmronFinsConnectedSt2;

            // 开启后台软件MessageSender发送Fins数据给St2
            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pro.StartInfo.FileName = strBaseDirectory + "MessageSender\\MessageSender.exe";
            pro.Start();

            // 根据登陆状态更新界面控件
            UpdateAccessLevel();
            _setting.SetSettingHelper(mSettingHelper);
        }

        /// <summary>
        /// 加载主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // 初始界面为主界面
            _homeSt1.Dock = DockStyle.Fill;
            ResetToolStripBackColor();
            toolStripHome1.BackColor = SystemColors.ControlLight;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(_homeSt1);
            this.Resize += new System.EventHandler(this.FormMain_Resize);

            // 更新控件的可用状态
            UpdateControlsEnabled();
            // 更新状态栏
            UpdateStatusLabel();

            // 初始化数据库连接
            InitSQLServerConnection();
            // 初始化获取数据库数据后台线程
            InitRetriveDatabaseDataBackWorker();
            // 后台线程开始工作
            bgwRetriveDatabaseData.RunWorkerAsync();
            // 初始化Omron Fins发送数据后台线程
            InitFinsTotalResultBackWorker();

            // Show ready to work
            //StartCaptureTriggerSignal(mCaptureMode);
            Splasher.Status = "Ready to work...";
            Splasher.Progress = 100;
            Thread.Sleep(200);
            // Close the splash form and show the main form
            Splasher.Close();
            // Bring the main form to the front of screen
            IntPtr hWnd = this.Handle;
            SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// 软件关闭之前执行的操作：文件保存，资源释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 弹出“确认退出”消息框
            // 如果要取消退出，软件恢复
            if (MessageBox.Show("是否要退出程序?", "请确认！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            { e.Cancel = true; return; }
            try
            {
                // 结束后台线程Message Sender
                StopProcess("MessageSender");
                bgwFinsTotalResultSenderSt1.CancelAsync();
                bgwFinsTotalResultSenderSt2.CancelAsync();
                omronFinsSt1.CloseOmronFins();
                //omronFinsSt2.CloseOmronFins();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 结束后台进程
        /// </summary>
        /// <param name="processName"></param>
        public static void StopProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(processName);
                foreach (System.Diagnostics.Process p in ps)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 初始化变量
        /// </summary>
        private void InitVariable()
        {
            outputData1 = new short[PINNUM * 3];
            currentOutputData1 = new short[PINNUM * 3];
            outputData2 = new short[PINNUM * 3];
            currentOutputData2 = new short[PINNUM * 3];
            // 默认情况下反馈数据为0
            for (int i = 0; i < PINNUM * 3; i++)
            {
                outputData1[i] = 0;
                outputData2[i] = 0;
            }
            try
            {
                // 从配置文件中读取上次保存的数据，作为初始值
                string strTemp = ConfigIniFile.IniReadValue("PLCData", "Data1");
                // 如果为空，则初始值为0
                if (string.IsNullOrEmpty(strTemp))
                {
                    // 默认情况下反馈数据为0
                    for (int i = 0; i < PINNUM * 3; i++)
                    {
                        currentOutputData1[i] = 0;
                    }
                }
                // 如果不为空，通过逗号分隔符读取75个数据作为初始值
                else
                {
                    string[] strArray = strTemp.Split(',');
                    // 默认情况下反馈数据为0
                    for (int i = 0; i < PINNUM * 3; i++)
                    {
                        currentOutputData1[i] = Convert.ToInt16(strArray[i]);
                    }
                }
                // 从配置文件中读取上次保存的数据，作为初始值
                strTemp = ConfigIniFile.IniReadValue("PLCData", "Data2");
                // 如果为空，则初始值为0
                if (string.IsNullOrEmpty(strTemp))
                {
                    // 默认情况下反馈数据为0
                    for (int i = 0; i < PINNUM * 3; i++)
                    {
                        currentOutputData2[i] = 0;
                    }
                }
                // 如果不为空，通过逗号分隔符读取75个数据作为初始值
                else
                {
                    string[] strArray = strTemp.Split(',');
                    // 默认情况下反馈数据为0
                    for (int i = 0; i < PINNUM * 3; i++)
                    {
                        currentOutputData2[i] = Convert.ToInt16(strArray[i]);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("检查配置文件中[PLCData]字段中数据是否合法！");
            }
        }

        /// <summary>
        /// 工具栏点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = sender as ToolStripButton;
            switch (btn.Name)
            {
                case "toolStripHome1":
                    _homeSt1.Dock = DockStyle.Fill;
                    ResetToolStripBackColor();
                    toolStripHome1.BackColor = SystemColors.ControlLight;
                    panelMain.Controls.Clear();
                    panelMain.Controls.Add(_homeSt1);
                    break;
                case "toolStripHome2":
                    _homeSt2.Dock = DockStyle.Fill;
                    ResetToolStripBackColor();
                    toolStripHome2.BackColor = SystemColors.ControlLight;
                    panelMain.Controls.Clear();
                    panelMain.Controls.Add(_homeSt2);
                    break;
                case "toolStripDatabase":
                    _databaseManage.Dock = DockStyle.Fill;
                    ResetToolStripBackColor();
                    toolStripDatabase.BackColor = SystemColors.ControlLight;
                    panelMain.Controls.Clear();
                    panelMain.Controls.Add(_databaseManage);
                    break;
                case "toolStripChart":
                    _dataChart.Dock = DockStyle.Fill;
                    ResetToolStripBackColor();
                    toolStripChart.BackColor = SystemColors.ControlLight;
                    panelMain.Controls.Clear();
                    panelMain.Controls.Add(_dataChart);
                    break;
                case "toolStripSetting":
                    FormSetting formSetting = new FormSetting();
                    formSetting.ShowDialog();
                    break;
                case "toolStripLogin":
                    FormLogin form = new FormLogin(mCurrentAccessLevel);
                    form.ShowDialog();
                    // Update gui to reflect current accessLevel
                    mCurrentAccessLevel = form.GetCurrentAccessLevel;
                    UpdateControlsEnabled();
                    UpdateStatusLabel();
                    // 根据登陆状态更新界面控件
                    UpdateAccessLevel();
                    break;
                case "toolStripSave":
                    // 保存配置文件到本地文件夹
                    break;
                case "toolStripTest":
                    omronFinsSt1.FinsSendData(100);
                    //short[] data = new short[PINNUM * 3];
                    //for (int i = 0; i < PINNUM * 3; i++)
                    //{
                    //    data[i] = 50;
                    //}
                    //bgwFinsTotalResultSenderSt1.RunWorkerAsync(data);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新控件
        /// </summary>
        public void UpdateControlsEnabled()
        {
        }

        /// <summary>
        /// 还原ToolStrip的背景颜色
        /// </summary>
        public void ResetToolStripBackColor()
        {
            this.toolStripHome1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripHome2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripDatabase.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripChart.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSetting.BackColor = System.Drawing.SystemColors.Control;
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool CheckFileExist(string path)
        {
            if (!File.Exists(path)) return false;
            return true;
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <returns></returns>
        public bool CheckFolderExist(string path)
        {
            // create the folder if the directory doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return true;
        }

        /// <summary>
        /// 读取配置文件 config.ini 中的各项信息
        /// </summary>
        private void ReadSettingConfigFile()
        {
            strConfigFilePath = strBaseDirectory + "Config\\Config.ini";
            ConfigIniFile = new IniFile(strConfigFilePath);
            strMessageSenderConfigFilePath = strBaseDirectory + "MessageSender\\Config\\Config.ini";
            MessageSenderConfigIniFile = new IniFile(strMessageSenderConfigFilePath);

            // 读取SQL Server连接字符串
            mSettingHelper.ConnectionString = ConfigIniFile.IniReadValue("SQL", "ConStr");

            // 读取PLC地址
            mSettingHelper.PLCIP1 = ConfigIniFile.IniReadValue("PLC Parameter", "IP1");
            mSettingHelper.PLCPort1 = Convert.ToInt16(ConfigIniFile.IniReadValue("PLC Parameter", "Port1"));

            // 从另外一个配置文件中读取
            mSettingHelper.PLCIP2 = MessageSenderConfigIniFile.IniReadValue("PLC Parameter", "IP2");
            mSettingHelper.PLCPort2 = Convert.ToInt16(MessageSenderConfigIniFile.IniReadValue("PLC Parameter", "Port2"));
            omronFinsSt1.mPLCIP = mSettingHelper.PLCIP1;
            omronFinsSt1.mPLCPort = mSettingHelper.PLCPort1;
            //omronFinsSt2.mPLCIP = mSettingHelper.PLCIP2;
            //omronFinsSt2.mPLCPort = mSettingHelper.PLCPort2;

            // 设置规格上下限
            mSettingHelper.PosXLowerLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "xLowerLimit"));
            mSettingHelper.PosXUpperLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "xUpperLimit"));
            mSettingHelper.PosYLowerLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "yLowerLimit"));
            mSettingHelper.PosYUpperLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "yUpperLimit"));
            mSettingHelper.PosZLowerLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "zLowerLimit"));
            mSettingHelper.PosZUpperLimit = Convert.ToDouble(ConfigIniFile.IniReadValue("LimitSetting", "zUpperLimit"));

            // 设置输出方向
            mSettingHelper.OutputXDirection = ConfigIniFile.IniReadValue("Output", "xDirection") == "1" ? true : false;
            mSettingHelper.OutputYDirection = ConfigIniFile.IniReadValue("Output", "yDirection") == "1" ? true : false;
            mSettingHelper.OutputZDirection = ConfigIniFile.IniReadValue("Output", "zDirection") == "1" ? true : false;

            // 设置输出规则
            mSettingHelper.OutputXPerfectLowerLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "xPerfectLowerLimit"));
            mSettingHelper.OutputXPerfectUpperLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "xPerfectUpperLimit"));
            mSettingHelper.OutputYPerfectLowerLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "yPerfectLowerLimit"));
            mSettingHelper.OutputYPerfectUpperLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "yPerfectUpperLimit"));
            mSettingHelper.OutputZPerfectLowerLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "zPerfectLowerLimit"));
            mSettingHelper.OutputZPerfectUpperLimit = Convert.ToInt16(ConfigIniFile.IniReadValue("Output", "zPerfectUpperLimit"));

            // 设置输出比例系数
            mSettingHelper.OutputRatio = Convert.ToInt32(ConfigIniFile.IniReadValue("Output", "outputRatio"));

            // 输出统计频次
            mSettingHelper.DataNum = Convert.ToInt32(ConfigIniFile.IniReadValue("Output", "dataNum"));

            _dataChart.XLowerLimit = mSettingHelper.PosXLowerLimit;
            _dataChart.XUpperLimit = mSettingHelper.PosXUpperLimit;
            _dataChart.YLowerLimit = mSettingHelper.PosYLowerLimit;
            _dataChart.YUpperLimit = mSettingHelper.PosYUpperLimit;
            _dataChart.ZLowerLimit = mSettingHelper.PosZLowerLimit;
            _dataChart.ZUpperLimit = mSettingHelper.PosZUpperLimit;
        }
        #endregion

        #region Send message to other software throw windows message
        //WM_COPYDATA消息所要求的数据结构
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        public const int WM_COPYDATA = 0x004A;

        //通过窗口的标题来查找窗口的句柄 
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        //在DLL库中的发送消息函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage
            (
            int hWnd,                         // 目标窗口的句柄  
            int Msg,                          // 在这里是WM_COPYDATA
            int wParam,                       // 第一个消息参数
            ref CopyDataStruct lParam        // 第二个消息参数
           );

        private void SendMsgToSt2OmronPLC(short[] data)
        {
            StringBuilder msg = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                msg.Append(data[i]);
                msg.Append(";");
            }

            //将文本框中的值， 发送给接收端           
            string strMsg = msg.ToString();
            CopyDataStruct cds;
            cds.dwData = (IntPtr)1; //这里可以传入一些自定义的数据，但只能是4字节整数      
            cds.lpData = strMsg;    //消息字符串
            cds.cbData = System.Text.Encoding.Default.GetBytes(strMsg).Length + 1;  //注意，这里的长度是按字节来算的
            SendMessage(FindWindow(null, "Message Sender"), WM_COPYDATA, 0, ref cds);      // 这里要修改成接收窗口的标题“接收端”
        }
        #endregion

        #region Backgroundworker to retrive data from SQL Server Database
        private void InitSQLServerConnection()
        {
            try
            {
                //连接数据库
                con = SqlHelper.GetConnection(mSettingHelper.ConnectionString);
                //查询
                DataSet ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St1_PinData order by Id desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataIndex1 = (int)ds.Tables[0].Rows[0][0];
                }
                else
                {
                    dataIndex1 = 0;
                }
                ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St2_PinData order by Id desc");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataIndex2 = (int)ds.Tables[0].Rows[0][0];
                }
                else
                {
                    dataIndex2 = 0;
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 初始化TwinCat重新连接的后台线程
        /// </summary>
        private void InitRetriveDatabaseDataBackWorker()
        {
            bgwRetriveDatabaseData = new BackgroundWorker();
            bgwRetriveDatabaseData.DoWork += BgW_RetriveDatabaseData_DoWork;
            bgwRetriveDatabaseData.ProgressChanged += BgwRetriveDatabaseData_ProgressChanged;
            bgwRetriveDatabaseData.WorkerReportsProgress = true;
            bgwRetriveDatabaseData.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// TwinCat断开连接后，尝试重新连接，时间间隔为10s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgW_RetriveDatabaseData_DoWork(object sender, DoWorkEventArgs e)
        {
            DataSet ds;
            do
            {
                int currentDataIndex1, currentDataIndex2;
                try
                {
                    //查询St1表格
                    ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St1_PinData order by Id desc");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currentDataIndex1 = (int)ds.Tables[0].Rows[0][0];
                    }
                    else
                    {
                        currentDataIndex1 = 0;
                    }

                    if (currentDataIndex1 > dataIndex1)
                    {
                        dataIndex1 = currentDataIndex1;
                        string totalResult = ds.Tables[0].Rows[0][2].ToString();
                        // 如果结果为OK或NG时数据才有效，如果Result为Error不更新数据和界面
                        if (totalResult.ToLower().Contains("ok") || totalResult.ToLower().Contains("ng"))
                        {
                            allPins1 = new AllPinDataXYZ();
                            for (int i = 0; i < PINNUM; i++)
                            {
                                // 如果Pin针数据为NA，表示该Pin针不存在
                                if (ds.Tables[0].Rows[0][3 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins1.PosXOrigData[i] = null;
                                }
                                else
                                {
                                    allPins1.PosXOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][3 + i * 3].ToString());
                                }
                                if (ds.Tables[0].Rows[0][4 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins1.PosYOrigData[i] = null;
                                }
                                else
                                {
                                    allPins1.PosYOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][4 + i * 3].ToString());
                                }
                                if (ds.Tables[0].Rows[0][5 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins1.PosZOrigData[i] = null;
                                }
                                else
                                {
                                    allPins1.PosZOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][5 + i * 3].ToString());
                                }
                            }
                            pinOrigDataSt1.Add(allPins1);
                            totalCount1++;
                            bgwRetriveDatabaseData.ReportProgress(20);
                            if (totalCount1 >= mSettingHelper.DataNum)
                            {
                                // 收集满30个数据后，进行数据处理，并将结果反馈给PLC
                                outputData1 = CalculateOutputData(pinOrigDataSt1);
                                pinOrigDataSt1.Clear();
                                totalCount1 = 0;
                                bgwRetriveDatabaseData.ReportProgress(40);
                                for (int i = 0; i < PINNUM *3; i++)
                                {
                                    currentOutputData1[i] += outputData1[i];
                                }
                                // 保存数据到配置文件中，作为软件下次启动的初始值
                                SavePLCDataToConfigFile(1, currentOutputData1);
                                // 后台线程发送Fins数据给1#号机台PLC
                                bgwFinsTotalResultSenderSt1.RunWorkerAsync(currentOutputData1);
                            }
                        }
                    }

                    //查询St2表格
                    ds = SqlHelper.ExecuteDataset(con, CommandType.Text, "select top 1 *  from St2_PinData order by Id desc");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        currentDataIndex2 = (int)ds.Tables[0].Rows[0][0];
                    }
                    else
                    {
                        currentDataIndex2 = 0;
                    }
                    if (currentDataIndex2 > dataIndex2)
                    {
                        dataIndex2 = currentDataIndex2;
                        string totalResult = ds.Tables[0].Rows[0][2].ToString();
                        // 如果结果为OK或NG时数据才有效，如果Result为Error不更新数据和界面
                        if (totalResult.ToLower().Contains("ok") || totalResult.ToLower().Contains("ng"))
                        {

                            allPins2 = new AllPinDataXYZ();
                            for (int i = 0; i < PINNUM; i++)
                            {
                                if (ds.Tables[0].Rows[0][3 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins2.PosXOrigData[i] = null;
                                }
                                else
                                {
                                    allPins2.PosXOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][3 + i * 3].ToString());
                                }
                                if (ds.Tables[0].Rows[0][4 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins2.PosYOrigData[i] = null;
                                }
                                else
                                {
                                    allPins2.PosYOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][4 + i * 3].ToString());
                                }
                                if (ds.Tables[0].Rows[0][5 + i * 3].ToString().ToLower().Contains("na"))
                                {
                                    allPins2.PosZOrigData[i] = null;
                                }
                                else
                                {
                                    allPins2.PosZOrigData[i] = Convert.ToDouble(ds.Tables[0].Rows[0][5 + i * 3].ToString());
                                }

                            }
                            pinOrigDataSt2.Add(allPins2);
                            totalCount2++;
                            bgwRetriveDatabaseData.ReportProgress(60);
                            if (totalCount2 >= mSettingHelper.DataNum)
                            {
                                // 收集满30个数据后，进行数据处理，并将结果反馈给PLC
                                outputData2 = CalculateOutputData(pinOrigDataSt2);
                                pinOrigDataSt2.Clear();
                                totalCount2 = 0;
                                bgwRetriveDatabaseData.ReportProgress(80);
                                for (int i = 0; i < PINNUM * 3; i++)
                                {
                                    currentOutputData2[i] += outputData2[i];
                                }
                                // 保存数据到配置文件中，作为软件下次启动的初始值
                                SavePLCDataToConfigFile(2, currentOutputData2);
                                // 发送消息给后台软件，后台软件通过线程发送Fins数据给2#号机台PLC
                                SendMsgToSt2OmronPLC(currentOutputData2);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
                // 延时1秒钟重新获取新数据
                Thread.Sleep(200);
            } while (!bgwRetriveDatabaseData.CancellationPending);
        }

        /// <summary>
        /// 根据进度更新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgwRetriveDatabaseData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;

            // 工位St1更新实时数据和散点图
            if (progress == 20)
            {
                for (int i = 0; i < PINNUM; i++)
                {
                    // 如果数据为Null，表示该Pin针不存在
                    if (allPins1.PosXOrigData[i] == null || allPins1.PosYOrigData[i] == null || allPins1.PosZOrigData[i] == null)
                    {
                        _homeSt1.UpdateDisplayView(i,
                            false,
                            null,
                            null,
                            null);
                    }
                    else
                    {
                        _homeSt1.UpdateDisplayView(i,
                            true,
                            allPins1.PosXOrigData[i],
                            allPins1.PosYOrigData[i],
                            allPins1.PosZOrigData[i]);
                    }
                }
                _dataChart.AddDataRecordToChartSt1(allPins1);
                _dataChart.UpdateChartSt1();
            }
            // 工位St1更新反馈数据
            else if (progress == 40)
            {
                for (int i = 0; i < PINNUM; i++)
                {
                    _homeSt1.UpdateOutputData(i, outputData1[PINNUM + i], outputData1[PINNUM * 2 + i], outputData1[i]);
                }
            }
            // 工位St2更新实时数据和散点图
            else if (progress == 60)
            {
                for (int i = 0; i < PINNUM; i++)
                {
                    // 如果数据为Null，表示该Pin针不存在
                    if (allPins2.PosXOrigData[i] == null || allPins2.PosYOrigData[i] == null || allPins2.PosZOrigData[i] == null)
                    {
                        _homeSt2.UpdateDisplayView(i,
                            false,
                            null,
                            null,
                            null);
                    }
                    else
                    {
                        _homeSt2.UpdateDisplayView(i,
                            true,
                            allPins2.PosXOrigData[i],
                            allPins2.PosYOrigData[i],
                            allPins2.PosZOrigData[i]);
                    }
                }
                _dataChart.AddDataRecordToChartSt2(allPins2);
                _dataChart.UpdateChartSt2();
            }
            // 工位St2更新反馈数据
            else if (progress == 80)
            {
                for (int i = 0; i < PINNUM; i++)
                {
                    _homeSt2.UpdateOutputData(i, outputData2[PINNUM + i], outputData2[PINNUM * 2 + i], outputData2[i]);
                }
            }
        }

        /// <summary>
        /// 保存要发送给PLC的数据，作为下次软件启动时的初始值
        /// </summary>
        /// <param name="index">PLC编号，1或者2</param>
        /// <param name="data">short型数组，大小为75个</param>
        private void SavePLCDataToConfigFile(int index, short[] data)
        {
            StringBuilder sb = new StringBuilder(200);
            foreach (short item in data)
            {
                sb.Append(item.ToString());
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            ConfigIniFile.IniWriteValue("PLCData", "Data" + index, sb.ToString());
        }
        #endregion

        #region Backgroundworker to send result through Omron Fins
        /// <summary>
        /// 初始化TwinCat重新连接的后台线程
        /// </summary>
        private void InitFinsTotalResultBackWorker()
        {
            bgwFinsTotalResultSenderSt1 = new BackgroundWorker();
            bgwFinsTotalResultSenderSt1.DoWork += BgW_FinsTotalResultSenderSt1_DoWork;
            bgwFinsTotalResultSenderSt2 = new BackgroundWorker();
            bgwFinsTotalResultSenderSt2.DoWork += BgwFinsTotalResultSenderSt2_DoWork;
        }

        /// <summary>
        /// Fins发送数据到第一工站
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BgW_FinsTotalResultSenderSt1_DoWork(object sender, DoWorkEventArgs e)
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
            if (isOmronFinsConnectedSt1)
            {
                omronFinsSt1.mFinsConnStatus = true;
                isOmronFinsConnectedSt1 = true;
                // 发送Fins数据，默认情况下失效模式信息以位的形式保存在D4500起始的地址里面
                omronFinsSt1.FinsSendData(outputData, PINNUM * 3 + 1); 
            }
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
            //if (isOmronFinsConnectedSt2)
            //{
            //    omronFinsSt2.mFinsConnStatus = true;
            //    isOmronFinsConnectedSt2 = true;
            //    // 发送Fins数据，默认情况下失效模式信息以位的形式保存在D4500起始的地址里面
            //    omronFinsSt2.FinsSendData(outputData, PINNUM * 3 + 1);
        }
        #endregion

        #region Calculate the output data according to the algrithm rules
        /// <summary>
        /// 轴数据类型
        /// </summary>
        public enum AxisType
        {
            DataX,
            DataY,
            DataZ
        }

        /// <summary>
        /// 计算出反馈控制输出的数据
        /// </summary>
        /// <param name="allPinData"></param>
        /// <returns></returns>
        private short[] CalculateOutputData(List<AllPinDataXYZ> allPinData)
        {
            short[] outputData = new short[PINNUM * 3];
            short[] outputDataX = new short[PINNUM];
            short[] outputDataY = new short[PINNUM];
            short[] outputDataZ = new short[PINNUM];

            // 默认情况下反馈数据为0
            for (int i = 0; i < PINNUM * 3; i++)
            {
                outputData[i] = 0;
            }

            // X坐标运算
            for (int i = 0; i < PINNUM; i++)
            {
                double?[] data = new double?[allPinData.Count];
                for (int j = 0; j < allPinData.Count; j++)
                {
                    data[j] = allPinData[j].PosXOrigData[i];
                }
                outputDataX[i] = CalculateOutput(data, AxisType.DataX);
            }

            // Y坐标运算
            for (int i = 0; i < PINNUM; i++)
            {
                double?[] data = new double?[allPinData.Count];
                for (int j = 0; j < allPinData.Count; j++)
                {
                    data[j] = allPinData[j].PosYOrigData[i];
                }
                outputDataY[i] = CalculateOutput(data, AxisType.DataY);
            }

            // Z坐标运算
            for (int i = 0; i < PINNUM; i++)
            {
                double?[] data = new double?[allPinData.Count];
                for (int j = 0; j < allPinData.Count; j++)
                {
                    data[j] = allPinData[j].PosZOrigData[i];
                }
                outputDataZ[i] = CalculateOutput(data, AxisType.DataZ);
            }

            // 输出的数据：
            // 地址D4500-D4524保存高度数据
            // 地址D4525-D4549保存X轴数据
            // 地址D4550-D4574保存Y轴数据
            // 需要根据这个顺序排列好输出数据
            for (int i = 0; i < PINNUM; i++)
            {
                outputData[i] = outputDataZ[i];
                outputData[PINNUM + i] = outputDataX[i];
                outputData[PINNUM * 2 + i] = outputDataY[i];
            }
            return outputData;
        }

        /// <summary>
        /// 根据规则计算pin针在某个轴上的反馈数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public short CalculateOutput(double?[] data, AxisType axis)
        {
            short output = 0;
            double[] input = new double[data.Length];

            // 遍历data中的元素，如果有null，表示该pin针不存在，反馈数据为0
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null)
                {
                    return output;
                }
                else if (data[i] > mSettingHelper.PosXLowerLimit && data[i] < mSettingHelper.PosXUpperLimit)
                {
                    input[i] = (double)data[i];
                }
                else
                {
                    // 如果pin针数据超出上下限，直接赋值0
                    input[i] = 0;
                }
            }

            // 计算反馈数据
            List<int> indexListRuleTwo;
            List<int> indexListRuleThree;
            List<int> indexListRuleFour;


            // 如果符合规则2
            if (ValidateRuleTwo(input, out indexListRuleTwo))
            {
                double sum1 = 0; // 符合规则的数据总和
                double sum2 = 0; // 不符合规则的数据总和
                short count1 = 0; // 符合规则的数据计数
                short count2 = 0; // 不符合规则的数据计数
                for (int i = 0; i < input.Length; i++)
                {
                    if (indexListRuleTwo.Contains(i))
                    {
                        count1++;
                        sum1 += input[i];
                    }
                    else
                    {
                        count2++;
                        sum2 += input[i];
                    }
                }
                // 按照一定的权重比例进行综合计算，所得结果放大1000倍输出(+/-0.25放大1000倍最大结果为+/-250)
                output = (short)(sum1 * mSettingHelper.OutputRatio * 0.7 / (double)count1 +
                    sum2 * mSettingHelper.OutputRatio * 0.3 / (double)count2);
            }
            // 如果符合规则3
            else if (ValidateRuleThree(input, out indexListRuleThree))
            {
                double sum1 = 0; // 符合规则的数据总和
                double sum2 = 0; // 不符合规则的数据总和
                short count1 = 0; // 符合规则的数据计数
                short count2 = 0; // 不符合规则的数据计数
                for (int i = 0; i < input.Length; i++)
                {
                    if (indexListRuleThree.Contains(i))
                    {
                        count1++;
                        sum1 += input[i];
                    }
                    else
                    {
                        count2++;
                        sum2 += input[i];
                    }
                }
                // 按照一定的权重比例进行综合计算，所得结果放大1000倍输出(+/-0.25放大1000倍最大结果为+/-250)
                output = (short)(sum1 * mSettingHelper.OutputRatio * 0.7 / (double)count1 +
                    sum2 * mSettingHelper.OutputRatio * 0.3 / (double)count2);
            }
            else if (ValidateRuleFour(input, out indexListRuleFour))
            {
                double sum1 = 0; // 符合规则的数据总和
                double sum2 = 0; // 不符合规则的数据总和
                short count1 = 0; // 符合规则的数据计数
                short count2 = 0; // 不符合规则的数据计数
                for (int i = 0; i < input.Length; i++)
                {
                    if (indexListRuleFour.Contains(i))
                    {
                        count1++;
                        sum1 += input[i];
                    }
                    else
                    {
                        count2++;
                        sum2 += input[i];
                    }
                }
                // 按照一定的权重比例进行综合计算，所得结果放大1000倍输出(+/-0.25放大1000倍最大结果为+/-250)
                output = (short)(sum1 * mSettingHelper.OutputRatio * 0.7 / (double)count1 +
                    sum2 * mSettingHelper.OutputRatio * 0.3 / (double)count2);
            }
            else
            {
                double sum1 = 0; // 符合规则的数据总和
                short count1 = 0; // 符合规则的数据计数
                for (int i = 0; i < input.Length; i++)
                {
                    count1++;
                    sum1 += input[i];
                }

                // 按照一定的权重比例进行综合计算，所得结果放大1000倍输出(+/-0.25放大1000倍最大结果为+/-250)
                output = (short)(sum1 * mSettingHelper.OutputRatio / (double)count1);
            }

            // 输出值进一步判断
            switch (axis)
            {
                case AxisType.DataX:
                    // 如果数据在较好的范围内，直接反馈0，表示不需要反馈
                    if (output >= mSettingHelper.OutputXPerfectLowerLimit && output <= mSettingHelper.OutputXPerfectUpperLimit)
                    {
                        output = 0;
                    }
                    else
                    {
                        // 根据测量数据与反馈数据的正方是否相同，乘以相应的系数
                        output = mSettingHelper.OutputXDirection ? (short)output : (short)(-1 * output);
                    }
                    break;
                case AxisType.DataY:
                    if (output >= mSettingHelper.OutputYPerfectLowerLimit && output <= mSettingHelper.OutputYPerfectUpperLimit)
                    {
                        output = 0;
                    }
                    else
                    {
                        output = mSettingHelper.OutputYDirection ? (short)output : (short)(-1 * output);
                    }
                    break;
                case AxisType.DataZ:
                    if (output >= mSettingHelper.OutputZPerfectLowerLimit && output <= mSettingHelper.OutputZPerfectUpperLimit)
                    {
                        output = 0;
                    }
                    else
                    {
                        output = mSettingHelper.OutputZDirection ? (short)output : (short)(-1 * output);
                    }
                    break;
                default:
                    break;
            }
            return output;
        }

        private short[] CalculateAddedValue(ref short[] value, short[] add)
        {
            short[] addedValue = new short[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                addedValue[i] = (short)(value[i] + add[i]);
            }
            return addedValue;
        }

        #endregion

        #region Update status label: PLC, Camera, TCP, IO board, etc.
        /// <summary>
        /// 初始化状态栏及可见状态
        /// </summary>
        private void InitStatusLabel()
        {
            //StatusLabel_Auto.Text = "";
            //StatusLabel_Auto.BackColor = Color.Transparent;
        }

        // 更新状态栏
        private void UpdateStatusLabel()
        {
            // Fins connection status
            if (isOmronFinsConnectedSt1)
            {
                StatusLabel_SerialPort.BackColor = Color.Lime;
                StatusLabel_SerialPort.Text = "TCP 已连接";
            }
            else
            {
                StatusLabel_SerialPort.BackColor = Color.Red;
                StatusLabel_SerialPort.Text = "TCP 断开";
            }

            // show current login information
            StatusLabel_Login.Text = string.Format("当前登陆用户：{0}", mCurrentAccessLevel.ToString());
        }

        /// <summary>
        /// 根据当前的登陆状态更新控件和界面
        /// </summary>
        private void UpdateAccessLevel()
        {
            this.toolStripSetting.Enabled = mCurrentAccessLevel >= AccessLevel.Supervisor ? true : false;
        }
        #endregion

        #region Calculate Algorithm and Rule
        // 数据点分布区间
        public enum DataZone
        {
            NegativeD,
            NegativeC,
            NegativeB,
            NegativeA,
            PositiveA,
            PositiveB,
            PositiveC,
            PositiveD,
        }

        // 数据点极性
        public enum DataPolarity
        {
            Positive,
            Negative
        }

        private const double LOWERLIMIT = -0.25;
        private const double UPPERLIMIT = 0.25;

        /// <summary>
        /// 判断数据所属的6Sigma区域，ABCD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataZone CheckDataZone(double data)
        {
            DataZone zone = DataZone.PositiveA;
            if (data > UPPERLIMIT)
            {
                zone = DataZone.PositiveD;
            }
            else if (data >= UPPERLIMIT * 2 / 3.0f && data <= UPPERLIMIT)
            {
                zone = DataZone.PositiveC;
            }
            else if (data >= UPPERLIMIT / 3.0f && data < UPPERLIMIT * 2 / 3.0f)
            {
                zone = DataZone.PositiveB;
            }
            else if (data >= 0 && data < UPPERLIMIT / 3.0f)
            {
                zone = DataZone.PositiveA;
            }
            else if (data >= LOWERLIMIT / 3.0f && data < 0)
            {
                zone = DataZone.NegativeA;
            }
            else if (data >= LOWERLIMIT * 2 / 3.0f && data < LOWERLIMIT / 3.0f)
            {
                zone = DataZone.NegativeB;
            }
            else if (data >= LOWERLIMIT && data < LOWERLIMIT * 2 / 3.0f)
            {
                zone = DataZone.NegativeC;
            }
            else if (data < LOWERLIMIT)
            {
                zone = DataZone.NegativeD;
            }
            return zone;
        }

        /// <summary>
        /// 判断数据所属的6Sigma区域，ABCD
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private DataPolarity CheckDataPolarity(double data)
        {
            DataPolarity pole = DataPolarity.Positive;
            if (data >= 0)
            {
                pole = DataPolarity.Positive;
            }
            else if (data < 0)
            {
                pole = DataPolarity.Negative;
            }

            return pole;
        }

        /// <summary>
        /// 规则一：连续三个点出现在区域D或D区以外
        /// </summary>
        /// <returns></returns>
        public bool ValidateRuleOne(double[] data, out List<int> index)
        {
            index = new List<int>();
            try
            {
                if (data.Length < 3)
                {
                    return false;
                }
                for (int i = 0; i < data.Length - 2; i++)
                {
                    DataZone zone = CheckDataZone(data[i]);
                    // 连续三个点出现在PositiveD中或者都出现在NegativeD中
                    if (zone == DataZone.PositiveD)
                    {
                        if (CheckDataZone(data[i + 1]) == DataZone.PositiveD &&
                            CheckDataZone(data[i + 2]) == DataZone.PositiveD)
                        {
                            index.Add(i);
                            index.Add(i + 1);
                            index.Add(i + 2);
                            return true;
                        }
                    }
                    else if (zone == DataZone.NegativeD)
                    {
                        if (CheckDataZone(data[i + 1]) == DataZone.NegativeD &&
                            CheckDataZone(data[i + 2]) == DataZone.NegativeD)
                        {
                            index.Add(i);
                            index.Add(i + 1);
                            index.Add(i + 2);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        /// <summary>
        /// 规则二：连续九个点出现在中心线同一侧
        /// </summary>
        /// <returns></returns>
        public bool ValidateRuleTwo(double[] data, out List<int> index)
        {
            index = new List<int>();
            try
            {
                if (data.Length < 9)
                {
                    return false;
                }
                for (int i = 0; i < data.Length - 8; i++)
                {
                    DataPolarity pole = CheckDataPolarity(data[i]);
                    if (pole == DataPolarity.Positive)
                    {
                        if (CheckDataPolarity(data[i + 1]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 2]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 3]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 4]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 5]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 6]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 7]) == DataPolarity.Positive &&
                            CheckDataPolarity(data[i + 8]) == DataPolarity.Positive)
                        {
                            index.Add(i);
                            index.Add(i + 1);
                            index.Add(i + 2);
                            index.Add(i + 3);
                            index.Add(i + 4);
                            index.Add(i + 5);
                            index.Add(i + 6);
                            index.Add(i + 7);
                            index.Add(i + 8);
                            return true;
                        }
                    }
                    else if (pole == DataPolarity.Negative)
                    {
                        if (CheckDataPolarity(data[i + 1]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 2]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 3]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 4]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 5]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 6]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 7]) == DataPolarity.Negative &&
                            CheckDataPolarity(data[i + 8]) == DataPolarity.Negative)
                        {
                            index.Add(i);
                            index.Add(i + 1);
                            index.Add(i + 2);
                            index.Add(i + 3);
                            index.Add(i + 4);
                            index.Add(i + 5);
                            index.Add(i + 6);
                            index.Add(i + 7);
                            index.Add(i + 8);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        /// <summary>
        /// 规则三：连续六个点递增或递减
        /// </summary>
        /// <returns></returns>
        public bool ValidateRuleThree(double[] data, out List<int> index)
        {
            index = new List<int>();
            try
            {
                if (data.Length < 6)
                {
                    return false;
                }
                for (int i = 0; i < data.Length - 5; i++)
                {
                    // 连续递增
                    if (data[i + 1] >= data[i] &&
                        data[i + 2] >= data[i + 1] &&
                        data[i + 3] >= data[i + 2] &&
                        data[i + 4] >= data[i + 3] &&
                        data[i + 5] >= data[i + 4])
                    {
                        index.Add(i);
                        index.Add(i + 1);
                        index.Add(i + 2);
                        index.Add(i + 3);
                        index.Add(i + 4);
                        index.Add(i + 5);
                        return true;
                    }
                    // 连续递减
                    if (data[i + 1] <= data[i] &&
                        data[i + 2] <= data[i + 1] &&
                        data[i + 3] <= data[i + 2] &&
                        data[i + 4] <= data[i + 3] &&
                        data[i + 5] <= data[i + 4])
                    {
                        index.Add(i);
                        index.Add(i + 1);
                        index.Add(i + 2);
                        index.Add(i + 3);
                        index.Add(i + 4);
                        index.Add(i + 5);
                        return true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        /// <summary>
        /// 规则四：连续八个点出现在A区以外
        /// </summary>
        /// <returns></returns>
        public bool ValidateRuleFour(double[] data, out List<int> index)
        {
            index = new List<int>();
            try
            {
                if (data.Length < 8)
                {
                    return false;
                }
                for (int i = 0; i < data.Length - 7; i++)
                {
                    DataZone zone = CheckDataZone(data[i]);
                    if (zone > DataZone.PositiveA || zone < DataZone.NegativeA)
                    {
                        if ((CheckDataZone(data[i + 1]) > DataZone.PositiveA || CheckDataZone(data[i + 1]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 2]) > DataZone.PositiveA || CheckDataZone(data[i + 2]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 3]) > DataZone.PositiveA || CheckDataZone(data[i + 3]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 4]) > DataZone.PositiveA || CheckDataZone(data[i + 4]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 5]) > DataZone.PositiveA || CheckDataZone(data[i + 5]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 6]) > DataZone.PositiveA || CheckDataZone(data[i + 6]) < DataZone.NegativeA) &&
                            (CheckDataZone(data[i + 7]) > DataZone.PositiveA || CheckDataZone(data[i + 7]) < DataZone.NegativeA))
                        {
                            index.Add(i);
                            index.Add(i + 1);
                            index.Add(i + 2);
                            index.Add(i + 3);
                            index.Add(i + 4);
                            index.Add(i + 5);
                            index.Add(i + 6);
                            index.Add(i + 7);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        #endregion
    }

    #region Internal helper class
    // helper class to store localized name and enum value, for use in combo box display
    internal class AccessLevel_Localized
    {
        public AccessLevel_Localized(AccessLevel v, string t)
        {
            val = v;
            text = t;
        }

        public override string ToString()
        {
            // return the localized name
            return text;
        }

        public AccessLevel val;
        public string text;
    }
    #endregion
}