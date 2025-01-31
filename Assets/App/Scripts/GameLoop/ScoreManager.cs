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
    [Header("Output")]
    [SerializeField] RSE_AudioFadeOut rseAudioFadeOut;

    private void OnEnable()
    {
        rsoCurrentEventCount.OnChanged += OnEventCountChange;
    }
    private void OnDisable()
    {
        rsoCurrentEventCount.OnChanged -= OnEventCountChange;
    }

    private void Awake()
    {
        rsoCurrentEventCount.Value = 0;
    }

    void OnEventCountChange(int eventCount)
    {
        if (isCall && eventCount >= maxEventToCallTheEnd) return;

        isCall = true;
        for (int i = 0; i < scoreValue.Length; i++)
        {
            if (scoreValue[i].minMax.x <= rsoScore.Value && scoreValue[i].minMax.y >= rsoScore.Value)
            {
                rsoCinematicVisual.Value = rsoCurrentCharacter.Value.isMan ? scoreValue[i].manVisuals : scoreValue[i].girlVisuals;
                rseAudioFadeOut.Call(() => SceneManager.LoadScene("Cinematic"));
                return;
            }
        }
    }
}

[System.Serializable]
public struct ScoreValue
{
    public Vector2 minMax;
    public Sprite[] manVisuals;
    public Sprite[] girlVisuals;
}