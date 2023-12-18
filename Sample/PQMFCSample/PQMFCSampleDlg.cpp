
// PQMFCSampleDlg.cpp : implementation file
//

#include "stdafx.h"
#include "PQMFCSample.h"
#include "PQMFCSampleDlg.h"
#include "afxdialogex.h"

#define RD_ROBOT 32
#define DEG2RAD 0.01745329251994329576923690768489

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CPQMFCSampleDlg dialog



CPQMFCSampleDlg::CPQMFCSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IDD_PQMFCSAMPLE_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hKitWnd = NULL;
	CoInitialize(NULL);
}

CPQMFCSampleDlg::~CPQMFCSampleDlg()
{
	
	CoUninitialize();
}

void CPQMFCSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CPQMFCSampleDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_SHOWWINDOW()
	ON_MESSAGE(WM_INIT_MSG, msgInitialize)
	ON_MESSAGE(WM_LOGIN_MSG, msgLogin)
	ON_MESSAGE(WM_PATHGENERATE_MSG, msgPathGenerate)
	ON_MESSAGE(WM_LBTNUP_MSG, msgLBtnUp)
	ON_MESSAGE(WM_POPMENU_MSG, msgPopMenu)
	ON_BN_CLICKED(IDC_BUTTON_OPENFILE, &CPQMFCSampleDlg::OnBnClickedButtonOpenfile)
	ON_BN_CLICKED(IDC_BUTTON_IK, &CPQMFCSampleDlg::OnBnClickedButtonIK)
	ON_BN_CLICKED(IDC_BUTTON_ABSJ,&CPQMFCSampleDlg::OnBnClickedButtonABSJoint)
	ON_BN_CLICKED(IDC_BUTTON_FACE_DATA, &CPQMFCSampleDlg::OnBnClickedButtonFaceShape)
	ON_BN_CLICKED(IDC_BUTTON_GENERATE_PATH, &CPQMFCSampleDlg::OnBnClickedButtonGeneratePath)
	ON_WM_CLOSE()
	ON_COMMAND(ID_MENU_ITEMA, &CPQMFCSampleDlg::OnMenuItemaA)
	ON_COMMAND(ID_MENU_ITEMB, &CPQMFCSampleDlg::OnMenuItemaB)
END_MESSAGE_MAP()


// CPQMFCSampleDlg message handlers

BOOL CPQMFCSampleDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
									//
	m_ptrComponentCall.RegisterUIHWND(this->GetSafeHwnd());
	HRESULT hr = m_ptrComponent.CoCreateInstance(__uuidof(PQPlatformComponent));
	if (S_OK != hr)
	{
		return FALSE;
	}
	m_ptrComponent->pq_InitPlatformComponent(&m_ptrComponentCall,(int)GetDlgItem(IDC_STATIC_CANVAS)->GetSafeHwnd(), 
		L"", L"");


	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CPQMFCSampleDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CPQMFCSampleDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CPQMFCSampleDlg::OnShowWindow(BOOL bShow, UINT nStatus)
{
	CDialogEx::OnShowWindow(bShow, nStatus);
}

void CPQMFCSampleDlg::ShowPlarformWindow()
{
	int nHWND = 0;
	m_ptrComponent->pq_GetPlatformView(&nHWND);
	HWND hWnd = (HWND)(UINT_PTR)nHWND;
	ShowChildWindow(hWnd, IDC_STATIC_CANVAS);
	m_hKitWnd = hWnd;
	
	ULONG_PTR nModelDoc = NULL;
	m_ptrComponent->pq_GetModelTreeView(&nModelDoc);
	HWND hModelWnd = (HWND)nModelDoc;
	ShowChildWindow(hModelWnd, IDC_STATIC_DOCK_LEFT);

	ULONG_PTR nDebugDoc = NULL;
	m_ptrComponent->pq_GetDebugTreeView(&nDebugDoc);
	HWND hDebugWnd = (HWND)nDebugDoc;
	ShowChildWindow(hDebugWnd, IDC_STATIC_DOCK_RIGHT);

	this->ShowWindow(SW_SHOW);
	
}

void CPQMFCSampleDlg::ShowChildWindow(HWND i_hWnd, UINT i_uResID)
{
	
	CWnd *pViewWnd = CWnd::FromHandle(i_hWnd);

	if (pViewWnd == NULL)
	{
		return;
	}

	CWnd* pCanvasWnd = GetDlgItem(i_uResID);
	if (pCanvasWnd == NULL)
	{
		return;
	}

	pViewWnd->SetParent(this);
	pCanvasWnd->ShowWindow(SW_HIDE);
	CRect rectCanvas;
	pCanvasWnd->GetWindowRect(&rectCanvas);
	ScreenToClient(&rectCanvas);
	pViewWnd->MoveWindow(rectCanvas);
	pViewWnd->ShowWindow(SW_SHOW);
}

