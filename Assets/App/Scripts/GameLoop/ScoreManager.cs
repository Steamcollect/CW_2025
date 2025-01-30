using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxEventToCallTheEnd;
    [SerializeField] ScoreValue[] scoreValue;

    bool isCall = false;

    [Header("References")]

    //[Space(10)]
    // RSO
    [SerializeField] RSO_PlayerScore rsoScore;
    [SerializeField] RSO_CurrentEventCount rsoCurrentEventCount;
    [SerializeField] RSO_CurrentCharacter rsoCurrentCharacter;
    [SerializeField] RSO_CinematicVisuals rsoCinematicVisual;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    void OnEventCountChange()
    {
        if (isCall) return;

        isCall = true;
        for (int i = 0; i < scoreValue.Length; i++)
        {
            if (scoreValue[i].minMax.x >= rsoScore.Value && scoreValue[i].minMax.y < rsoScore.Value)
            {
                rsoCinematicVisual.Value = rsoCurrentCharacter.Value.isMan ? scoreValue[i].manVisuals : scoreValue[i].girlVisuals;
                SceneManager.LoadScene("Cinematic");
                return;
            }
        }
    }
}

public struct ScoreValue
{
    public Vector2 minMax;
    public Sprite[] manVisuals;
    public Sprite[] girlVisuals;
}