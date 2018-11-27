using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conti_Speed_S_50P
{
    public class SingleCameraIO
    {
        // IO input
        private int _Trigger_Portnum = 0;
        private int _Trigger_Bitnum = 0;

        // IO output
        private int _LightSource_Portnum = 0;
        private int _LightSource_Bitnum = 0;

        private int _Ready_Portnum = 0;
        private int _Ready_Bitnum = 0;

        private int _InspectCompleted_PortNum = 0;
        private int _InspectCompleted_BitNum = 0;

        private int _OK_Portnum = 0;
        private int _OK_Bitnum = 0;

        private int _NG_Portnum = 0;
        private int _NG_Bitnum = 0;

        public int Trigger_Portnum { get => _Trigger_Portnum; set => _Trigger_Portnum = value; }
        public int Trigger_Bitnum { get => _Trigger_Bitnum; set => _Trigger_Bitnum = value; }
        public int LightSource_Portnum { get => _LightSource_Portnum; set => _LightSource_Portnum = value; }
        public int LightSource_Bitnum { get => _LightSource_Bitnum; set => _LightSource_Bitnum = value; }
        public int Ready_Portnum { get => _Ready_Portnum; set => _Ready_Portnum = value; }
        public int Ready_Bitnum { get => _Ready_Bitnum; set => _Ready_Bitnum = value; }
        public int OK_Portnum { get => _OK_Portnum; set => _OK_Portnum = value; }
        public int OK_Bitnum { get => _OK_Bitnum; set => _OK_Bitnum = value; }
        public int NG_Portnum { get => _NG_Portnum; set => _NG_Portnum = value; }
        public int NG_Bitnum { get => _NG_Bitnum; set => _NG_Bitnum = value; }
        public int InspectCompleted_PortNum { get => _InspectCompleted_PortNum; set => _InspectCompleted_PortNum = value; }
        public int InspectCompleted_BitNum { get => _InspectCompleted_BitNum; set => _InspectCompleted_BitNum = value; }
    }
}
