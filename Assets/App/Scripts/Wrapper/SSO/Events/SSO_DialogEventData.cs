using BT.ScriptablesObject;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogData", menuName = "SSO/Event/DialogData")]
public class SSO_DialogEventData : ScriptableObject
{
    [Header("Dialog")]
    [TextArea] public string text;
    public bool hideDialogPanel;
    public bool blockQTE;

    [Header("Answer")]
    public string awnserDescription;
    public Answer[] awnsers;

    [Header("Next Dialog")]
    public UnityEvent nextDialog;
}

[System.Serializable]
public struct Answer
{
    [TextArea] public string text;
    public int score;

    public UnityEvent eventToCall;
}