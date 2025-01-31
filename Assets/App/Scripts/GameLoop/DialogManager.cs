using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector2 delayBetweenDialogs;

    [Header("References")]    
    List<SSO_DialogEventData> dialogsInGame = new();
    Queue<SSO_DialogEventData> dialogsOnGoing = new();

    //[Space(10)]
    // RSO
    [SerializeField] RSO_CurrentCharacter rsoCurrentCharacter;
    [SerializeField] RSO_CurrentEventCount rsoCurrentEventCount;
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
        if (rsoCurrentCharacter.Value == null)
            Debug.LogError("There is no character setup");
        if (rsoCurrentCharacter.Value.dialogs.Length == 0)
            Debug.LogError("There is no dialog set in the DialogManager");

        //for (int i = 0; i < dialogOnGoingPreviewCount; i++)
        //    AddRandomDialogToOnGoing();

        StartCoroutine(DelayBetweenDialogs());
    }

    void AddNextDialogOnList()
    {
        if (dialogsInGame.Count == 0) dialogsInGame.AddRange(rsoCurrentCharacter.Value.dialogs);

        dialogsOnGoing.Enqueue(dialogsInGame[0]);
        dialogsInGame.RemoveAt(0);
    }
    void AddDialogToOnGoing(SSO_DialogEventData dialogToAdd)
    {
        dialogsOnGoing.Enqueue(dialogToAdd);
    }

    void HandleNewDialog()
    {
        if (dialogsOnGoing.Count == 0) AddNextDialogOnList();
        rseHandleDialog.Call(dialogsOnGoing.Dequeue());
    }

    void OnDialogEnd()
    {
        rsoCurrentEventCount.Add();
        StartCoroutine(DelayBetweenDialogs());
    }

    IEnumerator DelayBetweenDialogs()
    {
        yield return new WaitForSeconds(Random.Range(delayBetweenDialogs.x, delayBetweenDialogs.y));
        HandleNewDialog();
    }
}