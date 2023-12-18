
// RPCDlgDlg.h : header file
//

#pragma once
#include <atlcomcli.h>

#include "PQComponentCall.h"
#include <vector>
#include <string>
using namespace std;

// CPQMFCSampleDlg dialog
class CPQMFCSampleDlg : public CDialogEx
{
// Construction
public:
	CPQMFCSampleDlg(CWnd* pParent = NULL);	// standard constructor
	~CPQMFCSampleDlg();
// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_RPCDLG_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnShowWindow(BOOL bShow, UINT nStatus);
	afx_msg LRESULT msgInitialize(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT msgLogin(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT msgPathGenerate(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT msgLBtnUp(WPARAM wParam, LPARAM lParam);
	afx_msg LRESULT msgPopMenu(WPARAM wParam, LPARAM lParam);

	afx_msg void OnBnClickedButtonOpenfile();
	afx_msg void OnBnClickedButtonIK();
	afx_msg void OnBnClickedButtonABSJoint();
	afx_msg void OnBnClickedButtonFaceShape();
	afx_msg void OnBnClickedButtonGeneratePath();
	
	afx_msg void OnClose();
	afx_msg void OnMenuItemaA();
	afx_msg void OnMenuItemaB();
public:
	void ShowPlarformWindow();
	void ShowChildWindow(HWND i_hWnd,UINT i_uResID);
	void GetObjIDByName(int i_nType, std::wstring i_wsName,ULONG &o_uID);

public:
	CComPtr<IPQPlatformComponent> m_ptrComponent;
	CPQComponentCall m_ptrComponentCall;
	
	HWND m_hKitWnd;

	ULONG m_ulGeneratePathID;


	DECLARE_MESSAGE_MAP()
	
	
};

