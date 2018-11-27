using Automation.BDaq;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Conti_Speed_S_50P
{
    public class AdvantechIOHelper
    {
        private IniFile IOSettingIniFile;
        private InstantDoCtrl instantDoCtrl1;
        private InstantDiCtrl instantDiCtrl1;
        private int _camNum = 2;
        private int _deviceNum = 0;
        private bool _isIODeviceConnected;
        private byte _failureCode = 0;
        private TriggerEdge _trigEdge = TriggerEdge.Rising;
        private bool[] _isTriggerSignalHigh;
        private bool _isChangeoverSignalHigh;

        #region PCI input / output port and bit num definition
        // General input
        private int _ChangeoverEnable_PortNum = 0;
        private int _ChangeoverBit1_Portnum = 0;
        private int _ChangeoverBit2_Portnum = 0;
        private int _ChangeoverBit3_Portnum = 0;
        private int _ChangeoverBit4_Portnum = 0;

        private int _ChangeoverEnable_Bitnum = 0;
        private int _ChangeoverBit1_Bitnum = 0;
        private int _ChangeoverBit2_Bitnum = 0;
        private int _ChangeoverBit3_Bitnum = 0;
        private int _ChangeoverBit4_Bitnum = 0;

        // General output
        private int _CCDOnline_PortNum = 0;
        private int _CCDOnline_BitNum = 0;

        private int _FailureModeBit1_PortNum = 0;
        private int _FailureModeBit2_PortNum = 0;
        private int _FailureModeBit3_PortNum = 0;
        private int _FailureModeBit4_PortNum = 0;

        private int _FailureModeBit1_Bitnum = 0;
        private int _FailureModeBit2_Bitnum = 0;
        private int _FailureModeBit3_Bitnum = 0;
        private int _FailureModeBit4_Bitnum = 0;

        // Input and output for single camera
        public SingleCameraIO[] mSingleCameraIO;

        #endregion

        public int DeviceNum { get => _deviceNum; set => _deviceNum = value; }
        public bool IsIODeviceConnected { get => _isIODeviceConnected; set => _isIODeviceConnected = value; }
        public byte FailureCode { get => _failureCode; set => _failureCode = value; }
        public int CCDOnline_PortNum { get => _CCDOnline_PortNum; set => _CCDOnline_PortNum = value; }
        public int ChangeoverEnable_PortNum { get => _ChangeoverEnable_PortNum; set => _ChangeoverEnable_PortNum = value; }
        public int ChangeoverBit1_Portnum { get => _ChangeoverBit1_Portnum; set => _ChangeoverBit1_Portnum = value; }
        public int ChangeoverBit2_Portnum { get => _ChangeoverBit2_Portnum; set => _ChangeoverBit2_Portnum = value; }
        public int ChangeoverBit3_Portnum { get => _ChangeoverBit3_Portnum; set => _ChangeoverBit3_Portnum = value; }
        public int ChangeoverBit4_Portnum { get => _ChangeoverBit4_Portnum; set => _ChangeoverBit4_Portnum = value; }
        public int CCDOnline_BitNum { get => _CCDOnline_BitNum; set => _CCDOnline_BitNum = value; }
        public int ChangeoverEnable_Bitnum { get => _ChangeoverEnable_Bitnum; set => _ChangeoverEnable_Bitnum = value; }
        public int ChangeoverBit1_Bitnum { get => _ChangeoverBit1_Bitnum; set => _ChangeoverBit1_Bitnum = value; }
        public int ChangeoverBit2_Bitnum { get => _ChangeoverBit2_Bitnum; set => _ChangeoverBit2_Bitnum = value; }
        public int ChangeoverBit3_Bitnum { get => _ChangeoverBit3_Bitnum; set => _ChangeoverBit3_Bitnum = value; }
        public int ChangeoverBit4_Bitnum { get => _ChangeoverBit4_Bitnum; set => _ChangeoverBit4_Bitnum = value; }
        public int FailureModeBit1_PortNum { get => _FailureModeBit1_PortNum; set => _FailureModeBit1_PortNum = value; }
        public int FailureModeBit2_PortNum { get => _FailureModeBit2_PortNum; set => _FailureModeBit2_PortNum = value; }
        public int FailureModeBit3_PortNum { get => _FailureModeBit3_PortNum; set => _FailureModeBit3_PortNum = value; }
        public int FailureModeBit4_PortNum { get => _FailureModeBit4_PortNum; set => _FailureModeBit4_PortNum = value; }
        public int FailureModeBit1_Bitnum { get => _FailureModeBit1_Bitnum; set => _FailureModeBit1_Bitnum = value; }
        public int FailureModeBit2_Bitnum { get => _FailureModeBit2_Bitnum; set => _FailureModeBit2_Bitnum = value; }
        public int FailureModeBit3_Bitnum { get => _FailureModeBit3_Bitnum; set => _FailureModeBit3_Bitnum = value; }
        public int FailureModeBit4_Bitnum { get => _FailureModeBit4_Bitnum; set => _FailureModeBit4_Bitnum = value; }
        public int CamNum { get => _camNum; set => _camNum = value; }
        public bool[] IsTriggerSignalHigh { get => _isTriggerSignalHigh; set => _isTriggerSignalHigh = value; }
        public TriggerEdge TrigEdge { get => _trigEdge; set => _trigEdge = value; }
        public bool IsChangeoverSignalHigh { get => _isChangeoverSignalHigh; set => _isChangeoverSignalHigh = value; }

        public AdvantechIOHelper()
        {

        }

        /// <summary>
        /// 初始化IO定义
        /// </summary>
        /// <param name="strIOSettingIniFile"></param>
        public void InitializeIODefinition(string strIOSettingIniFile)
        {
            IOSettingIniFile = new IniFile(strIOSettingIniFile);
            // 根据相机数量初始化mSingleCameraIO变量
            mSingleCameraIO = new SingleCameraIO[CamNum];
            IsTriggerSignalHigh = new bool[CamNum];
            try
            {
                for (int i = 0; i < CamNum; i++)
                {
                    mSingleCameraIO[i] = new SingleCameraIO();
                    //初始化触发信号变量，如果是上升沿触发，设置初始值为Low，如果是下降沿触发，设置初始值为High
                    if (TrigEdge == TriggerEdge.Rising)
                        IsTriggerSignalHigh[i] = false;
                    else if(TrigEdge == TriggerEdge.Falling)
                        IsTriggerSignalHigh[i] = true;

                    mSingleCameraIO[i].Trigger_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "Trigger" + (i + 1)));
                    mSingleCameraIO[i].Trigger_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "Trigger" + (i + 1)));
                    mSingleCameraIO[i].Ready_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "CCDReady" + (i + 1)));
                    mSingleCameraIO[i].Ready_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "CCDReady" + (i + 1)));
                    mSingleCameraIO[i].InspectCompleted_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "InspectCompleted" + (i + 1)));
                    mSingleCameraIO[i].InspectCompleted_BitNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "InspectCompleted" + (i + 1)));
                    mSingleCameraIO[i].LightSource_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "LightSource" + (i + 1)));
                    mSingleCameraIO[i].LightSource_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "LightSource" + (i + 1)));
                    mSingleCameraIO[i].OK_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "OK" + (i + 1)));
                    mSingleCameraIO[i].OK_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "OK" + (i + 1)));
                    mSingleCameraIO[i].NG_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "NG" + (i + 1)));
                    mSingleCameraIO[i].NG_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "NG" + (i + 1)));
                }

                CCDOnline_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "CCDOnline"));
                CCDOnline_BitNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "CCDOnline"));

                ChangeoverEnable_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "ChangeoverEnable"));
                ChangeoverEnable_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "ChangeoverEnable"));

                ChangeoverBit1_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "ChangeoverBit1"));
                ChangeoverBit1_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "ChangeoverBit1"));
                ChangeoverBit2_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "ChangeoverBit2"));
                ChangeoverBit2_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "ChangeoverBit2"));
                ChangeoverBit3_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "ChangeoverBit3"));
                ChangeoverBit3_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "ChangeoverBit3"));
                ChangeoverBit4_Portnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "ChangeoverBit4"));
                ChangeoverBit4_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "ChangeoverBit4"));

                FailureModeBit1_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "FailureModeBit1"));
                FailureModeBit1_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "FailureModeBit1"));
                FailureModeBit2_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "FailureModeBit2"));
                FailureModeBit2_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "FailureModeBit2"));
                FailureModeBit3_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "FailureModeBit3"));
                FailureModeBit3_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "FailureModeBit3"));
                FailureModeBit4_PortNum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Port", "FailureModeBit4"));
                FailureModeBit4_Bitnum = Convert.ToInt16(IOSettingIniFile.IniReadValue("IO Bit", "FailureModeBit4"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Read config file fails", "Fails:" + ex);
            }
        }

        public bool InitializeIODevice()
        {
            instantDiCtrl1.SelectedDevice = new DeviceInformation(DeviceNum);
            instantDoCtrl1.SelectedDevice = new DeviceInformation(DeviceNum);

            //The default device of project is demo device, users can choose other devices according to their needs. 
            if (!instantDiCtrl1.Initialized)
            {
                MessageBox.Show("No device be selected or device open failed!", "StaticDI");
                return false;
            }
            return true;
        }

        public void CloseIO()
        {
            try
            {
                instantDoCtrl1.Write(0, 0);
                instantDoCtrl1.Write(1, 0);
                instantDoCtrl1.Dispose();
                instantDiCtrl1.Dispose();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        #region PCI board communicate with PLC and light source controller

        /// <summary>
        /// 检查Trigger信号
        /// </summary>
        /// <returns></returns>
        public bool[] CaptureTriggerSignal()
        {
            bool[] triggerData = new bool[CamNum];
            for (int i = 0; i < CamNum; i++)
            {
                byte data;
                triggerData[i] = false;
                instantDiCtrl1.ReadBit(mSingleCameraIO[i].Trigger_Portnum, mSingleCameraIO[i].Trigger_Bitnum, out data);
                switch (TrigEdge)
                {
                    case TriggerEdge.Rising:
                        if (!IsTriggerSignalHigh[i] && data == 1)
                            triggerData[i] = true;
                        break;
                    case TriggerEdge.Falling:
                        if (IsTriggerSignalHigh[i] && data == 0)
                            triggerData[i] = true;
                        break;
                    default:
                        break;
                }
            }
            return triggerData;
        }

        /// <summary>
        /// 检查changeover enable信号
        /// </summary>
        /// <returns></returns>
        public bool CaptureChangeoverSignal()
        {
            byte data;
            bool changeoverData = false;
            instantDiCtrl1.ReadBit(ChangeoverEnable_PortNum, ChangeoverEnable_Bitnum, out data);
            switch (TrigEdge)
            {
                case TriggerEdge.Rising:
                    if (!IsChangeoverSignalHigh && data == 1)
                        changeoverData = true;
                    break;
                case TriggerEdge.Falling:
                    if (IsChangeoverSignalHigh && data == 0)
                        changeoverData = true;
                    break;
                default:
                    break;
            }
            return changeoverData;
        }

        public int GetPartNoSignal()
        {
            int partNoIndex = 0;
            // the changeover signal is a four-bit signal from PLC
            // low bit is num1, and high bit is num4
            // for example, if the PLC send "1", then it is "0" product.
            byte num1 = 1;
            byte num2 = 1;
            byte num3 = 1;
            byte num4 = 1;
            byte product_num = 0x00;
            // read the PCI board output
            instantDiCtrl1.ReadBit(ChangeoverBit1_Portnum, ChangeoverBit1_Bitnum, out num1);
            instantDiCtrl1.ReadBit(ChangeoverBit2_Portnum, ChangeoverBit2_Bitnum, out num2);
            instantDiCtrl1.ReadBit(ChangeoverBit3_Portnum, ChangeoverBit3_Bitnum, out num3);
            instantDiCtrl1.ReadBit(ChangeoverBit4_Portnum, ChangeoverBit4_Bitnum, out num4);
            if (num1 == 0) product_num |= 0x01;
            if (num2 == 0) product_num |= 0x01 << 1;
            if (num3 == 0) product_num |= 0x01 << 2;
            if (num4 == 0) product_num |= 0x01 << 3;
            //      PLC product no.    ---------------     Recipe no.
            //          1              ---------------         0
            //          2              ---------------         1
            //    (1 based index)        and so on...    (zero based index)
            partNoIndex = product_num - 1;
            return partNoIndex;
        }
        /// <summary>
        /// turn on light source
        /// light index is from 1 to 4
        /// </summary>
        /// <param name="index"></param>
        public void TurnOnLight(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].LightSource_Portnum, 
                    mSingleCameraIO[index].LightSource_Bitnum, 1);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// turn off light source,
        /// light index is from 1 to 4
        /// </summary>
        /// <param name="index"></param>
        public void TurnOffLight(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].LightSource_Portnum,
                    mSingleCameraIO[index].LightSource_Bitnum, 0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// Send CCD onlie/offline signal
        /// </summary>
        public void SendCCDReadySingal(int index, bool isReady)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].Ready_Portnum,
                    mSingleCameraIO[index].Ready_Bitnum, isReady ? (byte)1 : (byte)0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        public void SetInspectCompletedSignalHigh(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].InspectCompleted_PortNum,
                    mSingleCameraIO[index].InspectCompleted_BitNum, 1);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        public void SetInspectCompletedSignalLow(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].InspectCompleted_PortNum,
                    mSingleCameraIO[index].InspectCompleted_BitNum, 0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// send ok signal high to PLC
        /// </summary>
        public void SetOKSignalHigh(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].OK_Portnum,
                    mSingleCameraIO[index].OK_Bitnum, 1);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// send ok signal low to PLC
        /// </summary>
        public void SetOKSignalLow(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].OK_Portnum,
                    mSingleCameraIO[index].OK_Bitnum, 0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// send NG signal high to PLC
        /// </summary>
        public void SetNGSignalHigh(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].NG_Portnum, 
                    mSingleCameraIO[index].NG_Bitnum, 1);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// send NG signal high to PLC
        /// </summary>
        public void SetNGSignalLow(int index)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(mSingleCameraIO[index].NG_Portnum,
                    mSingleCameraIO[index].NG_Bitnum, 0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        public void SendFailureCodeSignal(byte failureCode)
        {
            byte bit1, bit2, bit3, bit4;
            bit1 = failureCode &= 0x01;
            bit2 = (byte)((failureCode &= 0x02) >> 1);
            bit3 = (byte)((failureCode &= 0x04) >> 2);
            bit4 = (byte)((failureCode &= 0x08) >> 3);
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(FailureModeBit1_PortNum,
                    FailureModeBit1_Bitnum, bit1);
                err = instantDoCtrl1.WriteBit(FailureModeBit2_PortNum,
                    FailureModeBit2_Bitnum, bit2);
                err = instantDoCtrl1.WriteBit(FailureModeBit3_PortNum,
                    FailureModeBit3_Bitnum, bit3);
                err = instantDoCtrl1.WriteBit(FailureModeBit4_PortNum,
                    FailureModeBit4_Bitnum, bit4);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }

        /// <summary>
        /// send to PLC: vision system is ready !
        /// </summary>
        public void SendCCDOnlineSignal(bool isOnline)
        {
            ErrorCode err = ErrorCode.Success;
            try
            {
                err = instantDoCtrl1.WriteBit(CCDOnline_PortNum, CCDOnline_BitNum, isOnline ? (byte)1 : (byte)0);
                if (err != ErrorCode.Success)
                {
                    HandleError(err);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + err.ToString());
            }
        }
        #endregion

        /// <summary>
        /// message box to show the error if there is one
        /// </summary>
        /// <param name="err"></param>
        private void HandleError(ErrorCode err)
        {
            if (err != ErrorCode.Success)
            {
                MessageBox.Show("Sorry ! Some errors happened, the error code is: " + err.ToString(), "Static DI");
            }
        }
    }
}