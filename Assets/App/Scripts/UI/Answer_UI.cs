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
    Answer answer;

    public void PrintAnswer(Answer answer)
    {
        button.onClick.AddListener(delegate { OnClick(); } );
        text.text = answer.text;
        this.answer = answer;
    }

    private void OnClick()
    {
        rsoPlayerScore.Value += answer.score;
        answer.eventToCall?.Invoke();

        onDialogEnd.Call();
        onEventFinished.Call();
        closeAnswerUI.Call();
    }
}