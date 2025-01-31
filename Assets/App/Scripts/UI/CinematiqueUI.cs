using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class CinematiqueUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector2 minMaxRot;
    bool canInteract = true;

    [Header("References")]
    [SerializeField] Transform visualContent;
    [SerializeField] Image imageRef;
    [SerializeField] Button nextButton;

    [Space(10)]
    [SerializeField] Transform spawnPos;
    [SerializeField] Transform middlePos;
    [SerializeField] Transform swapPos;

    [Header("JumpToMiddle")]
    [SerializeField] float middleJumpPower;
    [SerializeField] int middleJumpCount;
    [SerializeField] float middleJumpDuration;

    [Header("FirstPartJump")]
    [SerializeField] float firstJumpPower;
    [SerializeField] int firstJumpCount;
    [SerializeField] float firstJumpDuration;
    
    [Header("SecPartJump")]
    [SerializeField] float secJumpPower;
    [SerializeField] int secJumpCount;
    [SerializeField] float secJumpDuration;

    [Space(10)]
    // RSO
    [SerializeField] RSO_CinematicVisuals visuals;
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_SetupCinematics rseSetupCinematics;
    [Header("Output")]
    [SerializeField] RSE_AudioFadeOut rseAudioFadeOut;
    [SerializeField] RSE_FadeIn rseFadeIn;
    [SerializeField] RSE_FadeOut rseFadeOut;

    private void OnEnable()
    {
        rseSetupCinematics.action += CreateImages;
    }
    private void OnDisable()
    {
        rseSetupCinematics.action -= CreateImages;
    }

    private void Awake()
    {
        rseFadeIn.Call();
    }

    private void Start()
    {
        CreateImages(visuals.Value);
    }

    void CreateImages(Sprite[] sprites)
    {
        visualContent.position = spawnPos.position;
        visualContent.rotation = spawnPos.rotation;

        foreach (var sprite in sprites)
        {
            Image image = Instantiate(imageRef, visualContent);
            image.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(minMaxRot.x, minMaxRot.y));
            image.sprite = sprite;

            image.gameObject.SetActive(true);
        }

        MoveToMiddle();
    }

    void MoveToMiddle()
    {
        visualContent.DOJump(middlePos.position, middleJumpPower, middleJumpCount, middleJumpDuration);
        visualContent.DORotate(Vector3.zero, middleJumpDuration).OnComplete(() =>
        {
            nextButton.interactable = true;
        });
    }

    public void NextButton()
    {
        nextButton.interactable = false;

        Image image = visualContent.GetChild(visualContent.childCount - 1).GetComponent<Image>();
        image.transform.DOJump(swapPos.position, firstJumpPower, firstJumpCount, firstJumpDuration);
        image.transform.DORotate(swapPos.eulerAngles, firstJumpDuration).OnComplete(() =>
        {
            image.transform.SetAsFirstSibling();

            image.transform.DOJump(visualContent.position, secJumpPower, secJumpCount, secJumpDuration);
            image.transform.DORotate(new Vector3(0, 0, Random.Range(minMaxRot.x, minMaxRot.y)), secJumpDuration).OnComplete(() =>
            {
                nextButton.interactable = true;
            });
        });
    }

    public void MainMenuButton()
    {
        if (!canInteract) return;
        canInteract = false;
        rseFadeOut.Call();
        rseAudioFadeOut.Call(() => SceneManager.LoadScene("MainMenu"));
    }
}