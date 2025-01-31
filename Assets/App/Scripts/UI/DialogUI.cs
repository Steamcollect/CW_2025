using System.Collections;
using TMPro;
using UnityEngine;
public class DialogUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] float timeToAnswer = 10;

    [Header("References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private SoundComponent soundComponentBrouah;

    [Header("RSE")]
    [SerializeField] private RSE_HandleDialog onHandleDialog;
    [SerializeField] private RSE_OnDialogEnd onDialogEnd;
    [SerializeField] private RSE_CloseAnswerUI onCloseAnswerUI;
    [Space(5)]
    [SerializeField] private RSE_ShowAnswerUI onShowAnswerUI;

    [Header("RSO")]
    [SerializeField] private RSO_CurrentCharacter character;
    

    private void OnEnable()
    {
        onHandleDialog.action += HandleDialog;
        onCloseAnswerUI.action += ClosePanel;

        character.OnChanged += SetCharacterName;
    }

    private void OnDisable()
    {
        onHandleDialog.action -= HandleDialog;
        onCloseAnswerUI.action -= ClosePanel;

        character.OnChanged -= SetCharacterName;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    private void SetCharacterName(SSO_Character character)
    {
        nameText.text = character.characterName;
    }

    private void HandleDialog(SSO_DialogEventData dialogEventData)
    {
        panel.SetActive(true);
        StartCoroutine(PrintText(dialogEventData));
    }

    private void ClosePanel()
    {
        text.text = "";
        panel.SetActive(false);
    }

    private IEnumerator PrintText(SSO_DialogEventData dialogEventData)
    {
        text.ForceMeshUpdate();

        for(int i = 0; i < dialogEventData.text.Length; i++)
        {
            text.text += dialogEventData.text[i];
            yield return new WaitForSeconds(speed);
        }

        if(dialogEventData.awnsers.Length == 0)
        {
            yield return new WaitForSeconds(2f);

            dialogEventData.nextDialog.Invoke();

            ClosePanel();
            onDialogEnd.Call();
        }
        else
        {
            if (dialogEventData.blockQTE ) // QTE is active
            {
                //yield return new WaitUntil(() => /*On event false */);
            }

            soundComponentBrouah.PlayClip();
            
            if (dialogEventData.hideDialogPanel) panel.SetActive(false);

            dialogEventData.nextDialog.Invoke();
            onShowAnswerUI.Call(dialogEventData, timeToAnswer);
        }
    }
}