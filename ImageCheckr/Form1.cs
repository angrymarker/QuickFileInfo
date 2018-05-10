using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;

namespace ImageCheckr
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) { e.Effect = DragDropEffects.Copy; }
            
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            bool longfound = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = new FileInfo(file).Length;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                if (Path.GetFileName(file).Length > Properties.Settings.Default.MaxFileLength)
                {
                    ListViewItem lsvitem = new ListViewItem(Path.GetFileName(file));
                    lsvitem.SubItems.Add(Convert.ToString(Path.GetFileName(file).Length) + " characters");
                    lsvitem.SubItems.Add(String.Format("{0:0.##} {1}", len, sizes[order]));
                    lsvitem.ForeColor = Color.Red;
                    lstFiles.Items.Insert(0, lsvitem);
                    lstFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    longfound = true;
                }
                else if ( len > Properties.Settings.Default.MaxFileSize && order == 2 || order >2)
                {
                    ListViewItem lsvitem = new ListViewItem(Path.GetFileName(file));
                    lsvitem.SubItems.Add(Convert.ToString(Path.GetFileName(file).Length) + " characters");
                    lsvitem.SubItems.Add(String.Format("{0:0.##} {1}", len, sizes[order]));
                    lsvitem.ForeColor = Color.Red;
                    lstFiles.Items.Insert(0, lsvitem);
                    lstFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    longfound = true;
                }
                else
                {
                    ListViewItem lsvitem = new ListViewItem(Path.GetFileName(file));
                    lsvitem.SubItems.Add(Convert.ToString(Path.GetFileName(file).Length) + " characters");
                    lsvitem.SubItems.Add(String.Format("{0:0.##} {1}", len, sizes[order]));
                    lsvitem.ForeColor = Color.Green;
                    lstFiles.Items.Add(lsvitem);
                    lstFiles.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }


            }
            if (longfound == true) {
                MessageBox.Show("Filename over 90 characters found, or size over 10mb!");
            }
        }
    }
}
