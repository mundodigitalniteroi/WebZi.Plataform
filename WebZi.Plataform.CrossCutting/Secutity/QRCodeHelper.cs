using System.Drawing;
using System.Drawing.Imaging;

using QRCoder;

namespace WebZi.Plataform.CrossCutting.Secutity
{
    public static class QRCodeHelper
    {
        public static byte[] CreateImageAsByteArray(string QRCodeValue, string imageFormat = "JPG")
        {
            using QRCodeGenerator QrGenerator = new();
            using (QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(QRCodeValue, QRCodeGenerator.ECCLevel.Q))
            {
                using QRCode QrCode = new(QrCodeInfo);
                using (Bitmap QrBitmap = QrCode.GetGraphic(60))
                {
                    return QrBitmap.BitmapToByteArray();
                };
            };
        }

        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using MemoryStream ms = new();
            bitmap.Save(ms, ImageFormat.Png);

            return ms.ToArray();
        }
    }
}