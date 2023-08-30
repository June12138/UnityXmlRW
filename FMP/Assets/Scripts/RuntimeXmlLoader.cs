using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class RuntimeXmlLoader : MonoBehaviour
{
    public string path;
    public XmlDataHolder xmlHolder;
    public RuntimeXmlLoader(string savePath = "")
    {
        path = savePath;
    }
    public virtual void SaveData(List<XmlAnnotation> entries, string name)
    {
        if (name == "") return;
        xmlHolder = new XmlDataHolder(Path(name), true);
        xmlHolder.ClearData();
        foreach (XmlAnnotation entry in entries)
        {
            SaveEntry(entry);
        }
        xmlHolder.Save();
        Debug.Log("data saved at " + xmlHolder.path);
    }
    ///<summary>how should specific xml elements be saved to .xml file</summary>
    public virtual void SaveEntry(XmlAnnotation gameObject)
    {
        //edit xml elements of a game object
    }
    public virtual void LoadData(string name, bool autoCreate = false)
    {
        if (name == "") return;
        xmlHolder = new XmlDataHolder(Path(name));
        foreach (XEHolder el in xmlHolder.xmlRoot.Elements())
        {
            XmlAnnotation entry = FindByID(el.SAttribute("id"));
            if (entry != null)
            {
                LoadEntry(entry);
            }
            else if (autoCreate)
            {
                LoadEntry(GenerateFromEntry(el));
            }
        }
    }
    XmlAnnotation FindByID(string id)
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
        output.prefab = entry.SAttribute("prefab");
        return output;
    }
    public virtual void LoadEntry(XmlAnnotation entry)
    {

    }
    public virtual string Path(string name)
    {
        return path + name + ".xml";
    }
    public string FullPath()
    {
        return Application.dataPath + "/Resources/Xmls/" + path;
    }
}