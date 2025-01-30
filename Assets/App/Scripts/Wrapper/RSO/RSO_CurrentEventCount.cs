using UnityEngine;

[CreateAssetMenu(fileName = "RSO_CurrentEventCount", menuName = "RSO/_/RSO_CurrentEventCount")]
public class RSO_CurrentEventCount : BT.ScriptablesObject.RuntimeScriptableObject<int>
{
    public void Add() => Value++;
}