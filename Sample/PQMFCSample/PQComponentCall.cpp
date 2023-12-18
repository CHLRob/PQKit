#include "stdafx.h"
#include "PQComponentCall.h"

CPQComponentCall::CPQComponentCall()
{
	m_hWnd = NULL;
}


CPQComponentCall::~CPQComponentCall()
{
}

STDMETHODIMP CPQComponentCall::GetTypeInfoCount(UINT FAR* pctinfo)
{
	return S_OK;
}

STDMETHODIMP CPQComponentCall::GetTypeInfo(UINT itinfo, LCID lcid, ITypeInfo FAR* FAR* pptinfo)
{
	return S_OK;
}

STDMETHODIMP CPQComponentCall::GetIDsOfNames(REFIID riid, OLECHAR FAR* FAR* rgszNames, UINT cNames, LCID lcid, DISPID FAR* rgdispid)
{
	return S_OK;
}

STDMETHODIMP CPQComponentCall::Invoke(DISPID dispidMember, REFIID riid, LCID lcid, WORD wFlags, DISPPARAMS FAR* pdispparams, VARIANT FAR* pvarResult, EXCEPINFO FAR* pexcepinfo, UINT FAR* puArgErr)
{
	return S_OK;
}

STDMETHODIMP CPQComponentCall::QueryInterface(const struct _GUID &iid, void ** ppv)
{
	*ppv = this;
	return S_OK;
}

ULONG __stdcall CPQComponentCall::AddRef(void)
{
	return 1;
}

ULONG __stdcall CPQComponentCall::Release(void)
{
	return 0;
}

HRESULT CPQComponentCall::Fire_Initialize_Result(long lResult)
{
	::PostMessage(m_hWnd, WM_INIT_MSG,NULL,(LPARAM)lResult);
	return S_OK;
}

HRESULT CPQComponentCall::Fire_RunCMD_Result(long lResult)
{
	return S_OK;
}

HRESULT CPQComponentCall::Fire_GetData_Result(long lResult)
{
	return S_OK;
}

HRESULT CPQComponentCall::Notify_Raise_Dockwindow(int i_nType)
{
	return S_OK;
}

HRESULT CPQComponentCall::Fire_Login_Result(int i_nLoginType)
{
	::PostMessage(m_hWnd, WM_LOGIN_MSG, NULL, i_nLoginType);
	return S_OK;
}

HRESULT CPQComponentCall::Fire_Path_Generate_Result(long i_bSuccess, int i_nPathCount, int i_nIndex, unsigned long i_ulPathID)
{
	PQPathGenerateInfo *pqpInfo = new PQPathGenerateInfo;
	pqpInfo->nCountTotal = i_nPathCount;
	pqpInfo->nIndex = i_nIndex;
	pqpInfo->uPathID = i_ulPathID;
	::PostMessage(m_hWnd, WM_PATHGENERATE_MSG, (WPARAM)pqpInfo, NULL);
	return S_OK;
}

HRESULT CPQComponentCall::Fire_Element_Pickup(unsigned long i_ulObjID, long i_lEntityID, int i_nEntityType)
{
	return S_OK;
}

HRESULT CPQComponentCall::Fire_RButton_Up(long i_lPosX, long i_lPosY)
{
	return S_OK;
}

HRESULT CPQComponentCall::Fire_LButton_Up(long i_lPosX, long i_lPosY)
{
	::PostMessage(m_hWnd, WM_LBTNUP_MSG, (WPARAM)i_lPosX, (LPARAM)i_lPosY);
	return S_OK;
}

HRESULT CPQComponentCall::Fire_Menu_Pop(unsigned long i_ulObjID, long i_lPosX, long i_lPosY, int * o_nHandled)
{
	bool bCustomMenu = false;
	if (bCustomMenu)
	{
		PQPopMenuInfo *pqmInfo = new PQPopMenuInfo;
		pqmInfo->ulObjID = i_ulObjID;
		pqmInfo->lPosX = i_lPosX;
		pqmInfo->lPosY = i_lPosY;
		::PostMessage(m_hWnd, WM_POPMENU_MSG, (WPARAM)pqmInfo, NULL);
		*o_nHandled = 1;
		return S_OK;
	}
	*o_nHandled = 0;
	return S_OK;
}

void CPQComponentCall::RegisterUIHWND(HWND i_hWnd)
{
	m_hWnd = i_hWnd;
}
