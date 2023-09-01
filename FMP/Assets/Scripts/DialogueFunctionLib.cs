using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFunctionLib : MonoBehaviour
{
    public static void Jue(string param1, string param2)
    {
        Debug.Log("被撅力" + param1 + "次，每次" + param2 + "分钟（悲");
    }
    public static void Escape(string param1, string param2)
    {
        Debug.Log("逃过一杰！");
    }
}
