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
    public void SaveData(List<GameObject> gameObjects, string name)
    {
        if (name == "") return;
        xmlHolder = new XmlDataHolder(Path(name), true);
        xmlHolder.ClearData();
        foreach (GameObject gameObject in gameObjects)
        {
            SaveEntry(gameObject);
        }
        xmlHolder.Save();
        Debug.Log("data saved at " + xmlHolder.path);
    }
    ///<summary>how should specific xml elements be saved to .xml file</summary>
    public virtual void SaveEntry(GameObject gameObject)
    {
        //editing xml elements of a character
        XEHolder el =
        new XEHolder("character", new XAttribute("id", gameObject.name),
            new XEHolder("transform",
                new XEHolder("position"),
                new XEHolder("rotation")
            )
        );
        el.Element("transform").Element("position").SetAttributeValue("x", gameObject.transform.localPosition.x);
        el.Element("transform").Element("position").SetAttributeValue("z", gameObject.transform.localPosition.z);
        el.Element("transform").Element("rotation").SetAttributeValue("y", gameObject.transform.localRotation.eulerAngles.y);
        xmlHolder.xmlRoot.Add(el);
    }
    public void LoadData(List<GameObject> gameObjects, string name)
    {
        if (name == "") return;
        xmlHolder = new XmlDataHolder(Path(name));
        foreach (GameObject gameObject in gameObjects)
        {
            LoadEntry(gameObject);
        }
        Debug.Log("data loaded from " + xmlHolder.path);
    }
    public virtual void LoadEntry(GameObject gameObject)
    {
        XEHolder position = xmlHolder.FindFirst("id", gameObject.name).Element("transform").Element("position");
        XEHolder rotation = xmlHolder.FindFirst("id", gameObject.name).Element("transform").Element("rotation");
        float x = position.FAttribute("x");
        float y = gameObject.transform.localPosition.y;
        float z = position.FAttribute("z");
        float w = rotation.FAttribute("y");
        //                                                    () ()
        //                                                    (0 w 0)
        gameObject.transform.localRotation = Quaternion.Euler(0, w, 0);
        gameObject.transform.localPosition = new Vector3(x, y, z);
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