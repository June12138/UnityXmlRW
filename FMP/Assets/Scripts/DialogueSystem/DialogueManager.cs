using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //xml tables
    XmlDataHolder characters;
    XmlDataHolder currentDialogue;
    XmlDataHolder functions;
    //ui elements
    [SerializeField]
    TextMeshProUGUI nameDisplay;
    [SerializeField]
    TextMeshProUGUI contentDisplay;
    [SerializeField]
    Image display;
    [SerializeField]
    List<Button> buttons;
    List<XEHolder> buttonHolders = new List<XEHolder>();
    //holding all xml elements of the current dialogue file
    List<XEHolder> dialogueQueue = new List<XEHolder>();
    //current dialogue index
    public int i = 0;
    bool waitSelection = false;
    // Start is called before the first frame update
    public void Init(string name)
    {
        LoadFunctions();
        LoadCharacters();
        LoadDialogues(name);
        ProcessEntry(dialogueQueue[0]);
    }
    public static DialogueManager Instance(string name)
    {
        GameObject instance = Instantiate(Resources.Load("Prefabs/DialogueManager")) as GameObject;
        DialogueManager manager = instance.GetComponent<DialogueManager>();
        manager.Init(name);
        if (manager.dialogueQueue.Count <= 0)
        {
            Debug.Log("dialogue not loaded, check file path");
            manager.EndDialogue();
            return null;
        }
        else
        {
            return manager;
        }
    }
    public void EndDialogue()
    {
        GameObject.Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !(waitSelection))
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
    void LoadFunctions()
    {
        functions = new XmlDataHolder("Dialogues/Functions/functions.xml");
    }
    #endregion
    #region process dialogues
    /*-1 = end
     * 0 = standard conversation
     * 1 = continuous button (auto load next button)
     * 2 = non-continuous button
     * 3 = empty, pass immediately but execute function
     */
    void ProcessEntry(XEHolder entry)
    {
        if (entry.IAttribute("function") != int.MinValue)
        {

            XEHolder functionEnt = functions.FindFirst("id", entry.SAttribute("function"));
            CallFunction(functionEnt, entry.SAttribute("params"));
        }
        int type = entry.IAttribute("type");
        if (type == int.MinValue) type = 0;
        switch (type)
        {
            case -1:
                //end
                EndDialogue();
                break;
            case 0:
                //dialogue
                PresentDialogue(entry);
                break;
            case 1:
                //option
                LoadButton(entry);
                break;
            case 2:
                //non-continuous button
                LoadButton(entry);
                break;
            case 3:
                Next();
                break;
        }
    }
    void LoadButton(XEHolder entry)
    {
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
        //load next button if current and next entry is type 1 (continuous buttons)
        if (dialogueQueue[i + 1].IAttribute("type") == 1 && dialogueQueue[i].IAttribute("type") == 1)
        {
            Next();
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
        ProcessEntry(dialogueQueue[i]);
    }
    void CallFunction(XEHolder func, string param)
    {
        string[] parameters = { };
        if (param != null) parameters = param.Split(",");
        string name = func.SAttribute("functionName");
        //Debug.Log("当前对话触发了" + name + "函数， 其参数为" + param);
        MethodInfo method = typeof(DialogueFunctionLib).GetMethod(name);
        method.Invoke(this, parameters);
    }
    void PresentDialogue(XEHolder entry)
    {
        XEHolder speaker = characters.FindFirst("id", entry.SAttribute("speakerId"));
        string varient = entry.SAttribute("varient");
        if (varient != null) varient = "_" + varient;
        string imagePath = speaker.SAttribute("display");
        //set speaker image
        if (imagePath != null)
        {
            LoadImage(imagePath + varient);
        }
        else
        {
            display.color = new Color(1f, 1f, 1f, 0f);
        }
        string content = entry.SAttribute("content");
        nameDisplay.text = speaker.SAttribute("name");
        contentDisplay.text = content;
    }
    void LoadImage(string path)
    {
        Sprite sprite = Resources.Load<Sprite>("Images/" + path);
        //set image scale
        float scaleX = sprite.rect.width / sprite.rect.height;
        display.transform.localScale = new Vector2(scaleX, 1f);
        display.sprite = sprite;
        display.color = new Color(1f, 1f, 1f, 1f);
    }
    void Next()
    {
        if (i <= dialogueQueue.Count - 1)
        {
            if (dialogueQueue[i].IAttribute("jumpTo") == int.MinValue 
                //if is continuous button, go to next
                || (dialogueQueue[i + 1].IAttribute("type") == 1 && dialogueQueue[i].IAttribute("type") == 1))
            {
                i++;
            }
            else
            {
                i = dialogueQueue[i].IAttribute("jumpTo");
            }
            ProcessEntry(dialogueQueue[i]);
        }
    }
    #endregion
}