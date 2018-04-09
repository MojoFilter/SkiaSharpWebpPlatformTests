using Imaging;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImagingClient.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            byte[] webp = File.ReadAllBytes("fireguy.webp");
            byte[] png = ImageConvert.WebpToPng(webp);
            var bmp = new BitmapImage();
            var stream = new MemoryStream(png);
            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();
            this.Img.Source = bmp;
        }
    }
}
