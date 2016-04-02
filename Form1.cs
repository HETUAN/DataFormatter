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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(textBox1.Text);
            XMLFormat xf = new XMLFormat(doc);
            textBox2.Text = xf.Format();
        }

    }

    public class XMLFormat
    {
        private int deep;
        private StringBuilder sb;
        private XmlDocument doc;
        public XMLFormat(XmlDocument doc)
        {
            deep = 0;
            sb = new StringBuilder();
            this.doc = doc;
            this.Analyze1(this.doc.ChildNodes);
        }

        public string Format()
        {
            return this.sb.ToString();
        }

        private void Analyze(XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);
                if (!node.HasChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.Append("<" + nodes.Item(i).Name + this.GetAttribute(node) + ">");
                        this.sb.Append(node.Value);
                        this.sb.AppendLine("</" + nodes.Item(i).Name + ">");
                    }
                    else
                    {
                        this.sb.Append(this.GetTabs());
                        //this.sb.Append("<" + nodes.Item(i).Name + ">");
                        this.sb.AppendLine(node.Value);
                        // this.sb.AppendLine("</" + nodes.Item(i).Name + ">");
                    }
                }
                else
                {
                    this.sb.Append(this.GetTabs());
                    this.sb.AppendLine("<" + node.Name + this.GetAttribute(node) + ">");
                    this.deep++;
                    this.Analyze(node.ChildNodes);
                    this.sb.Append(this.GetTabs());
                    this.sb.AppendLine("</" + node.Name + ">");
                }
            }
            this.deep--;
        }


        private void Analyze1(XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);
                if (!node.HasChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.Append("<" + nodes.Item(i).Name + this.GetAttribute(node) + ">");
                        this.sb.Append(node.Value);
                        this.sb.AppendLine("</" + nodes.Item(i).Name + ">");
                    }
                    else
                    { 
                        this.sb.Append(node.Value); 
                    }
                }
                else
                {
                    if (node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType != XmlNodeType.Element)
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.Append("<" + node.Name + this.GetAttribute(node) + ">");
                    }
                    else
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.AppendLine("<" + node.Name + this.GetAttribute(node) + ">");
                    }
                    this.deep++;
                    this.Analyze1(node.ChildNodes); 
                    this.sb.Append(this.GetTabs());
                    this.sb.AppendLine("</" + node.Name + ">");
                }
            }
            this.deep--;
        }

        private string GetTabs()
        {
            StringBuilder tabSb = new StringBuilder();
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
    }
}
