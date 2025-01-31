using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Answer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;

    [SerializeField] private RSE_OnDialogEnd onDialogEnd;
    [SerializeField] private RSE_EventFinished onEventFinished;
    [SerializeField] private RSE_CloseAnswerUI closeAnswerUI;
    [SerializeField] RSO_PlayerScore rsoPlayerScore;
    int scoreGiven;

    public void PrintAnswer(string text, int scoreGiven)
    {
        button.onClick.AddListener(delegate { OnClick(); } );
        this.text.text = text;
        this.scoreGiven = scoreGiven;
    }

    private void OnClick()
    {
        rsoPlayerScore.Value += scoreGiven;
        onDialogEnd.Call();
        onEventFinished.Call();
        closeAnswerUI.Call();
    }
}