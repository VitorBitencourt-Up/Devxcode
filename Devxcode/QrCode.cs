using System;
using QRCoder;
using System.IO;
using System.Drawing;

namespace Devxcode
{
    public static class QrCode
    {
        
        public static Bitmap ImageQrCode(string value)
        {
            var delegacao = new Devxcodes.Image(Devxcodes.Imagem);

            return delegacao.Invoke(value);
        }

        public static string SaveBitMap(Bitmap img, string diretorio, string filename = "", string extensao = "bmp")
        {
            var delegacao = new Devxcodes.SaveImage(Devxcodes.SaveImagem);

            return delegacao.Invoke(img, diretorio, filename, extensao);
        }

        public static string ImageBase64(string value)
        {
            var delegacao = new Devxcodes.Default(Devxcodes.BitmapToBase64);

            return delegacao.Invoke(value);
        }

        public static string ImageBase64(Bitmap value)
        {
            var delegacao = new Devxcodes.ImageBitMap(Devxcodes.BitmapToBase64);

            return delegacao.Invoke(value);
        }

    }
}
