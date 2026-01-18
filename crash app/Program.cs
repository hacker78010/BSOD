using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;

class Program
{
    
    [DllImport("ntdll.dll")]
    public static extern int RtlAdjustPrivilege(int privilege, bool bEnable, bool bCurrentThread, out bool bPrev);

    [DllImport("ntdll.dll")]
    public static extern int RtlSetProcessIsCritical(bool bNew, out bool bOld, bool bNeedFullPriv);

    static void Main(string[] args)
    {
        
        if (!IsAdministrator())
        {
            RestartAsAdmin();
            return;
        }

        Console.WriteLine("=== CRITICAL SYSTEM TOOL ===");
        Console.WriteLine("Press 'y' to trigger BSOD or any other key to exit.");

        if (Console.ReadKey().Key == ConsoleKey.Y)
        {
            Console.WriteLine("\nExecuting...");

        
            
            bool prev;
            RtlAdjustPrivilege(20, true, false, out prev);

            
            RtlAdjustPrivilege(19, true, false, out prev);

            
            bool old;
            int status = RtlSetProcessIsCritical(true, out old, false);

            if (status == 0) // 0 = Success
            {
                
                Environment.Exit(0);
            }
            else
            {
               
                Console.WriteLine("\nFailed! Status code: " + status);
                Console.WriteLine("Try disabling Antivirus/Tamper Protection.");
                Console.ReadLine();
            }
        }
    }

    static bool IsAdministrator()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    static void RestartAsAdmin()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule.FileName,
            UseShellExecute = true,
            Verb = "runas"
        };
        try { Process.Start(startInfo); } catch { }
    }

}
