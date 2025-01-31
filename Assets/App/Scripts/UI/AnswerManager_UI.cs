using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AnswerManager_UI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float delayBeetween;
    bool isTiming = false;
    float timeToAnswer;

    [Header("References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Answer_UI answerPrefab;
    [SerializeField] private GameObject list;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] Slider timeSlider;

    [Header("RSE")]
    [SerializeField] private RSE_ShowAnswerUI onShowAnswers;
    [SerializeField] private RSE_CloseAnswerUI onCloseAnswer;
    [SerializeField] private RSE_OnDialogEnd onDialogEnd;
    [SerializeField] private RSE_EventFinished onEventFinished;
    [SerializeField] RSO_PlayerScore rsoPlayerScore;

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

    private void Update()
    {
        if (isTiming)
        {
            timeToAnswer -= Time.deltaTime;
            timeSlider.value = timeToAnswer;

            if(timeToAnswer <= 0)
            {
                rsoPlayerScore.Value -= 1;
                onDialogEnd.Call();
                onEventFinished.Call();
                onCloseAnswer.Call();
            }
        }
    }

    public void ClosePanel()
    {
        isTiming = false;
        panel.SetActive(false);
        ClearAnswerList();
    }

    public void ShowAnswers(SSO_DialogEventData dialogEventData, float timeToAnswer)
    {
        this.timeToAnswer = timeToAnswer;
        timeSlider.maxValue = timeToAnswer;
        isTiming = true;
        //descriptionText.text = dialogEventData.awnserDescription;
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
            answer.PrintAnswer(answers[i]);
            answerList.Add(answer);
            yield return new WaitForSeconds(delayBeetween);
        }
    }
}