using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DialoguePreview : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown dropDown;
    [SerializeField]
    string dialogueName;
    public DialogueManager dialogueManager;
    //Fullscreen options
    int x;
    int y;
    // Start is called before the first frame update
    void Start()
    {

        RefreshDropdown();
        
        x = Display.main.systemWidth;
        y = Display.main.systemHeight;
    }
    public void StartDialogue(string name)
    {
        if (dialogueManager == null)
        {
            //Debug.Log("loaded " + dropDown.options[dropDown.value].text);
            dialogueManager = DialogueManager.Instance(name);
        }
    }
    public void StartButton()
    {
        StartDialogue(dropDown.options[dropDown.value].text);
    }
    public void EndDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.EndDialogue();
        }
    }
    // Update is called once per frame
    public void RefreshDropdown()
    {
        if (dropDown) RuntimeXmlLoader.RefreshXmlDropdown(dropDown, "Dialogues/");
    }
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1024, 512, false);
        }
        else
        {
            Screen.SetResolution(x, y, true);
        }
    }
}