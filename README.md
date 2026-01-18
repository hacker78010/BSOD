# 💀 Ultimate Windows BSOD Collection

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Language: C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Platform: Windows](https://img.shields.io/badge/Platform-Windows-0078d7.svg)](https://www.microsoft.com/windows)

A collection of scripts and tools to trigger a **Blue Screen of Death (BSOD)** on Windows for educational purposes and testing in Virtual Machines.

---

## ⚠️ CRITICAL WARNING / DISCLAIMER
**This project causes an immediate system crash.**
* All unsaved work **WILL BE LOST**.
* Use this **ONLY** in Virtual Machines (VMware, VirtualBox) or test environments.
* I am NOT responsible for any data loss or damage to your system. **Use at your own risk.**

---

## 🚀 Download Ready-to-Run Tool
If you don't want to compile the code yourself, download the compiled executable from the Releases section:
👉 **[Download BSOD Tool (.zip)](https://github.com/YOUR_USERNAME/YOUR_REPO_NAME/releases/latest)**

---

## 📁 Repository Structure

### 1. ⚡ C# Method (Native API)
Located in the `/exe_bsod` folder. This is the most advanced method using `ntdll.dll`.
* **Files:** `Program.cs`, `bsodcreator.csproj`, `App.config`.
* **How it works:** It elevates process privileges to `SeDebugPrivilege` and marks the process as **Critical**. When the process terminates, Windows assumes a vital system component failed and triggers a `CRITICAL_PROCESS_DIED` BSOD.

### 2. 📜 Batch Script (.bat)
A simple script that attempts to trigger a crash via system commands.
* **File:** `crash.bat`
* **Usage:** Right-click -> Run as Administrator.

### 3. 📝 VBScript (.vbs)
A classic script-based approach using Windows Script Host.
* **File:** `crash.vbs`

---

## 🛠️ Usage Instructions (C# Version)

1.  Download the `.zip` from the **Releases** section.
2.  Extract the archive.
3.  If Windows Defender blocks it, allow the file or temporarily disable real-time protection (this tool uses Native APIs often flagged as "Riskware").
4.  Right-click `bsodcreator.exe` -> **Run as Administrator**.
5.  Type `y` in the console to confirm and trigger the crash.

---

## 💻 How to Build from Source
If you want to modify or audit the code:
1.  Open the `.csproj` file with **Visual Studio**.
2.  Set the build configuration to **Release**.
3.  Press `Build Solution` (Ctrl+Shift+B).
4.  Your executable will be located in the `bin/Release` folder.

---

## 📸 Expected Result
Upon successful execution, your system will display the following:
![BSOD Example](https://upload.wikimedia.org/wikipedia/commons/5/56/Bsod_windows_10.png)

---

## ⚖️ License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
