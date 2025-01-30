using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Playlist
{
    public AudioClip[] clips;

    [Space(10)]
    public float volumMultiplier;
    public bool isLooping;
    public bool randomize;

    [HideInInspector] public AudioSource audioSource;
    public Coroutine coroutinePlaylist;


    public IEnumerator Play()
    {
        int index = 0;
        
        while (isLooping || (!isLooping && index < clips.Length))
        {
            audioSource.clip = GetNextClip(ref index);
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
    }

    private AudioClip GetNextClip(ref int index)
    {
        AudioClip clip = null;
            
        if (isLooping)
        {
            if (randomize)
            {
                clip = clips.GetRandom();
            }
            else
            {
                index = (index +1) % clips.Length;
                clip = clips[index];
            }
        }
        else
        {
            index = Mathf.Min(clips.Length, index + 1);
            clip = clips[index];
        }
        return clip;
    }

    public IEnumerator Fade(float targetVolum, float fadeTime)
    {
        targetVolum = Mathf.Clamp01(targetVolum);
        float startVolum = volumMultiplier;
        float timer = 0;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            volumMultiplier = Mathf.Lerp(startVolum, targetVolum, timer / fadeTime);
            audioSource.volume = volumMultiplier;
            yield return null;
        }
    }
}