LRESULT CPQMFCSampleDlg::msgInitialize(WPARAM wParam, LPARAM lParam)
{
	LONG lResult = (LONG)lParam;
	if (lResult > 0)
	{
		//show kit
		ShowPlarformWindow();
	}
	else
	{
		CString strError = L"组件初始化失败:\n";
		switch (lResult)
		{
		case -1:
			strError.Append(L"未知错误");
			break;
		case -2:
			strError.Append(L"初始化超时");
			break;
		case -3:
		case -4:
		case -6:
			strError.Append(L"加载组件动态库错误");
			break;
		case -5:
			return 1;
		case -7:
			strError.Append(L"组件重复初始化");
			break;
		case -9:
		case -10:
		case -11:
		case -12:
			strError.Append(L"内部数据错误");
			break;
		}
		MessageBox(strError, L"PQKit", MB_OK);
	}
	return 1;
}

LRESULT CPQMFCSampleDlg::msgPathGenerate(WPARAM wParam, LPARAM lParam)
{
	PQPathGenerateInfo *pqpPathInfo = (PQPathGenerateInfo*)wParam;
	m_ulGeneratePathID = pqpPathInfo->uPathID;
	delete pqpPathInfo;
	pqpPathInfo = nullptr;

	return 1;
}

LRESULT CPQMFCSampleDlg::msgLogin(WPARAM wParam, LPARAM lParam)
{
	int lResult = (int)lParam;
	if (lResult < 0)
	{
		CString strError = L"用户校验失败:\n";
		switch (lResult)
		{
		case -1:
			strError.Append(L"密码错误");
			break;
		case -2:
			strError.Append(L"用户不存在");
			break;
		case -4:
			strError.Append(L"用户已过期");
			break;
		case -6:
			strError.Append(L"网络连接失败");
			break;
		case -7:
			strError.Append(L"用户未激活");
			break;
		case -14:
			strError.Append(L"账号与产品不匹配");
			break;
		}
		MessageBox(strError, L"PQKit", MB_OK);
	}
	
	return 1;
}

LRESULT CPQMFCSampleDlg::msgLBtnUp(WPARAM wParam, LPARAM lParam)
{

	return 1;
}

LRESULT CPQMFCSampleDlg::msgPopMenu(WPARAM wParam, LPARAM lParam)
{
	PQPopMenuInfo *pqInfo = (PQPopMenuInfo*)wParam;
	ULONG ulID = pqInfo->ulObjID;
	long lPosX = pqInfo->lPosX;
	long lPosY = pqInfo->lPosY;
	delete pqInfo;
	pqInfo = nullptr;

	CMenu menu;
	menu.LoadMenu(IDR_MENU_TEST);
	CMenu* pPopup = menu.GetSubMenu(0);
	pPopup->TrackPopupMenu(TPM_LEFTALIGN | TPM_RIGHTBUTTON, lPosX, lPosY, AfxGetMainWnd());

	return 1;
}


void CPQMFCSampleDlg::OnBnClickedButtonOpenfile()
{
	CFileDialog dlg(TRUE, L"robx", NULL, 6, L"机器人离线编程文件|*.robx||");
	if (dlg.DoModal() != IDOK)
	{
		return;
	}
	std::wstring wsFilePath = dlg.m_ofn.lpstrFile;
	long long lResult = 0;
	CComVariant bsFile(wsFilePath.c_str());
	m_ptrComponent->pq_RunCommand(L"RO_CMD_FILE_OPEN", NULL, NULL, L"", bsFile, &lResult);

}


void CPQMFCSampleDlg::OnBnClickedButtonIK()
{
	//get robot id
	BSTR bsName = L"";
	BSTR bsID = L"";
	m_ptrComponent->pq_GetAllDataObjectsByType(RD_ROBOT, &bsName, &bsID);
	std::wstring t_wsName = bsName;
	if (t_wsName.empty())
	{
		return;
	}
	std::wstring t_wsID = bsID;
	size_t sPos = t_wsID.find(L"#");
	t_wsID = t_wsID.substr(0, sPos);
	ULONG uID = std::stoul(t_wsID);

	//
	double i_EndPostureA[6] = { 1343.437, -177.336, 290.528, -142.058, -37.608, -50.491 };
	int nResult = 0;
	double io_pJointValues[6] = { 0.0000,0.0000,0.0000,0.0000,0.0000,0.0000 };
	int i_nAxisCfg[6] = { 0,0,0,0,0,0 };
	m_ptrComponent->PQAPIInverseKinematicsFanuc(uID, i_EndPostureA, 6, io_pJointValues, 6, i_nAxisCfg, 6, &nResult);
	double dDisplay[6] = { 0.0 };
	for (int i = 0; i < 6; i++)
	{
		dDisplay[i] = io_pJointValues[i] * (180 / 3.14);
	}
	CString csResult;
	csResult.Format(L"J1: %.4lf,J2: %.4lf,J3: %.4lf,J4: %.4lf,J5: %.4lf,J6: %.4lf", dDisplay[0], dDisplay[1], dDisplay[2], dDisplay[3], dDisplay[4], dDisplay[5]);
	AfxMessageBox(csResult, MB_OK);

	double i_EndPostureB[6] = { 1405.193, 238.343 , 500.350, -132.409 , -44.858, -39.674 };
	m_ptrComponent->PQAPIInverseKinematicsFanuc(uID, i_EndPostureB, 6, io_pJointValues, 6, i_nAxisCfg, 6, &nResult);
	for (int i = 0; i < 6; i++)
	{
		dDisplay[i] = io_pJointValues[i] * (180 / 3.14);
	}
	csResult.Format(L"J1: %.4lf,J2: %.4lf,J3: %.4lf,J4: %.4lf,J5: %.4lf,J6: %.4lf", dDisplay[0], dDisplay[1], dDisplay[2], dDisplay[3], dDisplay[4], dDisplay[5]);
	AfxMessageBox(csResult, MB_OK);


}

