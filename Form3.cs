using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WFormsXMLFormatter
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(textBox1.Text);
                XmlFormatTreeView tv = new XmlFormatTreeView(doc, treeView1);
                tv.InitTreeView();
                treeView1.AfterSelect += treeView1_AfterSelect;

                XMLFormat xf = new XMLFormat(doc);
                textBox1.Text = xf.Format();
                treeView1.ExpandAll();
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
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tv.SelectedNode.Tag.ToString());
                XMLFormat xf = new XMLFormat(doc);
                textBox2.Text = xf.Format();
                textBox3.Text = GetAttribute(doc.FirstChild);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetAttribute(XmlNode node)
        {
            if (node.Attributes == null)
                return "";
            StringBuilder attrSb = new StringBuilder();
            foreach (XmlAttribute item in node.Attributes)
            {
                attrSb.AppendLine(" " + item.Name + "=\"" + item.Value + "\" ");
            }
            return attrSb.ToString();
        }
    }
}
