using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFLLEGIR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pdfViewer2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Pdf File|*.pdf";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(opf.FileName))
                {
                    this.pdfViewer1.LoadFromFile(opf.FileName);
                }
            }
        }
    }
}
