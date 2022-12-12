using BatteryFeederDemo;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Dyestripping.Models;
using HalconDotNet;
using HslCommunication;
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
        public MyCamera m_MyCamera = null;
        private string strUserID = null;
        public static bool IsStartReadCode=false;
       
        [XmlIgnore]
        public long imageWidth = 0;         // 图像宽
        [XmlIgnore]
        public long imageHeight = 0;        // 图像高
        [XmlIgnore]
        public long minExposureTime = 0;    // 最小曝光时间
        [XmlIgnore]
        public long maxExposureTime = 0;    // 最大曝光时间
        [XmlElement(ElementName = "ExposureTime")]
        public float currentExposureTime = 0;    // 当前曝光时间
        [XmlIgnore]
        public long minGain = 0;            // 最小增益
        [XmlIgnore]
        public long maxGain = 0;            // 最大增益
        [XmlElement(ElementName = "currentGain")]
        public float currentGain = 0;            // 当前增益
        [XmlIgnore]
        public bool isOpen = false; //相机是否打开
        private HObject hMvImage = null;
        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver = IntPtr.Zero;
        private static Object BufForDriverLock = new Object();
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
        IntPtr pTemp = IntPtr.Zero;


         int[] m_nFrames;      // ch:帧数 | en:Frame SerialNumber
        public static Object[] m_BufFerSaveImageLock;

        private static Dictionary<int, List<int>> m_dictImageIndex1 = new Dictionary<int, List<int>>();

        //相机委托的定义
        MyCamera.cbOutputExdelegate cbImage;
        //内存相关变量
        /*MotionProcess motionProcess;*/
       // private Object m_BufFerSaveImageLock = new Object();

        int CamerNum = 4;
        public static int[] m_nImage_In = new int[MotionProcess.m_nCameraSaveNum];
        private int[] m_nImageConnection_In = new int[MotionProcess.m_nCameraSaveConnectionNum];
        IntPtr[] m_hDisplayHandle = new  IntPtr[MotionProcess.m_nCameraTotalNum];
        List<IntPtr> Linprt = new List<IntPtr>();

        /// <summary>
        /// 图像处理自定义委托
        /// </summary>
        /// <param name="hImage">halcon图像变量</param>
        public delegate void delegateProcessHImage(HObject hImage,int motorNum);
        /// <summary>
        /// 图像处理委托事件
        /// </summary>
        public event delegateProcessHImage eventProcessImage;

 
        public delegate void delegateProcessData(int camNum,MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo,IntPtr Image);
        public static event delegateProcessData eventProcessData;
        public static int[] numFlag=new int[4];

        private void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {

            try
            {
              
                int nCamIndex = (int)pUser;
                // ch:抓取的帧数 | en:Aquired Frame SerialNumber
                ++m_nFrames[nCamIndex];
                
                //定位部分 8--》定位A    6--》定位B
                if (/*nCamIndex == 4 ||*/ nCamIndex == 6 || nCamIndex == 8)
                {

                    HOperatorSet.GenImage1Extern(out hMvImage, "byte", (HTuple)pFrameInfo.nWidth, (HTuple)pFrameInfo.nHeight, pData, IntPtr.Zero);
                   // HOperatorSet.WriteImage(hImage, "jpg", 0, "D:/1.jpg");
                    //抛出图像处理事件
                    eventProcessImage(hMvImage, 0);
                    switch (nCamIndex)
                    {
                        case 4:
                           
                                MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status4, (float)0);
                           
                            
                            break;
                        case 8:
                          
                                MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status8, (float)0);
                           
                          
                            break;
                        case 6:
                           
                                MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status6, (float)0);
                          
                            break;
                        default:
                            break;
                    }

                }
                //本体检测部分
                else if (nCamIndex == 0 || nCamIndex == 1 || nCamIndex == 2 || nCamIndex == 3)

                {
                    if (pFrameInfo.nFrameNum - m_nFrames[nCamIndex]>=1)
                    {
                        MotionProcess.Loseimage = true;
                        MotionProcess.m_bWhileEnd = true;
                        MotionProcess.m_bWhileStatus_SaveConnectionImage = true;
                        MotionProcess.IsSaveQrcode = false;
                        MotionProcess.IsSaveQrcode2 = false;
                        LogHelper.LogError("丢帧报警停机" );
                        switch (nCamIndex)
                        {
                            case 0:
                               
                                LogHelper.LogError("00J26664742丢帧" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
                                break;
                            case 1:
                               
                                LogHelper.LogError("00J27960925丢帧" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
                                break;
                            case 2:
                             
                                LogHelper.LogError("00J27960926丢帧" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
                                break;
                            case 3:
                              
                                LogHelper.LogError("00J26664733丢帧" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
                                break;
                        }
                       
                    }                

                    lock (m_BufFerSaveImageLock[nCamIndex])
                    {
                        MotionProcess.Linprt[nCamIndex].Add(pData);
                        if (numFlag[nCamIndex] >= 10)
                        {
                            numFlag[nCamIndex] = 0;

                        }
                        switch (nCamIndex)
                        {
                            case 0:
                                MotionProcess.ListNumflag1.Add(numFlag[nCamIndex]);
                                break;
                            case 1:
                                MotionProcess.ListNumflag2.Add(numFlag[nCamIndex]);
                                break;
                            case 2:
                                MotionProcess.ListNumflag3.Add(numFlag[nCamIndex]);
                                break;
                            case 3:
                                MotionProcess.ListNumflag4.Add(numFlag[nCamIndex]);
                                break;
                            default:
                                break;
                        }
                        MotionProcess.ListNumflag[nCamIndex].Add(numFlag[nCamIndex]);
                         numFlag[nCamIndex]++;
                       
                        
                       // CopyMemory(MotionProcess.m_pSaveImageBuf_N[nCamIndex, m_nImage_In[nCamIndex]], pData, pFrameInfo.nFrameLen);

                        //switch (nCamIndex)
                        //{
                        //    case 0:
                        //        LogHelper.LogInfo("00J26664742-AddList" + m_nFrames[nCamIndex].ToString());
                        //        break;
                        //    case 1:
                        //        LogHelper.LogInfo("00J27960925-AddList" + m_nFrames[nCamIndex].ToString());
                        //        break;
                        //    case 2:
                        //        LogHelper.LogInfo("00J27960926-AddList" + m_nFrames[nCamIndex].ToString());
                        //        break;
                        //    case 3:
                        //        LogHelper.LogInfo("00J26664733-AddList" + m_nFrames[nCamIndex].ToString());
                        //        break;
                        //}

                    }

                    ++m_nImage_In[nCamIndex];
                    if (m_nImage_In[nCamIndex] >= 10)
                    {
                        m_nImage_In[nCamIndex] = 0;
                    }

                    MotionProcess.m_stSaveParamInfer[nCamIndex].enPixelType = pFrameInfo.enPixelType;
                    MotionProcess.m_stSaveParamInfer[nCamIndex].nDataLen = pFrameInfo.nFrameLen;
                    MotionProcess.m_stSaveParamInfer[nCamIndex].nHeight = pFrameInfo.nHeight;
                    MotionProcess.m_stSaveParamInfer[nCamIndex].nWidth = pFrameInfo.nWidth;

                    MotionProcess.m_stSaveParam_N[nCamIndex].nDataLen = pFrameInfo.nFrameLen;

                    MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
                    stDisplayInfo.nDataLen = pFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = pFrameInfo.nWidth;
                    stDisplayInfo.nHeight = pFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = pFrameInfo.enPixelType;
                    stDisplayInfo.hWnd = m_hDisplayHandle[nCamIndex];
                    stDisplayInfo.pData = pData;
                    if (numFlag[nCamIndex] == 1)
                    {
                        eventProcessData(nCamIndex, stDisplayInfo, pData);// 20221025屏蔽显示测试
                    }

                    // LogHelper.LogInfo("outcallback" + DateTime.Now.ToString("yyyyMMddHHmmssff"));
                    //switch (nCamIndex)
                    //{
                    //    case 0:
                    //        LogHelper.LogInfo("00J26664742-outcallback" + m_nFrames[nCamIndex].ToString());
                    //        break;
                    //    case 1:
                    //        LogHelper.LogInfo("00J27960925-outcallback" + m_nFrames[nCamIndex].ToString());
                    //        break;
                    //    case 2:
                    //        LogHelper.LogInfo("00J27960926-outcallback" + m_nFrames[nCamIndex].ToString());
                    //        break;
                    //    case 3:
                    //        LogHelper.LogInfo("00J26664733-outcallback" + m_nFrames[nCamIndex].ToString());
                    //        break;
                    //}

                }
                //连接器部分
                else if (nCamIndex == 7 || nCamIndex == 5)//5 连接器反面
                {
                    MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
                    stDisplayInfo.nDataLen = pFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = pFrameInfo.nWidth;
                    stDisplayInfo.nHeight = pFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = pFrameInfo.enPixelType;
                    stDisplayInfo.hWnd = m_hDisplayHandle[nCamIndex];
                    stDisplayInfo.pData = pData;
                    //抛出图像显示事件
                    eventProcessData(nCamIndex, stDisplayInfo, pData);

                    if (nCamIndex == 5)
                    {
                        nCamIndex = 1;
                        IsStartReadCode = true;
                    }
                    else
                    {
                        nCamIndex = 2;
                    }
                    

                    MotionProcess.m_stSaveParamConnection[nCamIndex].enPixelType = pFrameInfo.enPixelType;
                    MotionProcess.m_stSaveParamConnection[nCamIndex].nDataLen = pFrameInfo.nFrameLen;
                    MotionProcess.m_stSaveParamConnection[nCamIndex].nHeight = pFrameInfo.nHeight;
                    MotionProcess.m_stSaveParamConnection[nCamIndex].nWidth = pFrameInfo.nWidth;
                   
                    lock (m_BufFerSaveImageLock[nCamIndex])
                    {
                        CopyMemory(MotionProcess.m_pSaveImageBuf_Connection[nCamIndex, m_nImageConnection_In[nCamIndex]], pData, pFrameInfo.nFrameLen);
                        MotionProcess.m_dictImageConnectionIndex[nCamIndex].Add(m_nImageConnection_In[nCamIndex]);
                    }
                    ++m_nImageConnection_In[nCamIndex];
                    if (m_nImageConnection_In[nCamIndex] >= 6)
                    {
                        m_nImageConnection_In[nCamIndex] = 0;
                    }
                }
                //连接器钢片检测
                else if (nCamIndex == 9)
                {
                    /*HOperatorSet.GenImage1Extern(out hMvImage, "byte", (HTuple)pFrameInfo.nWidth, (HTuple)pFrameInfo.nHeight, pData, IntPtr.Zero);
                    //HOperatorSet.WriteImage(hImage, "jpg", 0, "D:/1.jpg");
                    // 抛出图像处理事件
                    OperateResult<float> flag = MotorsClass.omronInstance.ReadFloat(MotorsClass.plc_cam_MotorFlag5);
                    if (flag.Content == 0)
                    {
                        eventProcessImage(hMvImage, 0);
                        MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status8, (float)0);
                    }
                    else
                    {
                        eventProcessImage(hMvImage, 1);
                        MotorsClass.omronInstance.Write(MotorsClass.plc_cam_status6, (float)0);
                    }*/

                    MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
                    stDisplayInfo.nDataLen = pFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = pFrameInfo.nWidth;
                    stDisplayInfo.nHeight = pFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = pFrameInfo.enPixelType;
                    stDisplayInfo.hWnd = m_hDisplayHandle[nCamIndex];
                    stDisplayInfo.pData = pData;
                    //抛出图像显示事件
                   eventProcessData(nCamIndex, stDisplayInfo, pData);
                   // nCamIndex = 0;
                    MotionProcess.m_stSaveParamConnection[0].enPixelType = pFrameInfo.enPixelType;
                    MotionProcess.m_stSaveParamConnection[0].nDataLen = pFrameInfo.nFrameLen;
                    MotionProcess.m_stSaveParamConnection[0].nHeight = pFrameInfo.nHeight;
                    MotionProcess.m_stSaveParamConnection[0].nWidth = pFrameInfo.nWidth;

                    lock (m_BufFerSaveImageLock[0])
                    {
                        CopyMemory(MotionProcess.m_pSaveImageBuf_Connection[0, m_nImageConnection_In[0]], pData, pFrameInfo.nFrameLen);
                        MotionProcess.m_dictImageConnectionIndex[0].Add(m_nImageConnection_In[0]);
                    }
                    ++m_nImageConnection_In[0];
                    if (m_nImageConnection_In[0] >= 6)
                    {
                        m_nImageConnection_In[0] = 0;
                    }
                }

            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());

            }
        }
        /******************    实例化相机    ******************/
        public MvClass() { }
        /// <summary>
        /// 根据相机UserID实例化相机
        /// </summary>
        /// <param name="SerialNumber"></param>
        private MvClass(string SerialNumber,int Number)
        {
            try
            {
               

                m_nFrames = new int[MotionProcess.m_nCameraTotalNum];
              
                m_BufFerSaveImageLock = new object[MotionProcess.m_nCameraTotalNum];
                for(int i = 0; i < MotionProcess.m_nCameraTotalNum; i++)
                {
                    m_BufFerSaveImageLock[i] = new object();
                }


                //相机相关变量
                MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
                CameraNo = Number;
                CameraId = SerialNumber;
                string TmpSerialNumber = null;
                // 枚举相机列表
                int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
                for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        TmpSerialNumber = gigeInfo.chSerialNumber;
                    }
                    else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        TmpSerialNumber = usbInfo.chSerialNumber;
                    }
                   
                    if (CameraId == TmpSerialNumber)
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
                        /*m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                        m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);*/
                       
                        //注册回调函数
                        cbImage = new MyCamera.cbOutputExdelegate(ImageCallBack);

                        currentExposureTime = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraId == CameraId).currentExposureTime;
                        currentGain = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraId == CameraId).currentGain;
                    }

                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                throw;
            }
        }
        
        public static MvClass GetInstance(int cameraNo)
        {
            
            if (MvList == null)
            {
                MvList = new List<MvClass>();
                for (int i = 0; i < MotionProcess.m_nCameraTotalNum; i++)
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
                        
                        if (ConfigVars.configInfo.Cameras.Count>0)
                        {
                            string id = ConfigVars.configInfo.Cameras.FirstOrDefault(c => c.CameraNo == cameraNo).CameraId;
                            if (id!=null)
                            {
                                MvList[cameraNo] = new MvClass(id, cameraNo);
                            }
                            
                        }
                       
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
        public bool OpenCam(int iCamNum)
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
            
            /*// ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);*/
            //设置曝光和增益
            m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", currentExposureTime);
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }
            if (iCamNum==0|| iCamNum == 1 || iCamNum == 2 || iCamNum == 3)
            {
                nRet = m_MyCamera.MV_CC_SetImageNodeNum_NET(60);//设置缓存节点数
            }
            nRet = m_MyCamera.MV_CC_SetImageNodeNum_NET(35);//设置缓存节点数
           /* m_MyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gain", currentGain);
            if (nRet != MyCamera.MV_OK)
            {
                return false;
            }*/
            //打开触发模式
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
            
            m_MyCamera.MV_CC_RegisterImageCallBackEx_NET(cbImage, (IntPtr)iCamNum);
            //设置软触发
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
            //开始采集
            m_stFrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            // ch:开始采集 | en:Start Grabbing
            nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Start Grabbing Fail!", nRet);
                return false;
            }
            
            //motionProcess = new MotionProcess();
            isOpen = true;
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
        public void StopGrabbing()
        {
            // ch:停止采集 | en:Stop Grabbing
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
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
        MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
        public void GetExposureTime() 
        {
            
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                currentExposureTime = (long)stParam.fCurValue;
            }
        }
        public void GetGain() 
        {
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                currentGain = (long)stParam.fCurValue;
            }
        }
    }
}
