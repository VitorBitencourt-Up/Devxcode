using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Devxcode;
using WSCorreiosPreco;
using static webServices.AtendeClienteClient;

namespace DevxcodeTester
{
    class Program
    {
        private static bool repeticao = true;

        static void Main(string[] args)
        {
            var opcoes = new List<string>() {
                " 0 -> Sair do Software",
                " 1 -> Geração de QrCode ",
                " 2 -> Bitmap to Base64",
                " 3 -> BitMap parâmetro to Base64",
                " 4 -> Complemento de caracteres",
                " 5 -> IsNumeric"
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
                        case 0:
                            repeticao = false;
                            break;
                        case 1:
                            QrCodes();
                            break;
                        case 2:
                            QrCodes1();
                            break;
                        case 3:
                            QrCodes2();
                            break;
                        case 4:
                            OneLine();
                            break;
                        case 5:
                            OnNumeric();
                            break;
                        case 6:
                            OpenCep();
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
        static void QrCodes1()
        {
            Console.WriteLine("Digite o teste a ser convertido em qrcode");
            Console.WriteLine($"Para testo padrão clique em Enter");

            var texto = Console.ReadLine();

            if (string.IsNullOrEmpty(texto))
                texto = "https://loremipsum.io/generator/?n=5&t=p";


            var imagem = QrCode.ImageBase64(texto);

            Console.WriteLine($"Imagem em base 64: {imagem}");

        }

        static void QrCodes2()
        {
            Console.WriteLine("Digite o teste a ser convertido em qrcode");
            Console.WriteLine($"Para testo padrão clique em Enter");

            var texto = Console.ReadLine();

            if (string.IsNullOrEmpty(texto))
                texto = "https://loremipsum.io/generator/?n=5&t=p";


            var imagemBitMap = QrCode.ImageQrCode(texto);


            var imagem = QrCode.ImageBase64(imagemBitMap);


            Console.WriteLine($"Imagem em base 64: {imagem}");

        }

        static void OneLine()
        {
            var resultado = "3387".CompleteWithCaracters('0', 10, Devxcode.Enumeration.DirectionEnum.Left);
            Console.WriteLine($"Complemento de caracteres a esquerda: {resultado}");

            resultado = "3387".CompleteWithCaracters('0', 10, Devxcode.Enumeration.DirectionEnum.Right);
            Console.WriteLine($"Complemento de caracteres a direita : {resultado}");
        }

        static void OnNumeric()
        {
            while (true)
            {
                var texto = Console.ReadLine();

                var numero = texto.IsNumeric();

                if (numero)
                    Console.WriteLine("É númerico");
                else
                    Console.WriteLine("Não é númerico");
            }
        }

        static void OpenCep()
        {
            try
            {

                System.Threading.Tasks.Task<webServices.consultaCEPResponse> resposta = new webServices.AtendeClienteClient().consultaCEPAsync("13212573");

                resposta.Wait();

                Console.WriteLine("Endereço: {0}", resposta.Result.@return.end);
                Console.WriteLine("Complemento 2: {0}", resposta.Result.@return.complemento2);
                Console.WriteLine("Bairro: {0}", resposta.Result.@return.bairro);
                Console.WriteLine("Cidade: {0}", resposta.Result.@return.cidade);
                Console.WriteLine("Estado: {0}", resposta.Result.@return.uf);
                Console.WriteLine("Unidades de Postagem: {0}", resposta.Result.@return.unidadesPostagem);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao efetuar busca do CEP: {0}", ex.Message);
            }

        }
        
    }
}
