using Imaging;
using System.IO;
using Xamarin.Forms;

namespace ImagingClient.XamarinForms
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            var assembly = this.GetType().Assembly;
            var webpStream = assembly.GetManifestResourceStream("ImagingClient.XamarinForms.fireguy.webp");
            byte[] webp;
            using (var ms = new MemoryStream())
            {
                webpStream.CopyTo(ms);
                webp = ms.ToArray();
            }
            byte[] png = ImageConvert.WebpToPng(webp);
            this.Img.Source = ImageSource.FromStream(() => new MemoryStream(png));
		}
	}
}
