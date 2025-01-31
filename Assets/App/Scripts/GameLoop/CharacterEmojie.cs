using UnityEngine;
public class CharacterEmojie : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] SpriteRenderer characterVisual;
    [SerializeField] ScoreValue[] scoreValues;

    //[Space(10)]
    // RSO
    [SerializeField] RSO_CurrentCharacter character;
    [SerializeField] RSO_PlayerScore rsoScore;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void OnEnable()
    {
        rsoScore.OnChanged += OnScoreChange;
    }
    private void OnDisable()
    {
        rsoScore.OnChanged -= OnScoreChange;
    }

    private void Start()
    {
        OnScoreChange(0);
    }

    void OnScoreChange(int currentScore)
    {
        for (int i = 0; i < scoreValues.Length; i++)
        {
            if (scoreValues[i].minMax.x <= rsoScore.Value && scoreValues[i].minMax.y >= rsoScore.Value)
            {
                characterVisual.sprite = character.Value.isMan ? scoreValues[i].manVisuals.GetRandom() : scoreValues[i].girlVisuals.GetRandom();
                characterVisual.transform.BumpVisual();
                return;
            }
        }
    }
}