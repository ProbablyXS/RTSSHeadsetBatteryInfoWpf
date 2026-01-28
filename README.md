# 🎧 RTSS Headset Battery Info (WPF)

RTSS Headset Battery Info est une application **WPF en C#** qui affiche le **niveau de batterie d’un casque sans fil** directement dans le **RivaTuner Statistics Server (RTSS) OSD**.

L’application utilise **headsetcontrol** pour récupérer les informations du casque et les affiche en temps réel grâce à RTSS.

---

## 📷 Showcase
<img width="581" height="289" alt="{9F9AC018-CF73-4C6F-8409-9E58AA0FD503}" src="https://github.com/user-attachments/assets/fc7c5c18-bf6b-4583-aed9-a1303fe39e85" />
<img width="506" height="224" alt="WPF UI Example" src="https://github.com/user-attachments/assets/8596ce5c-12ed-4936-8611-52493efd9121" />

---

## ✨ Features

- 🔋 Affiche le pourcentage de batterie du casque
- 🎧 Détecte automatiquement le modèle du casque
- 🔌 Indique si le casque est connecté ou éteint
- 🔄 Actualisation automatique toutes les 2 secondes
- 📊 Intégration native dans le OSD de RTSS
- 🖼️ Interface graphique WPF légère et responsive

---

## 📦 Requirements

- **Windows**
- **.NET** (compatible avec RTSSSharedMemoryNET)
- **RivaTuner Statistics Server (RTSS)** installé et en cours d’exécution
- **headsetcontrol** installé et disponible dans le PATH  
  👉 https://github.com/Sapd/HeadsetControl

---

## 🛠️ Dépendances

- [`RTSSSharedMemoryNET`](https://github.com/spencerhakim/RTSSSharedMemoryNET)
- `headsetcontrol.exe`
- WPF (.NET Desktop Runtime)

---

## 🚀 Installation

1. Clonez le dépôt :
   ```bash
   git clone https://github.com/your-username/RTSSHeadsetBatteryInfo.git
