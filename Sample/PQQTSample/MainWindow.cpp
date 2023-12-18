#include "MainWindow.h"

#include "ui_MainWindow.h"
#include <QFileDialog>
#include <QWindow>
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
	setAttribute(Qt::WA_DeleteOnClose);
#ifdef _DEBUG
    ui->wModelTree->setStyleSheet("background-color: rgb(211,211,211)");
	ui->wDebugTree->setStyleSheet("background-color: rgb(211,211,211)");
	ui->wMainView->setStyleSheet("background-color: rgb(211,211,211)");
#endif // _DEBUG

	InitPQKit();

	connect(ui->pbOpenFile, SIGNAL(clicked()), this, SLOT(OnClickBtnOpenFile()));
	connect(ui->pbSaveAs, SIGNAL(clicked()), this, SLOT(OnClickBtnSaveAs()));
	connect(ui->pbArcBall, SIGNAL(clicked()), this, SLOT(OnClickBtnArcBall()));
	connect(ui->pbInsertPoint, SIGNAL(clicked()), this, SLOT(OnClickBtnInsertPoint()));
	connect(ui->pbDesignRobot, SIGNAL(clicked()), this, SLOT(OnClickBtnDesignRobot()));
}

MainWindow::~MainWindow()
{
    delete ui;

	if (nullptr!=m_kitCall)
	{
		delete m_kitCall;
		m_kitCall = nullptr;
	}

	CoUninitialize();
}

void MainWindow::closeEvent(QCloseEvent *event)
{
	m_ptrKit->pq_CloseComponent();
}

void MainWindow::InitPQKit()
{
	CoInitializeEx(0, COINIT_APARTMENTTHREADED);
	
	m_ptrKit.CoCreateInstance(__uuidof(PQPlatformComponent));
	m_kitCall = new CPQKitCallback();
	connect(m_kitCall, &CPQKitCallback::signalInitializeResult, this, &MainWindow::handleInitializeResult);
	connect(m_kitCall, &CPQKitCallback::signalRunCMDResult, this, &MainWindow::handleRunCMDResult);
	connect(m_kitCall, &CPQKitCallback::signalGetDataResult, this, &MainWindow::handleGetDataResult);
	connect(m_kitCall, &CPQKitCallback::signalRaiseDockwindow, this, &MainWindow::handleRaiseDockwindow);
	connect(m_kitCall, &CPQKitCallback::signalLoginResult, this, &MainWindow::handleLoginResult);
	connect(m_kitCall, &CPQKitCallback::signalPathGenerateResult, this, &MainWindow::handlePathGenerateResult);
	connect(m_kitCall, &CPQKitCallback::signalElementPickup, this, &MainWindow::handleElementPickup);
	connect(m_kitCall, &CPQKitCallback::signalLButtonUp, this, &MainWindow::handleLButtonUp);
	connect(m_kitCall, &CPQKitCallback::signalRButtonUp, this, &MainWindow::handleRButtonUp);
	connect(m_kitCall, &CPQKitCallback::signalMenuPop, this, &MainWindow::handleMenuPop);

	m_thKitInitThread = new PQKitInitThread;
	HWND hWnd = (HWND)(this->winId());
	
	connect(m_thKitInitThread, &PQKitInitThread::signalInitializeKit, this, &MainWindow::handleInitializeKit);
	m_thKitInitThread->start();
}

void MainWindow::handleInitializeResult(long lResult)
{
	if (lResult > 0)
	{
		//show kit
		ShowPQKit();
	}
	else
	{
		QString strError = QString::fromLocal8Bit("组件初始化失败:\n");
		switch (lResult)
		{
		case -1:
			strError.append(QString::fromLocal8Bit("未知错误"));
			break;
		case -2:
			strError.append(QString::fromLocal8Bit("初始化超时"));
			break;
		case -3:
		case -4:
		case -6:
			strError.append(QString::fromLocal8Bit("加载组件动态库错误"));
			break;
		case -7:
			strError.append(QString::fromLocal8Bit("组件重复初始化"));
			break;
		case -9:
		case -10:
		case -11:
		case -12:
			strError.append(QString::fromLocal8Bit("内部数据错误"));
			break;
		case -14:
			strError.append(QString::fromLocal8Bit("非开发版账号不能登录开发版"));
			break;
		default:
			strError.append(QString::fromLocal8Bit("错误码: "));
			strError.append(QString::number(lResult,10));
			break;
		}

		QMessageBox::information(NULL, "PQKit Info", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);
	}
}

