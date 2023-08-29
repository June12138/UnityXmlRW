using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class XmlIngameEditor : MonoBehaviour
{
    [SerializeField]
    ///<param name="path"> path to save xml profiles, under Resources/Xmls </param>
    string path;
    RuntimeXmlLoader loader;
    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    TMP_Dropdown dropDown;
    /// <summary>list of game objects to be stored in the .xml file</summary>
    public List<GameObject> gameObjects = new List<GameObject>();
    public virtual void Start()
    {
        loader = new RuntimeXmlLoader(gameObjects, path);
        RefreshDropdown();
        dropDown.onValueChanged.AddListener(UpdateInputField);
    }
    public void SaveButton()
    {
        loader.SaveData(inputField.text);
        RefreshDropdown();
    }
    public void LoadButton()
    {
        loader.LoadData(dropDown.options[dropDown.value].text);
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
        inputField.text = dropDown.options[value].text;
    }
}
