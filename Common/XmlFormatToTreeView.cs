using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WFormsXMLFormatter.Common
{
    public class XmlFormatToTreeView
    {
        private int deep = 0;
        public string originalXmlStr { get; set; }
        private XmlDocument originalXmlDoc = new XmlDocument();
        public StringBuilder foramteXml = new StringBuilder();
        public TreeView treeView;

        public XmlFormatToTreeView(string xmlStr, TreeView treeView)
        {
            try
            {
                this.originalXmlStr = xmlStr;
                string ss = this.DealXmlStr();
                this.originalXmlDoc.LoadXml(ss);
                this.treeView = treeView;
                this.Analyze(this.originalXmlDoc.ChildNodes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ResetXmlStr(string xmlStr)
        {
            try
            {
                this.originalXmlStr = xmlStr;
                this.originalXmlDoc.LoadXml(this.DealXmlStr()); 
                this.Analyze(this.originalXmlDoc.ChildNodes);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #region 格式化成TreeView
        public bool Formate()
        {
            try
            {
                treeView.Nodes.Clear();
                foreach (XmlNode xnode in originalXmlDoc.ChildNodes)
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
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

        private string DealXmlStr()
        {
            try
            {
                Regex reg = new Regex("<!--.*?-->");
                return reg.Replace(this.originalXmlStr, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }
        #endregion

        #region 格式化成StringBuilder
        private void Analyze(XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);
                if (!node.HasChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        this.foramteXml.Append(this.GetTabs());
                        this.foramteXml.Append("<" + nodes.Item(i).Name + this.GetAttribute(node) + ">");
                        this.foramteXml.Append(node.Value);
                        this.foramteXml.AppendLine("</" + nodes.Item(i).Name + ">");
                    }
                    else
                    {
                        this.foramteXml.Append(node.Value);
                    }
                }
                else
                {
                    if (node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType != XmlNodeType.Element)
                    {
                        this.foramteXml.Append(this.GetTabs());
                        this.foramteXml.Append("<" + node.Name + this.GetAttribute(node) + ">");
                        this.deep++;
                        this.Analyze(node.ChildNodes);
                        //this.foramteXml.Append(this.GetTabs());
                        this.foramteXml.AppendLine("</" + node.Name + ">");
                    }
                    else
                    {
                        this.foramteXml.Append(this.GetTabs());
                        this.foramteXml.AppendLine("<" + node.Name + this.GetAttribute(node) + ">");
                        this.deep++;
                        this.Analyze(node.ChildNodes);
                        this.foramteXml.Append(this.GetTabs());
                        this.foramteXml.AppendLine("</" + node.Name + ">");
                    }
                }
            }
            this.deep--;
        }

        private string GetTabs()
        {
            StringBuilder tabSb = new StringBuilder();
            tabSb.Append("");
            for (int i = 0; i < this.deep; i++)
            {
                tabSb.Append("      ");
            }
            return tabSb.ToString();
        }

        private string GetAttribute(XmlNode node)
        {
            StringBuilder attrSb = new StringBuilder();
            foreach (XmlAttribute item in node.Attributes)
            {
                attrSb.Append(" " + item.Name + "=\"" + item.Value + "\" ");
            }
            return attrSb.ToString();
        }
        #endregion
    }
}