void MainWindow::handleRunCMDResult(long lResult)
{

}

void MainWindow::handleGetDataResult(long lResult)
{

}

void MainWindow::handleRaiseDockwindow(int i_nType)
{

}

void MainWindow::handleLoginResult(int i_nLoginType)
{
	if (i_nLoginType < 0)
	{
		QString strError = QString::fromLocal8Bit("用户校验失败:\n");
		switch (i_nLoginType)
		{
		case -1:
			strError.append(QString::fromLocal8Bit("密码错误"));
			break;
		case -2:
			strError.append(QString::fromLocal8Bit("用户不存在"));
			break;
		case -4:
			strError.append(QString::fromLocal8Bit("用户已过期"));
			break;
		case -6:
			strError.append(QString::fromLocal8Bit("网络连接失败"));
			break;
		case -7:
			strError.append(QString::fromLocal8Bit("用户未激活"));
			break;
		case -14:
			strError.append(QString::fromLocal8Bit("账号与产品不匹配"));
			break;
		default:
			strError.append(QString::fromLocal8Bit("错误码: "));
			strError.append(QString::number(i_nLoginType));
			break;
		}

		QMessageBox::information(NULL, "PQKit Info", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);

	}
}

void MainWindow::handlePathGenerateResult(long i_bSuccess, int i_nPathCount, int i_nIndex, unsigned long i_ulPathID)
{

}

void MainWindow::handleElementPickup(unsigned long i_ulObjID, long i_lEntityID, int i_nEntityType)
{

}

void MainWindow::handleRButtonUp(long i_lPosX, long i_lPosY)
{

}

void MainWindow::handleLButtonUp(long i_lPosX, long i_lPosY)
{

}

void MainWindow::handleMenuPop(unsigned long i_ulObjID, long i_lPosX, long i_lPosY, int *o_nHandled)
{

}

void MainWindow::handleInitializeKit()
{
	CComBSTR bsName = L"";
	CComBSTR bsPWD = L"";
	HWND hWnd = (HWND)(ui->wMainView->winId());
	m_ptrKit->pq_InitPlatformComponent(m_kitCall, (int)hWnd, bsName, bsPWD);
}

