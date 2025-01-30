using BT.ScriptablesObject;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogData", menuName = "SSO/Event/DialogData")]
public class SSO_DialogEventData : ScriptableObject
{
    [TextArea] public string text;

    public DialogType type;

    [Space(10)]
    [TextArea] public string[] awnsers;

    [Space(10)]
    public UnityEvent awnsersEvents;

    [Space(10)]
    public UnityEvent events;
}

public enum DialogType
{
    NoAwnser,
    Awnser,
    Event
}