using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OmronFins_TCP;

namespace FinsTest
{
    public class OmronFinsHelper
    {
        public EtherNetPLC mOmronFins;
        public bool mFinsConnStatus = false;
        public string mPLCIP;
        public short mPLCPort;
        public short mDMStartAddress = 4500;
        public short mDMDataLength = 16;

        public OmronFinsHelper()
        {

        }

        /// <summary>
        /// 初始化Omron Fins通讯
        /// </summary>
        /// <returns></returns>
        public bool InitializeOmronFins(string ip, short port)
        {
            try
            {
                // mOmronFins.Close();
                mOmronFins = new EtherNetPLC();
                short conn = mOmronFins.Link(ip, port, 1500);
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

        /// <summary>
        /// 测试Fins通讯是否正常
        /// </summary>
        /// <returns></returns>
        public bool TestFinsConnection()
        {
            try
            {
                mOmronFins = new EtherNetPLC();
                short conn = mOmronFins.Link(mPLCIP, mPLCPort, 1500);
                if (conn == 0) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 关闭Fins通讯
        /// </summary>
        public void CloseOmronFins()
        {
            mOmronFins.Close();
        }

        /// <summary>
        /// Fins发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        public void FinsSendData(short[] data, short dataLength)
        {
            try
            {
                short mSendComlet = -1;
                mSendComlet = mOmronFins.WriteWords(PlcMemory.DM, 4500, dataLength, data);
            }
            catch (Exception)
            {
                mFinsConnStatus = false;
                throw;
            }
            if (mOmronFins.FinsConnected == false) mFinsConnStatus = false;
        }

        /// <summary>
        /// Fins发送单个short型数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        public void FinsSendData(short data)
        {
            try
            {
                short mSendComlet = -1;
                mSendComlet = mOmronFins.WriteWord(PlcMemory.DM, 4501, data);
            }
            catch (Exception)
            {
                mFinsConnStatus = false;
                throw;
            }
            if (mOmronFins.FinsConnected == false) mFinsConnStatus = false;
        }

        /// <summary>
        /// Fins发送单个short型数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        public void FinsSendData(short address, short data)
        {
            try
            {
                short mSendComlet = -1;
                mSendComlet = mOmronFins.WriteWord(PlcMemory.DM, address, data);
            }
            catch (Exception)
            {
                mFinsConnStatus = false;
                throw;
            }
            if (mOmronFins.FinsConnected == false) mFinsConnStatus = false;
        }

        /// <summary>
        /// Fins发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLength"></param>
        public void FinsSendPN(short data)
        {
            try
            {
                short mSendComlet = -1;
                mSendComlet = mOmronFins.WriteWord(PlcMemory.DM, mDMStartAddress, data);
            }
            catch (Exception)
            {
                mFinsConnStatus = false;
                throw;
            }
            if (mOmronFins.FinsConnected == false) mFinsConnStatus = false;
        }

        /// <summary>
        /// Fins发送数据
        /// </summary>
        /// <param name="data">Dictionary中key为地址，value为地址对应的数据，都为short类型</param>
        public void FinsSendData(Dictionary<short, short> data)
        {
            try
            {
                short mSendComlet = -1;
                //KeyValuePair<T,K>
                foreach (KeyValuePair<short, short> kv in data)
                {
                    mSendComlet = mOmronFins.WriteWord(PlcMemory.DM, kv.Key, kv.Value);
                }
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
