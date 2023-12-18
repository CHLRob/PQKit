# 怎么在C# WPF程序中集成PQKit
以C# WPF程序为例介绍如何集成PQKit。
## 要求
- Visual Studio 2015及以上版本。
- .NET Framework 4.5.2

## 步骤
- 新建C# WPF工程，设计应用程序界面布局，其中PQKit主显示界面以及4个可浮动窗口需要使用WindowsFormsHost内嵌WinForm Panel控件来显示。
```
<WindowsFormsHost Width = "968" Height="769" Background="#FFE1E7ED"  HorizontalAlignment="Left" Name="WindowsFormsHostDisplay" VerticalAlignment="Top"  AllowDrop="False" Margin="220,10,0,0"  >
	<wf:Panel x:Name="DesignPanel"   Location="0,0"  Width="1152" Height="768"    />
</WindowsFormsHost>
<WindowsFormsHost Width = "320" Height="382" Background="#FFE1E7ED"  HorizontalAlignment="Left" x:Name="WindowsFormsHostDisplay_Copy" VerticalAlignment="Top"  AllowDrop="False" Margin="1193,10,0,0"  >
	<wf:Panel x:Name="ModelTreePanel"   Location="0,0"    />
</WindowsFormsHost>
<WindowsFormsHost Width = "320" Height="382" Background="#FFE1E7ED"  HorizontalAlignment="Left" x:Name="WindowsFormsHostDisplay_Copy1" VerticalAlignment="Top"  AllowDrop="False" Margin="1193,397,0,0"  >
	<wf:Panel x:Name="DebugTreePanel"   Location="0,0"    />
</WindowsFormsHost>
```
- 将SDK安装目录下Sample\include\C#下PQKitPlugin.cs拷贝至新建工程目录，并加入工程。
- 新增PQPlatformComponentCallBack类实现IPQPlatformComponentCallBack接口方法。（详见Sample）
- 重写MainWindow类Window_Loaded函数。
- 在Window_Loaded函数中创建PQKit对象，并创建Thread线程，在线程中调用pq_InitPlatformComponent方法初始化PQKit组件。其中账户名、密码需要申请方可获取。
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

