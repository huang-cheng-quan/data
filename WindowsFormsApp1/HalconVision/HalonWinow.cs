using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.HalconVision
{
    class HalonWinow
    {
        HWindow m_hwWin1 = new HWindow();
        HImage image = new HImage();

        public HWindow inital_halconWin(HWindow HalconWindow)
        {
            m_hwWin1 = HalconWindow;//将Halcon窗口对象与控件进行关联
            image = new HImage();//实例化图像变量
            image.ReadImage("Normal.jpg");//读取图像
            int width = 0, height = 0;//创建变量用于接收图像尺寸
            image.GetImageSize(out width, out height);//获取图像尺寸
            m_hwWin1.SetPart(0, 0, height - 1, width - 1);//设置Halcon控件中图像的显示尺寸
            return m_hwWin1;
        }
    }
}
