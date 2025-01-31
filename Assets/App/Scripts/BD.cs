using System.Collections;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BD : MonoBehaviour
{
    //[Header("Settings")]
    int index = 0;

    //[Header("References")]
    public Image image;
    public Image bk;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    public RSO_CurrentCharacter character;
    //[Header("Output")]
    [SerializeField] RSE_FadeOut fadeOut;

    private void Start()
    {
        bk.sprite = character.Value.bdSprites[0];
    }

    public void NextButton()
    {
        if (index < character.Value.bdSprites.Length - 1)
        {
            index++;
            image.sprite = character.Value.bdSprites[index];
        }
        else
        {
            fadeOut.Call();
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
}