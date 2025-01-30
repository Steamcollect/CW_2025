using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class AnswerManager_UI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float delayBeetween;

    [Header("References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Answer_UI answerPrefab;
    [SerializeField] private GameObject list;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("RSE")]
    [SerializeField] private RSE_ShowAnswerUI onShowAnswers;
    [SerializeField] private RSE_CloseAnswerUI onCloseAnswer;

    private List<Answer_UI> answerList = new List<Answer_UI>();

    private void OnEnable()
    {
        onShowAnswers.action += ShowAnswers;
        onCloseAnswer.action += ClosePanel;
    }

    private void OnDisable()
    {
        onShowAnswers.action -= ShowAnswers;
        onCloseAnswer.action -= ClosePanel;
    }

    private void Awake()
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        ClearAnswerList();
    }

    public void ShowAnswers(string[] answers, string text)
    {
        descriptionText.text = text;
        panel.SetActive(true);
        StartCoroutine(ShowAnswerCoroutine(answers));
    }

    private void ClearAnswerList()
    {
        foreach(var answer in answerList)
        {
            Destroy(answer.gameObject);
        }

        answerList.Clear();
    }

    private IEnumerator ShowAnswerCoroutine(string[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            Answer_UI answer = Instantiate(answerPrefab, list.transform);
            answer.PrintAnswer(answers[i]);
            answerList.Add(answer);
            yield return new WaitForSeconds(delayBeetween);
        }
    }
}