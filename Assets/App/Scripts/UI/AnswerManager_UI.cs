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

    public void ShowAnswers(SSO_DialogEventData dialogEventData)
    {
        descriptionText.text = dialogEventData.awnserDescription;
        panel.SetActive(true);
        StartCoroutine(ShowAnswerCoroutine(dialogEventData.awnsers));
    }

    private void ClearAnswerList()
    {
        foreach(var answer in answerList)
        {
            Destroy(answer.gameObject);
        }

        answerList.Clear();
    }

    private IEnumerator ShowAnswerCoroutine(Answer[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            Answer_UI answer = Instantiate(answerPrefab, list.transform);
            answer.PrintAnswer(answers[i].text);
            answerList.Add(answer);
            yield return new WaitForSeconds(delayBeetween);
        }
    }
}