# 🎧 RTSS Headset Battery Info (WPF)

**RTSS Headset Battery Info** is a **C# WPF application** that displays the **battery level of a wireless headset** directly in the **RivaTuner Statistics Server (RTSS) OSD**.

The application uses **headsetcontrol** to retrieve headset information and displays it in real time through RTSS.

---

## 📷 Showcase

<img width="581" height="289" alt="RTSS OSD Example" src="https://github.com/user-attachments/assets/fc7c5c18-bf6b-4583-aed9-a1303fe39e85" />
<img width="27" height="28" alt="Battery Icon" src="https://github.com/user-attachments/assets/a8fd0a0b-99c0-4929-9175-2b61bdfc8867" />
<img width="25" height="27" alt="Charging Icon" src="https://github.com/user-attachments/assets/19da4b9f-8bc1-4526-a078-b8305f061811" />
<img width="506" height="224" alt="WPF UI Example" src="https://github.com/user-attachments/assets/8596ce5c-12ed-4936-8611-52493efd9121" />

---

## ✨ Features

- 🔋 Displays headset battery percentage
- 🎧 Automatically detects the headset model
- 🔌 Indicates whether the headset is connected or powered off
- 🔄 Auto-refresh every 2 seconds
- 📊 Native integration with RTSS OSD
- 🖼️ Lightweight and responsive WPF interface

---

## 📦 Requirements

- **Windows**
- **.NET** (compatible with RTSSSharedMemoryNET)
- **RivaTuner Statistics Server (RTSS)** installed and running
- **headsetcontrol** installed and available in the system `PATH`  
  👉 https://github.com/Sapd/HeadsetControl

---

## 🛠️ Dependencies

- [RTSSSharedMemoryNET](https://github.com/spencerhakim/RTSSSharedMemoryNET)
- `headsetcontrol.exe`
- WPF (.NET Desktop Runtime)

---

## 🚀 Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/RTSSHeadsetBatteryInfo.git
