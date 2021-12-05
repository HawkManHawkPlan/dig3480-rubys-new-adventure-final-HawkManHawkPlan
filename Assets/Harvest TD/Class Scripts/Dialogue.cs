using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    //public Sprite[] characterExpressions;
    [TextArea(3, 10)]
    public string[] sentences;
}
