using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int dialogOnGoingPreviewCount;
    [SerializeField] Vector2 delayBetweenDialogs;

    [Header("References")]
    [SerializeField] SSO_DialogEventData[] dialogsPossible;

    List<SSO_DialogEventData> dialogsInGame = new();
    Queue<SSO_DialogEventData> dialogsOnGoing = new();

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddDialogToOnGoing rseAddDialogToOnGoing;
    [SerializeField] RSE_OnDialogEnd rseOnDialogEnd;

    [Header("Output")]
    [SerializeField] RSE_HandleDialog rseHandleDialog;

    private void OnEnable()
    {
        rseOnDialogEnd.action += OnDialogEnd;
        rseAddDialogToOnGoing.action += AddDialogToOnGoing;
    }
    private void OnDisable()
    {
        rseOnDialogEnd.action -= OnDialogEnd;
        rseAddDialogToOnGoing.action -= AddDialogToOnGoing;
    }

    private void Start()
    {
        if (dialogsPossible.Length == 0)
            Debug.LogError("There is no dialog set in the DialogManager");

        for (int i = 0; i < dialogOnGoingPreviewCount; i++)
            AddRandomDialogToOnGoing();

        StartCoroutine(DelayBetweenDialogs());
    }

    void AddRandomDialogToOnGoing()
    {
        if (dialogsInGame.Count == 0) dialogsInGame.AddRange(dialogsPossible);

        int rnd = Random.Range(0, dialogsInGame.Count);
        dialogsOnGoing.Enqueue(dialogsInGame[rnd]);
        dialogsInGame.RemoveAt(rnd);
    }
    void AddDialogToOnGoing(SSO_DialogEventData dialogToAdd)
    {
        dialogsOnGoing.Enqueue(dialogToAdd);
    }

    void HandleNewDialog()
    {
        if (dialogsOnGoing.Count == 0) AddRandomDialogToOnGoing();
        rseHandleDialog.Call(dialogsOnGoing.Dequeue());
    }

    void OnDialogEnd()
    {
        AddRandomDialogToOnGoing();

        StartCoroutine(DelayBetweenDialogs());
    }

    IEnumerator DelayBetweenDialogs()
    {
        yield return new WaitForSeconds(Random.Range(delayBetweenDialogs.x, delayBetweenDialogs.y));
        HandleNewDialog();
    }
}