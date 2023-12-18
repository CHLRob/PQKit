using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PQKitPlugin;
using System.Threading;
using System.Drawing;

namespace PQWinFormSample
{
    //PQKit初始化委托
    public delegate void OnStartKitInitialize();
    //PQKit初始化完成委托
    public delegate void OnKitInitialized();
    //PQKit绘图区左键UP事件委托
    public delegate void OnKitLBtnUP(int posX, int posY);
    //PQKit绘图区右键菜单事件委托
    public delegate void OnKitPopMenu(uint objID, int posX, int posY);
    public partial class Form1 : Form
    {
        //
        List<uint> m_listRobots = new List<uint>();
        List<uint> m_listTools = new List<uint>();
        List<uint> m_listPaths = new List<uint>();
        uint m_ulPtID = 0;
        uint m_uImportPathID = 0;

        public OnKitLBtnUP lbuCaller = null;
        public OnKitPopMenu pmCaller = null;
        //是否自己弹出右键菜单
        public bool bHandlePopMenu = false;
        //
        IPQPlatformComponent m_ptrKit = null;
        //
        PQPlatformComponentCallBack m_CallBack = null;
       

        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public Form1()
        {
            this.WindowState = FormWindowState.Minimized;

            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //创建内核对象
            CPQKitPlugin pqKitPlugin = new CPQKitPlugin();
            m_ptrKit = pqKitPlugin.LoadPQKit();
            if(null == m_ptrKit)
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

            //WinForm程序自己的后续业务逻辑
            InitPostureTypeUI();

            //
            lbuCaller = new OnKitLBtnUP(KitLBtnUpEvent);
            //kit 绘图区右键菜单响应
            pmCaller = new OnKitPopMenu(KitPopMenuEvent);
        }
        
        ~Form1()
        {
            Marshal.ReleaseComObject(m_ptrKit);
        }

        private void InitPostureTypeUI()
        {
            //这里要注意匹配api手册中的位姿描述定义
            this.cbBoxPosType.Text = "请选择姿态类型";
            cbBoxPosType.Items.Add("四元数");
            cbBoxPosType.Items.Add("欧拉角XYZ");
            cbBoxPosType.Items.Add("欧拉角ZYX");
            cbBoxPosType.Items.Add("欧拉角ZXZ");
            cbBoxPosType.Items.Add("欧拉角ZYZ");
            cbBoxPosType.SelectedIndex = 1;
        }

        private void InitializeKit()
        {
            this.Invoke(new OnStartKitInitialize(KitInitializedFuc));
        }

        public void KitInitializedFuc()
        {
            struct_PQKitOption strOption;
            strOption.nEmbedded = 0;
            m_ptrKit.PQAPISetOption(ref strOption);

            //此处为内核组件的初始化，需依次传入内核组件回调接口，用于显示内核组件的句柄，用户名、密码
            int nHWND = (int)panel1.Handle;
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

        public void KitRButtonUpEvent(int posX, int posY)
        {
            //
        }

        public void KitLBtnUpEvent(int posX, int posY)
        {
            contextMenuStrip1.Hide();
        }
        public void KitPopMenuEvent(uint objID, int posX, int posY)
        {
            PopRightBtnUpMenu(objID, posX, posY);
        }

        public void PopRightBtnUpMenu(uint objID, int posX, int posY)
        {
            Point pCur = new Point();
            pCur.X = posX;
            pCur.Y = posY;
            contextMenuStrip1.Show(pCur);
        }

        public void ShowPlarformWindow()
        {
            //此处将内核组件显示在C#窗体的句柄中
            //由回调接口Fire_Initialize_Result函数中按初始化结果判定调用与否
            int nHWND = 0;
            m_ptrKit.pq_GetPlatformView(out nHWND);
            IntPtr child = (IntPtr)nHWND;
            SetParent(child, panel1.Handle);

            MoveWindow(child, 0, 0, panel1.Width, panel1.Height, true);

            ShowDockWindow();

            this.WindowState = FormWindowState.Normal;
        }

        private void ShowDockWindow()
        {
            ulong nHWND = m_ptrKit.pq_GetModelTreeView();
            IntPtr child = (IntPtr)nHWND;
            SetParent(child, panelWorkTree.Handle);
            MoveWindow(child, 0, 0, panelWorkTree.Width, panelWorkTree.Height, true);
            ShowWindow(child, 1);

            ulong nHWNDD = m_ptrKit.pq_GetDebugTreeView();
            IntPtr childD = (IntPtr)nHWNDD;
            SetParent(childD, panelDebugTree.Handle);
            MoveWindow(childD, 0, 0, panelDebugTree.Width, panelDebugTree.Height, true);
            ShowWindow(childD, 1);
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

        //btn onclick
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.robx)|*.robx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            string strFilePath;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
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

        private void GetRobots_Click(object sender, EventArgs e)
        {
            GetObjects(32, m_listRobots, true);
        }

        private void GetTools_Click(object sender, EventArgs e)
        {
            GetObjects(48, m_listTools, true);
        }

        private void GetJoints_Click(object sender, EventArgs e)
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
                MessageInfo("joints: ", dJoints, nCount);
            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void GetVelocity_Click(object sender, EventArgs e)
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

        private void GetLinks_Click(object sender, EventArgs e)
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

        private void GetTCP_Click(object sender, EventArgs e)
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
                IntPtr ptrTcpPos = m_ptrKit.Tool_get_tcp_posture(ulTool, strTcpName, 0, out nCount);
                double[] dTcpPos = new double[nCount];
                Marshal.Copy(ptrTcpPos, dTcpPos, 0, nCount);
                MessageInfo("TCPPosture: ", dTcpPos, nCount);

            }
            catch (COMException ex)
            {
                MessageBox.Show("debug info:\n" + "COMException! " + ex.Message);
            }
        }

