using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Event", menuName = "SSO/SSO_Event")]
public class SSO_Event : ScriptableObject
{
    [TextArea]public string text;
    public EventLocationType eventLocationType;
}

public enum EventLocationType
{
    City,
    Farm,
    Forest
}