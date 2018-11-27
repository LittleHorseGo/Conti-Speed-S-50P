using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using System.IO;
using System.Windows.Forms;

namespace TE_Vision_System
{
    public class TwincatHelper
    {
        private int _mCamNum;
        private string _AdsAmsNetID = "5.54.159.254.1.1"; // 默认的ADS NetID
        private int _AdsPortNumber = 851; // 默认的ADS Port Number

        #region Input from TwinCat PLC
        public static string[] VARIABLETRIGGER;
        public static string VARIABLEPLCONLINE = "VisionDefines.OnLine";
        public static string VARIABLEPARTNO = "VisionDefines.PartNo";
        private bool[] triggerFlag; // 触发信号
        private short partNoIndex; // 程序号
        private bool plcOnline; // PLC online 信息
        #endregion

        #region Output to TwinCat PLC
        public static string VARIABLECCDONLINE = "VisionDefines.CCDOnline";
        public static string[] VARIABLECCDREADY;
        public static string[] VARIABLERESULTOK;
        public static string[] VARIABLERESULTNG;
        public static string[] VARIABLEINSPECTCOMPLETED;
        public const string VARIABLEERRORCODE = "VisionDefines.ErrorCode";
        private bool ccdOnline; // CCD是否在线或离线， 在线 = true
        private bool[] ccdReady; // CCD ready信号，表示CCD可以触发，不是处于忙碌状态
        private bool[] resultOK; // CCD结果OK
        private bool[] resultNG; // CCD结果NG
        private bool[] inspectCompleted; // 检测完成
        private short errorCode; // 报警代码，跟PLC对应好，显示在PLC屏幕上
        #endregion

        // TwinCat ADS
        private TcAdsClient adsClient;
        private int[] hConnect;
        private AdsStream dataStream;
        private BinaryReader binRead;

        public string AdsAmsNetID { get => _AdsAmsNetID; set => _AdsAmsNetID = value; }
        public int AdsPortNumber { get => _AdsPortNumber; set => _AdsPortNumber = value; }
        public bool PlcOnline { get => plcOnline; set => plcOnline = value; }
        public bool[] TriggerFlag { get => triggerFlag; set => triggerFlag = value; }
        public short PartNoIndex { get => partNoIndex; set => partNoIndex = value; }
        public bool CcdOnline { get => ccdOnline; set => ccdOnline = value; }
        public bool[] ResultOK { get => resultOK; set => resultOK = value; }
        public bool[] ResultNG { get => resultNG; set => resultNG = value; }
        public bool[] InspectCompleted { get => inspectCompleted; set => inspectCompleted = value; }
        public TcAdsClient AdsClient { get => adsClient; set => adsClient = value; }
        public int[] HConnect { get => hConnect; set => hConnect = value; }
        public AdsStream DataStream { get => dataStream; set => dataStream = value; }
        public BinaryReader BinRead { get => binRead; set => binRead = value; }
        public bool[] CcdReady { get => ccdReady; set => ccdReady = value; }
        public short ErrorCode { get => errorCode; set => errorCode = value; }
        public int CamNum { get => _mCamNum; set => _mCamNum = value; }

