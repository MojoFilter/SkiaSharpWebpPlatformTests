using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Imaging;
using System.IO;

namespace ImagingClient.Android
{
    [Activity(Label = "ImagingClient.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            var input = Assets.Open("fireguy.webp");
            byte[] webp;
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                webp = ms.ToArray();
            }
            byte[] png = ImageConvert.WebpToPng(webp);

            var img = FindViewById<ImageView>(Resource.Id.imageView1);
            var bmp = BitmapFactory.DecodeByteArray(png, 0, png.Length);
            img.SetImageBitmap(bmp);
        }
    }
}

