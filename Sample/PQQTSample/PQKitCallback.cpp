#include "PQKitCallback.h"

CPQKitCallback::CPQKitCallback(QObject *parent)
	: QObject(parent)
{
}

CPQKitCallback::~CPQKitCallback()
{
}

STDMETHODIMP CPQKitCallback::GetTypeInfoCount(UINT FAR* pctinfo)
{
	return S_OK;
}

STDMETHODIMP CPQKitCallback::GetTypeInfo(UINT itinfo, LCID lcid, ITypeInfo FAR* FAR* pptinfo)
{
	return S_OK;
}

STDMETHODIMP CPQKitCallback::GetIDsOfNames(REFIID riid, OLECHAR FAR* FAR* rgszNames, UINT cNames, LCID lcid, DISPID FAR* rgdispid)
{
	return S_OK;
}

STDMETHODIMP CPQKitCallback::Invoke(DISPID dispidMember, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS FAR* pdispparams, VARIANT FAR* pvarResult, EXCEPINFO FAR* pexcepinfo, UINT FAR* puArgErr)
{
	return S_OK;
}

STDMETHODIMP CPQKitCallback::QueryInterface(const struct _GUID &iid, void ** ppv)
{
	*ppv = this;
	return S_OK;
}

ULONG __stdcall CPQKitCallback::AddRef(void)
{
	return 1;
}

ULONG __stdcall CPQKitCallback::Release(void)
{
	return 0;
}

HRESULT CPQKitCallback::Fire_Initialize_Result(long lResult)
{
	emit signalInitializeResult(lResult);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_RunCMD_Result(long lResult)
{
	emit signalRunCMDResult(lResult);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_GetData_Result(long lResult)
{
	emit signalGetDataResult(lResult);
	return S_OK;
}

HRESULT CPQKitCallback::Notify_Raise_Dockwindow(int i_nType)
{
	emit signalRaiseDockwindow(i_nType);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_Login_Result(int i_nLoginType)
{
	emit signalLoginResult(i_nLoginType);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_Path_Generate_Result(long i_bSuccess, int i_nPathCount, int i_nIndex, unsigned long i_ulPathID)
{
	emit signalPathGenerateResult(i_bSuccess, i_nPathCount, i_nIndex, i_ulPathID);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_Element_Pickup(unsigned long i_ulObjID, long i_lEntityID, int i_nEntityType)
{
	emit signalElementPickup(i_ulObjID, i_lEntityID,i_nEntityType);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_RButton_Up(long i_lPosX, long i_lPosY)
{
	emit signalRButtonUp(i_lPosX, i_lPosY);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_LButton_Up(long i_lPosX, long i_lPosY)
{
	emit signalLButtonUp(i_lPosX, i_lPosY);
	return S_OK;
}

HRESULT CPQKitCallback::Fire_Menu_Pop(unsigned long i_ulObjID, long i_lPosX, long i_lPosY, int *o_nHandled)
{
	emit signalMenuPop(i_ulObjID, i_lPosX, i_lPosY, o_nHandled);
	return S_OK;
}