        public bool InitTwincatADS()
        {
            dataStream = new AdsStream(CamNum + 3);
            //Encoding wird auf ASCII gesetzt, um Strings lesen zu k鰊nen
            binRead = new BinaryReader(dataStream, System.Text.Encoding.ASCII);
            // Instanz der Klasse TcAdsClient erzeugen
            adsClient = new TcAdsClient();
            hConnect = new int[CamNum + 2];
            VARIABLETRIGGER = new string[CamNum];
            VARIABLECCDREADY = new string[CamNum];
            VARIABLERESULTOK = new string[CamNum];
            VARIABLERESULTNG = new string[CamNum];
            VARIABLEINSPECTCOMPLETED = new string[CamNum];
            TriggerFlag = new bool[CamNum];
            CcdReady = new bool[CamNum]; // CCD ready信号，表示CCD可以触发，不是处于忙碌状态
            ResultOK = new bool[CamNum]; // CCD结果OK
            resultNG = new bool[CamNum]; // CCD结果NG
            InspectCompleted = new bool[CamNum]; // 检测完成

            for (int i = 0; i < CamNum; i++)
            {
                // TwinCat 变量命名为 Trigger1, Trigger2...
                VARIABLETRIGGER[i] = "VisionDefines.Trigger" + (i + 1);
                VARIABLECCDREADY[i] = "VisionDefines.CCDReady" + (i + 1);
                VARIABLERESULTOK[i] = "VisionDefines.ResultOK" + (i + 1);
                VARIABLERESULTNG[i] = "VisionDefines.ResultNG" + (i + 1);
                VARIABLEINSPECTCOMPLETED[i] = "VisionDefines.InspectCompleted" + (i + 1);
            }

            try
            {
                adsClient.Connect(AdsAmsNetID, AdsPortNumber);
                for (int i = 0; i < CamNum; i++)
                {
                    // bool 数组，存储触发信号
                    hConnect[i] = adsClient.AddDeviceNotification(VARIABLETRIGGER[i], dataStream, i, 1,
                        AdsTransMode.OnChange, 20, 0, null);
                }
                // bool 型变量，存储PLC在线信息
                hConnect[CamNum] = adsClient.AddDeviceNotification(VARIABLEPLCONLINE, dataStream, CamNum, 1,
                    AdsTransMode.OnChange, 100, 0, null);
                // short 型变量，存储料号信息
                hConnect[CamNum + 1] = adsClient.AddDeviceNotification(VARIABLEPARTNO, dataStream, CamNum + 1, 2,
                    AdsTransMode.OnChange, 100, 0, null);
            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 关闭TwinCat通讯，断开连接
        /// </summary>
        public void CloseTwinCatConnection()
        {
            try
            {
                bool conn = CheckAdsConnectionStatus();
                if (conn)
                {
                    // 复位CCD online变量
                    WritePLC(VARIABLECCDONLINE, false);
                    for (int i = 0; i < CamNum; i++)
                    {
                        // 复位CCD Ready变量
                        WritePLC(VARIABLECCDREADY[i], false);
                        // 复位OK结果变量
                        WritePLC(VARIABLERESULTOK[i], false);
                        // 复位NG结果变量
                        WritePLC(VARIABLERESULTNG[i], false);
                        // 复位检测完成变量
                        WritePLC(VARIABLEINSPECTCOMPLETED[i], false);
                    }
                }
                adsClient.Disconnect();
                adsClient.Close();
                adsClient.Dispose();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 尝试读取PLC数据，如果没有读到表示连接失败
        /// </summary>
        /// <returns></returns>
        public bool CheckAdsConnectionStatus()
        {
            StateInfo state;
            AdsErrorCode errCode = AdsClient.TryReadState(out state);
            //int readBytes = 0;
            // AdsStream which gets the data
            //AdsStream adsStream = new AdsStream(1);
            //AdsBinaryReader reader = new AdsBinaryReader(adsStream);
            //AdsErrorCode errCode = AdsClient.TryRead(hConnect[CamNum], adsStream, 0, 1, out readBytes);
            if (errCode == AdsErrorCode.NoError) return true;
            else return false;
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        public void ReConnectToTwinCat()
        {
            try
            {
                adsClient.Connect(AdsAmsNetID, AdsPortNumber);
                for (int i = 0; i < CamNum; i++)
                {
                    // bool 数组，存储触发信号
                    hConnect[i] = adsClient.AddDeviceNotification(VARIABLETRIGGER[i], dataStream, i, 1,
                        AdsTransMode.OnChange, 20, 0, null);
                }
                // bool 型变量，存储PLC在线信息
                hConnect[CamNum] = adsClient.AddDeviceNotification(VARIABLEPLCONLINE, dataStream, CamNum, 1,
                    AdsTransMode.OnChange, 100, 0, null);
                // short 型变量，存储料号信息
                hConnect[CamNum + 1] = adsClient.AddDeviceNotification(VARIABLEPARTNO, dataStream, CamNum + 1, 2,
                    AdsTransMode.OnChange, 100, 0, null);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 写bool型变量
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="str"></param>
        public void WritePLC(string variableName, bool flag)
        {
            try
            {
                int varHandle = adsClient.CreateVariableHandle(variableName);
                //length of the stream
                AdsStream adsStream = new AdsStream(1);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(flag);
                adsClient.Write(varHandle, adsStream);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 写字节
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public void WritePLC(string variableName, byte value)
        {
            try
            {
                int varHandle = adsClient.CreateVariableHandle(variableName);
                //length of the stream
                AdsStream adsStream = new AdsStream(1);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(value);
                adsClient.Write(varHandle, adsStream);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 写short型变量
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public void WritePLC(string variableName, short value)
        {
            try
            {
                int varHandle = adsClient.CreateVariableHandle(variableName);
                //length of the stream
                AdsStream adsStream = new AdsStream(2);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(value);
                adsClient.Write(varHandle, adsStream);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 写int型变量
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        public void WritePLC(string variableName, int value)
        {
            try
            {
                int varHandle = adsClient.CreateVariableHandle(variableName);
                //length of the stream
                AdsStream adsStream = new AdsStream(4);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(value);
                adsClient.Write(varHandle, adsStream);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="str"></param>
        public void WritePLC(string variableName, string str)
        {
            try
            {
                int varHandle = adsClient.CreateVariableHandle(variableName);
                //length of the stream = length of string + 1
                AdsStream adsStream = new AdsStream(str.Length + 1);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(str.ToCharArray());
                //add terminating zero
                writer.Write('\0');
                adsClient.Write(varHandle, adsStream);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        /// <summary>
        /// 关闭TwinCat通讯
        /// </summary>
        private void CloseTwincatADS()
        {
            try
            {
                adsClient.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
