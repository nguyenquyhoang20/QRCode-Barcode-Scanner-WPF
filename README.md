# 🎯 QR Code & Barcode Scanner

> A modern WPF MVVM desktop application for real-time QR Code and Barcode scanning using OpenCvSharp and ZXing.NET

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![WPF](https://img.shields.io/badge/WPF-MVVM-blue)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![OpenCV](https://img.shields.io/badge/OpenCV-4.11-green?logo=opencv)](https://github.com/shimat/opencvsharp)
[![ZXing](https://img.shields.io/badge/ZXing-.NET-orange)](https://github.com/micjahn/ZXing.Net)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## ✨ Features

- 📹 **Real-time Camera**: Live video stream from webcam with OpenCvSharp
- 🔍 **Multi-format Support**: QR Code, Code128, Code39, EAN13, EAN8, UPC, DataMatrix, PDF417, Aztec
- 🎨 **Modern UI**: Beautiful dark theme with smooth animations
- 📊 **Scan History**: Store and display all scan results with timestamps
- 📋 **Quick Copy**: Copy scan results to clipboard with one click
- 🔔 **Audio Feedback**: Play beep sound on successful scan
- 🎯 **Visual Bounding Box**: Draw green box around detected codes
- 📈 **Statistics**: Display total scan count

## 🛠️ Tech Stack

- **.NET 9.0** - Core framework
- **WPF** - Windows Presentation Foundation for UI
- **MVVM Pattern** - Application architecture
- **OpenCvSharp4** - Camera capture and image processing
- **ZXing.NET** - Barcode/QR code scanning engine

## 📦 Dependencies

```xml
<PackageReference Include="OpenCvSharp4" Version="4.11.0.20250507" />
<PackageReference Include="OpenCvSharp4.Extensions" Version="4.11.0.20250507" />
<PackageReference Include="OpenCvSharp4.runtime.win" Version="4.11.0.20250507" />
<PackageReference Include="System.Drawing.Common" Version="10.0.0" />
<PackageReference Include="ZXing.Net" Version="0.16.11" />
<PackageReference Include="ZXing.Net.Bindings.Windows.Compatibility" Version="0.16.14" />
```

## 🚀 Getting Started

### Prerequisites
- Windows 10/11
- .NET 9.0 SDK
- Webcam

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/QRCode-Barcode-Scanner-WPF.git
   cd QRCode-Barcode-Scanner-WPF
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

## 📖 Usage

1. **Start Camera**
   - Click "▶️ Start Scanning" button
   - Allow camera access if prompted

2. **Scan Codes**
   - Point camera at QR Code or Barcode
   - Application automatically detects and scans
   - Results display instantly

3. **View History**
   - All scan results saved in right panel
   - Click "📋 Copy" to copy result to clipboard

4. **Stop Scanning**
   - Click "⏹️ Stop Scanning" button to turn off camera

## 🏗️ Project Structure

```
QRBarcodeScanner/
├── Commands/
│   └── RelayCommand.cs          # ICommand implementation
├── Converters/
│   ├── MatToBitmapSourceConverter.cs  # OpenCV Mat → WPF BitmapSource
│   └── ValueConverters.cs       # XAML value converters
├── Models/
│   └── ScanResult.cs            # Scan result model
├── Services/
│   ├── CameraService.cs         # Webcam management
│   └── BarcodeService.cs        # Barcode/QR scanning
├── ViewModels/
│   ├── ViewModelBase.cs         # MVVM base class
│   └── MainViewModel.cs         # Main window ViewModel
├── App.xaml                     # Application resources & styles
├── MainWindow.xaml              # Main UI
└── MainWindow.xaml.cs           # Code-behind
```

## 🎓 Learning Outcomes

This project demonstrates:

### C# Fundamentals & Advanced
- ✅ Variables and data types
- ✅ Xử lý ngoại lệ (try-catch)
- ✅ Collections (ObservableCollection)
- ✅ Class và Object
- ✅ Interface (INotifyPropertyChanged, ICommand)
- ✅ Properties và Events
- ✅ Delegate và Lambda Expression
- ✅ Lập trình bất đồng bộ (async/await, Task)
- ✅ Đa luồng (Threading, CancellationToken)
- ✅ Generic Types
- ✅ Nullable Types

### Module 2: WPF
- ✅ XAML cú pháp và cấu trúc
- ✅ Layout Controls (Grid, StackPanel, Border)
- ✅ Controls cơ bản (Button, TextBox, Image, ListBox)
- ✅ Data Binding
- ✅ Resources và Styles
- ✅ MVVM Pattern
- ✅ INotifyPropertyChanged
- ✅ ICommand
- ✅ RelayCommand/DelegateCommand
- ✅ Converters

### Module 3: OpenCvSharp
- ✅ Cài đặt và cấu hình OpenCvSharp
- ✅ Đối tượng Mat
- ✅ Đọc và xử lý ảnh từ camera
- ✅ Chuyển đổi không gian màu
- ✅ Vẽ hình (Rectangle, Text)

### Module 4: Machine Vision
- ✅ Barcode/QR Code Detection
- ✅ Multi-format Recognition
- ✅ Real-time Processing

## 🐛 Troubleshooting

### Camera không khởi động
- Kiểm tra webcam đã kết nối chưa
- Đảm bảo không có ứng dụng nào khác đang sử dụng camera
- Thử restart ứng dụng

### Không quét được mã
- Đảm bảo mã rõ ràng và đủ sáng
- Giữ mã ổn định trước camera
- Thử điều chỉnh khoảng cách

### Lỗi build
- Chạy `dotnet clean` và `dotnet restore`
- Kiểm tra .NET SDK đã cài đặt đúng phiên bản

## 📝 License

MIT License - Tự do sử dụng cho mục đích học tập và thương mại.

## 👨‍💻 Tác giả

Dự án học tập - Module 3.1-3.4, 4.4

## 🙏 Credits

- [OpenCvSharp](https://github.com/shimat/opencvsharp) - OpenCV wrapper for .NET
- [ZXing.NET](https://github.com/micjahn/ZXing.Net) - Barcode scanning library
