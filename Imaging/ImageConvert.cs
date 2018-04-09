using SkiaSharp;
using System;

namespace Imaging
{
    public static class ImageConvert
    {
        public static byte[] WebpToPng(byte[] webpData)
        {
            using (var origBmp = SKBitmap.Decode(webpData))
            using (var stream = new SKDynamicMemoryWStream())
            {
                origBmp.Encode(stream, SKEncodedImageFormat.Png, 100);
                return stream.DetachAsData().ToArray();
            }
        }
    }
}
