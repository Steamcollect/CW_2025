using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class AnswerManager_UI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float delayBeetween;

    [Header("References")]
    [SerializeField] private GameObject answerPanel;
    [SerializeField] private Answer_UI answerPrefab;
    [SerializeField] private GameObject list;

    [Header("RSE")]
    [SerializeField] private RSE_ShowAnswerUI onShowAnswers;

    private void OnEnable()
    {
        onShowAnswers.action += ShowAnswers;
    }

    private void OnDisable()
    {
        onShowAnswers.action -= ShowAnswers;
    }

    private void Awake()
    {
        answerPanel.SetActive(false);
    }

    public void ShowAnswers(DialogAwnser[] answers)
    {
        answerPanel.SetActive(true);
        StartCoroutine(ShowAnswerCoroutine(answers));
    }

    private IEnumerator ShowAnswerCoroutine(DialogAwnser[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            Answer_UI answer = Instantiate(answerPrefab, list.transform);
            answer.PrintAnswer(answers[i].text);
            yield return new WaitForSeconds(delayBeetween);
        }
    }
}