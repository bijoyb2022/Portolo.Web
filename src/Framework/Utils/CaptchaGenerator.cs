using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Portolo.Framework.Utils
{
    public class CaptchaGenerator
    {
        private readonly Random random = new Random();

        public CaptchaGenerator(int width, int height)
        {
            this.CaptchaCode = this.GenerateCode();
            this.SetDimensions(width, height);
            this.GenerateImage();
        }

        public string CaptchaCode { get; }
        public Bitmap Image { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Image.Dispose();
            }
        }

        private string GenerateCode()
        {
            var randomCode = new Random();
            var retVal = string.Empty;

            for (var j = 0; j < 4; j++)
            {
                var i = randomCode.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = randomCode.Next(1, 9);
                        retVal += ch.ToString();
                        break;
                    case 2:
                        ch = randomCode.Next(65, 90);
                        retVal += Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = randomCode.Next(65, 90);
                        retVal += Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = randomCode.Next(65, 90);
                        retVal += Convert.ToChar(ch).ToString();
                        break;
                }

                randomCode.NextDouble();
                randomCode.Next(100, 1999);
            }

            return retVal;
        }

        private void SetDimensions(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
            }

            this.Width = width;
            this.Height = height;
        }

        private void GenerateImage()
        {
            var bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.Default;
            var rect = new Rectangle(0, 0, this.Width, this.Height);
            var hatchBrush = new HatchBrush(HatchStyle.DottedGrid, Color.LightGray, Color.LightGray);
            g.FillRectangle(hatchBrush, rect);
            SizeF size;
            float fontSize = rect.Height - 1;
            Font font;

            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericMonospace, fontSize, FontStyle.Bold);
                size = g.MeasureString(this.CaptchaCode, font);
            }
            while (size.Width > rect.Width);

            var format = new StringFormat();
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Center;
            var path = new GraphicsPath();

            path.AddString(this.CaptchaCode, font.FontFamily, (int)font.Style, 40, rect, format);
            var v = 5F;
#pragma warning disable SA1407 // Arithmetic expressions should declare precedence
            PointF[] points =
            {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
            };
#pragma warning restore SA1407 // Arithmetic expressions should declare precedence
            var matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            hatchBrush = new HatchBrush(HatchStyle.DottedGrid, Color.White, Color.Blue);
            g.FillPath(hatchBrush, path);
            var m = Math.Max(rect.Width, rect.Height);
            for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                var x = this.random.Next(rect.Width);
                var y = this.random.Next(rect.Height);
                var w = this.random.Next(m / 50);
                var h = this.random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            this.Image = bitmap;
        }
    }
}