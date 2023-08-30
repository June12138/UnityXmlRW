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
        loader.LoadData(dropDown.options[dropDown.value].text, true);
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
        Instantiate(Resources.Load("Prefabs/" + generateInputField.text.ToString()));
    }
}
