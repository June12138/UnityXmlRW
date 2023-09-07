using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using TMPro;
using UnityEngine;

public class RuntimeXmlLoader : MonoBehaviour
{
    public string path;
    public XmlDataHolder xmlHolder;
    public RuntimeXmlLoader(string savePath = "")
    {
        path = savePath;
    }
    /// <param name="entries">entries to be saved inside the .xml file</param>
    /// <param name="name">name of the file without extension</param>
    public virtual void SaveData(List<XmlAnnotation> entries, string name)
    {
        if (name == "") return;
        xmlHolder = new XmlDataHolder(Path(name), true, fromResources:false);
        xmlHolder.ClearData();
        foreach (XmlAnnotation entry in entries)
        {
            xmlHolder.xmlRoot.Add(SaveEntry(entry));
        }
        xmlHolder.Save();
        Debug.Log("data saved at " + xmlHolder.path);
    }
    ///<summary>how should specific xml elements be saved to .xml file</summary>
    public virtual XEHolder SaveEntry(XmlAnnotation entry)
    {
        //edit xml elements of a game object
        return new XEHolder("entry", new XAttribute("id", entry.key), new XAttribute("name", entry.gameObject.name), new XAttribute("prefab", entry.prefab));
    }
    /// <param name="name">name of the .xml file to load from, without file extension</param>
    /// <param name="autoCreate">weather or not to create object when it is not found in the scene</param>
    /// <returns>list of entries affected by the data load</returns>
    public virtual List<XmlAnnotation> LoadData(string name, bool autoCreate = false)
    {
        List<XmlAnnotation> list = new List<XmlAnnotation>();
        if (name == "") return list;
        xmlHolder = new XmlDataHolder(Path(name), fromResources:false);
        foreach (XEHolder el in xmlHolder.xmlRoot.Elements())
        {
            XmlAnnotation entry = FindByID(el.SAttribute("id"));
            if (entry != null)
            {
                list.Add(entry);
                LoadEntry(entry);
            }
            else if (autoCreate)
            {
                entry = GenerateFromEntry(el);
                list.Add(entry);
                LoadEntry(entry);
            }
        }
        return list;
    }
    static XmlAnnotation FindByID(string id)
    {
        XmlAnnotation[] annotations = GameObject.FindObjectsOfType<XmlAnnotation>();
        foreach (XmlAnnotation annotation in annotations)
        {
            if (annotation.key == id)
            {
                return annotation;
            }
        }
        return null;
    }
    XmlAnnotation GenerateFromEntry(XEHolder entry)
    {
        GameObject instance = Instantiate(Resources.Load(entry.SAttribute("prefab"))) as GameObject;
        XmlAnnotation output = instance.GetComponent<XmlAnnotation>();
        output.key = entry.SAttribute("id");
        return output;
    }
    public virtual void LoadEntry(XmlAnnotation entry)
    {
        XEHolder holder = xmlHolder.FindFirst("id", entry.key);
        entry.gameObject.name = holder.SAttribute("name");
        entry.prefab = holder.SAttribute("prefab");
    }
    public virtual string Path(string name)
    {
        return path + name;
    }
    public string FullPath()
    {
        return Application.dataPath + "/Resources/Xmls/" + path + ".xml";
    }
    public static void RefreshXmlDropdown(TMP_Dropdown dropDown, string path)
    {
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        string[] files = Directory.GetFiles(Application.dataPath + "/Resources/Xmls/" + path);
        foreach (string file in files)
        {
            string[] split = file.Split('/')[file.Split('/').Length - 1].Split('.');
            if (split[split.Length - 1] == "xml")
            {
                options.Add(split[split.Length - 2]);
            }
        }
        dropDown.AddOptions(options);
    }
}