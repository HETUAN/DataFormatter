using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFormsXMLFormatter.Common;

namespace WFormsXMLFormatter
{
    public partial class Form7 : Form
    {
        XmlFormatToTreeView tv;
        public Form7()
        {
            InitializeComponent();
            //richTextBox1.MaxLength
            //richTextBox1.MaxLength = Int32.MaxValue;
            textBox2.MaxLength = Int32.MaxValue;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    return;
                }
                label1.Text = "正在转化...";
                tv = new XmlFormatToTreeView(richTextBox1.Text, treeView1);
                if (!tv.Formate())
                {
                    label1.Text = "失败";
                    return;
                }
                textBox2.Text = tv.foramteXml.ToString();
                treeView1.AfterSelect += treeView1_AfterSelect;
                treeView1.ExpandAll();
                label1.Text = "成功";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void treeView1_AfterSelect(object sender, EventArgs e)
        {
            try
            {
                TreeView tv = (TreeView)sender;
                if (tv.SelectedNode == null)
                    return;
                if (tv.SelectedNode.Tag == null)
                    return;
                XmlFormatToStr xfs = new XmlFormatToStr(tv.SelectedNode.Tag.ToString());
                textBox2.Text = xfs.Format();
                Dictionary<string, string> dic = xfs.GetAttributeDic();

                dataGridView1.Rows.Clear();
                int i = 0;
                foreach (KeyValuePair<string, string> item in dic)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = item.Key;
                    row.Cells[1].Value = item.Value;
                    //cell1.Value = item.Key;
                    //DataGridViewCell cell2 = new DataGridViewCell();
                    //cell2.Value = item.Value;
                    //row.Cells.Add();
                    //row.Cells.Add();
                    dataGridView1.Rows.Add(row);
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("暂未开放");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                richTextBox1.SelectAll();
            }
        }

        private void 显示原始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tv == null)
                return;
            richTextBox1.Text = tv.originalXmlStr;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("未开放");
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            MessageBox.Show(path);

            //if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            //{
            //    e.Effect = DragDropEffects.All; 
            //    GetDragData((string[])e.Data.GetData(DataFormats.FileDrop)); 
            //}
        }

        private void GetDragData(string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                string ss = strs[i];
                Console.WriteLine(ss);
            }
        }

        private void Form5_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            MessageBox.Show(path);
        }

    }
}
