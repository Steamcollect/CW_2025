using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Character", menuName = "SSO/Characters/SSO_Character")]
public class SSO_Character : ScriptableObject
{
    public string characterName;
    public SSO_DialogEventData[] dialogs;
}