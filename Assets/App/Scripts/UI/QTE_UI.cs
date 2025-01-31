using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

public class QTE_UI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Color letterBkColor;
    [SerializeField] Color letterSelectedColor;
    [SerializeField] Color letterWinColor;
    [SerializeField] Color letterLoseColor;

    int currentLetterIndex;

    [Header("References")]
    [SerializeField] Slider timerSlider;
    [SerializeField] TMP_Text letterRef;
    [SerializeField] TMP_Text eventTxt;

    [Space(5)]
    [SerializeField] Transform lettersContent;

    List<TMP_Text> letters = new();

    [Space(10)]
    // RSO
    [SerializeField] RSO_PlayerScore rsoScore;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_SetupQTE_UI rseSetupUI;
    [SerializeField] RSE_SetQTE_TimeSlider rseSetTimeSlider;
    [Space(5)]
    [SerializeField] RSE_OnQTE_KeyMissed rseOnQTEKeyMissed;
    [SerializeField] RSE_OnQTE_KeyWin rseOnQTEKeyWin;
    [SerializeField] RSE_OnQTE_Win rseOnQTEWin;
    [SerializeField] RSE_OnQTE_Missed rseOnQTELose;

    //[Header("Output")]

    private void OnEnable()
    {
        rseSetupUI.action += SetupQTE_UI;
        rseSetTimeSlider.action += SetupTimerSlider;

        rseOnQTEKeyWin.action += OnQTE_KeyWin;
        rseOnQTEKeyMissed.action += OnQTE_KeyLose;
        rseOnQTEWin.action += OnQTE_Win;
        rseOnQTELose.action += OnQTE_Lose;
    }
    private void OnDisable()
    {
        rseSetupUI.action -= SetupQTE_UI;
        rseSetTimeSlider.action -= SetupTimerSlider;

        rseOnQTEKeyWin.action -= OnQTE_KeyWin;
        rseOnQTEKeyMissed.action -= OnQTE_KeyLose;
        rseOnQTEWin.action -= OnQTE_Win;
        rseOnQTELose.action -= OnQTE_Lose;
    }

    void SetupQTE_UI(string[] keys, float maxTime, SSO_Event ssoEvent)
    {
        timerSlider.maxValue = maxTime;
        currentLetterIndex = 0;
        eventTxt.text = ssoEvent.text;
        StartCoroutine(PrintTxt(ssoEvent.text));

        for (int i = 0; i < keys.Length; i++)
        {
            TMP_Text letter = Instantiate(letterRef, lettersContent);
            letter.text = keys[i];
            letter.color = letterBkColor;

            letter.gameObject.SetActive(true);
            letters.Add(letter);
        }

        TMP_Text nextLetter = letters[currentLetterIndex];
        nextLetter.transform.DOScale(Vector3.one * 1.2f, 0.3f);
        nextLetter.DOColor(letterSelectedColor, .3f);

        timerSlider.gameObject.SetActive(true);
    }
    IEnumerator PrintTxt(string txt)
    {
        eventTxt.DOFade(1, .1f);
        eventTxt.text = "";

        for (int i = 0;i < txt.Length; i++)
        {
            eventTxt.text += txt[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    void ResetQTE_Visual()
    {
        for (int i = 0; i < letters.Count; i++)
        {
            Destroy(letters[i].gameObject);
        }
        letters.Clear();
        timerSlider.gameObject.SetActive(false);
    }

    void OnQTE_KeyWin()
    {
        if (currentLetterIndex < letters.Count)
        {
            TMP_Text currentLetter = letters[currentLetterIndex];
            currentLetter.DOColor(letterWinColor, 0.3f); // Change de couleur
            currentLetter.transform.DOScale(Vector3.one, 0.3f); // Remet le scale normal

            currentLetterIndex++;
            if (currentLetterIndex < letters.Count)
            {
                TMP_Text nextLetter = letters[currentLetterIndex];
                nextLetter.transform.DOScale(Vector3.one * 1.2f, 0.3f); // Grossit la nouvelle lettre
                nextLetter.DOColor(letterSelectedColor, .3f);
            }
        }
    }

    void OnQTE_KeyLose()
    {
        if (currentLetterIndex < letters.Count)
        {
            TMP_Text currentLetter = letters[currentLetterIndex];
            currentLetter.DOColor(letterLoseColor, 0.3f); // Change la couleur
            currentLetter.transform.DOPunchRotation(Vector3.forward * 10, 0.5f, 10, 1); // Fait trembler
        }
    }

    void OnQTE_Win()
    {
        foreach (TMP_Text letter in letters)
        {
            letter.DOColor(letterWinColor, 0.5f); // Change toutes les couleurs
        }

        StartCoroutine(DelayBeforeReset());
    }

    void OnQTE_Lose()
    {
        foreach (TMP_Text letter in letters)
        {
            letter.DOColor(letterLoseColor, 0.5f); // Change toutes les couleurs
            letter.transform.DOPunchRotation(Vector3.forward * 10, 0.5f, 10, 1); // Toutes tremblent
        }

        ScoreManager.instance.Lose();
    }


    void SetupTimerSlider(float time)
    {
        timerSlider.value = time;
    }

    IEnumerator DelayBeforeReset()
    {
        yield return new WaitForSeconds(.5f);

        eventTxt.DOFade(0, .3f);
        foreach (TMP_Text letter in letters)
        {
            letter.DOFade(0f, .3f); // Change toutes les couleurs
        }
        yield return new WaitForSeconds(.5f);
        ResetQTE_Visual();
    }
}