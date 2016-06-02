using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

//ESSAS SAO AS BIBLIOTECAS QUE DEVEREMOS ADICIONAR EM NOSSO PROJETO
using System.IO;      //A BIBLIOTECA DE ENTRADA E SAIDA DE ARQUIVOS


using iTextSharp;//E A BIBLIOTECA ITEXTSHARP E SUAS EXTENÇÕES
using iTextSharp.text;//ESTENSAO 1 (TEXT)
using iTextSharp.text.pdf;//ESTENSAO 2 (PDF)
using iTextSharp.text.html;//ESTENSAO 3 (HTML)

//UTILIZANDO ABCPDF
using WebSupergoo.ABCpdf6;

namespace ItextConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //#region iTextSharp

            string imagepath = System.IO.Path.GetFullPath("Imagens"); //Server.MapPath("Images");
                                                                      //Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                                                                      //doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
                                                                      //doc.AddCreationDate();//adicionando as configuracoes

            ////caminho onde sera criado o pdf + nome desejado
            ////OBS: o nome sempre deve ser terminado com .pdf
            //string caminho = @"F:\" + "CONTRATO.pdf";

            ////criando o arquivo pdf embranco, passando como parametro a variavel                
            ////doc criada acima e a variavel caminho 
            ////tambem criada acima.
            //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            //doc.Open();
            //Image image = Image.GetInstance(imagepath + "/Rio.png");
            //doc.Add(image);

            ////criando uma string vazia
            //string dados = "";

            ////criando a variavel para paragrafo
            //Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
            ////etipulando o alinhamneto
            //paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
            ////Alinhamento Justificado
            ////adicioando texto
            //paragrafo.Add("TESTE TESTE TESTE");
            ////acidionado paragrafo ao documento
            //doc.Add(paragrafo);
            ////fechando documento para que seja salva as alteraçoes.
            //doc.Close();

            //#endregion

            #region ABCPDF

            ConstruirArquivo ConstruirArquivo = new ConstruirArquivo();

            Doc theDoc = new Doc();

            theDoc.HtmlOptions.PageCacheEnabled = false;
            theDoc.HtmlOptions.AddForms = true;
            theDoc.HtmlOptions.AddLinks = false;
            theDoc.HtmlOptions.AddMovies = false;
            theDoc.HtmlOptions.FontEmbed = false;
            theDoc.HtmlOptions.UseResync = false;
            theDoc.HtmlOptions.UseVideo = false;
            theDoc.HtmlOptions.UseScript = false;
            theDoc.HtmlOptions.HideBackground = true;
            theDoc.HtmlOptions.Timeout = 100000;
            theDoc.HtmlOptions.ImageQuality = 101;

            theDoc.MediaBox.String = "A4";
            theDoc.MediaBox.Height = 841.5; 
            theDoc.MediaBox.Width = 595.3;
            theDoc.Rect.String = theDoc.MediaBox.String;

            theDoc.Page = theDoc.AddPage();

            string itemCorpo = "ABCPDF";
            string caminho = imagepath + "/Fundo.jpg";

            string item = string.Empty;
            item = ConstruirArquivo.ConstruirCorpoHTML(itemCorpo);
            theDoc.AddHtml(item);
            theDoc.AddImageFile(caminho);

            Byte[] b = theDoc.GetData();

            System.IO.File.WriteAllBytes(@"F:\\myfile.pdf", b);

            #endregion

        }


    }


    public class ConstruirArquivo
    {

        public string ConstruirCorpoHTML(string item)
        {
            item = item.Trim();
            if (!string.IsNullOrEmpty(item))
            {
                return String.Concat("<html><header><title></title></header><body>", item.Replace("<TBODY>", string.Empty).Replace("</TBODY>", string.Empty), "</body></html>");
            }
            return string.Empty;
        }


    }
}

