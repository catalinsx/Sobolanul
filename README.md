<p align="center">
  <img src="https://images.down.monster/XUDA2/SekEtaJo72.jpg/raw" alt="sobolanul RAT"/ width = ""/>
</p>

# 🐀 Sobolanul - Remote Access Trojan (RAT)

> This is a WinForms-based educational RAT (Remote Access Trojan) built in C# for understanding networking, multithreading, client-server communication, and Windows internals.

---

## ⚙️ Build Requirements

### 🖥 sobolanul-server

- **.NET 8**
- Requires `rcedit-x86.exe` in the same folder (used to set client icon)
- Requires the `sobolanul-client` compiled `.exe`

### 🖥 sobolanul-client

- **.NET Framework 4.8**

---

## 🚀 Features

- ✅ **Keylogger** (with active window logging and mouse movement detection)
- ✅ **Execute commands remotely** via shell (`cmd`)
- ✅ **Live screenshots** (multi-display support)
- ✅ **File manager** — browse remote filesystem and download files
- ✅ **Open arbitrary URLs** on the victim’s machine via default browser
- ✅ **WinForms UI** with rounded corners and context menus per client

---

## 🧪 Build & Run

1. Build the `sobolanul-client`
2. Build the `sobolanul-server` (targeting .NET 8)
3. Place `rcedit-x86.exe` next to `sobolanul-server.exe`
4. Run `sobolanul-server`
5. Open the **Builder** in server:
   - Set IP and Port
   - Optionally select a `.ico` file
   - Click **Build** → generates `Built.exe`
6. Execute `Built.exe` on target system (test VM recommended)

---

## 🖼 Screenshots

<p align="center">
   <img src="https://images.down.monster/XUDA2/deWopuWi47.png/raw">
</p>

---

## 🧾 VirusTotal Report

> Scan of `Built.exe` using [VirusTotal](https://www.virustotal.com/)

<p align="center">
   <img src="https://images.down.monster/XUDA2/fEhAyEFE32.jpeg/raw">
</p>
