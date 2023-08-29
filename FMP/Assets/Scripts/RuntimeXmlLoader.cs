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
    public virtual void SaveData(List<GameObject> gameObjects, string name)
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
        //edit xml elements of a game object
    }
    public virtual void LoadData(List<GameObject> gameObjects, string name)
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