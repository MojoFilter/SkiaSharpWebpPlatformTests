using Imaging;
using System;
using System.IO;

namespace ImagingClient.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] webp = File.ReadAllBytes("fireguy.webp");
            byte[] png = ImageConvert.WebpToPng(webp);
            File.WriteAllBytes("output.png", png);
        }
    }
}
