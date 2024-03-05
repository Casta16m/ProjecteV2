﻿
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
using System.Collections.Generic;
using java.awt;







namespace MusiFy_Lib
{
    public class CreatePDF

    {


        /// <summary>
        /// Method to create a PDF and save it in the PDF folder 
        /// </summary>
        /// <param name="pdfName">Name of the PDF file</param>
        /// <param name="content">Content to write on the PDF</param>
        /// <param name="pdfSize">Size of the PDF</param>
        public void createPDF(List<string?> content, string filePath)
        {
            try
            {
                PdfWriter pdfWriter = new PdfWriter(filePath);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument, PageSize.A4);

                foreach (string? s in content)
                {
                    document.Add(new Paragraph("- "));
                    document.Add(new Paragraph($"-> {s}"));
                }
                document.Close();
                pdfDocument.Close();
                pdfWriter.Close();

            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí

            }


        }


        






    }
}
