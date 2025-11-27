using OpenCvSharp;
using OpenCvSharp.Extensions;
using QRBarcodeScanner.Models;
using System;
using System.Drawing;
using ZXing;
using ZXing.Windows.Compatibility;

namespace QRBarcodeScanner.Services
{
    /// <summary>
    /// Service quét barcode và QR code sử dụng ZXing.NET
    /// </summary>
    public class BarcodeService
    {
        private readonly BarcodeReader _reader;
        private string _lastScannedContent = string.Empty;
        private DateTime _lastScanTime = DateTime.MinValue;

        public BarcodeService()
        {
            _reader = new BarcodeReader();
            _reader.AutoRotate = true;
            _reader.Options = new ZXing.Common.DecodingOptions
            {
                TryHarder = true,
                PossibleFormats = new[]
                {
                    BarcodeFormat.QR_CODE,
                    BarcodeFormat.CODE_128,
                    BarcodeFormat.CODE_39,
                    BarcodeFormat.EAN_13,
                    BarcodeFormat.EAN_8,
                    BarcodeFormat.UPC_A,
                    BarcodeFormat.UPC_E,
                    BarcodeFormat.DATA_MATRIX,
                    BarcodeFormat.PDF_417,
                    BarcodeFormat.AZTEC
                }
            };
        }

        /// <summary>
        /// Quét mã từ OpenCV Mat
        /// </summary>
        public ScanResult? ScanBarcode(Mat frame)
        {
            if (frame == null || frame.Empty())
                return null;

            try
            {
                // Chuyển đổi Mat sang Bitmap
                using var bitmap = BitmapConverter.ToBitmap(frame);
                
                // Quét mã
                var result = _reader.Decode(bitmap);

                if (result != null)
                {
                    // Tránh quét trùng lặp trong thời gian ngắn
                    if (result.Text == _lastScannedContent && 
                        (DateTime.Now - _lastScanTime).TotalMilliseconds < 1000)
                    {
                        return null;
                    }

                    _lastScannedContent = result.Text;
                    _lastScanTime = DateTime.Now;

                    return new ScanResult
                    {
                        Content = result.Text,
                        BarcodeFormat = result.BarcodeFormat.ToString(),
                        ScanTime = DateTime.Now,
                        Confidence = 100 // ZXing không cung cấp confidence score
                    };
                }
            }
            catch
            {
                // Bỏ qua lỗi và tiếp tục
            }

            return null;
        }

        /// <summary>
        /// Vẽ bounding box xung quanh mã được quét
        /// </summary>
        public void DrawBarcodeBox(Mat frame, ScanResult scanResult)
        {
            if (frame == null || frame.Empty() || scanResult == null)
                return;

            try
            {
                using var bitmap = BitmapConverter.ToBitmap(frame);
                var result = _reader.Decode(bitmap);

                if (result?.ResultPoints != null && result.ResultPoints.Length > 0)
                {
                    // Tìm bounding box
                    var points = result.ResultPoints;
                    var minX = int.MaxValue;
                    var minY = int.MaxValue;
                    var maxX = int.MinValue;
                    var maxY = int.MinValue;

                    foreach (var point in points)
                    {
                        minX = Math.Min(minX, (int)point.X);
                        minY = Math.Min(minY, (int)point.Y);
                        maxX = Math.Max(maxX, (int)point.X);
                        maxY = Math.Max(maxY, (int)point.Y);
                    }

                    // Vẽ rectangle
                    Cv2.Rectangle(frame, 
                        new OpenCvSharp.Point(minX, minY), 
                        new OpenCvSharp.Point(maxX, maxY),
                        new Scalar(0, 255, 0), 3);

                    // Vẽ text
                    Cv2.PutText(frame, scanResult.BarcodeFormat,
                        new OpenCvSharp.Point(minX, minY - 10),
                        HersheyFonts.HersheySimplex, 0.7,
                        new Scalar(0, 255, 0), 2);
                }
            }
            catch
            {
                // Bỏ qua lỗi vẽ
            }
        }

        /// <summary>
        /// Reset trạng thái để cho phép quét lại cùng một mã
        /// </summary>
        public void Reset()
        {
            _lastScannedContent = string.Empty;
            _lastScanTime = DateTime.MinValue;
        }
    }
}
