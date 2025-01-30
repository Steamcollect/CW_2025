using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CinematiqueUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector2 minMaxRot;

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

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_SetupCinematics rseSetupCinematics;
    //[Header("Output")]

    private void OnEnable()
    {
        rseSetupCinematics.action += CreateImages;
    }
    private void OnDisable()
    {
        rseSetupCinematics.action -= CreateImages;
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
}