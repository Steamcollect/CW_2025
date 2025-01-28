using BT.ScriptablesObject;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DialogData", menuName = "SSO/Dialog/DialogData")]
public class SSO_DialogData : ScriptableObject
{
    [TextArea] public string text;

    public DialogType type;

    [Space(10)]
    public DialogAwnser[] awnsers;

    [Space(10)]
    public UnityEvent events;
}

[System.Serializable]
public struct DialogAwnser
{
    [TextArea] public string text;

    [Space(10)]
    public UnityEvent events;
}

public enum DialogType
{
    NoAwnser,
    Awnser,
    Event
}