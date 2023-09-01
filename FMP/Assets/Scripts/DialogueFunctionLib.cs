using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFunctionLib : MonoBehaviour
{
    public static void Jue(string param1, string param2)
    {
        Debug.Log("对话函数库：被撅力" + param1 + "次，每次" + param2 + "分钟（悲");
    }
    public static void Escape(string param1)
    {
        Debug.Log("对话函数库：逃过一杰！你跑了" + param1 + "米");
    }
    public static void End()
    {
        Debug.Log("对话函数库：对话结束，提示");
    }
    public static void Log(string message)
    {
        Debug.Log("对话函数库：" + message);
    }
}
