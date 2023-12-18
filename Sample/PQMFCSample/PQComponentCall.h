#pragma once

typedef struct stPathGenerateInfo
{
	int nCountTotal;
	int nIndex;
	ULONG uPathID;
}PQPathGenerateInfo;

typedef struct stPopMenuInfo
{
	ULONG ulObjID;
	long lPosX;
	long lPosY;
}PQPopMenuInfo;

class CPQComponentCall : public IPQPlatformComponentCallBack
{
public:
	CPQComponentCall();
	~CPQComponentCall();

	//IDispatch
	STDMETHOD(GetTypeInfoCount)(UINT FAR* pctinfo);
	STDMETHOD(GetTypeInfo)(UINT itinfo, LCID lcid, ITypeInfo FAR* FAR* pptinfo);
	STDMETHOD(GetIDsOfNames)(REFIID riid, OLECHAR FAR* FAR* rgszNames, UINT cNames, LCID lcid, DISPID FAR* rgdispid);
	STDMETHOD(Invoke)(DISPID dispidMember, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS FAR* pdispparams, VARIANT FAR* pvarResult, EXCEPINFO FAR* pexcepinfo, UINT FAR* puArgErr);

	STDMETHOD(QueryInterface)(const struct _GUID &iid, void ** ppv);
	ULONG __stdcall AddRef(void);
	ULONG __stdcall Release(void);


public:
	//
	HRESULT Fire_Initialize_Result(long lResult);
	HRESULT Fire_RunCMD_Result(long lResult);
	HRESULT Fire_GetData_Result(long lResult);
	HRESULT Notify_Raise_Dockwindow(int i_nType);
	HRESULT Fire_Login_Result(int i_nLoginType);
	HRESULT Fire_Path_Generate_Result(long i_bSuccess, int i_nPathCount, int i_nIndex, unsigned long i_ulPathID);
	HRESULT Fire_Element_Pickup(unsigned long i_ulObjID,long i_lEntityID, int i_nEntityType);
	HRESULT Fire_RButton_Up(long i_lPosX,long i_lPosY);
	HRESULT Fire_LButton_Up(long i_lPosX,long i_lPosY);
	HRESULT Fire_Menu_Pop(unsigned long i_ulObjID,long i_lPosX,long i_lPosY,int * o_nHandled);
	//
	void RegisterUIHWND(HWND i_hWnd);


protected:
	HWND m_hWnd;

};

