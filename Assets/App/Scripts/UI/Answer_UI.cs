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

    public void PrintAnswer(string text)
    {
        button.onClick.AddListener(delegate { OnClick(); } );
        this.text.text = text;
    }

    private void OnClick()
    {
        onDialogEnd.Call();
        onEventFinished.Call();
        closeAnswerUI.Call();
    }
}