using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/// <summary>
/// A class that contains the responses to a dialogue question.
/// </summary>
public class DialogueResponse
{
    [TextArea(4, 6)]
    public string[] responses;
    public string correctResponse;
    public string correctDialogue;
    public string incorrectDialogue;
}