

using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Commons.Bouncycastle.Cert;
using System.Security.Cryptography;
using System.Text;
using iText.Kernel.Geom;
using iText.Forms.Fields.Properties;
using iText.Forms.Form.Element;
using iText.Layout;
using iText.Layout.Element;
using java.io;
using IOException = System.IO.IOException;
using com.sun.tools.@internal.jxc.ap;


namespace MusiFy_Lib
{
     public class CreatePDF

    {
         



       public  void createPDF()
        {
            

            {
                FileStream fileStream = new FileStream(fp , FileMode.Create, FileAccess.Write);
                PdfWriter pdfWriter = new PdfWriter(fileStream);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument, PageSize.A4);
                document.Add(new Paragraph("Hello World!"));
                document.Close();
            }
           

           
        }
   }
}
