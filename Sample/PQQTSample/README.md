# 怎么在QT程序中集成PQKit
以QT Main Window程序为例介绍如何集成PQKit。
## 要求
- Visual Studio 2015及以上版本。
- QT 5.12.8

## 步骤
- 新建QT MainWindow工程，设计应用程序界面布局。
- 将SDK安装目录下Sample\include\C++下RPC.tlb拷贝至新建工程目录。
- 在工程代码中import RPC.tlb，Rebuild工程。
```
#import "RPC.tlb" no_namespace, named_guids, raw_interfaces_only, raw_native_types
```
- 新增CPQKitCallback类实现IPQPlatformComponentCallBack接口方法。（详见Sample）
- 在MainWindow类构造函数中创建PQKit对象，并创建QThread线程，在线程中调用pq_InitPlatformComponent方法初始化PQKit组件。其中账户名、密码需要申请方可获取。
```
// 创建线程，绑定信号与槽函数
m_thKitInitThread = new PQKitInitThread;
connect(m_thKitInitThread, &PQKitInitThread::signalInitializeKit, this, &MainWindow::handleInitializeKit);
m_thKitInitThread->start();

// 线程Run函数中发送信号
void PQKitInitThread::run()
{
	emit signalInitializeKit();
}

// MainWindow槽函数中初始化PQKit
void MainWindow::handleInitializeKit()
{
	CComBSTR bsName = L"";
	CComBSTR bsPWD = L"";
	HWND hWnd = (HWND)(ui->wMainView->winId());
	m_ptrKit->pq_InitPlatformComponent(m_kitCall, (int)hWnd, bsName, bsPWD);
}
```
- 根据Fire_Initialize_Result的返回结果判断PQKit初始化结果，如初始化成功则进行显示PQKit主窗口以及停靠窗口相关逻辑。
