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
    // Start is called before the first frame update
    void Start()
    {
        RefreshDropdown();
    }
    public void StartDialogue()
    {
        Debug.Log("loaded " + dropDown.options[dropDown.value].text);
        //dialogueManager = DialogueManager.Instance(dialogueName);
        dialogueManager = DialogueManager.Instance(dropDown.options[dropDown.value].text);
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
        RuntimeXmlLoader.RefreshXmlDropdown(dropDown, "Dialogues/");
    }
}
