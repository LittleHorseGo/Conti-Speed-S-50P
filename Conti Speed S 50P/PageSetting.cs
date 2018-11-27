using System;
using System.Windows.Forms;
using Automation.BDaq;
using System.Globalization;

namespace Conti_Speed_S_50P
{
    public partial class PageSetting : UserControl
    {
        #region private variable
        private InstantDoCtrl instantDoCtrl1 = new InstantDoCtrl();
        private InstantDiCtrl instantDiCtrl1 = new InstantDiCtrl();
        private SettingHelper settingHelper;
        private string strConfigFilePath;
        private IniFile configIniFile;

        //input
        private Label[] m_portNumStaticDI;
        private Label[] m_portHexStaticDI;
        private PictureBox[,] m_pictrueBoxStaticDI;
        private const int m_startPort = 0;
        private const int m_portCountShow = 2;

        //output
        private Label[] m_portNum;
        private Label[] m_portHex;
        private PictureBox[,] m_pictrueBox;

        public InstantDoCtrl InstantDoCtrl1 { get => instantDoCtrl1; set => instantDoCtrl1 = value; }
        public InstantDiCtrl InstantDiCtrl1 { get => instantDiCtrl1; set => instantDiCtrl1 = value; }
        #endregion

        public PageSetting()
        {
            InitializeComponent();
        }

        private void PageSetting_Load(object sender, EventArgs e)
        {

        }

        public void SetSettingHelper(SettingHelper helper)
        {
            this.settingHelper = helper;
        }
    }
}
