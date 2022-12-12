using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsFormsApp1.InferData.InferServerData;

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
        string FilePath = @"E:\";
        public void MoveFile(SampleResultData2_NVT sampleInfo, string fileOkDirect, string fileNgDirect, string fileDirect1)
        {
            string current_day = DateTime.Now.ToString("MM-dd");//读取当前时间的月份和日期，如:06-16
            string current_month = DateTime.Now.ToString("MM"); //读取当前时间的月份，如:06
            string current_year = DateTime.Now.Year.ToString(); //读取当前时间的年份，如:2022
            string year_Path = FilePath + "\\" + current_year;
            if (!Directory.Exists(year_Path))   //创建年文件夹
            {
                Directory.CreateDirectory(year_Path);
            }
            string month_Path = year_Path + "\\" + current_month;
            if (!Directory.Exists(month_Path))//创建月文件夹
            {
                Directory.CreateDirectory(month_Path);
            }
            string day_Path = month_Path + "\\" + current_day;
            if (!Directory.Exists(day_Path))    //创建日文件夹
            {
                Directory.CreateDirectory(day_Path);
            }
          string  TodayPath = day_Path;
            if (sampleInfo.data.code != "OK")
            {
               
                Directory.CreateDirectory(fileNgDirect + sampleInfo.data.sample_id);
                string[] str = Directory.GetFiles(fileDirect1);
                foreach (var file in str)
                {
                    string s = file.Remove(0, 6);
                    if (file.Contains(sampleInfo.data.sample_id))
                    {
                       
                       
                        File.Move(file, fileNgDirect + sampleInfo.data.sample_id + s);

                    }
                }
            }
            else
            {
                Directory.CreateDirectory(fileOkDirect + sampleInfo.data.sample_id);
                string[] str = Directory.GetFiles(fileDirect1);
                foreach (var file in str)
                {
                    string s = file.Remove(0, 6);
                    if (file.Contains(sampleInfo.data.sample_id))
                    {
                        File.Move(file, fileOkDirect + sampleInfo.data.sample_id + s);

                    }
                }
            }


        }
    }
}
