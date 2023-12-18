#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "PQKitInitThread.h"
#include "PQKItCallback.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

	void closeEvent(QCloseEvent *);


private:
	void ShowPQKit();
	void ShowPQKitWindow(HWND i_hWnd, QWidget *i_ptrWidget);
	void InitPQKit();
	void GetObjIDByName(int i_nType, std::wstring i_wsName, ULONG &o_uID);

public slots:
	void handleInitializeResult(long lResult);
	void handleRunCMDResult(long lResult);
	void handleGetDataResult(long lResult);
	void handleRaiseDockwindow(int i_nType);
	void handleLoginResult(int i_nLoginType);
	void handlePathGenerateResult(long i_bSuccess, int i_nPathCount, int i_nIndex, unsigned long i_ulPathID);
	void handleElementPickup(unsigned long i_ulObjID, long i_lEntityID, int i_nEntityType);
	void handleRButtonUp(long i_lPosX, long i_lPosY);
	void handleLButtonUp(long i_lPosX, long i_lPosY);
	void handleMenuPop(unsigned long i_ulObjID, long i_lPosX, long i_lPosY, int *o_nHandled);

	//
	void handleInitializeKit();
	void OnClickBtnOpenFile();
	void OnClickBtnSaveAs();
	void OnClickBtnArcBall();
	void OnClickBtnInsertPoint();
	void OnClickBtnDesignRobot();
private:
    Ui::MainWindow *ui;
	PQKitInitThread* m_thKitInitThread;

	CComPtr<IPQPlatformComponent> m_ptrKit;
	CPQKitCallback *m_kitCall;
	
};
#endif // MAINWINDOW_H
