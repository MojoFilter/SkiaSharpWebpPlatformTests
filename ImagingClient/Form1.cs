using Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagingClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            byte[] webp = File.ReadAllBytes("fireguy.webp");
            byte[] png = ImageConvert.WebpToPng(webp);
            using (var stream = new MemoryStream(png))
            {
                this.pictureBox1.Image = new Bitmap(stream, false);
            }
        }
    }
}
