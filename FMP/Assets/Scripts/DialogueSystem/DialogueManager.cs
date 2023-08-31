using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameDisplay;
    [SerializeField]
    TextMeshProUGUI contentDisplay;
    [SerializeField]
    Image display;
    XmlDataHolder characters;
    XmlDataHolder currentDialogue;
    List<XEHolder> dialogueQueue = new List<XEHolder>();
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadCharacters();
        LoadDialogues("testingDialogue");
        PresentDialogue(dialogueQueue[i]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    void LoadCharacters()
    {
        characters = new XmlDataHolder("Dialogues/Characters/characters.xml");
        
    }
    void LoadDialogues(string name)
    {
        currentDialogue = new XmlDataHolder("Dialogues/" + name + ".xml");
        dialogueQueue = currentDialogue.xmlRoot.Elements().ToList();
    }
    void PresentDialogue(XEHolder entry)
    {
        XEHolder speaker = characters.FindFirst("id", entry.SAttribute("speakerId"));
        string imagePath = speaker.SAttribute("display");
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
            PresentDialogue(dialogueQueue[i]);
            i ++;
        }
    }
}
