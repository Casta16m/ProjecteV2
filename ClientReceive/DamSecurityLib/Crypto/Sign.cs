
using DAMSecurityLib.Certificates;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Bouncycastle.Crypto;
using iText.Bouncycastle.X509;
using iText.Commons.Bouncycastle.Cert;
using Org.BouncyCastle.Crypto;
using System.Security.Cryptography;
using System.Text;
using iText.Kernel.Geom;
using iText.Forms.Fields.Properties;
using iText.Forms.Form.Element;
using System.Runtime.CompilerServices;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using iText.Layout.Properties;
using iText.Signatures;
using Path = System.IO.Path;

namespace DAMSecurityLib.Crypto
{
    /// <summary>
    /// This class is used to sign documents 
    /// </summary>
    public class Sign
    {

        #region Private attributes

        private X509Certificate2? certificate;
        private Certificates.CertificateInfo? certificateInfo;
        private Pkcs12Store pkcs12Store = new Pkcs12StoreBuilder().Build();
        private string storeAlias = "";

        #endregion

        /// <summary>
        /// Init class certificate attributes with the disk certificate
        /// </summary>
        /// <param name="pfxFileName">Certificate file disk path</param>
        /// <param name="pfxPassword">Certificate password</param>
        public void InitCertificate(string pfxFileName, string pfxPassword)
        {
            certificate = new X509Certificate2(pfxFileName, pfxPassword);

            pkcs12Store.Load(new FileStream(pfxFileName, FileMode.Open, FileAccess.Read), pfxPassword.ToCharArray());
            foreach (string currentAlias in pkcs12Store.Aliases)
            {
                if (pkcs12Store.IsKeyEntry(currentAlias))
                {
                    storeAlias = currentAlias;
                    break;
                }
            }
            certificateInfo = Certificates.CertificateInfo.FromCertificate(pfxFileName, pfxPassword);
        }

