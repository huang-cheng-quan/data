using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Camera_Capture_demo.Helpers
{
    public class IniFileHelper
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        #endregion

        #region 读Ini文件

        //public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        //{

        //    if (File.Exists(iniFilePath))
        //    {
        //        StringBuilder temp = new StringBuilder(1024);
        //        GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
        //        return temp.ToString();
        //    }
        //    else
        //    {
        //        return String.Empty;
        //    }
        //}
        public static T ReadIniData<T>( string Key, string Section = "Parameters", string NoText = "",string iniFilePath = "default" )
        {
            if (iniFilePath.Equals("default"))
            {
                iniFilePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini";
            }
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                try
                {
                    if (typeof(T).Equals(typeof(string)))
                    {
                        return (T)(object)temp.ToString();
                    }
                    if (typeof(T).Equals(typeof(int)))
                    {
                        if (String.IsNullOrEmpty(temp.ToString()))
                        {
                            return default(T);
                        }
                        return (T)(object)Convert.ToInt32(temp.ToString());
                    }
                    if (typeof(T).Equals(typeof(bool)))
                    {
                        return (T)(object)(temp.ToString().ToLower() == "true" ? true : false) ;
                    }
                    return default(T);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                return default(T);
            }
        }



        #endregion

        #region 写Ini文件

        public static bool WriteIniData<T>(string Key, T Value, string Section = "Parameters",string iniFilePath= "default")
        {
            if (iniFilePath.Equals("default"))
            {
                iniFilePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "config.ini";
            }
            if (!File.Exists(iniFilePath))
            {
                using (FileStream fs = new FileStream(iniFilePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Close();
                }
            }
            if (File.Exists(iniFilePath) && Value != null)
            {
                string StrValue = Value.ToString();
                long OpStation = WritePrivateProfileString(Section, Key, StrValue, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
