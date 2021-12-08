using Camera_Capture_demo.GlobalVariable;
using HalconDotNet;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApp1.Models
{
    public class MvClass
    {
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        [XmlAttribute("CameraNo")]
        public int CameraNo { get; set; }
        [XmlElement(ElementName = "CameraId")]
        public string CameraId { get; set; }
        [XmlElement(ElementName = "IpAddress")]
        public string IpAddress { get; set; }
        private static List<MvClass> MvList;
        private static readonly object locker = new object();
        private MyCamera m_MyCamera = null;
        private string strUserID = null;
        static int m_nCameraNum = 8;
        [XmlIgnore]
        public long imageWidth = 0;         // 图像宽
        [XmlIgnore]
        public long imageHeight = 0;        // 图像高
        [XmlIgnore]
        public long minExposureTime = 0;    // 最小曝光时间
        [XmlIgnore]
        public long maxExposureTime = 0;    // 最大曝光时间
        [XmlElement(ElementName = "ExposureTime")]
        public long currentExposureTime = 0;    // 当前曝光时间
        [XmlIgnore]
        public long minGain = 0;            // 最小增益
        [XmlIgnore]
        public long maxGain = 0;            // 最大增益
        [XmlElement(ElementName = "currentGain")]
        public long currentGain = 0;            // 当前增益

        private long grabTime = 0;          // 采集图像时间
        [XmlIgnore]
        public bool isOpen = false; //相机是否打开
        private HObject hMvImage = null;
        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver = IntPtr.Zero;
        private static Object BufForDriverLock = new Object();
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        IntPtr pTemp = IntPtr.Zero;

        //相机委托的定义
        MyCamera.cbOutputExdelegate cbImage;

        private HObject hPylonImage = null;
        private IntPtr latestFrameAddress = IntPtr.Zero;
        private Stopwatch stopWatch = new Stopwatch();
        /// <summary>
        /// 图像处理自定义委托
        /// </summary>
        /// <param name="hImage">halcon图像变量</param>
        public delegate void delegateProcessHImage(HObject hImage);
        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        public event delegateProcessHImage eventProcessImage;
        private void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            
            try
            {
                lock (BufForDriverLock)
                {
                    if (m_BufForDriver == IntPtr.Zero || pFrameInfo.nFrameLen > m_nBufSizeForDriver)
                    {
                        if (m_BufForDriver != IntPtr.Zero)
                        {
                            Marshal.Release(m_BufForDriver);
                            m_BufForDriver = IntPtr.Zero;
                        }

                        m_BufForDriver = Marshal.AllocHGlobal((Int32)pFrameInfo.nFrameLen);
                        if (m_BufForDriver == IntPtr.Zero)
                        {
                            return;
                        }
                        m_nBufSizeForDriver = pFrameInfo.nFrameLen;
                    }

                    m_stFrameInfo = pFrameInfo;
                    pTemp = m_BufForDriver;
                }
                HOperatorSet.GenImage1Extern(out hMvImage, "byte", (HTuple)pFrameInfo.nWidth, (HTuple)pFrameInfo.nHeight, pTemp, IntPtr.Zero);
                // 抛出图像处理事件
                eventProcessImage(hMvImage);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        /******************    实例化相机    ******************/
        public MvClass() { }
        /// <summary>
        /// 根据相机UserID实例化相机
        /// </summary>
        /// <param name="SerialNumber"></param>
        private MvClass(string SerialNumber)
        {
            try
            {
                //相机相关变量
                MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
                strUserID = SerialNumber;
                // 枚举相机列表
                int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
                for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (strUserID == usbInfo.chSerialNumber)
                    {
                        if (null == m_MyCamera)
                        {
                            m_MyCamera = new MyCamera();
                            if (null == m_MyCamera)
                            {
                                return;
                            }
                        }
                        //创建相机
                        int nRet2 = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
                        if (MyCamera.MV_OK != nRet2)
                        {
                            return;
                        }
                        // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
                        m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                        m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                        //注册回调函数
                        m_MyCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)i);
                        //注册回调函数
                        cbImage = new MyCamera.cbOutputExdelegate(ImageCallBack);
                        currentExposureTime = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraId == strUserID).currentExposureTime;
                        currentGain = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraId == strUserID).currentGain;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public static MvClass GetInstance(int cameraNo)
        {
            if (MvList == null)
            {
                MvList = new List<MvClass>();
                for (int i = 0; i < 2; i++)
                {
                    MvList.Add(null);
                }
            }
            if (MvList[cameraNo] == null)
            {
                lock (locker)
                {
                    if (MvList[cameraNo] == null)
                    {
                        string id = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraNo == cameraNo).CameraId;
                        MvList[cameraNo] = new MvClass(id);
                    }
                }
            }
            return MvList[cameraNo];
        }

        //清除图像处理委托事件绑定的方法
        public void Clear_EventProcessImage_Event()
        {
            if (eventProcessImage != null)
            {
                Delegate[] dels = eventProcessImage.GetInvocationList();
                foreach (Delegate d in dels)
                {
                    //得到方法名
                    object delObj = d.GetType().GetProperty("Method").GetValue(d, null);
                    string funcName = (string)delObj.GetType().GetProperty("Name").GetValue(delObj, null);
                    Debug.Print(funcName);
                    if (!funcName.Contains("MotionProcess"))
                        eventProcessImage -= d as delegateProcessHImage;
                }
            }
        }
        /*****************************************************/
        /******************    相机操作     ******************/
        public bool OpenCam()
        {
            int nRet = 0;
            // ch:打开设备 | en:Open device
            if (null == m_MyCamera)
            {
                m_MyCamera = new MyCamera();
                if (null == m_MyCamera)
                {
                    return false;
                }
            }

            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                return false;
            }

          
            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
            //设置曝光和增益
            m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", currentExposureTime);
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }

            m_MyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gain", currentGain);
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }
            return true;
        }
        public void CloseCam() 
        {
            try
            {
                if (m_BufForDriver != IntPtr.Zero)
                {
                    Marshal.Release(m_BufForDriver);
                }

                // ch:关闭设备 | en:Close Device
                m_MyCamera.MV_CC_CloseDevice_NET();
                m_MyCamera.MV_CC_DestroyDevice_NET();
            }
            catch (Exception e)
            {

                ShowException(e);
            }
            
        }

        public bool OneShot() 
        {
            try
            {
                if (m_MyCamera == null)
                {
                    return false;
                }
                int nRet = m_MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK != nRet)
                {
                    ShowErrorMsg("Trigger Software Fail!", nRet);
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                ShowException(e);
                return false;
            }
        }


        private void ShowException(Exception exception)
        {
            System.Windows.MessageBox.Show("Exception caught:\n" + exception.Message, "Error", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
        }

        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            System.Windows.MessageBox.Show(errorMsg, "PROMPT");
        }
    }
}
