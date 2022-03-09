using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.TimeDelete
{
    public class SetTime
    {
        public void DeleteFile(string fileDirect, int saveDay)
        {
            DateTime nowTime = DateTime.Now;
            string[] files = Directory.GetFiles(fileDirect, "*.jpg", SearchOption.AllDirectories);  //获取该目录下所有 .jpg文件
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                TimeSpan t = nowTime - fileInfo.CreationTime;  //当前时间减去文件创建时间
                int day = t.Days;
                if (day > saveDay)   //保存的时间 ；  单位：天
                {
                    File.Delete(file);  //删除超过时间的文件
                }
            }

        }
    }
}
