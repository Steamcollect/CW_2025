using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Answer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;

    [SerializeField] private RSE_OnDialogEnd onDialogEnd;
    [SerializeField] private RSE_EventFinished onEventFinished;

    public void PrintAnswer(string text)
    {
        button.onClick.AddListener(delegate 
        { 
            _ = onDialogEnd; 
            _ = onEventFinished;
            Clear();
        } );
        this.text.text = text;
    }

    private void Clear()
    {
        Destroy(this);
    }
}