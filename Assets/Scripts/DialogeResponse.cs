using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    [TextArea(4, 6)]
    public string[] responses;
}