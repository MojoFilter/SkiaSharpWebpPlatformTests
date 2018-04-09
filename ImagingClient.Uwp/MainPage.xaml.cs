using Imaging;
using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ImagingClient.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var resources = this.GetType().Assembly.GetManifestResourceNames();
            var resourceStream = this.GetType().Assembly.GetManifestResourceStream(resources[0]);
            byte[] webp = new byte[resourceStream.Length];
            await resourceStream.ReadAsync(webp, 0, webp.Length);
            byte[] png = ImageConvert.WebpToPng(webp);
            var bmp = new BitmapImage();
            using (var stream = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(png);
                    await writer.StoreAsync();
                }
                await bmp.SetSourceAsync(stream);
                this.Img.Source = bmp;
            }
        }
    }
}
