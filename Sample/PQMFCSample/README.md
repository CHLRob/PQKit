# 怎么在MFC程序中集成PQKit
以MFC 对话框程序为例介绍如何集成PQKit。
## 要求
- Visual Studio 2015及以上版本。

## 步骤
- 新建MFC对话框工程，设计应用程序界面布局。
- 将SDK安装目录下Sample\include\C++下RPC.tlb拷贝至新建工程目录。
- 在工程代码中import RPC.tlb，Rebuild工程。
```
  #import "RPC.tlb" no_namespace, named_guids, raw_interfaces_only, raw_native_types
```
- 新增CPQComponentCall类实现IPQPlatformComponentCallBack接口方法。（详见Sample）
- 在对话框OnInitDialog()中创建PQKit对象，并调用pq_InitPlatformComponent方法初始化PQKit组件。其中账户名、密码需要申请方可获取。
```
  HRESULT hr = m_ptrComponent.CoCreateInstance(__uuidof(PQPlatformComponent));
	if (S_OK != hr)
	{
		return FALSE;
	}
	m_ptrComponent->pq_InitPlatformComponent(&m_ptrComponentCall,(int)GetDlgItem(IDC_STATIC_CANVAS)->GetSafeHwnd(), 
		L"", L"");
```
- 根据Fire_Initialize_Result的返回结果判断PQKit初始化结果，如初始化成功则进行显示PQKit主窗口以及停靠窗口相关逻辑。
