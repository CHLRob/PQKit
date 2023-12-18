using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using PQKitPlugin;


namespace Machine
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        List<uint> m_listRobots = new List<uint>();
        List<uint> m_listTools = new List<uint>();
        List<uint> m_listPaths = new List<uint>();
        List<string> m_listPostureType = new List<string>();
        uint m_ulPtID = 0;
        uint m_uImportPathID = 0;

        //创建内核组件回调接口
        PQPlatformComponentCallBack m_CallBack = null;
        //创建内核组件接口
        IPQPlatformComponent m_ptrKit = null;

        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        public static extern int CloseWindow(IntPtr hWndChild);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public MainWindow()
        {
            InitializeComponent();
        }

        public void KitInitializedFuc()
        {
            struct_PQKitOption strOption;
            strOption.nEmbedded = 0;
            m_ptrKit.PQAPISetOption(ref strOption);

            //此处为内核组件的初始化，需依次传入内核组件回调接口，用于显示内核组件的句柄，用户名、密码
            int nHWND = (int)DesignPanel.Handle;
            string strName = "";
            string strPWD = "";
            try
            {
                m_ptrKit.pq_InitPlatformComponent(m_CallBack, nHWND, strName, strPWD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PQKit init info:\n" + ex.Message);
            }

        }

        public void ShowPlarformWindow()
        {
            //此处将内核组件显示在C#窗体的句柄中
            //由回调接口Fire_Initialize_Result函数中按初始化结果判定调用与否
            int nHWND = 0;
            m_ptrKit.pq_GetPlatformView(out nHWND);
            IntPtr child = (IntPtr)nHWND;

            SetParent(child, DesignPanel.Handle);

            MoveWindow(child, 0, 0, DesignPanel.Width, DesignPanel.Height, true);

            ShowDockWindow();
        }

        private void ShowDockWindow()
        {
            ulong nHWND = m_ptrKit.pq_GetModelTreeView();
            IntPtr child = (IntPtr)nHWND;
            SetParent(child, ModelTreePanel.Handle);
            MoveWindow(child, 0, 0, ModelTreePanel.Width, ModelTreePanel.Height, true);
            ShowWindow(child,1);

            ulong nHWNDD = m_ptrKit.pq_GetDebugTreeView();
            IntPtr childD = (IntPtr)nHWNDD;
            SetParent(childD, DebugTreePanel.Handle);
            MoveWindow(childD, 0, 0, DebugTreePanel.Width, DebugTreePanel.Height, true);
            ShowWindow(childD, 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //创建内核对象
            CPQKitPlugin pqKitPlugin = new CPQKitPlugin();
            m_ptrKit = pqKitPlugin.LoadPQKit();
            if (null == m_ptrKit)
            {
                MessageBox.Show("PQKit load error");
            }
            //创建callback对象
            m_CallBack = new PQPlatformComponentCallBack();
            m_CallBack.SetMainWindow(this);

            //在线程中初始化kit
            Thread workThread = new Thread(new ThreadStart(InitializeKit));
            workThread.SetApartmentState(ApartmentState.MTA);
            workThread.IsBackground = true;
            workThread.Start();

            InitPostureTypeUI();
        }
        
        private void InitializeKit()
        {
            this.Dispatcher.BeginInvoke((Action)delegate ()
            {
                this.KitInitializedFuc();
            });
            
        }

        private void InitPostureTypeUI()
        {
            //这里要注意匹配api手册中的位姿描述定义
            m_listPostureType.Add("四元数");
            m_listPostureType.Add("欧拉角XYZ");
            m_listPostureType.Add("欧拉角ZYX");
            m_listPostureType.Add("欧拉角ZXZ");
            m_listPostureType.Add("欧拉角ZYZ");

            comboBoxPosType.ItemsSource = m_listPostureType;
            comboBoxPosType.SelectedIndex = 1;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (m_ptrKit != null)
            {
                Marshal.ReleaseComObject(m_CallBack);
                Marshal.ReleaseComObject(m_ptrKit);
            }
            Environment.Exit(0);

        }

        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {          
          
        }

        private void GetObjects(int nType, List<uint> luObj, bool i_bShowLog)
        {
            luObj.Clear();
            //
            string sName = "";
            string sID = "";
            m_ptrKit.pq_GetAllDataObjectsByType(nType, out sName, out sID);
            if (i_bShowLog)
            {
                MessageBox.Show("debug info:\n" + "Name: " + sName + "ID:" + sID);
            }

            //prase name and id
            string[] strNameArray = sName.Split(new char[] { '#' }, options: StringSplitOptions.RemoveEmptyEntries);
            string[] strIDArray = sID.Split(new char[] { '#' }, options: StringSplitOptions.RemoveEmptyEntries);
            if (strNameArray.Length != strIDArray.Length)
            {
                if (i_bShowLog)
                {
                    MessageBox.Show("debug info:\n" + "指定的对象名称与ID个数不匹配");
                }
                return;
            }

            for (int i = 0; i < strNameArray.Length; i++)
            {
                luObj.Add(uint.Parse(strIDArray[i]));
            }

        }

        private void GetObjIDByName(string i_strName, int i_nType, out uint o_uID)
        {
            string sName = "";
            string sID = "";
            m_ptrKit.pq_GetAllDataObjectsByType(i_nType, out sName, out sID);

            //prase name and id
            string[] strNameArray = sName.Split(new char[] { '#' }, options: StringSplitOptions.RemoveEmptyEntries);
            string[] strIDArray = sID.Split(new char[] { '#' }, options: StringSplitOptions.RemoveEmptyEntries);
            if (strNameArray.Length != strIDArray.Length)
            {
                o_uID = 0;
                return;
            }

            uint uID = 0;
            for (int i = 0; i < strNameArray.Length; i++)
            {
                if (i_strName == strNameArray[i])
                {
                    uID = uint.Parse(strIDArray[i]);
                    break;
                }
            }

            o_uID = uID;
        }

        private void MessageInfo(string strTile, double[] dValue, int nValueSize)
        {
            string strMsg = "debug info:\n";
            strMsg += strTile;
            for (int i = 0; i < nValueSize; i++)
            {
                //strMsg += Convert.ToString(dValue[i]);
                strMsg += dValue[i].ToString("#0.00");
                strMsg += " ";
            }
            MessageBox.Show(strMsg);
        }



        //bt func
        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.robx)|*.robx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            string strFilePath;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                strFilePath = openFileDialog.FileName;
                //此处为打开文件的范例，strFilePath为文件路径，请修改为你自己的路径
                string strCMD = "RO_CMD_FILE_OPEN";
                string strParam = "";
                ulong wParam = 0;
                long lParam = 0;
                //string strFilePath = "D:\\Debugs\\A20220820.robx";
                object varParam = strFilePath as object;
                long lResult = 0;
                m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
            }
        }

        private void buttonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            //保存文档
            string strCMD = "RO_CMD_FILE_SAVE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void buttonImport3DFile_Click(object sender, RoutedEventArgs e)
        {
            //导入3d文件
            string strCMD = "RO_CMD_IMPORT3D_TOWP";
            string strParam = "D:\\Debugs\\FanBlade.ply";
            ulong wParam = 0;
            long lParam = 0;
            string strType = "Stanford Triangle Mesh";
            object varParam = strType as object;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void buttonGeneratePath_Click(object sender, RoutedEventArgs e)
        {
            //生成轨迹
            string strCMD = "RO_CMD_EXTERNAL_PATH_GENERATE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void buttonSimulate_Click(object sender, RoutedEventArgs e)
        {
            //启动仿真
            string strCMD = "RO_CMD_SIMULATE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);

        }

        private void buttonCompile_Click(object sender, RoutedEventArgs e)
        {
            string strCMD = "RO_CMD_COMPILE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void buttonPost_Click(object sender, RoutedEventArgs e)
        {
            //strFilePath指定后置文件的绝对路径，多文件后置如KUKA机器人后置，指定src、dat之一即可
            string strFilePath = "C:\\Users\\Public\\Desktop\\sample.XPL";
            string strCMD = "RO_CMD_POST";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = strFilePath as object;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);            System.Diagnostics.Process.Start("notepad.exe", strFilePath);        }
    

        private void buttonCollision_Click(object sender, RoutedEventArgs e)
        {
            //请指定实际打开文档中的欲检测对象ID
            uint uRobotID = 0;
            GetObjIDByName("ABB-IRB2400-16", 32, out uRobotID);
            uint uPartID = 0;
            GetObjIDByName("立方体（500）", 16, out uPartID);
            int bCollide = 0;
            m_ptrKit.Doc_collide_obj_single(uRobotID, uPartID, out bCollide);
            MessageBox.Show("collide result:" + Convert.ToString(bCollide));
        }

        private void buttonRobot_Click(object sender, RoutedEventArgs e)
        {
            GetObjects(32, m_listRobots, true);
        }

        private void buttonTool_Click(object sender, RoutedEventArgs e)
        {
            GetObjects(48, m_listTools, true);
        }

        private void buttonJoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (0 == m_listRobots.Count())
                {
                    GetObjects(32, m_listRobots, false);
                }

                uint ulRobot = m_listRobots.First();
                int nCount = 0;
                IntPtr ptrJoints = m_ptrKit.Doc_get_obj_joints(ulRobot, out nCount);
                double[] dJoints = new double[nCount];
                Marshal.Copy(ptrJoints, dJoints, 0, nCount);
                for(int i = 0;i<nCount;i++)
                {
                    dJoints[i] = dJoints[i] * 57.2958;
                }
                MessageInfo("joints: ", dJoints, nCount);
            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void buttonLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (0 == m_listRobots.Count())
                {
                    GetObjects(32, m_listRobots, false);
                }
                uint ulRobotID = m_listRobots.First();

                int nCount = 0;
                IntPtr ptrLinks = m_ptrKit.Doc_get_obj_links(ulRobotID, out nCount);
                //get links
                double[] dLinks = new double[nCount];
                Marshal.Copy(ptrLinks, dLinks, 0, nCount);
                MessageInfo("links: ", dLinks, nCount);
            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void buttonVel_Click(object sender, RoutedEventArgs e)
        {
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            //get velocity
            double dVelocity = 0.0;
            double dRAD = 0.0;
            uint ulRobotID = m_listRobots.First();
            m_ptrKit.Doc_get_obj_velocity(ulRobotID, out dVelocity, out dRAD);
            MessageBox.Show("debug info:\n" + "Velocity: " + Convert.ToString(dVelocity) + ", RAD:" + Convert.ToString(dRAD));

        }

        private void buttonTCP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (0 == m_listTools.Count())
                {
                    GetObjects(48, m_listTools, false);
                }

                // get tcp 
                int nCount = 0;
                uint ulTool = m_listTools.First();
                string strTcpName = "";
                IntPtr ptrTcpPos = m_ptrKit.Tool_get_tcp_posture(ulTool, strTcpName, 1, out nCount);
                double[] dTcpPos = new double[nCount];
                Marshal.Copy(ptrTcpPos, dTcpPos, 0, nCount);
                MessageInfo("TCPPosture: ", dTcpPos, nCount);

            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void buttonEndPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (0 == m_listRobots.Count())
                {
                    GetObjects(32, m_listRobots, false);
                }
                //get robot end posture
                int nCount = 0;
                uint ulRobotID = m_listRobots.First();
                IntPtr ptrPos = m_ptrKit.Robot_get_end_posture(ulRobotID, 2, out nCount);
                double[] dEndPos = new double[nCount];
                Marshal.Copy(ptrPos, dEndPos, 0, nCount);
                MessageInfo("RobotEndPos: ", dEndPos, nCount);
            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void buttonName_Click(object sender, RoutedEventArgs e)
        {
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            //get name
            string sName = "";
            uint ulRobotID = m_listRobots.First();
            m_ptrKit.Doc_get_obj_name(ulRobotID, out sName);
            MessageBox.Show("debug info:\n" + "name: " + sName);
        }

        private void buttonPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (0 == m_listRobots.Count())
                {
                    GetObjects(32, m_listRobots, false);
                }
                //get obj posture
                int nCount = 0;
                uint ulRobotID = m_listRobots.First();
                IntPtr ptrPos = m_ptrKit.Doc_get_obj_posture(ulRobotID, 1, out nCount);
                double[] dPos = new double[nCount];
                Marshal.Copy(ptrPos, dPos, 0, nCount);
                MessageInfo("joints: ", dPos, nCount);
            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void buttonInsertPt_Click(object sender, RoutedEventArgs e)
        {
            //
            uint uRobotID = 0;
            if (0 == m_listRobots.Count)
            {
                GetObjects(32, m_listRobots, false);
            }
            uRobotID = m_listRobots.First();

            //
            const int nPtCount = 1;
            double[] dPoint = new double[nPtCount * 6] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            dPoint[0] = Convert.ToDouble(textBoxX.Text);
            dPoint[1] = Convert.ToDouble(textBoxY.Text);
            dPoint[2] = Convert.ToDouble(textBoxZ.Text);
            dPoint[3] = Convert.ToDouble(textBoxA.Text);
            dPoint[4] = Convert.ToDouble(textBoxB.Text);
            dPoint[5] = Convert.ToDouble(textBoxC.Text);
            //                     
            int[] nInstruct = new int[nPtCount] { 1 };
            double[] nVel = new double[nPtCount] { 100 };
            double[] nVelPer = new double[nPtCount] { 100 };
            int[] nApproach = new int[nPtCount] { -1 };
            //
            string strPathName = "test-path";
            string strGroupName = "test-group";
            Random rd = new Random();
            string strRD = rd.Next(10, 100).ToString();
            strPathName += "-";
            strPathName += strRD;

            //
            int nType = comboBoxPosType.SelectedIndex;
            uint uPathID = 0;
            m_ptrKit.Path_insert_from_point(uRobotID, nPtCount, ref dPoint[0], nType, ref nInstruct[0], ref nVel[0],
                ref nVelPer[0], ref nApproach[0], strPathName, strGroupName, 0, 0, out uPathID);
            m_uImportPathID = uPathID;
            MessageBox.Show("debug info:\n" + "new Path ID: " + Convert.ToString(uPathID));

        }

        private void buttonModifyPt_Click(object sender, RoutedEventArgs e)
        {
            object varParam = null;
            m_ptrKit.PQAPIGetPointsID(m_uImportPathID, out varParam);
            uint[] objs = (uint[])varParam;
            if (objs == null || objs.Length == 0)
            {
                return;
            }
            uint uHeadPtID = (uint)objs[0];

            const int nPostureArraySize = 6;
            double[] dPoint = new double[nPostureArraySize] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            dPoint[0] = Convert.ToDouble(textBoxX.Text);
            dPoint[1] = Convert.ToDouble(textBoxY.Text);
            dPoint[2] = Convert.ToDouble(textBoxZ.Text);
            dPoint[3] = Convert.ToDouble(textBoxA.Text);
            dPoint[4] = Convert.ToDouble(textBoxB.Text);
            dPoint[5] = Convert.ToDouble(textBoxC.Text);

            int nType = comboBoxPosType.SelectedIndex;
            m_ptrKit.PQAPIModifyPointPosture(uHeadPtID, ref dPoint[0], nPostureArraySize, nType);

        }

        private void buttonKinematics_Click(object sender, RoutedEventArgs e)
        {
            //
            uint uRobotID = 0;
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            uRobotID = m_listRobots.First();
            //
            const int nPostureArrSize = 6;
            double[] dPoint = new double[nPostureArrSize] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            dPoint[0] = Convert.ToDouble(textBoxX.Text);
            dPoint[1] = Convert.ToDouble(textBoxY.Text);
            dPoint[2] = Convert.ToDouble(textBoxZ.Text);
            dPoint[3] = Convert.ToDouble(textBoxA.Text);
            dPoint[4] = Convert.ToDouble(textBoxB.Text);
            dPoint[5] = Convert.ToDouble(textBoxC.Text);

            double[] dResult = new double[6] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            int[] nAxisCfg = new int[4] { 0, 0, 0, 0 };
            int nStatus = 0;
            int nType = comboBoxPosType.SelectedIndex;
            m_ptrKit.PQAPIInverseKinematics(uRobotID, ref dPoint[0], nPostureArrSize, nType, ref dResult[0], 6, ref nAxisCfg[0], 4, out nStatus);
            for (int i = 0; i < nPostureArrSize; i++)
            {
                dResult[i] = dResult[i] * 57.2957;
            }
            MessageInfo("joints: ", dResult, nPostureArrSize);
        }
    }


    public class PQPlatformComponentCallBack : IPQPlatformComponentCallBack
    {
        MainWindow mainWindow;
        public void SetMainWindow(MainWindow i_Form)
        {
            mainWindow = i_Form;
        }
        public void Fire_Initialize_Result(int lResult)
        {
            if (lResult > 0)
            {
                mainWindow.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    mainWindow.ShowPlarformWindow();
                });

            }
            else
            {
                string strError = "组件初始化失败:\n";
                switch (lResult)
                {
                    case -1:
                        MessageBox.Show(strError + "未知错误");
                        break;
                    case -2:
                        MessageBox.Show(strError + "初始化超时");
                        break;
                    case -3:
                    case -4:
                    case -6:
                        MessageBox.Show(strError + "加载组件动态库错误");
                        break;
                    //case -5:
                    //    MessageBox.Show(strError + "用户校验失败");
                    //    break;
                    case -7:
                        MessageBox.Show(strError + "组件重复初始化");
                        break;
                    case -9:
                    case -10:
                    case -11:
                    case -12:
                        MessageBox.Show(strError + "内部数据错误");
                        break;
                }
            }
        }

        public void Fire_RunCMD_Result(int lResult)
        {

        }

        public void Fire_GetData_Result(int lResult)
        {

        }

        public void Notify_Raise_Dockwindow(int i_nType)
        {

        }

        public void Fire_Login_Result(int lResult)
        {
            if (lResult < 0)
            {
                string strError = "用户校验失败:\n";
                switch (lResult)
                {
                    case -1:
                        MessageBox.Show(strError + "密码错误");
                        break;
                    case -2:
                        MessageBox.Show(strError + "用户不存在");
                        break;
                    case -4:
                        MessageBox.Show(strError + "用户已过期");
                        break;
                    case -6:
                        MessageBox.Show(strError + "网络连接失败");
                        break;
                    case -7:
                        MessageBox.Show(strError + "用户未激活");
                        break;
                    case -14:
                        MessageBox.Show(strError + "账号与产品不匹配");
                        break;
                }
            }
        }

        public void Fire_Path_Generate_Result(int i_bSuccess, int i_nPathCount, int i_nIndex, uint i_ulPathID)
        {

        }

        public void Fire_Element_Pickup(uint i_ulObjID, int i_lEntityID, int i_nEntityType)
        {
            MessageBox.Show("对象ID: " + i_ulObjID.ToString() + "实体ID: " + i_lEntityID.ToString());
        }

        public void Fire_RButton_Up(int i_lPosX, int i_lPosY)
        {
            //MessageBox.Show("相对于Kit世界坐标系下，X: " + i_lPosX.ToString() + "Y: " + i_lPosY.ToString());
        }

        public void Fire_LButton_Up(int i_lPosX, int i_lPosY)
        {
            //MessageBox.Show("相对于Kit世界坐标系下，X: " + i_lPosX.ToString() + "Y: " + i_lPosY.ToString());
        }

        public void Fire_Menu_Pop(uint i_ulObjID, int i_lPosX, int i_lPosY, out int o_nHandled)
        {
            //o_nHandled置0表示WPF并不弹出自己的右键菜单，选中物体时弹出PQKit自身菜单
            //o_nHandled置1表示WPF弹出自己的右键菜单，屏蔽选中物体时弹出的PQKit自身菜单
            o_nHandled = 0;
        }

    }
}
