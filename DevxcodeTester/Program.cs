using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Devxcode;

namespace DevxcodeTester
{
    class Program
    {
        private static bool repeticao = true;

        static void Main(string[] args)
        {
            var opcoes = new List<string>() { 
                " 0 -> Sair do Software",
                " 1 -> Geração de QrCode " 
            };



            Console.WriteLine("*****************************************************");
            Console.WriteLine("* Aplicação de Testes de Bibliotecas Personalizadas *");
            Console.WriteLine("*****************************************************");


            while (repeticao)
            {
                Console.WriteLine($"\nDigite o número de uma das opções a seguir");

                foreach (var opcao in opcoes)
                    Console.WriteLine(opcao);

                
                var valor = Console.ReadLine();

                if (valor.IsNumeric())
                {
                    var value = Int64.Parse(valor);

                    switch (value)
                    {
                        case 0: repeticao = false;
                            break;
                        case 1:  QrCodes();
                            break;
                        default:
                            Console.WriteLine("Opção inválida, por favor selecionar uma das opções listadas.");
                            break;
                    }

                }
                else
                    Console.WriteLine($"Opção inválida, por favor selecionar uma das opções listadas");
            }

            Console.ReadKey();
        }

        static void QrCodes()
        {
            Console.WriteLine("Digite o teste a ser convertido em qrcode");
            Console.WriteLine($"Para testo padrão clique em Enter");

            var texto = Console.ReadLine();

            if (string.IsNullOrEmpty(texto))
                texto = "https://loremipsum.io/generator/?n=5&t=p";


            var imagem = QrCode.ImageQrCode(texto);


            var dir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program)).Location) + "\\imagem";

            var diretorioSalvo = QrCode.SaveBitMap(imagem, dir);

            Console.WriteLine($"Arquivo salvo em : {diretorioSalvo}");


        }
    }
}
