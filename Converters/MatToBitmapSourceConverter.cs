using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Windows.Media.Imaging;

namespace QRBarcodeScanner.Converters
{
    /// <summary>
    /// Converter để chuyển đổi OpenCV Mat sang WPF BitmapSource
    /// </summary>
    public static class MatToBitmapSourceConverter
    {
        /// <summary>
        /// Chuyển đổi Mat sang BitmapSource để hiển thị trong WPF
        /// </summary>
        public static BitmapSource? Convert(Mat? mat)
        {
            if (mat == null || mat.Empty())
                return null;

            try
            {
                // Sử dụng BitmapConverter từ OpenCvSharp.Extensions
                var bitmap = BitmapConverter.ToBitmap(mat);
                var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                
                bitmap.Dispose();
                return bitmapSource;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Chuyển đổi Mat sang WriteableBitmap
        /// </summary>
        public static WriteableBitmap? ConvertToWriteableBitmap(Mat? mat)
        {
            if (mat == null || mat.Empty())
                return null;

            try
            {
                var bitmapSource = Convert(mat);
                if (bitmapSource == null)
                    return null;

                return new WriteableBitmap(bitmapSource);
            }
            catch
            {
                return null;
            }
        }
    }
}
