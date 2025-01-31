using UnityEngine;
public class CharacterEmojie : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] SpriteRenderer characterVisual;

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
        characterVisual.sprite = character.Value.charVisual;
        //OnScoreChange(0);
    }

    void OnScoreChange(int currentScore)
    {
        //for (int i = 0; i < character.Value.scoreValues.Length; i++)
        //{
        //    if (character.Value.scoreValues[i].minMax.x <= rsoScore.Value && character.Value.scoreValues[i].minMax.y >= rsoScore.Value)
        //    {
        //        characterVisual.sprite = character.Value.isMan ? character.Value.scoreValues[i].manVisuals.GetRandom() : character.Value.scoreValues[i].girlVisuals.GetRandom();
        //        characterVisual.transform.BumpVisual();
        //        return;
        //    }
        //}
    }
}