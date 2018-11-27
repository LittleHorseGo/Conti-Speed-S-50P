using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conti_Speed_S_50P
{
    public class SettingHelper
    {
        private double _PosXlowerLimit = -0.25;
        private double _PosXUpperLimit = 0.25;
        private double _PosYLowerLimit = -0.25;
        private double _PosYUpperLimit = 0.25;
        private double _PosZLowerLimit = -0.25;
        private double _PosZUpperLimit = 0.25;
        private short _OutputXPerfectLowerLimit = -50;
        private short _OutputXPerfectUpperLimit = 50;
        private short _OutputYPerfectLowerLimit = -50;
        private short _OutputYPerfectUpperLimit = 50;
        private short _OutputZPerfectLowerLimit = -50;
        private short _OutputZPerfectUpperLimit = 50;
        private bool _OutputXDirection = true; // 反馈数据正方向和测量数据正方向是否相同
        private bool _OutputYDirection = true;
        private bool _OutputZDirection = false;
        private int _OutputRatio = 1000;
        private int _DataNum = 30;

        private string _PLCIP1;
        private short _PLCPort1;
        private string _PLCIP2;
        private short _PLCPort2;
        private string _ConnectionString;

        public string PLCIP1 { get => _PLCIP1; set => _PLCIP1 = value; }
        public short PLCPort1 { get => _PLCPort1; set => _PLCPort1 = value; }
        public string PLCIP2 { get => _PLCIP2; set => _PLCIP2 = value; }
        public short PLCPort2 { get => _PLCPort2; set => _PLCPort2 = value; }
        public string ConnectionString { get => _ConnectionString; set => _ConnectionString = value; }
        public double PosXLowerLimit { get => _PosXlowerLimit; set => _PosXlowerLimit = value; }
        public double PosXUpperLimit { get => _PosXUpperLimit; set => _PosXUpperLimit = value; }
        public double PosYLowerLimit { get => _PosYLowerLimit; set => _PosYLowerLimit = value; }
        public double PosYUpperLimit { get => _PosYUpperLimit; set => _PosYUpperLimit = value; }
        public double PosZLowerLimit { get => _PosZLowerLimit; set => _PosZLowerLimit = value; }
        public double PosZUpperLimit { get => _PosZUpperLimit; set => _PosZUpperLimit = value; }
        public short OutputXPerfectLowerLimit { get => _OutputXPerfectLowerLimit; set => _OutputXPerfectLowerLimit = value; }
        public short OutputXPerfectUpperLimit { get => _OutputXPerfectUpperLimit; set => _OutputXPerfectUpperLimit = value; }
        public short OutputYPerfectLowerLimit { get => _OutputYPerfectLowerLimit; set => _OutputYPerfectLowerLimit = value; }
        public short OutputYPerfectUpperLimit { get => _OutputYPerfectUpperLimit; set => _OutputYPerfectUpperLimit = value; }
        public short OutputZPerfectLowerLimit { get => _OutputZPerfectLowerLimit; set => _OutputZPerfectLowerLimit = value; }
        public short OutputZPerfectUpperLimit { get => _OutputZPerfectUpperLimit; set => _OutputZPerfectUpperLimit = value; }
        public bool OutputXDirection { get => _OutputXDirection; set => _OutputXDirection = value; }
        public bool OutputYDirection { get => _OutputYDirection; set => _OutputYDirection = value; }
        public bool OutputZDirection { get => _OutputZDirection; set => _OutputZDirection = value; }
        public int OutputRatio { get => _OutputRatio; set => _OutputRatio = value; }
        public int DataNum { get => _DataNum; set => _DataNum = value; }

        public static void ReadConfig()
        {

        }

        public static void WriteConfig()
        {

        }
    }
}
