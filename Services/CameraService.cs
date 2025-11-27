using OpenCvSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QRBarcodeScanner.Services
{
    /// <summary>
    /// Service quản lý webcam và capture video stream
    /// </summary>
    public class CameraService : IDisposable
    {
        private VideoCapture? _capture;
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _isRunning;

        /// <summary>
        /// Event được raise khi có frame mới từ camera
        /// </summary>
        public event EventHandler<Mat>? FrameCaptured;

        /// <summary>
        /// Kiểm tra camera có đang chạy không
        /// </summary>
        public bool IsRunning => _isRunning;

        /// <summary>
        /// Khởi động camera
        /// </summary>
        public bool Start(int cameraIndex = 0)
        {
            if (_isRunning)
                return false;

            try
            {
                _capture = new VideoCapture(cameraIndex);
                
                if (!_capture.IsOpened())
                {
                    _capture.Dispose();
                    _capture = null;
                    return false;
                }

                // Cấu hình camera
                _capture.Set(VideoCaptureProperties.FrameWidth, 640);
                _capture.Set(VideoCaptureProperties.FrameHeight, 480);
                _capture.Set(VideoCaptureProperties.Fps, 30);

                _isRunning = true;
                _cancellationTokenSource = new CancellationTokenSource();

                // Bắt đầu capture frames trong background
                Task.Run(() => CaptureLoop(_cancellationTokenSource.Token));

                return true;
            }
            catch
            {
                Stop();
                return false;
            }
        }

        /// <summary>
        /// Dừng camera
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _cancellationTokenSource?.Cancel();
            
            _capture?.Release();
            _capture?.Dispose();
            _capture = null;

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        /// <summary>
        /// Vòng lặp capture frames liên tục
        /// </summary>
        private void CaptureLoop(CancellationToken cancellationToken)
        {
            using var frame = new Mat();

            while (_isRunning && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (_capture == null || !_capture.IsOpened())
                        break;

                    _capture.Read(frame);

                    if (!frame.Empty())
                    {
                        // Clone frame để tránh vấn đề threading
                        var clonedFrame = frame.Clone();
                        FrameCaptured?.Invoke(this, clonedFrame);
                    }

                    // Delay nhỏ để tránh CPU quá tải
                    Thread.Sleep(33); // ~30 FPS
                }
                catch
                {
                    // Tiếp tục nếu có lỗi tạm thời
                    Thread.Sleep(100);
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
