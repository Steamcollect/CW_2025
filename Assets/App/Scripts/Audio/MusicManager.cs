using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Playlist[] playlists;

    [Header("References")]
    [SerializeField] AudioManager audioManager;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AudioFadeOut rseAudioFadeOut;

    //[Header("Output")]

    private void OnEnable()
    {
        rseAudioFadeOut.action += FadeOut;
    }
    private void OnDisable()
    {
        rseAudioFadeOut.action -= FadeOut;
    }

    private void Start()
    {
        audioManager.SetupPlaylist(playlists);
        foreach (var playlist in playlists)
        {
            Coroutine coroutine = StartCoroutine(playlist.Play());
            playlist.coroutinePlaylist = coroutine;
        }
    }

    void FadeIn(Action onComplete)
    {
        foreach (var playlist in playlists)
        {
            StartCoroutine(playlist.Fade(1, 1));
        }

        StartCoroutine(Utils.Delay(1, onComplete));
    }

    void FadeOut(Action onComplete)
    {
        foreach (var playlist in playlists)
        {
            StartCoroutine(playlist.Fade(0, 1));
        }

        StartCoroutine(Utils.Delay(1, onComplete));
    }
}