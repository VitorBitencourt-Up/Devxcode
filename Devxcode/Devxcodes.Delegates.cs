using System.Drawing;

namespace Devxcode
{
    public partial class Devxcodes
    {
        public delegate string Default(string NameParametro);
        
        public delegate string ImageBitMap(Bitmap NameParametro);

        public delegate string ErrorDefault(string MensagemPersonalizada);

        public delegate Bitmap Image(string value);

        public delegate string SaveImage(Bitmap img, string diretorio, string filename = "", string extensao = "bmp");

    }
}
