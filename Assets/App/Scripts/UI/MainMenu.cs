using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //[Header("Settings")]
    bool canInteract = true;

    [Header("References")]

    //[Space(10)]
    // RSO
    [SerializeField] RSO_CurrentCharacter rsoCurrentCharacter;
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_AudioFadeOut rseAudioFadeOut;

    public void SelectCharacterButton(SSO_Character character)
    {
        if (!canInteract) return;

        rsoCurrentCharacter.Value = character;
        rseAudioFadeOut.Call(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }

    public void QuitButton()
    {
        if (!canInteract) return;

        Application.Quit();
    }
}