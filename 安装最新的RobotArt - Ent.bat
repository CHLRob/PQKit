
@echo off
>nul 2>&1 "%SYSTEMROOT%\system32\cacls.exe" "%SYSTEMROOT%\system32\config\system"
if '%errorlevel%' NEQ '0' (
	goto UACPrompt
	) else ( goto gotAdmin )
	:UACPrompt
	echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
	echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs"
	"%temp%\getadmin.vbs"
	exit /B
	:gotAdmin
	if exist "%temp%\getadmin.vbs" ( del "%temp%\getadmin.vbs" )
		pushd "%CD%"
		CD /D "%~dp0"

"\\192.168.99.185\RA-Files\Tools\pack\Python36-32\python.exe" "\\192.168.99.185\RA-Files\Tools\pack\Tools\UpdateRobotArtEnt.py"
pause