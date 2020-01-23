using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace duplicateFileFinder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void buFindFiles_Click(object sender, EventArgs e)
        {
            var index = 0;
            var fdb = new FileDB();
            var fl = new FileList();

            folderBrowserDialog1.ShowDialog();
            var path = folderBrowserDialog1.SelectedPath;
            if (path.Count() > 0)
            {
                var files = fl.GetFileList(path, checkBox1.Checked);
                var md5 = MD5.Create();

                ClearAll();

                foreach (var f in files)
                {
                    try
                    {
                        var data = File.ReadAllBytes(f);
                        var m = BitConverter.ToString(md5.ComputeHash(data)).Replace("-", string.Empty);
                        fdb.AddFile(Path.GetDirectoryName(f), Path.GetFileName(f), Path.GetExtension(f), m);
                        this.dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells["gdFolder"].Value = Path.GetDirectoryName(f);
                        dataGridView1.Rows[index].Cells["gdFileName"].Value = Path.GetFileName(f);
                        dataGridView1.Rows[index].Cells["gdMD5"].Value = m;
                        
                        ++index;
                    }
                    catch { }
                }
            }
            var sig = SignatureList.GetSignatures();
            index = 0;
            var DuplicateExist = false;
            foreach (var s in sig)
            {
                if (s.Count > 1)
                {
                    this.dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells[0].Value = s.Md5;
                    dataGridView2.Rows[index].Cells[1].Value = s.Count.ToString();
                    ++index;
                    DuplicateExist = true;
                }
            }
            if (!DuplicateExist)
                MessageBox.Show("Duplicate files not found");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var signature = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (signature.Count() > 0)
            {
                dataGridView1.Rows.Clear();
                var fl = FileDB.FileListWithSignature(signature);
                var i = 0;
                foreach (var f in fl)
                {
                    this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells["gdFolder"].Value = f.Path; ;
                    dataGridView1.Rows[i].Cells["gdFileName"].Value = f.NameWithExt;
                    dataGridView1.Rows[i].Cells["gdMD5"].Value = f.Md5;
                    i++;
                }

            }
        }

        private void ClearAll ()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            SignatureList.Clear();
            FileDB.Clear();
        }
    }
}
