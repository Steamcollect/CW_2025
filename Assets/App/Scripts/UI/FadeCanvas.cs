using UnityEngine;
public class FadeCanvas : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]
    [SerializeField] Animator anim;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_FadeIn rseFadeIn;
    [SerializeField] RSE_FadeOut rseFadeOut;
    //[Header("Output")]

    private void OnEnable()
    {
        rseFadeIn.action += FadeIn;
        rseFadeOut.action += FadeOut;
    }
    private void OnDisable()
    {
        rseFadeIn.action -= FadeIn;
        rseFadeOut.action -= FadeOut;
    }

    void FadeIn()
    {
        anim.SetTrigger("FadeIn");
    }
    void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}