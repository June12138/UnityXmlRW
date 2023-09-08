using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFunctionLib : MonoBehaviour
{
    public static void Jue(string param1, string param2)
    {
        Debug.Log("�Ի������⣺������" + param1 + "�Σ�ÿ��" + param2 + "���ӣ���");
    }
    public static void Escape(string param1)
    {
        Debug.Log("�Ի������⣺������˴�粢�ӹ�һ�ܣ�������" + param1 + "��");
    }
    public static void End()
    {
        Debug.Log("�Ի������⣺�Ի���������ʾ");
    }
    public static void Log(string message)
    {
        Debug.Log("�Ի������⣺" + message);
    }
    public static void Ii(string replace)
    {
        int index = int.Parse(replace);
        DialogueManager dm = GetDM();
        dm.interpolations[index] = dm.i.ToString();
    }
    public static void Interpolate(string replace, string text)
    {
        int index = int.Parse(replace);
        DialogueManager dm = GetDM();
        dm.interpolations[index] = text;
    }
    static DialogueManager GetDM()
    {
        DialogueManager dm = GameObject.FindObjectOfType<DialogueManager>();
        return dm;
    }
}
