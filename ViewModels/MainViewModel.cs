using OpenCvSharp;
using QRBarcodeScanner.Commands;
using QRBarcodeScanner.Converters;
using QRBarcodeScanner.Models;
using QRBarcodeScanner.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QRBarcodeScanner.ViewModels
{
    /// <summary>
    /// ViewModel chính của ứng dụng
    /// </summary>
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private readonly CameraService _cameraService;
        private readonly BarcodeService _barcodeService;

        private BitmapSource? _currentFrame;
        private ScanResult? _lastScanResult;
        private bool _isScanning;
        private string _statusMessage = "Sẵn sàng";
        private int _totalScans;

        public MainViewModel()
        {
            _cameraService = new CameraService();
            _barcodeService = new BarcodeService();

            // Subscribe to camera events
            _cameraService.FrameCaptured += OnFrameCaptured;

            // Initialize commands
            StartScanCommand = new RelayCommand(_ => StartScanning(), _ => !IsScanning);
            StopScanCommand = new RelayCommand(_ => StopScanning(), _ => IsScanning);
            ClearHistoryCommand = new RelayCommand(_ => ClearHistory(), _ => ScanHistory.Count > 0);
            CopyResultCommand = new RelayCommand(_ => CopyToClipboard(), _ => LastScanResult != null);

            ScanHistory = new ObservableCollection<ScanResult>();
        }

        #region Properties

        /// <summary>
        /// Frame hiện tại từ camera
        /// </summary>
        public BitmapSource? CurrentFrame
        {
            get => _currentFrame;
            set => SetProperty(ref _currentFrame, value);
        }

        /// <summary>
        /// Kết quả quét gần nhất
        /// </summary>
        public ScanResult? LastScanResult
        {
            get => _lastScanResult;
            set => SetProperty(ref _lastScanResult, value);
        }

        /// <summary>
        /// Trạng thái đang quét
        /// </summary>
        public bool IsScanning
        {
            get => _isScanning;
            set
            {
                if (SetProperty(ref _isScanning, value))
                {
                    OnPropertyChanged(nameof(ScanButtonText));
                    ((RelayCommand)StartScanCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)StopScanCommand).RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Thông báo trạng thái
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        /// <summary>
        /// Tổng số lần quét
        /// </summary>
        public int TotalScans
        {
            get => _totalScans;
            set => SetProperty(ref _totalScans, value);
        }

        /// <summary>
        /// Text cho nút quét
        /// </summary>
        public string ScanButtonText => IsScanning ? "Đang quét..." : "Bắt đầu quét";

        /// <summary>
        /// Lịch sử quét
        /// </summary>
        public ObservableCollection<ScanResult> ScanHistory { get; }

        #endregion

        #region Commands

        public ICommand StartScanCommand { get; }
        public ICommand StopScanCommand { get; }
        public ICommand ClearHistoryCommand { get; }
        public ICommand CopyResultCommand { get; }

        #endregion

        #region Methods

        private void StartScanning()
        {
            if (_cameraService.Start())
            {
                IsScanning = true;
                StatusMessage = "Đang quét... Hướng camera vào mã QR/Barcode";
                _barcodeService.Reset();
            }
            else
            {
                StatusMessage = "Lỗi: Không thể khởi động camera";
                MessageBox.Show("Không thể khởi động camera. Vui lòng kiểm tra kết nối camera.",
                    "Lỗi Camera", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopScanning()
        {
            _cameraService.Stop();
            IsScanning = false;
            StatusMessage = "Đã dừng quét";
            CurrentFrame = null;
        }

        private void ClearHistory()
        {
            ScanHistory.Clear();
            TotalScans = 0;
            StatusMessage = "Đã xóa lịch sử";
            ((RelayCommand)ClearHistoryCommand).RaiseCanExecuteChanged();
        }

        private void CopyToClipboard()
        {
            if (LastScanResult != null)
            {
                try
                {
                    Clipboard.SetText(LastScanResult.Content);
                    StatusMessage = "Đã copy vào clipboard";
                }
                catch (Exception ex)
                {
                    StatusMessage = $"Lỗi copy: {ex.Message}";
                }
            }
        }

        private void OnFrameCaptured(object? sender, Mat frame)
        {
            try
            {
                // Quét barcode/QR code
                var scanResult = _barcodeService.ScanBarcode(frame);

                if (scanResult != null)
                {
                    // Vẽ bounding box
                    _barcodeService.DrawBarcodeBox(frame, scanResult);

                    // Update UI trên UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        LastScanResult = scanResult;
                        ScanHistory.Insert(0, scanResult);
                        TotalScans++;
                        StatusMessage = $"Đã quét: {scanResult.BarcodeFormat}";
                        ((RelayCommand)ClearHistoryCommand).RaiseCanExecuteChanged();
                        ((RelayCommand)CopyResultCommand).RaiseCanExecuteChanged();

                        // Phát âm thanh (optional)
                        System.Media.SystemSounds.Beep.Play();
                    });
                }

                // Chuyển đổi frame sang BitmapSource
                var bitmapSource = MatToBitmapSourceConverter.Convert(frame);

                // Update UI
                Application.Current.Dispatcher.Invoke(() =>
                {
                    CurrentFrame = bitmapSource;
                });
            }
            catch
            {
                // Bỏ qua lỗi xử lý frame
            }
            finally
            {
                frame?.Dispose();
            }
        }

        public void Dispose()
        {
            _cameraService.FrameCaptured -= OnFrameCaptured;
            _cameraService.Dispose();
        }

        #endregion
    }
}
