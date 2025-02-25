using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Character", menuName = "SSO/Characters/SSO_Character")]
public class SSO_Character : ScriptableObject
{
    public string characterName;
    public Sprite charVisual;
    public SSO_DialogEventData[] dialogs;
    public Sprite[] bdSprites;
    public bool isMan;

    public ScoreValue[] scoreValues;
}