        /// <summary>
        /// Sign pdf document and save result to disk.
        /// This method puts digital signature inside pdf document
        /// </summary>
        /// <param name="inputFileName">Input pdf file path to sign</param>
        /// <param name="outputFileName">Ouput pdf file path to save the result file</param>
        /// <param name="showSignature">If signatature is visible in pdf document</param>
        public void SignPdf(string inputFileName, string outputFileName, bool showSignature)
        {
            AsymmetricKeyParameter key = pkcs12Store.GetKey(storeAlias).Key;

            X509CertificateEntry[] chainEntries = pkcs12Store.GetCertificateChain(storeAlias);
            IX509Certificate[] chain = new IX509Certificate[chainEntries.Length];
            for (int i = 0; i < chainEntries.Length; i++)
                chain[i] = new X509CertificateBC(chainEntries[i].Certificate);
            PrivateKeySignature signature = new PrivateKeySignature(new PrivateKeyBC(key), "SHA256");

            using (PdfReader pdfReader = new PdfReader(inputFileName))
            using (FileStream result = File.Create(outputFileName))
            {
                PdfSigner pdfSigner = new PdfSigner(pdfReader, result, new StampingProperties().UseAppendMode());

                if (showSignature)
                {
                    CreateSignatureApperanceField(pdfSigner);
                }

                pdfSigner.SignDetached(signature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
            }
        }
        /// <summary>
        /// This Function creates a pdf document in memory and signs it to PDF
        /// </summary>
        /// <param name="outputFileName"></param>
        /// <param name="showSignature"></param>
        public void CreateAndSignPdf(string outputFileName, bool showSignature)
        {

            //Create original PDF in memory 
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                document.Add(new Paragraph("Hello World!"));
                document.Close();
                //Sign the PDF in memory
                AsymmetricKeyParameter key = pkcs12Store.GetKey(storeAlias).Key;
                X509CertificateEntry[] chainEntries = pkcs12Store.GetCertificateChain(storeAlias);
                IX509Certificate[] chain = new IX509Certificate[chainEntries.Length];
                for (int i = 0; i < chainEntries.Length; i++)
                {
                    chain[i] = new X509CertificateBC(chainEntries[i].Certificate);
                }

                PrivateKeySignature signature = new PrivateKeySignature(new PrivateKeyBC(key), "SHA256");

                using (MemoryStream signedMs = new MemoryStream())
                {
                    PdfReader reader = new PdfReader(new MemoryStream(ms.ToArray()));


                    PdfSigner signer = new PdfSigner(reader, signedMs, new StampingProperties().UseAppendMode());
                    if (showSignature == true)
                    {
                        CreateSignatureApperanceField(signer);


                        // Save signed PDF to disk
                        signer.SignDetached(signature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                        File.WriteAllBytes(outputFileName, signedMs.ToArray());


                    }
                    if (showSignature == false)
                    {
                        signer.SignDetached(signature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);
                        File.WriteAllBytes(outputFileName, signedMs.ToArray());
                    }

                }
            }
        }

        /// <summary>
        /// Sign filedisk file with the global class certificate
        /// </summary>
        /// <param name="inputFileName">Filedisk input file path to sign</param>
        /// <param name="outputFileName">Filedisk output file path to save the result</param>
        public void SignFile(string inputFileName, string outputFileName)
        {
            if (certificate != null)
            {
                byte[] inputBytes = File.ReadAllBytes(inputFileName);
                byte[] outputBytes = SignDocument(certificate, inputBytes);

                File.WriteAllBytes(outputFileName, outputBytes);
            }
        }





        /// <summary>
        /// Returns SHA-256 HASH from input byte array
        /// </summary>
        /// <param name="input">Input byte array to obtain SHA-256 HASH</param>
        /// <returns>SHA-256 HASH</returns>
        public string SHA256Hash(byte[] input)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    builder.Append(input[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        /// <summary>
        /// Create a pdf document with the text "Hello World!"
        /// </summary>
        /// <param name="path"></param>
        public void CreatePdf(string path)
        {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            document.Add(new Paragraph("Hello World!"));
            document.Close();

        }


        /// <summary>
        /// Sign byte array document with the certificate
        /// </summary>
        /// <param name="certificate">Certificated used to sign the document</param>
        /// <param name="document">Document byte array to sign</param>
        /// <returns>Byte array with the signed document</returns>
        internal static byte[] SignDocument(X509Certificate2 certificate, byte[] document)
        {
            ContentInfo contentInfo = new ContentInfo(document);
            SignedCms signedCms = new SignedCms(contentInfo, false);
            CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
            signedCms.ComputeSignature(signer);

            return signedCms.Encode();
        }

        /// <summary>
        /// Adds signature field rectangle inside pdf document
        /// </summary>
        /// <param name="pdfSigner">PdfSigner used to sign document</param>
        internal void CreateSignatureApperanceField(PdfSigner pdfSigner)
        {
            var pdfDocument = pdfSigner.GetDocument();
            var pageRect = pdfDocument.GetPage(1).GetPageSize();
            var size = new PageSize(pageRect);
            pdfDocument.AddNewPage(size);
            var totalPages = pdfDocument.GetNumberOfPages();
            float yPos = pdfDocument.GetPage(totalPages).GetPageSize().GetHeight() - 100;
            float xPos = 0;
            Rectangle rect = new Rectangle(xPos, yPos, 200, 100);

            pdfSigner.SetFieldName("signature");

            SignatureFieldAppearance appearance = new SignatureFieldAppearance(pdfSigner.GetFieldName())
                    .SetContent(new SignedAppearanceText()
                        .SetSignedBy(certificateInfo?.Organization)
                        .SetReasonLine("" + " - " + "")
                        .SetLocationLine("Location: " + certificateInfo?.Locality)
                        .SetSignDate(pdfSigner.GetSignDate()));

            pdfSigner.SetPageNumber(totalPages).SetPageRect(rect)
                    .SetSignatureAppearance(appearance);

        }
        /// <summary>
        /// function to create a pdf document with a table, a header, a subheader, a paragraph and a footer
        /// </summary>
        /// <param name="path"></param>
        public void CreateDetailedPdf(string path)
        {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Add header
            Paragraph header = new Paragraph("Capçalera de la pàgina")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(header);

            // Add sub-header
            Paragraph subHeader = new Paragraph("Sub-capçalera")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15);
            document.Add(subHeader);

            // Add table
            Table table = new Table(3, false);
            for (int i = 0; i < 9; i++)
            {
                table.AddCell(new Cell().Add(new Paragraph(i.ToString())));
            }
            document.Add(table);

            // Add paragraph
            Paragraph paragraph = new Paragraph("Aquest és un paràgraf. Proporciona informació més detallada.")
                .SetTextAlignment(TextAlignment.JUSTIFIED);
            document.Add(paragraph);

            // Add footer
            Paragraph footer = new Paragraph("Peu de pàgina")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);
            document.Add(footer);

            document.Close();
        }
        /// <summary>
        /// this Function signs a pdf document and encrypts it with a password
        /// </summary>
        /// <param name="src"></param>
        /// <param name="password"></param>
        /// <param name="showSignature"></param>
        /// <param name="outFile"></param>
        public void SignAndEncryptPdf(string src, string password, bool showSignature, string outFile)
        {
            string tempFile = Path.GetTempFileName();
            string signedFile = Path.GetTempFileName();

            // Load the document
            using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(src), new PdfWriter(tempFile)))
            {
                Document document = new Document(pdfDoc);

                // Close the document before signing
                document.Close();
            }

            // Sign the document
            SignPdf(tempFile, signedFile, showSignature);

            // Encrypt the document
            using (PdfWriter writer = new PdfWriter(outFile, new WriterProperties().SetStandardEncryption(Encoding.ASCII.GetBytes(password), null, EncryptionConstants.ENCRYPTION_AES_128, EncryptionConstants.ENCRYPTION_AES_256)))
            using (PdfDocument encryptedPdfDoc = new PdfDocument(new PdfReader(signedFile), writer))
            {
                // Close the encrypted document
                encryptedPdfDoc.Close();
            }

            // Delete the temporary files
            File.Delete(tempFile);
            File.Delete(signedFile);
        }

        public static bool? ValidatePdfSignature(string signedPdfPath, object certInfo)
        {
            throw new NotImplementedException();
        }
    }
}
