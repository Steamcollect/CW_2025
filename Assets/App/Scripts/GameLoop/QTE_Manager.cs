using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTE_Manager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] AnimationCurve timeBetweenQTE_InTime;
    [SerializeField] AnimationCurve QTE_KeyCountInTime;
    [SerializeField] AnimationCurve timeToResolveQTE_InTime;

    [Space(10)]
    [SerializeField] Vector2 delayBetweenQTE;
    [SerializeField] KeyCode[] keyCodePossible;
    [Header("References")]
    [Space(10)]
    // RSO
    [SerializeField] RSO_GameLoopTime rsoGameTime;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OnQTE_End rseOnQTE_End;
    [SerializeField] RSE_QTEEvent rseQTEEvent;

    [Header("Output")]
    [SerializeField] RSE_StartNewQTE rseStartNewQTE;
    [SerializeField] RSE_SendEvent rseSendEvent;
    [SerializeField] RSE_EventFinished rseEventFinished;
    [SerializeField] RSO_CurrentEventCount rsoCurrentEventCount;

    private void OnEnable()
    {
        rseOnQTE_End.action += OnQTE_End;
        rseQTEEvent.action += StartNewQTE;
    }
    private void OnDisable()
    {
        rseOnQTE_End.action -= OnQTE_End;
        rseQTEEvent.action -= StartNewQTE;
    }

    private void Start()
    {
        StartCoroutine(DelayBetweenQTE());
    }

    IEnumerator DelayBetweenQTE()
    {
        yield return new WaitForSeconds(
            timeBetweenQTE_InTime.Evaluate(Time.time)
            + Random.Range(delayBetweenQTE.x, delayBetweenQTE.y));

        StartNewQTE();
        //rseSendEvent.Call(new Event{eventType = EventType.QTE, time = 3f}, false);
    }

    void StartNewQTE()
    {
        StartCoroutine(LaunchQTE());
    }
    IEnumerator LaunchQTE()
    {        
        //yield return new WaitUntil(() => /*On event false */);

        List<KeyCode> keys = new List<KeyCode>();
        for (int i = 0; i < QTE_KeyCountInTime.Evaluate(Time.time); i++)
            keys.Add(keyCodePossible.GetRandom());

        rseStartNewQTE.Call(keys.ToArray(), timeToResolveQTE_InTime.Evaluate(Time.time));

        yield return null;
    }

    void OnQTE_End()
    {
        rseEventFinished.Call();
        StartCoroutine(DelayBetweenQTE());
        rsoCurrentEventCount.Add();
    }
}