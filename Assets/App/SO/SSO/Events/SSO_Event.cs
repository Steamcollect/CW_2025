using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Event", menuName = "SSO/SSO_Event")]
public class SSO_Event : ScriptableObject
{
    public EventType eventType;
    public SSO_DialogEventData eventData;
    public float time;
    public EventLocationType eventLocationType;
    public GameObject eventVisual;
}

public enum EventLocationType
{
    City,
    Farm,
    Forest
}

public enum EventType
{
    QTE,
    Question
}