        private void GetRobotEndPos_Click(object sender, EventArgs e)
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

        private void GetName_Click(object sender, EventArgs e)
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

        private void Import3DObj_Click(object sender, EventArgs e)
        {
            //导入3d文件
            string strCMD = "RO_CMD_IMPORT_ACCESSORY_PART";
            string strParam = "D:\\Debugs\\hemisphere.stp";
            ulong wParam = 0;
            long lParam = 0;
            string strType = "STEP with colors";
            object varParam = strType as object;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void GetPosture_Click(object sender, EventArgs e)
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

        private void Measure_Click(object sender, EventArgs e)
        {
            //测量
            string strCMD = "RO_CMD_MEASUREMENT";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void ArcBox_Click(object sender, EventArgs e)
        {
            //三维球
            string strCMD = "RO_CMD_ARCBALL_PROXY";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void AlignPart_Click(object sender, EventArgs e)
        {
            //三点校准
            string strCMD = "RO_CMD_AlignPart3Point";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void ChangeColor_Click(object sender, EventArgs e)
        {
            //更改颜色
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            uint ulRobotID = m_listRobots.First();
            m_ptrKit.Doc_set_obj_color(ulRobotID, 218, 112, 214);
        }

        private void CloseFile_Click(object sender, EventArgs e)
        {
            //关闭文档
            string strFilePath = "";
            m_ptrKit.Doc_get_name(out strFilePath);
            m_ptrKit.pq_CloseDocument(strFilePath);
        }

        private void buttonSim_Click(object sender, EventArgs e)
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

        private void buttonSave_Click(object sender, EventArgs e)
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
        
        private void buttonImportPoints_Click(object sender, EventArgs e)
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
            int nType = this.cbBoxPosType.SelectedIndex;
            //
            uint uPathID = 0;
            m_ptrKit.Path_insert_from_point(uRobotID, nPtCount, ref dPoint[0], nType, ref nInstruct[0], ref nVel[0],
                ref nVelPer[0], ref nApproach[0], strPathName, strGroupName, 0, 0, out uPathID);
            m_uImportPathID = uPathID;
            MessageBox.Show("debug info:\n" + "new Path ID: " + Convert.ToString(uPathID));

        }

        private void buttonIK_Click(object sender, EventArgs e)
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
            int[] nAxisCfg = new int[4] { 0,0,0,0 };
            int nStatus = 0;
            int nType = this.cbBoxPosType.SelectedIndex;
            m_ptrKit.PQAPIInverseKinematics(uRobotID, ref dPoint[0], nPostureArrSize, nType, ref dResult[0],6, ref nAxisCfg[0],4, out nStatus);
            for (int i = 0; i < nPostureArrSize; i++)
            {
                dResult[i] = dResult[i] * 57.2957;
            }
            MessageInfo("joints: ", dResult, nPostureArrSize);
        }
        

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            uint ulRobot = m_listRobots.First();
            string strName = "test-group";
            m_ptrKit.PQAPIDeletePathGroup(ulRobot, strName);
        }

        private void buttonGetPointID_Click(object sender, EventArgs e)
        {
            if (0 == m_listPaths.Count())
            {
                GetObjects(80, m_listPaths, false);
            }
            uint uPathID = m_listPaths.First();
            object varParam = null;
            m_ptrKit.PQAPIGetPointsID(uPathID, out varParam);
            uint[] objs = (uint[])varParam;
            if (objs == null || objs.Length == 0)
            {
                return;
            }
            uint uHeadPtID = objs[0];
            m_ulPtID = uHeadPtID;

            uint uTailPtID = objs[objs.Length - 1];
            MessageBox.Show("debug info:\n" + "pt count: " + Convert.ToString(objs.Length) + "\n" 
                + "first pt id: " + Convert.ToString(uHeadPtID) + "\n" + "tail pt id:" + Convert.ToString(uTailPtID));
        }

        private void buttonGetPointInfo_Click(object sender, EventArgs e)
        {
            if(0 == m_ulPtID)
            {
                MessageBox.Show("debug info:\n" + "请先获取轨迹点ID");
                return;
            }
            double[] dPointPos = new double[6];
            double dVel = 0.0;
            double dpercent = 0.0;
            int nInstruct = 0;
            int nApproach = 0;
            int nPostureType = 1;
            m_ptrKit.PQAPIGetPointInfo(m_ulPtID, 6, out dPointPos[0], out dVel, out dpercent, out nInstruct, 
                out nApproach, nPostureType);
            MessageInfo("Pt pos: ", dPointPos, 6);
            
        }

        private void buttonAddCustomEvent_Click(object sender, EventArgs e)
        {
            if (0 == m_listRobots.Count())
            {
                GetObjects(32, m_listRobots, false);
            }
            uint uRobotID = m_listRobots.First();
            if (0 == m_ulPtID)
            {
                MessageBox.Show("debug info:\n" + "请先获取轨迹点ID");
                return;
            }
            uint[] uPts = new uint[1];
            uPts[0] = m_ulPtID;
            int nCount = 1;
            
            int nFront = 0;
            string strEventName = "Custom_Event";
            string strEventContent = "Do test event;";
            m_ptrKit.PQAPIAddCustomEvent(ref uPts[0], nCount, uRobotID, nFront, strEventName, strEventContent);
        }
   
        private void buttonCompile_Click(object sender, EventArgs e)
        {
            string strCMD = "RO_CMD_COMPILE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);
        }

        private void buttonGeneratePath_Click(object sender, EventArgs e)
        {
            //生成轨迹
            string strCMD = "RO_CMD_EXTERNAL_PATH_GENERATE";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = null;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);        }
       
