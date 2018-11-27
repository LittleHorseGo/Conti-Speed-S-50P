using System;
using System.Windows.Forms;
using System.Resources;
using System.IO;

namespace Conti_Speed_S_50P
{
    class Utility
    {
        string mSelectedProduct = "";
        double Num1 = 0;
        string PCIName = "";
        private void writeToCSV()
        {
            try
            {
                string filename = FormMain.strBaseDirectory + "Saved Data//" + mSelectedProduct;
                writedata(filename, Num1.ToString() + "," + PCIName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void writedata(string filename, string content)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filename, true, System.Text.Encoding.GetEncoding("GB2312"));
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool TypeIsNumeric(Type t)
        {
            if (t == null)
                return false;

            if (t == typeof(double) ||
              t == typeof(long) ||
              t == typeof(sbyte) || t == typeof(byte) ||
              t == typeof(short) || t == typeof(ushort) ||
              t == typeof(int) || t == typeof(uint) ||
              t == typeof(ulong))
                return true;

            return false;
        }

        public static string GetThisExecutableDirectory()
        {
            string loc = Application.ExecutablePath;
            loc = System.IO.Path.GetDirectoryName(loc) + "\\";
            return loc;
        }

        public static bool AccessAllowed(string stringLevelRequired, AccessLevel currentLogin)
        {
            // return true if the currentLogin is equal to or greater than the given access
            // level (expressed as a string)
            AccessLevel needed = AccessLevel.Administrator;

            try
            {
                object obj = Enum.Parse(typeof(AccessLevel), stringLevelRequired, true);
                needed = (AccessLevel)obj;
            }
            catch (ArgumentException)
            {
            }

            return currentLogin >= needed;
        }

        /// <summary>
        /// Take a filename (generally a relative path) and determine the full path to the file to
        /// use.  First the directory containing the current .vpp file is checked for the given filename,
        /// then the directory containing this code's assembly is checked.
        /// </summary>
        public static string ResolveAssociatedFilename(string vppfname, string fname)
        {
            // check for the given file in the same directory as the developer vpp file path
            string trydev = System.IO.Path.GetDirectoryName(vppfname) + "\\" + fname;
            if (System.IO.File.Exists(trydev))
            {
                fname = trydev;
            }
            else
            {
                // otherwise use same directory as this executable
                fname = GetThisExecutableDirectory() + fname;
            }

            return fname;
        }


    }

    public class ResourceUtility
    {
        // helper class to wrap string resources for this application

        private static ResourceManager mResources;

        static ResourceUtility()
        {
            mResources = new ResourceManager("Conti_Speed_S_50P.strings",
              System.Reflection.Assembly.GetExecutingAssembly());
        }

        public static string GetString(string resname)
        {
            string str = mResources.GetString(resname);
            if (str == null)
                str = "ERROR(" + resname + ")";
            return str;
        }

        public static string FormatString(string resname, string arg0)
        {
            try
            {
                return string.Format(GetString(resname), arg0);
            }
            catch (Exception)
            {
            }

            return "ERROR(" + resname + ")";
        }

        public static string FormatString(string resname, string arg0, string arg1)
        {
            try
            {
                return string.Format(GetString(resname), arg0, arg1);
            }
            catch (Exception)
            {
            }

            return "ERROR(" + resname + ")";
        }

    }
}
