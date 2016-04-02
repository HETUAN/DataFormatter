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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(textBox1.Text);
            XmlFormatTreeView tv = new XmlFormatTreeView(doc, treeView1);
            tv.InitTreeView();
            treeView1.AfterSelect += treeView1_AfterSelect;
        }

        void treeView1_AfterSelect(object sender, EventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (tv.SelectedNode == null)
                return;
            if (tv.SelectedNode.Tag == null)
                return;
            textBox1.Text = tv.SelectedNode.Tag.ToString();
        }
    }

    public class XmlFormatTreeView
    {
        private XmlDocument xmlDocument;
        private TreeView treeView;
        public XmlFormatTreeView(XmlDocument doc, TreeView tview)
        {
            this.xmlDocument = doc;
            this.treeView = tview;
        }

        public void InitTreeView()
        {
            treeView.Nodes.Clear();
            foreach (XmlNode xnode in xmlDocument.ChildNodes)
            {
                TreeNode tnode = new TreeNode();
                tnode.Name = xnode.Name;
                tnode.Text = xnode.Name;
                tnode.ToolTipText = xnode.Value;
                tnode.Tag = xnode.OuterXml; 
                if (xnode.HasChildNodes)
                {
                    RenderTreeNode(tnode, xnode.ChildNodes);
                }
                treeView.Nodes.Add(tnode);
            }
        }


        private void RenderTreeNode(TreeNode treeNode, XmlNodeList nodeList)
        {
            foreach (XmlNode xnode in nodeList)
            {
                TreeNode tnode = new TreeNode();
                tnode.Name = xnode.Name;
                tnode.Text = xnode.Name;
                tnode.ToolTipText = xnode.Value;
                tnode.Tag = xnode.OuterXml; 
                if (xnode.HasChildNodes)
                {
                    if (xnode.ChildNodes[0].NodeType == XmlNodeType.Element)
                        RenderTreeNode(tnode, xnode.ChildNodes);
                }
                treeNode.Nodes.Add(tnode);
            }
        }

    }


}
