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
        Debug.Log("�Ի������⣺�ӹ�һ�ܣ�������" + param1 + "��");
    }
    public static void End()
    {
        Debug.Log("�Ի������⣺�Ի���������ʾ");
    }
    public static void Log(string message)
    {
        Debug.Log("�Ի������⣺" + message);
    }
}
