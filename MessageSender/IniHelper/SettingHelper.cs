using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender
{
    public class SettingHelper
    {
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

        public static void ReadConfig()
        {

        }

        public static void WriteConfig()
        {

        }
    }
}
