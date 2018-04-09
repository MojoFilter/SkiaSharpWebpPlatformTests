using SkiaSharp;
using SkiaSharp.Views.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpImaging
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".webp");
            var file = await picker.PickSingleFileAsync();
            this.imagePanel.Children.Clear();
            if (file != null)
            {           
                foreach (var child in await this.GetDerivatives(file))
                {
                    this.imagePanel.Children.Add(child);
                }
            }
           
        }

        private async Task<IEnumerable<UIElement>> GetDerivatives(StorageFile file)
        {
            var ret = new List<UIElement>();
            byte[] originalData;
            using (var inputStream = await file.OpenReadAsync())
            using (var stream = inputStream.AsStreamForRead())
            using (var reader = new DataReader(inputStream.GetInputStreamAt(0)))
            {
                await reader.LoadAsync((uint)inputStream.Size);
                originalData = new byte[inputStream.Size];
                reader.ReadBytes(originalData);
            }
            ret.Add(this.Rendering(originalData, "Original"));
            var formats = new[] { SKEncodedImageFormat.Jpeg, SKEncodedImageFormat.Png, SKEncodedImageFormat.Webp };
            foreach (var format in formats)
            {
                ret.Add(ConvertedRendering(originalData, format));
            }
            return ret;
        }

        private UIElement ConvertedRendering(byte[] data, SKEncodedImageFormat format)
         => Rendering(Convert(data, format), format.ToString());

        private byte[] Convert(byte[] orig, SKEncodedImageFormat format)
        {
            using (var origBmp = SKBitmap.Decode(orig))
            using (var stream = new SKDynamicMemoryWStream())
            {
                origBmp.Encode(stream, format, 100);
                return stream.DetachAsData().ToArray();
             }
        }

        private UIElement Rendering(byte[] resourceStream, string title)
        {
            var control = new SKXamlCanvas();
            var bmp = SKBitmap.Decode(resourceStream);
            var width = bmp.Info.Width;
            var height = bmp.Info.Height;
            control.Width = width;
            control.Height = height;
            control.PaintSurface += (s, e) =>
            {
                var canvas = e.Surface.Canvas;
                canvas.DrawBitmap(bmp, 0, 0);

                var textPos = new SKPoint(width / 2f, height / 2f);
                using (var paint = new SKPaint())
                {
                    paint.TextSize = 45f;
                    paint.IsAntialias = true;
                    paint.TextAlign = SKTextAlign.Center;

                    paint.Color = SKColors.Cyan;
                    canvas.DrawText(title, textPos, paint);

                    paint.Color = SKColors.Crimson;
                    paint.IsStroke = true;
                    paint.StrokeWidth = 2f;
                    canvas.DrawText(title, textPos, paint);
                }
            };
            return control;
        }
    }
}
