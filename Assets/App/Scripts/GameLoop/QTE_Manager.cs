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

    [Header("Output")]
    [SerializeField] RSE_StartNewQTE rseStartNewQTE;
    [SerializeField] RSE_SendEvent rseSendEvent;

    private void OnEnable()
    {
        rseOnQTE_End.action += OnQTE_End;
    }
    private void OnDisable()
    {
        rseOnQTE_End.action -= OnQTE_End;
    }

    private void Start()
    {
        StartNewQTE();
    }

    IEnumerator DelayBetweenQTE()
    {
        yield return new WaitForSeconds(
            timeBetweenQTE_InTime.Evaluate(rsoGameTime.Value.timeSinceStart)
            + Random.Range(delayBetweenQTE.x, delayBetweenQTE.y));

        StartNewQTE();
    }

    void StartNewQTE()
    {
        List<KeyCode> keys = new List<KeyCode>();
        for (int i = 0; i < QTE_KeyCountInTime.Evaluate(rsoGameTime.Value.timeSinceStart); i++)
            keys.Add(keyCodePossible.GetRandom());

        rseStartNewQTE.Call(keys.ToArray(), timeToResolveQTE_InTime.Evaluate(rsoGameTime.Value.timeSinceStart));
    }

    void OnQTE_End()
    {
        StartCoroutine(DelayBetweenQTE());
    }
}