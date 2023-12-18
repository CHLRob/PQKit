# 怎么在C# WinForm程序中集成PQKit
以C# WinForm程序为例介绍如何集成PQKit。
## 要求
- Visual Studio 2015及以上版本。
- .NET Framework 4.5.2

## 步骤
- 新建C# WinForm工程，设计应用程序界面布局。
- 将SDK安装目录下Sample\include\C#下PQKitPlugin.cs拷贝至新建工程目录，并加入工程。
- 新增PQPlatformComponentCallBack类实现IPQPlatformComponentCallBack接口方法。（详见Sample）
- 重写Form类Form_Load函数。
- 在Form_Load函数中创建PQKit对象，并创建Thread线程，在线程中调用pq_InitPlatformComponent方法初始化PQKit组件。其中账户名、密码需要申请方可获取。
```
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
```
- 根据Fire_Initialize_Result的返回结果判断PQKit初始化结果，如初始化成功则进行显示PQKit主窗口以及停靠窗口相关逻辑。

