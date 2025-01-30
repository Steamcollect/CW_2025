using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_AudioFadeOut rseAudioFadeOut;

    public void PlayButton()
    {
        rseAudioFadeOut.Call(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}