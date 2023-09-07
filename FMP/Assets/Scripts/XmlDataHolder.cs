using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;

public class XmlDataHolder
{
    //xml document folder location of the current environment. By default, this is set to Application.dataPath + "/Resources/Xmls/"
    public string environmentLocation;
    public XEHolder xmlRoot;
    public string path;
    ///<param name="xmlName">path to the .xml file (without .xml extension), under environmentLocation</param>
    ///<param name="autoCreate">when set to true, create .xml file in that path if file is not found</param>
    ///<param name="rootName">name of the root element when creating .xml file. Set to "xml" by default</param>
    ///<param name="fromResources">load from unity resources or directly from file path. When set to resources, nothing will update at runtime</param>
    public XmlDataHolder(string xmlName, bool autoCreate = false, string rootName = "xml", bool fromResources = true)
    {
        environmentLocation = Application.dataPath + "/Resources/Xmls/";
        XDocument xmlDocument = new XDocument();
        path = environmentLocation + xmlName + ".xml";
        if((fromResources && Resources.Load("Xmls/" + xmlName)) 
            || (!fromResources && System.IO.File.Exists(path))
        )
        {
            //Debug.Log(XDocument.Load(path));
            LoadFile();
            xmlRoot = ConvertToXEHolder(xmlDocument.Root);
        }
        else
        {
            if (autoCreate)
            {
                XDocument doc = new XDocument();
                doc.Add(new XElement(rootName));
                doc.Save(path);
                LoadFile();
                xmlRoot = ConvertToXEHolder(xmlDocument.Root);
            }
        }
        void LoadFile()
        {
            if (fromResources)
            {
                TextAsset xmlText = (TextAsset)Resources.Load("Xmls/" + xmlName, typeof(TextAsset));
                xmlDocument = XDocument.Parse(xmlText.text);
            }
            else
            {
                xmlDocument = XDocument.Load(path);
            }
        }
    }

    //converts everything under and including last to XEHolder
    XEHolder ConvertToXEHolder(XElement last)
    {
        if (!last.HasElements)
        {
            return new XEHolder(Only(last));
        }
        else
        {
            XEHolder output = new XEHolder(Only(last));
            foreach (XElement el in last.Elements())
            {
                output.Add(ConvertToXEHolder(el));
            }
            return output;
        }
    }
    XElement Only(XElement el)
    {
        XElement output = new XElement(el);
        output.Descendants().Remove();
        return output;
    }
    ///<param name="fieldName">name of the field you want to search</param>
    ///<param name="keyWord">search keyword in that field</param>
    ///<param name="root">root element of the search pattern, if left null this is set to xmlRoot</param>
    ///<returns>array of XElement that fits description</returns>
    public List<XEHolder> FindAll(string fieldName, string keyWord, XEHolder root = null)
    {
        List<XEHolder> result = new List<XEHolder>();
        XEHolder searchRoot;
        if (root == null) searchRoot = xmlRoot;
        else searchRoot = root;
        foreach (XEHolder el in searchRoot.Elements())
        {
            string str;
            if (el.Attribute(fieldName) != null)
            {
                str = el.Attribute(fieldName).Value;
                if (str.Equals(keyWord))
                {
                    result.Add(el);
                }
            }
        }
        return result;
    }

    ///<param name="fieldName">name of the field you want to search</param>
    ///<param name="keyWord">search keyword in that field</param>
    ///<param name="root">root element of the search pattern, if left null this is set to xmlRoot</param>
    ///<returns>first XElement that fits the description</returns>
    public XEHolder FindFirst(string fieldName, string keyWord, XEHolder root = null)
    {
        XEHolder searchRoot;
        if (root == null) searchRoot = xmlRoot;
        else searchRoot = root;
        foreach (XEHolder el in searchRoot.Elements())
        {
            string str;
            if (el.Attribute(fieldName) != null)
            {
                str = el.Attribute(fieldName).Value;
                if (str.Equals(keyWord))
                {
                    return el;
                }
            }
        }
        return null;
    }

    public void PrintAllElements()
    {
        foreach (XEHolder el in xmlRoot.Elements())
        {
            Debug.Log(el);
        }
    }
    /// <summary> Saves the document to the path where it is loaded </summary>
    public void Save()
    {
        xmlRoot.Save(path);
    }
    public void ClearData()
    {
        xmlRoot.RemoveAll();
    }
}