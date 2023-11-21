using System;
using System.Drawing;
using System.IO;
#if NET472 || NET45
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using ZXing;

#endif

namespace Portolo.Framework.Utils
{
    public static class Pdf
    {
        public static void ToPdf(this string content, string path)
        {
            if (content.IsNullOrEmpty())
            {
                throw new ArgumentNullException("Content is not Null Or Empty");
            }

            if (path.IsNullOrEmpty())
            {
                throw new ArgumentNullException("Path is not Null Or Empty");
            }

            ToPdf(content, new FileStream(path, FileMode.Create));
        }

        public static void ToPdf(this string content, Stream stream, bool closeStream = true)
        {
            if (content.IsNullOrEmpty())
            {
                throw new ArgumentNullException("Content is not Null Or Empty");
            }

#if NET472 || NET45
            var html = new StringReader(content);
            using (var document = new Document(PageSize.A4, 0f, 0f, 0f, 0f))
            {
                var writer = PdfWriter.GetInstance(document, stream);
                writer.CloseStream = closeStream;
                document.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }

            if (!closeStream)
            {
                stream.Position = 0;
            }
#else
            throw new InvalidOperationException();
#endif
        }

        public static void ToPdfWithBarcode(this string content, Stream stream, string barcodeText, bool closeStream = true)
        {
            if (content.IsNullOrEmpty())
            {
                throw new ArgumentNullException("Content is not Null Or Empty");
            }

#if NET472 || NET45
            var html = new StringReader(content);
            using (var document = new Document(PageSize.A4, 0f, 0f, 0f, 0f))
            {
                var writer = PdfWriter.GetInstance(document, stream);
                writer.CloseStream = closeStream;
                document.Open();
                if (!string.IsNullOrEmpty(barcodeText))
                {
                    System.Drawing.Image img;
                    BarcodeWriter barcodewriter = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
                    barcodewriter.Options.Height = 30;
                    barcodewriter.Options.Width = 100;
                    barcodewriter.Options.PureBarcode = false;
                    img = barcodewriter.Write(barcodeText);
                    iTextSharp.text.Image ınstance = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Png);
                    ınstance.SetDpi(200, 200);
                    ınstance.SetAbsolutePosition(350, 500);
                    ınstance.ScaleToFit(200f, 180f);
                    document.Add(ınstance);
                }

                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
            }

            if (!closeStream)
            {
                stream.Position = 0;
            }
#else
            throw new InvalidOperationException();
#endif
        }
    }
}