using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WFormsXMLFormatter.Common
{
    public class XmlFormatToStr
    {

        private int deep;
        private StringBuilder sb;
        private XmlDocument doc;
        public XmlFormatToStr(string str)
        {
            deep = 0;
            sb = new StringBuilder();
            doc = new XmlDocument();
            this.doc.LoadXml(str);
            this.Analyze(this.doc.ChildNodes);
        }

        public XmlFormatToStr(string str,bool isMultLine)
        {
            deep = 0;
            sb = new StringBuilder();
            doc = new XmlDocument();
            this.doc.LoadXml(str);
            if (isMultLine)
            {
                this.AnalyzeMultLine(this.doc.ChildNodes);
            }
            else
            {
                this.Analyze(this.doc.ChildNodes);
            }
        }

        public string Format()
        {
            return this.sb.ToString();
        }

        private void AnalyzeMultLine(XmlNodeList nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);
                if (!node.HasChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        this.sb.AppendLine(this.GetTabs() + "<" + nodes.Item(i).Name + this.GetAttribute(node) + ">");
                        this.sb.AppendLine(this.GetTabs()+"     "+node.Value);
                        this.sb.AppendLine(this.GetTabs() + "</" + nodes.Item(i).Name + ">");
                    }
                    else
                    {
                        this.sb.AppendLine(this.GetTabs() + "     " + node.Value);
                    }
                }
                else
                {
                    if (node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType != XmlNodeType.Element)
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.AppendLine("<" + node.Name + this.GetAttribute(node) + ">");
                        this.deep++;
                        this.AnalyzeMultLine(node.ChildNodes);
                        //this.sb.Append(this.GetTabs());
                        this.sb.Append(this.GetTabs());
                        this.sb.AppendLine("</" + node.Name + ">");
                    }
                    else
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.AppendLine("<" + node.Name + this.GetAttribute(node) + ">");
                        this.deep++;
                        this.AnalyzeMultLine(node.ChildNodes); 
                        this.sb.AppendLine(this.GetTabs()+"</" + node.Name + ">");
                    }
                }
            }
            this.deep--;
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
                        this.sb.Append(node.Value);
                    }
                }
                else
                {
                    if (node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType != XmlNodeType.Element)
                    {
                        this.sb.Append(this.GetTabs());
                        this.sb.Append("<" + node.Name + this.GetAttribute(node) + ">");
                        this.deep++;
                        this.Analyze(node.ChildNodes);
                        //this.sb.Append(this.GetTabs());
                        this.sb.AppendLine("</" + node.Name + ">");
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


        public Dictionary<string, string> GetAttributeDic()
        {
            if (this.doc.FirstChild.Attributes == null)
                return null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlAttribute item in this.doc.FirstChild.Attributes)
            {
                dic.Add(item.Name, item.Value);
            }
            return dic;
        }
    }
}
