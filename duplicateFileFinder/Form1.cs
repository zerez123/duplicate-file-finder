﻿using System;
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
            folderBrowserDialog1.ShowDialog();
            var files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
            var md5 = MD5.Create();
            foreach (var f in files)
            {
                var data = File.ReadAllBytes(f);
                var m = BitConverter.ToString(md5.ComputeHash(data)).Replace("-", string.Empty);
                FileList.AddFile(Path.GetDirectoryName(f), Path.GetFileName(f), Path.GetExtension(f), m);
                this.dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["gdFolder"].Value = Path.GetDirectoryName(f);
                dataGridView1.Rows[index].Cells["gdFileName"].Value = Path.GetFileName(f);
                dataGridView1.Rows[index].Cells["gdMD5"].Value = m;
                ++index;
            }

            index = 0;
            foreach (var h in FileList.hashs)
            {
                this.dataGridView2.Rows.Add();
                dataGridView2.Rows[index++].Cells[0].Value = h;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
