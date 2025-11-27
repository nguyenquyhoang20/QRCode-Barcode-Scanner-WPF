using System.Windows;
using QRBarcodeScanner.ViewModels;

namespace QRBarcodeScanner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            // Cleanup resources
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.Dispose();
            }
            base.OnClosed(e);
        }
    }
}