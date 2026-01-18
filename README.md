# 💀 Ultimate Windows BSOD Collection: Advanced System Internals & Kernel Research

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![C++](https://img.shields.io/badge/Language-C%2B%2B-red.svg)](https://visualstudio.microsoft.com/vs/features/cplusplus/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078d7.svg)](https://www.microsoft.com/windows)
[![Research](https://img.shields.io/badge/Field-Cybersecurity-informational.svg)](https://github.com/YOUR_USERNAME)

Welcome to the most comprehensive repository for simulating system-level failures on Windows environments. This project is a specialized toolkit designed to demonstrate how the Windows NT Kernel handles critical process termination and security privileges.

---

## 🛑 MANDATORY LEGAL & SAFETY DISCLAIMER
**ATTENTION: READ THIS BEFORE PROCEEDING**

This software is provided for **educational and research purposes only**. By using this repository, you acknowledge and agree to the following:
1. **System Instability:** These tools are engineered to bypass standard OS protections and force a Kernel Panic (BSOD).
2. **Permanent Data Loss:** Any unsaved data in the system's memory will be lost immediately. The CPU will stop executing instructions to prevent potential data corruption.
3. **Hardware Risk:** While rare, repeated forced shutdowns can, in some cases, lead to file system corruption or hardware wear.
4. **Authorized Use Only:** You must only execute these files on hardware or virtual environments that you own or have explicit permission to test.

---

## 🚀 Quick Start Guide
To begin testing immediately, navigate to the [Releases](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/releases) section and download the latest compiled bundle.

| Asset | Description | Stability |
| :--- | :--- | :--- |
| `bsod_csharp.exe` | C# Implementation (.NET 4.7.2+) | High |
| `bsod_cpp.exe` | Native C++ (No Dependencies) | Maximum |
| `crash_script.bat` | Batch Scripting Method | Medium |
| `crash_vbs.vbs` | VBScript Automation Method | Low |

---

## 📁 Detailed Methodology & Modules

### 1. 🟥 Native C++ Core (`/cpp_version`)
This is the flagship module of the project. It operates at the lowest possible user-land level by directly interfacing with the Windows Native API.

* **Technology:** C++17 with Win32/NTAPI linking.
* **Mechanism:** It dynamically resolves the address of `RtlSetProcessIsCritical` from `ntdll.dll` at runtime.
* **Security Context:** The binary includes an embedded XML Manifest that forces the Windows User Account Control (UAC) to prompt for Administrator privileges upon execution.
* **Key Advantage:** It does not require any installed runtimes (like .NET or Java), making it perfect for "bare-bones" Windows testing.

### 2. 🟦 C# .NET Wrapper (`/exe_bsod`)
A managed implementation that showcases the power of **Platform Invocation Services (P/Invoke)**.

* **Technology:** C# targeting .NET Framework 4.7.2.
* **Mechanism:** Uses `[DllImport("ntdll.dll")]` to bridge the gap between managed C# code and unmanaged Windows system files.
* **Workflow:**
    1.  The program identifies its own Process ID (PID).
    2.  It elevates its token to `SeDebugPrivilege`.
    3.  It invokes the critical flag.
* **Key Advantage:** Highly readable and modifiable code for developers familiar with the .NET ecosystem.

### 3. 📄 Scripting & Automation (`/scripts`)
Lightweight methods for quick environment testing without the need for binaries.
* **Batch:** Utilizes a loop of task-kill commands against system processes or forced memory overflows.
* **VBS:** Uses the `WScript.Shell` object to interact with the OS environment.

---

## 🧠 Deep Technical Analysis: The Anatomy of a BSOD

### The Role of `ntdll.dll`
Unlike standard applications that use `kernel32.dll`, our advanced tools bypass the Win32 subsystem. We target `ntdll.dll`, which is the "Native API" used by the kernel itself.

### Privilege Escalation: `RtlAdjustPrivilege`
Before the OS allows a process to become "Critical," the process must prove it has sufficient authority. We use `RtlAdjustPrivilege` to enable **Privilege 20** (`SeDebugPrivilege`). This allows the process to inspect and act upon the memory of other processes and the kernel itself.

### The Kill Switch: `RtlSetProcessIsCritical`
When this function is called with a `TRUE` parameter:
1.  The Kernel modifies the **EPROCESS** structure in the system's memory.
2.  The `BreakOnTermination` flag is set to 1.
3.  When the process exits, the Windows Kernel's **Process Manager** detects that a vital component has died.
4.  The Kernel calls `KeBugCheckEx(0xEF)`, resulting in the **CRITICAL_PROCESS_DIED** stop code.

---

## 🛠️ Step-by-Step Compilation Guide

### How to Build the C++ Version
1.  Open **Visual Studio 2022**.
2.  Go to `File > Open > Project/Solution` and select `BSOD_CPP.vcxproj`.
3.  In the top toolbar, set the Solution Configuration to **Release** and Solution Platform to **x64**.
4.  Navigate to `Project Properties > Linker > Input` and ensure `ntdll.lib` is added to **Additional Dependencies**.
5.  Go to `Build > Build Solution`.
6.  Your binary will be located in the `x64/Release/` directory.

### How to Build the C# Version
1.  Open the `.sln` file in Visual Studio.
2.  Ensure the "App.config" is set to the correct .NET version.
3.  Right-click the Solution and select **Rebuild**.
4.  The output will be generated in `bin/Release/`.

---

## 🖥️ Safe Testing Environment Setup
To observe a BSOD without risking your main PC, follow these steps:
1.  **Virtualization:** Install [VMware Player](https://www.vmware.com/) or [VirtualBox](https://www.virtualbox.org/).
2.  **OS Installation:** Install a fresh copy of Windows 10 or 11.
3.  **Snapshotting:** Create a "Clean State" snapshot.
4.  **Debugging (Optional):** Install **WinDbg** on the host machine to analyze the memory dumps created by the crash.

---

## 🔍 Frequently Asked Questions (FAQ)

**Q: Is this a virus?**
**A:** No. This is a technical demonstration tool. A virus replicates and steals data; these tools simply trigger a built-in Windows error-handling routine.

**Q: Why does Windows Defender flag the .exe?**
**A:** Defender is trained to block any program that tries to mark itself as a "Critical Process," as this is a technique used by some high-level malware to prevent being closed by Antivirus software.

**Q: Can I use this to "prank" friends?**
**A:** **NO.** This is a destructive action that results in data loss. This tool is for authorized research environments only.

**Q: Will this break my hardware?**
**A:** In 99.9% of cases, no. A simple reboot restores the PC. However, forced shutdowns are never 100% risk-free for hard drives.

---

## 📈 Roadmap & Future Development
- [ ] **Release 3.0:** Integration of a Kernel-Mode Driver (.sys) for Ring 0 execution.
- [ ] **GUI Manager:** A C# WPF application to control all methods from a single dashboard.
- [ ] **Technical Wiki:** Detailed documentation of every NTAPI function used.
- [ ] **ARM64 Support:** Compatibility for Windows on ARM devices (Surface Pro, etc.).

---

## 🤝 Contributing & Feedback
I welcome contributions from the community!
* **Found a bug?** Open an [Issue](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/issues).
* **Have a new method?** Submit a [Pull Request](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/pulls).
* **Show Support:** Give the project a ⭐ to help others find this research tool!

---

## ⚖️ License
This project is licensed under the **MIT License**. You are free to use, copy, and modify the code, provided the original disclaimer and author credits are included in your version.

**Contact Information:**
* **GitHub:** [@YOUR_USERNAME](https://github.com/YOUR_USERNAME)
* **Website:** [Your_Portfolio_Link]
