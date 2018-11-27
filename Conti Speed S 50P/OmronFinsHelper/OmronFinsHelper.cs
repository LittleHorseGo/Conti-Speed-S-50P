using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OmronFins_TCP;

namespace TE_Vision_System
{
    public class OmronFinsHelper
    {
        public EtherNetPLC mOmronFins;
        public bool mFinsConnStatus = false;
        public string mPLCIP;
        public short mPLCPort;

        public OmronFinsHelper()
        {

        }

        /// <summary>
        /// 初始化Omron Fins通讯
        /// </summary>
        /// <returns></returns>
        public bool InitializeOmronFins()
        {
            try
            {
                mOmronFins.Close();
                mOmronFins = new EtherNetPLC();
                short conn = mOmronFins.Link(mPLCIP, mPLCPort, 1500);
                if (conn == 0)
                    mFinsConnStatus = true;
                else
                    mFinsConnStatus = false;
                return mFinsConnStatus;
            }
            catch (Exception)
            {
                // MessageBox.Show("PLC连接失败");
            }
            return false;
        }

        public void CloseOmronFins()
        {
            mOmronFins.Close();
        }

        public void FinsSendData(short data)
        {
            try
            {
                short mSendComlet = -1;
                // mTcPSendData = mTcpDataCollect();
                mSendComlet = mOmronFins.WriteWord(PlcMemory.DM, 4225, data);
                // log file
            }
            catch (Exception)
            {
                mFinsConnStatus = false;
                throw;
            }
            if (mOmronFins.FinsConnected == false) mFinsConnStatus = false;
        }
    }
}
