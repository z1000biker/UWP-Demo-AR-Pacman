# 🟡 PacMan Video UWP AR Demo

A fun **UWP (Universal Windows Platform)** demo app built with **C# and XAML** that displays your **webcam feed** and overlays an animated, moving **Pac-Man** character that opens and closes its mouth while gliding back and forth across the screen.

[https://github.com/<your-username>/PacMan-Video-UWP-AR-Demo](https://github.com/z1000biker/UWP-Demo-AR-Pacman/blob/master)

---

🚀 Features
📷 Real-time webcam preview using MediaCapture

🟡 Animated Pac-Man (mouth opens/closes smoothly)

↔️ Bidirectional movement with automatic flip at window edges

🪟 Modern UWP XAML interface

⚡ Lightweight, 100% managed C# — no external game engine required

🧩 Technologies Used
Component	Purpose
C# / .NET 9 (UWP)	Core app framework
XAML	UI layout and overlay
MediaCapture API	Webcam access
DispatcherTimer	Smooth animation loop
PathGeometry	Procedural Pac-Man drawing

🏗️ Build & Run Instructions
Clone this repository


git clone https://github.com/<your-username>/PacMan-Video-UWP-AR-Demo.git
Open the solution in Visual Studio 2026 (or 2022+)

Ensure UWP workload and Windows 11 SDK (10.0.22621+) are installed

Enable Developer Mode on Windows:

Settings → System → For Developers → Developer Mode: ON

Press F5 (Run)

When launched, your webcam activates and Pac-Man appears at the upper-left corner, happily chomping away as he moves across the video.

⚙️ Project Structure

App1/
│
├── MainPage.xaml              # UI layout with camera and overlay
├── MainPage.xaml.cs           # App logic + Pac-Man animation
├── App.xaml / App.xaml.cs     # UWP app entry point
└── Assets/                    # (optional) icons, demo media
🧠 How It Works
The app uses UWP’s MediaCapture API to show a live webcam feed inside a CaptureElement.
On top of that, a Canvas hosts a PathGeometry Pac-Man shape, drawn mathematically as a circle with a wedge-mouth.
A DispatcherTimer continuously:

Moves Pac-Man’s X-position,

Flips him horizontally when reaching edges,

Oscillates the mouth angle to simulate chomping.

🪪 Window Title
The window title is dynamically set via:


ApplicationView.GetForCurrentView().Title = "PacMan Video UWP AR Demo";



📜 License
MIT License © 2025 — Feel free to modify and use for learning or demos.
Created by Nik