        private void buttonModifyPoint_Click(object sender, EventArgs e)
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

            int nType = this.cbBoxPosType.SelectedIndex;
            m_ptrKit.PQAPIModifyPointPosture(uHeadPtID, ref dPoint[0], nPostureArraySize, nType);

        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            //strFilePath指定后置文件的绝对路径，多文件后置如KUKA机器人后置，指定src、dat之一即可
            string strFilePath = "C:\\Users\\Public\\Desktop\\sample.mod";
            string strCMD = "RO_CMD_POST";
            string strParam = "";
            ulong wParam = 0;
            long lParam = 0;
            object varParam = strFilePath as object;
            long lResult = 0;
            m_ptrKit.pq_RunCommand(strCMD, wParam, lParam, strParam, varParam, out lResult);        }

        private void buttonCollide_Click(object sender, EventArgs e)
        {
            //请指定实际打开文档中的欲检测对象ID
            uint uRobotID = 0;
            GetObjIDByName("ABB-IRB2400-16",32, out uRobotID);
            uint uPartID = 0;
            GetObjIDByName("立方体（500）",16, out uPartID);
            int bCollide = 0;
            m_ptrKit.Doc_collide_obj_single(uRobotID,uPartID,out bCollide);
            MessageBox.Show("collide result:" + Convert.ToString(bCollide));
        }

        private void Test_MenuItemA_Click(object sender, EventArgs e)
        {
            MessageBox.Show("用户自定义菜单项A响应！");
        }

        private void Test_MenuItemB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("用户自定义菜单项A响应！");
        }

        
    }
    public class PQPlatformComponentCallBack : IPQPlatformComponentCallBack
    {
        Form1 mainWindow;
        public void SetMainWindow(Form1 i_Form)
        {
            mainWindow = i_Form;
        }
        public void Fire_Initialize_Result(int lResult)
        {
            if (lResult > 0)
            {
                //show kit
                mainWindow.Invoke(new OnKitInitialized(mainWindow.ShowPlarformWindow));
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
                    case -14:
                        MessageBox.Show(strError + "非开发版账号不能登录开发版");
                        break;
                    default:
                        MessageBox.Show(strError + "错误码: " + lResult.ToString());
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
                    default:
                        MessageBox.Show(strError + "错误码: " + lResult.ToString());
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
            mainWindow.lbuCaller(i_lPosX, i_lPosY);
        }

        public void Fire_Menu_Pop(uint i_ulObjID, int i_lPosX, int i_lPosY, out int o_nHandled)
        {
            if(true == mainWindow.bHandlePopMenu)
            {
                mainWindow.pmCaller(i_ulObjID, i_lPosX,i_lPosY);
                o_nHandled = 1;
                return;
            }
            o_nHandled = 0;
        }
    }
}