void CPQMFCSampleDlg::GetObjIDByName(int i_nType, std::wstring i_wsName, ULONG &o_uID)
{
	VARIANT vNamePara;
	vNamePara.parray = NULL;
	VARIANT vIDPara;
	vIDPara.parray = NULL;
	m_ptrComponent->Doc_get_obj_bytype(i_nType, &vNamePara, &vIDPara);
	if (NULL == vNamePara.parray || NULL == vIDPara.parray)
	{
		return;
	}
	//缓存指定对象名称
	BSTR* bufName;
	
	long lenName = vNamePara.parray->rgsabound[0].cElements;
	SafeArrayAccessData(vNamePara.parray, (void**)&bufName);
	int nTarIndex = 0;
	for (int i = 0; i < lenName; i++)
	{
		if (0 == i_wsName.compare(bufName[i]))
		{
			nTarIndex = i;
		}
	}
	SafeArrayUnaccessData(vNamePara.parray);
	//缓存指定对象ID
	ULONG* bufID;
	long lenID = vIDPara.parray->rgsabound[0].cElements;
	SafeArrayAccessData(vIDPara.parray, (void**)&bufID);
	o_uID = bufID[nTarIndex];
	SafeArrayUnaccessData(vIDPara.parray);

}

void CPQMFCSampleDlg::OnBnClickedButtonABSJoint()
{
	ULONG uRobotID = 0;
	GetObjIDByName(32,_T("MOTOMAN-MA2010"),uRobotID);
	double dRobotJoints[12] = {0.0};
	dRobotJoints[0] = 129.516 * DEG2RAD;
	dRobotJoints[1] = -22.455 * DEG2RAD;
	dRobotJoints[2] = -64.416 * DEG2RAD;
	dRobotJoints[3] = 32.014 * DEG2RAD;
	dRobotJoints[4] = -71.176 * DEG2RAD;
	dRobotJoints[5] = -152.961 * DEG2RAD;

	dRobotJoints[6] = 139.516 * DEG2RAD;
	dRobotJoints[7] = -32.455 * DEG2RAD;
	dRobotJoints[8] = -74.416 * DEG2RAD;
	dRobotJoints[9] = 42.014 * DEG2RAD;
	dRobotJoints[10] = -61.176 * DEG2RAD;
	dRobotJoints[11] = -142.961 * DEG2RAD;

	double dGuideJoints[2] = { 0.0 };
	dGuideJoints[0] = -844.693;
	dGuideJoints[1] = -944.693;

	double dPosinterJoints[2] = { 0.0 };
	dPosinterJoints[0] = 24.545 * DEG2RAD;
	dPosinterJoints[1] = 34.545 * DEG2RAD;
	
	double dVel[2] = { 200.0,200.0 };
	double dSpeedP[2] = { 100.0,100.0 };
	int nApproach[2] = { -1 , -1};
	ULONG uPathID = 0;
	m_ptrComponent->PQAPIAddAbsJointPath(uRobotID,dRobotJoints,12,dGuideJoints,2,dPosinterJoints,2, dVel, dSpeedP, nApproach, 2,uPathID);

}

void CPQMFCSampleDlg::OnBnClickedButtonFaceShape()
{
	ULONG uPathID = m_ulGeneratePathID;
	int nCount = 0;
	LONG_PTR *lpFacePtr = NULL;
	m_ptrComponent->Path_get_generate_face(uPathID, &nCount, &lpFacePtr);
	for (int i = 0;i<nCount;i++)
	{
		LPVOID lpData = (LPVOID)lpFacePtr[i];
	}
	delete[] lpFacePtr;
	lpFacePtr = NULL;
}

void CPQMFCSampleDlg::OnBnClickedButtonGeneratePath()
{
	long long lResult = 0;
	m_ptrComponent->pq_RunCommand(L"RO_CMD_EXTERNAL_PATH_GENERATE", NULL, NULL, L"", CComVariant(), &lResult);
}

void CPQMFCSampleDlg::OnClose()
{
	m_ptrComponent->pq_CloseComponent();
	m_ptrComponent.Release();

	return CDialogEx::OnClose();
}


void CPQMFCSampleDlg::OnMenuItemaA()
{
	MessageBox(_T("测试菜单A"), _T("Debug"), MB_OK);
}

void CPQMFCSampleDlg::OnMenuItemaB()
{
	MessageBox(_T("测试菜单B"), _T("Debug"), MB_OK);
}