void MainWindow::GetObjIDByName(int i_nType, std::wstring i_wsName, ULONG &o_uID)
{
	VARIANT vNamePara;
	vNamePara.parray = NULL;
	VARIANT vIDPara;
	vIDPara.parray = NULL;
	m_ptrKit->Doc_get_obj_bytype(i_nType, &vNamePara, &vIDPara);
	if (NULL == vNamePara.parray || NULL == vIDPara.parray)
	{
		return;
	}
	//缓存指定对象名称
	BSTR* bufName;
	long lenName = vNamePara.parray->rgsabound[0].cElements;
	SafeArrayAccessData(vNamePara.parray, (void**)&bufName);
	int nTarIndex = 0;
	if (!i_wsName.empty())
	{
		for (int i = 0; i < lenName; i++)
		{
			if (0 == i_wsName.compare(bufName[i]))
			{
				nTarIndex = i;
			}
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



void MainWindow::OnClickBtnOpenFile()
{
	QString fileName = QFileDialog::getOpenFileName(this,tr("open robx file"),"",
		tr("Robx files(*.robx)"));
	if (fileName.isEmpty())
	{
		return;
	}
	fileName = QDir::toNativeSeparators(fileName);
	std::wstring wsFilePath = fileName.toStdWString();
	long long lResult = 0;
	CComVariant varPara(wsFilePath.c_str());
	CComBSTR bsPara = "";
	CComBSTR bsCmd = "RO_CMD_FILE_OPEN";
	m_ptrKit->pq_RunCommand(bsCmd, NULL, NULL, bsPara, varPara, &lResult);
}

void MainWindow::OnClickBtnSaveAs()
{
	QString fileName = QFileDialog::getSaveFileName(this, tr("save robx file"), "",
		tr("Robx files(*.robx)"));
	if (fileName.isEmpty())
	{
		return;
	}
	fileName = QDir::toNativeSeparators(fileName);
	std::wstring t_wsFile = fileName.toStdWString();
	long long lResult = 0;
	CComVariant varPara(t_wsFile.c_str());
	CComBSTR bsPara = "";
	CComBSTR bsCmd = "RO_CMD_FILE_SAVE_AS";
	m_ptrKit->pq_RunCommand(bsCmd, NULL, NULL, bsPara, varPara, &lResult);
}

void MainWindow::OnClickBtnArcBall()
{
	long long lResult = 0;
	CComBSTR bsPara = "";
	CComBSTR bsCmd = "RO_CMD_ARCBALL_PROXY";
	m_ptrKit->pq_RunCommand(bsCmd, NULL, NULL, bsPara, CComVariant(), &lResult);
}

void MainWindow::OnClickBtnInsertPoint()
{
	ULONG uRobotID = 0;
	GetObjIDByName(32, _T(""), uRobotID);

	//
	double dPosition[6] = { 0.0 };
	dPosition[0] = ui->lineEditPtX->text().toDouble();
	dPosition[1] = ui->lineEditPtY->text().toDouble();
	dPosition[2] = ui->lineEditPtZ->text().toDouble();
	dPosition[3] = ui->lineEditPtA->text().toDouble();
	dPosition[4] = ui->lineEditPtB->text().toDouble();
	dPosition[5] = ui->lineEditPtC->text().toDouble();
	
	int nInstruction[1] = {1};
	double dVel[1] = { 200.0 };
	double dSpeedP[1] = { 100.0 };
	int nApproach[1] = { -1 };
	CComBSTR bsPathName = "Test_Path";
	CComBSTR bsPathGroupName = "Test_Group";

	ULONG uPathID = 0;
	m_ptrKit->Path_insert_from_point(uRobotID, 1 ,dPosition, 1, nInstruction, dVel, dSpeedP, nApproach, bsPathName, bsPathGroupName, 0, FALSE, &uPathID);
}

void MainWindow::OnClickBtnDesignRobot()
{
	long long lResult = 0;
	CComBSTR bsPara = "";
	CComBSTR bsCmd = "RO_CMD_DEFINE_MECHANISM";
	m_ptrKit->pq_RunCommand(bsCmd, NULL, NULL, bsPara, CComVariant(), &lResult);
}

void MainWindow::ShowPQKit()
{
	int nHWND = 0;
	m_ptrKit->pq_GetPlatformView(&nHWND);
	HWND hWnd = (HWND)(UINT_PTR)nHWND;
	if (nullptr != hWnd)
	{
		ShowPQKitWindow(hWnd, ui->wMainView);
	}
	

	ULONG_PTR nModelDoc = NULL;
	m_ptrKit->pq_GetModelTreeView(&nModelDoc);
	HWND hModelWnd = (HWND)nModelDoc;
	if (nullptr != hModelWnd)
	{
		ShowPQKitWindow(hModelWnd, ui->wModelTree);
	}

	ULONG_PTR nDebugDoc = NULL;
	m_ptrKit->pq_GetDebugTreeView(&nDebugDoc);
	HWND hDebugWnd = (HWND)nDebugDoc;
	if (nullptr != hDebugWnd)
	{
		ShowPQKitWindow(hDebugWnd, ui->wDebugTree);
	}
	show();
}

void MainWindow::ShowPQKitWindow(HWND i_hWnd, QWidget *i_ptrWidget)
{
	QWindow* externWindow = QWindow::fromWinId((WId)i_hWnd);
	QWidget* containerWidget = nullptr;
	containerWidget = QWidget::createWindowContainer(externWindow, this);
	QSize qs = i_ptrWidget->size();
	QPoint qp = i_ptrWidget->pos();
	i_ptrWidget->hide();
	containerWidget->setGeometry(qp.x(), qp.y(), qs.width(), qs.height());
	containerWidget->show();
}
