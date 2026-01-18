# 💀 Ultimate Windows BSOD Collection: The Kernel Research Project

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![C++](https://img.shields.io/badge/Language-C%2B%2B-red.svg)](https://visualstudio.microsoft.com/vs/features/cplusplus/)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078d7.svg)](https://www.microsoft.com/windows)
[![Version](https://img.shields.io/badge/Version-2.0.0-green.svg)](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/releases)

A high-performance, multi-layered repository exploring the Windows NT Kernel's error-handling mechanisms. This project demonstrates how to programmatically trigger a **Blue Screen of Death (BSOD)** using various methods, ranging from high-level scripts to native kernel-level API calls.

---

## ⚠️ MANDATORY SAFETY NOTICE & LEGAL DISCLAIMER
**BY PROCEEDING, YOU ACKNOWLEDGE THE FOLLOWING:**

* **INSTANT CRASH:** These tools are designed to destabilize the Windows Kernel immediately.
* **ZERO DATA RETENTION:** Any unsaved buffers (Office documents, browser sessions, gaming progress) will be purged.
* **VIRTUALIZATION ONLY:** It is strictly recommended to run these binaries within an isolated **Virtual Machine (VM)** like VMware Workstation, Oracle VirtualBox, or Microsoft Hyper-V.
* **NO LIABILITY:** The developer(s) of this project assume no responsibility for destroyed operating systems, hardware failure, or any legal repercussions resulting from the misuse of these tools.

---

## 🚀 Quick Access (Ready-to-Use)
| Download Link | Contents | Target Architecture |
| :--- | :--- | :--- |
| [**Download v2.0 Release**](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/releases/latest) | .exe (C# & C++), .bat, .vbs | x64 / x86 Windows |

---

## 📁 Repository Overview

### 1. 🟥 C++ Native Implementation (`/cpp_version`)
* **Technology:** Win32 API / NTAPI (Native API).
* **Compilation:** MSVC (Visual Studio) / MinGW (g++).
* **Logic:** Dynamic loading of `ntdll.dll` and execution of `RtlSetProcessIsCritical`.
* **Advantages:** Zero dependencies. No .NET runtime required. Tiny binary footprint.

### 2. 🟦 C# .NET Framework Version (`/exe_bsod`)
* **Technology:** .NET Interop / P-Invoke.
* **Compilation:** Visual Studio (C# compiler).
* **Logic:** Managed wrapper around unmanaged Windows DLLs.
* **Advantages:** Readable code, easier to modify for UI-based applications.

### 3. 📄 Legacy Scripting Suite
* **Batch (`crash.bat`):** Exploits command-line process termination.
* **VBScript (`crash.vbs`):** Uses Windows Script Host to invoke shell commands.

---

## 🔬 In-Depth Technical Analysis

### Understanding `ntdll.dll`
Most Windows applications communicate with the kernel through `kernel32.dll` or `user32.dll`. However, this project goes deeper by targeting `ntdll.dll`, the gateway between User Mode and Kernel Mode.

#### The `RtlSetProcessIsCritical` Function
This is an undocumented function within the Windows Native API. Its signature is:
```cpp
NTSTATUS RtlSetProcessIsCritical(
    BOOLEAN NewValue, 
    PBOOLEAN OldValue, 
    BOOLEAN NeedStop
);
