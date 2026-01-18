#include <windows.h>
#include <iostream>

typedef NTSTATUS(NTAPI* pRtlSetProcessIsCritical)(BOOLEAN, PBOOLEAN, BOOLEAN);
typedef NTSTATUS(NTAPI* pRtlAdjustPrivilege)(ULONG, BOOLEAN, BOOLEAN, PBOOLEAN);

int main() {
    std::cout << "C++ BSOD Tool v2.0" << std::endl;
    std::cout << "Warning: This will crash your system!" << std::endl;
    std::cout << "Are you sure? (y/n): ";

    char confirm;
    std::cin >> confirm;
    if (confirm != 'y' && confirm != 'Y') return 0;

    HMODULE ntdll = LoadLibraryA("ntdll.dll");
    if (ntdll) {
        auto RtlAdjustPrivilege = (pRtlAdjustPrivilege)GetProcAddress(ntdll, "RtlAdjustPrivilege");
        auto RtlSetProcessIsCritical = (pRtlSetProcessIsCritical)GetProcAddress(ntdll, "RtlSetProcessIsCritical");

        if (RtlAdjustPrivilege && RtlSetProcessIsCritical) {
            BOOLEAN enabled;
            // 20 = SeDebugPrivilege
            RtlAdjustPrivilege(20, TRUE, FALSE, &enabled);

            // Set as critical
            RtlSetProcessIsCritical(TRUE, NULL, FALSE);

            std::cout << "System will crash when this window closes." << std::endl;
            Sleep(2000);
            return 0;
        }
    }
    return 0;
}
