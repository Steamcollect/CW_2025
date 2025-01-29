using TMPro;
using UnityEngine;
public class Answer_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void PrintAnswer(string text)
    {
        this.text.text = text;
    }
}