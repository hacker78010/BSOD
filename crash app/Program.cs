using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Principal;

class Program
{
    // Εισαγωγή συναρτήσεων από το ntdll.dll
    [DllImport("ntdll.dll")]
    public static extern int RtlAdjustPrivilege(int privilege, bool bEnable, bool bCurrentThread, out bool bPrev);

    [DllImport("ntdll.dll")]
    public static extern int RtlSetProcessIsCritical(bool bNew, out bool bOld, bool bNeedFullPriv);

    static void Main(string[] args)
    {
        // 1. Έλεγχος αν το πρόγραμμα τρέχει ως Admin
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

            // 2. ΕΝΕΡΓΟΠΟΙΗΣΗ ΠΡΟΝΟΜΙΩΝ (Το βήμα που έλειπε)
            // Το 20 είναι το SeDebugPrivilege, απαραίτητο για critical processes
            bool prev;
            RtlAdjustPrivilege(20, true, false, out prev);

            // Το 19 είναι το SeShutdownPrivilege
            RtlAdjustPrivilege(19, true, false, out prev);

            // 3. Εντολή BSOD
            bool old;
            int status = RtlSetProcessIsCritical(true, out old, false);

            if (status == 0) // 0 = Success
            {
                // Το σύστημα θα κρασάρει ακαριαία εδώ
                Environment.Exit(0);
            }
            else
            {
                // Αν αποτύχει, θα μας πει τον κωδικό
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