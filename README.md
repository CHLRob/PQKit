# PQKit
PQKit，是一款高自由度、可定制的工业机器人离线编程仿真组件。适用于本身已经具备了一定的业务系统，但是该系统内缺少离线编程能力，急需将离线编程、仿真调试能力集成到现有系统中去的用户。或者需要开发一套属于自己的离线编程软件、智慧焊接系统、智慧打磨系统等场景的用户。

PQKit采用C++语言封装开发，跨平台，具有良好的软件兼容性。支持 C++、C#、Python 等语言开发的应用集成调用。PQKit包含PQ 3D、PQ 2D、PQ Robot、PQ Path、PQ Simulation等离线编程仿真技术所需核心技术模块。支持功能、菜单裁剪，以打造属于用户自己的工业软件。

## 典型应用案例
- 智慧焊接。搭配视觉系统，由视觉系统扫描出焊缝数据，通过PQKit API将焊缝数据导入PQKit生成焊接轨迹数据。再对轨迹进行编译优化，仿真验证，后置出机器人作业程序给真机运行。
- 焊缝识别。通过PQKit API返回指定工件的目标几何信息，通过对几何特征的分析得出焊缝数据，实现一键识别焊缝。

## Sample
用户可以从0开始搭建集成PQKit的应用程序，也可以将PQKit集成到现有应用程序中。
以下是用不同开发组件集成PQKit的范例：
- [WinForm](https://github.com/CHLRob/PQKit/tree/main/Sample/PQWinFormSample)
  ![image text](https://github.com/CHLRob/PQKit/blob/main/Sample/PQWinFormSample/PQKitWinForm.png "PQKit WinForm Sample")
- [WPF](https://github.com/CHLRob/PQKit/tree/main/Sample/PQWPFSample)
  ![image text](https://github.com/CHLRob/PQKit/blob/main/Sample/PQWPFSample/PQKitWPF.png "PQKit WPF Sample")
- [MFC](https://github.com/CHLRob/PQKit/tree/main/Sample/PQMFCSample)
  ![image text](https://github.com/CHLRob/PQKit/blob/main/Sample/PQMFCSample/PQKitMFC.png "PQKit MFC Sample")
- [QT](https://github.com/CHLRob/PQKit/tree/main/Sample/PQQTSample)
  ![image text](https://github.com/CHLRob/PQKit/blob/main/Sample/PQQTSample/PQKitQT.png "PQKit QT Sample")

## 联系我们
如果您有需求或者兴趣，可以通过下面邮箱随时联系我们。
ra@robotart.com
