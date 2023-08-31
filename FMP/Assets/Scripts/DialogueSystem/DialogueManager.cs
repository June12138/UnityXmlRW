using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    XmlDataHolder characters;
    XmlDataHolder currentDialogue;
    [SerializeField]
    TextMeshProUGUI nameDisplay;
    [SerializeField]
    TextMeshProUGUI contentDisplay;
    [SerializeField]
    Image display;
    [SerializeField]
    List<Button> buttons;
    List<XEHolder> buttonHolders = new List<XEHolder>();
    List<XEHolder> dialogueQueue = new List<XEHolder>();
    int i = 0;
    bool waitSelection = false;
    // Start is called before the first frame update
    void Start()
    {
        LoadCharacters();
        LoadDialogues("testingDialogue");
        ProcessEntry(dialogueQueue[0]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !waitSelection)
        {
            Next();
        }
    }
    #region loadData
    void LoadCharacters()
    {
        characters = new XmlDataHolder("Dialogues/Characters/characters.xml");
        
    }
    void LoadDialogues(string name)
    {
        currentDialogue = new XmlDataHolder("Dialogues/" + name + ".xml");
        dialogueQueue = currentDialogue.xmlRoot.Elements().ToList();
    }
    #endregion
    void ProcessEntry(XEHolder entry)
    {
        int type = entry.IAttribute("type");
        if (type == int.MinValue) type = 0;
        switch (type)
        {
            case -1:
                GameObject.Destroy(gameObject);
                break;
            case 0:
                //dialogue
                PresentDialogue(entry);
                break;
            case 1:
                //button
                foreach (Button button in buttons)
                {
                    if (!button.gameObject.active)
                    {
                        button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = entry.SAttribute("content");
                        button.gameObject.SetActive(true);
                        buttonHolders.Add(entry);
                        break;
                    }
                }
                waitSelection = true;
                if (dialogueQueue[i+1].IAttribute("type") == 1)
                {
                    i++;
                    Next();
                }
                break;
        }
    }
    public void ButtonOp(int index)
    {
        i = buttonHolders[index].IAttribute("jumpTo");
        waitSelection = false;
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
        buttonHolders.Clear();
        Next();
    }
    void PresentDialogue(XEHolder entry)
    {
        XEHolder speaker = characters.FindFirst("id", entry.SAttribute("speakerId"));
        string imagePath = speaker.SAttribute("display");
        //set speaker image
        if (imagePath != null)
        {
            Sprite sprite = Resources.Load<Sprite>(imagePath);
            //set image scale
            float scaleX = sprite.rect.width / sprite.rect.height;
            display.transform.localScale = new Vector2(scaleX, 1f);
            display.sprite = sprite;
            display.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            display.color = new Color(1f, 1f, 1f, 0f);
        }
        string content = entry.SAttribute("content");
        nameDisplay.text = speaker.SAttribute("name");
        contentDisplay.text = content;
    }
    void Next()
    {
        if (i <= dialogueQueue.Count - 1)
        {
            ProcessEntry(dialogueQueue[i]);
            if (dialogueQueue[i].IAttribute("jumpTo") == int.MinValue)
            {
                i++;
            }
            else
            {
                i = dialogueQueue[i].IAttribute("jumpTo");
            }
        }
    }
}
