using IronBarCode;

namespace WebZi.Plataform.CrossCutting.Secutity
{
    public static class QRCodeHelper
    {
        public static byte[] CreateImageAsByteArray(string QRCodeValue, string imageFormat = "JPG")
        {
            GeneratedBarcode GeneratedBarcode = QRCodeWriter.CreateQrCode(QRCodeValue);

            if (imageFormat.Equals("PNG"))
            {
                return GeneratedBarcode.ToPngBinaryData();
            }
            else if (imageFormat.Equals("JPG") || imageFormat.Equals("JPEG"))
            {
                return GeneratedBarcode.ToJpegBinaryData();
            }
            else
            {
                return null;
            }
        }
    }
}