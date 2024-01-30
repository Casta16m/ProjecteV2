namespace PDFLLEGIR
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pdfViewer2 = new Spire.PdfViewer.Forms.PdfViewer();
            this.SuspendLayout();
            // 
            // pdfViewer2
            // 
            this.pdfViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer2.FindTextHighLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(153)))), ((int)(((byte)(193)))), ((int)(((byte)(218)))));
            this.pdfViewer2.FormFillEnabled = false;
            this.pdfViewer2.IgnoreCase = false;
            this.pdfViewer2.IsToolBarVisible = true;
            this.pdfViewer2.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer2.MultiPagesThreshold = 60;
            this.pdfViewer2.Name = "pdfViewer2";
            this.pdfViewer2.OnRenderPageExceptionEvent = null;
            this.pdfViewer2.Size = new System.Drawing.Size(1192, 337);
            this.pdfViewer2.TabIndex = 0;
            this.pdfViewer2.Text = "pdfViewer2";
            this.pdfViewer2.Threshold = 60;
            this.pdfViewer2.ViewerBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.pdfViewer2.Click += new System.EventHandler(this.pdfViewer2_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1192, 337);
            this.Controls.Add(this.pdfViewer2);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Spire.PdfViewer.Forms.PdfViewer pdfViewer1;
        private Spire.PdfViewer.Forms.PdfViewer pdfViewer2;
    }
}