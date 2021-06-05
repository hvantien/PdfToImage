using Pdf2Image;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
namespace PdfToImage
{
    class Program
    {
        static void Main(string[] args)
        {
            //string file = "C:\\Users\\hvant\\Documents\\Result.pdf";

            var result = MainAsync().GetAwaiter().GetResult();

            List<System.Drawing.Image> images = PdfSplitter.GetImages(result, PdfSplitter.Scale.High);
            int i = 0;
            foreach (var item in images)
            {
                File.WriteAllBytes("C:\\Users\\hvant\\Documents\\" + i + ".jpg", ImageToByteArray(item));
                i++;
            }
            //PdfSplitter.WriteImages(file, "C:\\Users\\hvant\\Documents", PdfSplitter.Scale.High, PdfSplitter.CompressionLevel.Medium);
        }

        private static async Task<byte[]> MainAsync()
        {
            HttpClient client = new HttpClient();
            return await client.GetByteArrayAsync("http://www.pdf995.com/samples/pdf.pdf");
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
