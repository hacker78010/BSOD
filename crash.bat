@echo off

echo msgbox "Are you sure for BSOD?", 16, "System Warning" > %temp%\msg.vbs
wscript %temp%\msg.vbs
if %errorlevel%==2 exit


net session >nul 2>&1
if %errorLevel% == 0 (
    goto :run_crash
) else (
    echo Set UAC = CreateObject^("Shell.Application"^) > "%temp%\getadmin.vbs"
    echo UAC.ShellExecute "%~s0", "", "", "runas", 1 >> "%temp%\getadmin.vbs"
    "%temp%\getadmin.vbs"
    del "%temp%\getadmin.vbs"
    exit /B
)

:run_crash

powershell -Command "$p=[RunspaceFactory]::CreateRunspace().Debugger; Add-Type -TypeDefinition 'using System; using System.Runtime.InteropServices; public class B { [DllImport(\"ntdll.dll\")] public static extern int RtlSetProcessIsCritical(bool b1, out bool b2, bool b3); }'; $o=$false; [B]::RtlSetProcessIsCritical($true, [ref]$o, $false); exit"

pause