using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[ExecuteInEditMode]

public class XmlAnnotation : MonoBehaviour
{
    // Start is called before the first frame update
    public string key = "";
    public string prefab;
    [TextArea]
    public string annotation;
    public void GenerateKey(int digit = 15)
    {
        string key = "";
        for (int i = 0; i < digit; i++)
        {
            key += GenerateDigit();
        }
        Debug.Log(GenerateDigit());
        char GenerateDigit()
        {
            int rnd;
            char output = '1';
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    //upper case char
                    rnd = Random.Range(65, 91);
                    output = Convert.ToChar(rnd);
                    break;
                case 1:
                    //lower case char
                    rnd = Random.Range(97, 123);
                    output = Convert.ToChar(rnd);
                    break;
                case 2:
                    //digit
                    rnd = Random.Range(0, 10);
                    output = rnd.ToString()[0];
                    break;
            }
            return output;
        }
        this.key = key;
    }
    private void OnEnable()
    {
        if (key == "")
        {
            GenerateKey();
        }
    }
}
