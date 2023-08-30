using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class XmlIngameEditor : MonoBehaviour
{
    [SerializeField]
    RuntimeXmlLoader loader;
    [SerializeField]
    TMP_InputField saveInputField;
    [SerializeField]
    TMP_InputField generateInputField;
    [SerializeField]
    TMP_Dropdown dropDown;
    /// <summary>list of game objects to be stored in the .xml file</summary>
    public List<XmlAnnotation> saveObjects = new List<XmlAnnotation>();
    public virtual void Start()
    {
        RefreshDropdown();
        dropDown.onValueChanged.AddListener(UpdateInputField);
    }
    public void SaveButton()
    {
        loader.SaveData(saveObjects, saveInputField.text);
        RefreshDropdown();
    }
    public void LoadButton()
    {
        List<XmlAnnotation> loadObjects = loader.LoadData(dropDown.options[dropDown.value].text, true);
        foreach (XmlAnnotation anno in loadObjects)
        {
            if (!saveObjects.Contains(anno))
            {
                saveObjects.Add(anno);
            }
        }
        //clean unrecorded objects
        List<XmlAnnotation> destroyList = new List<XmlAnnotation>();
        foreach (XmlAnnotation anno in saveObjects)
        {
            if (!loadObjects.Contains(anno))
            {
                destroyList.Add(anno);
            }
        }
        foreach (XmlAnnotation anno in destroyList)
        {
            GameObject.Destroy(anno.gameObject);
            saveObjects.Remove(anno);
        }
    }
    void RefreshDropdown()
    {
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        string[] files = Directory.GetFiles(loader.FullPath());
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
    public void UpdateInputField(int value)
    {
        saveInputField.text = dropDown.options[value].text;
    }
    public void Generate()
    {
        GameObject instance = Instantiate(Resources.Load("Prefabs/" + generateInputField.text.ToString())) as GameObject;
        saveObjects.Add(instance.GetComponent<XmlAnnotation>());
    }
}
