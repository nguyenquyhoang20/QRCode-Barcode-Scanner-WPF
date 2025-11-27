using System;

namespace QRBarcodeScanner.Models
{
    /// <summary>
    /// Model chứa thông tin kết quả quét mã
    /// </summary>
    public class ScanResult
    {
        /// <summary>
        /// Nội dung của mã được quét
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Loại mã (QR Code, Code128, EAN13, etc.)
        /// </summary>
        public string BarcodeFormat { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian quét
        /// </summary>
        public DateTime ScanTime { get; set; }

        /// <summary>
        /// Độ tin cậy của kết quả quét (0-100)
        /// </summary>
        public int Confidence { get; set; }

        public ScanResult()
        {
            ScanTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{ScanTime:HH:mm:ss}] {BarcodeFormat}: {Content}";
        }
    }
}
