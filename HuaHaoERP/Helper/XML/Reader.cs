using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HuaHaoERP.Helper.XML
{
    class Reader
    {
        /// <summary>
        /// 读取二级节点的值
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        internal string ReadLevelTwo(string Element)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList rootList = xmldoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlNode xn1 in rootList)
            {
                XmlElement xe1 = (XmlElement)xn1;
                XmlNodeList xnl1 = xe1.ChildNodes;
                foreach (XmlNode xn2 in xnl1)
                {
                    XmlElement xe2 = (XmlElement)xn2;
                    if (xe2.Name == Element)
                    {
                        return xe2.InnerText;
                    }
                }
            }
            return null;
        }
    }
}
