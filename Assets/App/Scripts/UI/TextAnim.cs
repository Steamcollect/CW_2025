using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
public class TextAnim : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("RSE")]
    [SerializeField] private RSE_HandleDialog onHandleDialog;
    [SerializeField] private RSE_OnDialogEnd onDialogEnd;
    [Space(5)]
    [SerializeField] private RSE_ShowAnswerUI onShowAnswerUI;

    private void OnEnable()
    {
        onHandleDialog.action += HandleDialog;
    }

    private void OnDisable()
    {
        onHandleDialog.action -= HandleDialog;
    }

    private void HandleDialog(SSO_DialogEventData dialogEventData)
    {
        StartCoroutine(PrintText(dialogEventData));
    }

    private IEnumerator PrintText(SSO_DialogEventData dialogEventData)
    {
        text.ForceMeshUpdate();

        for(int i = 0; i < dialogEventData.text.Length; i++)
        {
            text.text += dialogEventData.text[i];
            yield return new WaitForSeconds(speed);
        }

        switch (dialogEventData.type) {
            case DialogType.Awnser:
                onShowAnswerUI.Call(dialogEventData.awnsers, dialogEventData.text);
                break;
            case DialogType.Event:
                Debug.Log("Call Event");
                break;
            default:
                Debug.Log("Call Next Dialog");
                break;
        }
    }
}