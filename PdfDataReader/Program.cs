using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Text;

namespace PdfDataReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string pdfPath = "C:\\Users\\umudi\\OneDrive\\Desktop\\Startup\\file2023-2024_unlocked.pdf";

            using (PdfReader reader = new PdfReader(pdfPath))
            {
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    StringBuilder text = new StringBuilder();

                    for (int i = 110; i <= 110; i++)
                    {
                        PdfPage page = pdfDoc.GetPage(i);

                        // FOR LEFT

                        // Define the rectangle for text extraction (adjust as needed)
                        float leftXLeft = 0;  // Adjust as needed for half width
                        float bottomYLeft = 70;
                        float rightXLeft = page.GetPageSize().GetWidth() / 2;  // Adjust as needed for half width
                        float topYLeft = page.GetPageSize().GetHeight() - 160;

                        text.Append(DataReader.ReadTextFromPdf(page, leftXLeft, bottomYLeft, rightXLeft, topYLeft));

                        // FOR RIGHT

                        float leftXRight = page.GetPageSize().GetWidth() / 2;  // Adjust as needed for half width
                        float bottomYRight = 70;
                        float rightXRight = page.GetPageSize().GetWidth();  // Adjust as needed for half width
                        float topYRight = page.GetPageSize().GetHeight() - 160;

                        text.Append(DataReader.ReadTextFromPdf(page, leftXRight, bottomYRight, rightXRight, topYRight));

                        // REPLACE

                        text.Replace('ӂ', 'ə');
                        text.Replace('ˬ', 'ə');
                        text.Replace('ø', 'İ');
                        text.Replace('Õ', 'ı');
                        text.Replace('÷', 'ğ');
                        text.Replace('ú', 'ş');
                        text.Replace('Ӂ', 'Ə');
                        Console.WriteLine(text);
                    }
                }
            }

            #region old
            //Console.OutputEncoding = Encoding.Unicode;
            //string pdfPath = "C:\\Users\\umudi\\Downloads\\file2023-2024_unlocked.pdf";

            //using (PdfReader reader = new PdfReader(pdfPath))
            //{
            //    PdfDocument pdfDoc = new PdfDocument(reader);
            //    StringBuilder text = new StringBuilder();

            //    for (int i = 110; i <= pdfDoc.GetNumberOfPages(); i++)
            //    {
            //        text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
            //        Console.WriteLine(text.ToString());
            //    }

            //    Console.WriteLine(text.ToString());
            //}




            //Console.OutputEncoding = Encoding.Unicode;
            //string pdfPath = "C:\\Users\\umudi\\Downloads\\file2023-2024_unlocked.pdf";

            //using (PdfReader reader = new PdfReader(pdfPath))
            //{
            //    PdfDocument pdfDoc = new PdfDocument(reader);
            //    StringBuilder text = new StringBuilder();

            //    for (int i = 110; i <= pdfDoc.GetNumberOfPages(); i++)
            //    {
            //        PdfPage page = pdfDoc.GetPage(i);

            //        // Define the rectangle for text extraction (adjust as needed)
            //        float leftX = page.GetPageSize().GetWidth() / 4;  // Adjust as needed for half width
            //        float bottomY = 0;
            //        float rightX = page.GetPageSize().GetWidth() / 2;  // Adjust as needed for half width
            //        float topY = page.GetPageSize().GetHeight();

            //        // Define the text extraction region
            //        iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(leftX, bottomY, rightX, topY);

            //        // Set the text extraction region for this page
            //        LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
            //        strategy.AddRenderFilter(new RegionTextRenderFilter(rect));
            //        PdfCanvasProcessor processor = new PdfCanvasProcessor(strategy);
            //        processor.ProcessPageContent(page);

            //        text.Append(strategy.GetResultantText());
            //        Console.WriteLine(text.ToString());
            //    }

            //    Console.WriteLine(text.ToString());
            //}






            //Console.OutputEncoding = Encoding.UTF8;
            //string pdfPath = "C:\\Users\\umudi\\Downloads\\tableNew.pdf";
            //string outputFolderPath = "C:\\Users\\umudi\\Downloads";

            //using (PdfReader reader = new PdfReader(pdfPath))
            //{
            //    PdfDocument pdfDoc = new PdfDocument(reader);
            //    for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            //    {
            //        PdfPage page = pdfDoc.GetPage(i);

            //        PdfCanvasProcessor parser = new PdfCanvasProcessor(new ImageRenderListener(outputFolderPath, i));
            //        parser.ProcessPageContent(page);
            //    }
            //}

            //Console.WriteLine("Images extracted successfully.");
            #endregion
        }
    }

    public static class DataReader
    {
        public static string ReadTextFromPdf(PdfPage page, float leftX, float bottomY, float rightX, float topY)
        {
            // Define the text extraction region
            iText.Kernel.Geom.Rectangle rectLeft = new iText.Kernel.Geom.Rectangle(leftX, bottomY, rightX, topY);

            // Create a filter for the specified region
            TextRegionEventFilter regionFilterLeft = new TextRegionEventFilter(rectLeft);

            // Create a strategy for text extraction
            var strategyLeft = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilterLeft);

            // Extract text using the specified strategy
            string text = PdfTextExtractor.GetTextFromPage(page, strategyLeft);

            text += "\n";

            return text;
        }
    }
}

#region old
//class ImageRenderListener : IEventListener
//{
//    private readonly string outputFolderPath;
//    private readonly int pageNumber;
//    private int imageCounter = 0;

//    public ImageRenderListener(string outputFolderPath, int pageNumber)
//    {
//        this.outputFolderPath = outputFolderPath;
//        this.pageNumber = pageNumber;
//    }

//    public void EventOccurred(IEventData data, EventType type)
//    {
//        if (type == EventType.RENDER_IMAGE)
//        {
//            ImageRenderInfo renderInfo = (ImageRenderInfo)data;
//            PdfImageXObject imageObject = renderInfo.GetImage();
//            byte[] imageBytes = imageObject.GetImageBytes();
//            string imagePath = $"image_{pageNumber}_{imageCounter++}.png";
//            File.WriteAllBytes(Path.Combine(outputFolderPath, imagePath), imageBytes);
//            DataExtractFromPicture.GetText(outputFolderPath + "\\" + imagePath);
//        }
//    }

//    public ICollection<EventType> GetSupportedEvents()
//    {
//        return null;
//    }
//}

//static class DataExtractFromPicture
//{
//    public static void GetText(string imagePath)
//    {
//        using (var engine = new TesseractEngine(@"./tessdata", "aze", EngineMode.Default))
//        {
//            using (var image = Pix.LoadFromFile(imagePath))
//            {
//                using (var page = engine.Process(image))
//                {
//                    string text = page.GetText();
//                    Console.WriteLine(text);
//                }
//            }
//        }
//    }
//}
#endregion